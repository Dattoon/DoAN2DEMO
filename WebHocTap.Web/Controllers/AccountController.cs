using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.ViewModels;
using WebHocTap.Web.ViewModels.Account;
using WebHocTap.Web.WebConfig;
using WebHocTap.Web.WebConfig.Const;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace WebHocTap.Web.Controllers
{
    public class AccountController : BaseClientController
    {
        public readonly IMapper _mapper;
        public AccountController(BaseReponsitory _repo,IMapper mapper):base(_repo)
        {
            _mapper = mapper;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMesg("Dữ liệu không hợp lệ vui lòng kiểm tra lại!");
                return View(model);
            }
            model.UserName = model.UserName.ToLower();
            if (await _repo.AnyAsync<User>(x => x.UserName == model.UserName))
            {
                SetErrorMesg("Tên đăng nhập đã tồn tại vui lòng kiểm tra lại!");
                return View(model);
            }

            if (model.IsSubmit == false)
            {
                SetErrorMesg("Bạn chưa đồng ý điều khoản và điều kiện!");
                return View(model);
            }
            try
            {
                var hashResult = HashHMACSHA512(model.Password);
                model.PasswordHash = hashResult.Value;
                model.PasswordSalt = hashResult.Key;
                var user = _mapper.Map<User>(model);
                user.IdRole = 2;
                await _repo.AddAsync(user);
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        public IActionResult Login()
        {
            return PartialView();
        }
        
        [HttpPost]
        public async Task<IActionResult>Login(LoginClientVM model)
        {
            var user = await _repo.GetOneAsync<User, UserDataForApp>
                           (
                               where: x => x.UserName == model.UserName.ToLower(),
                               MapperConfig.LoginConf
                           );
            if (user == null)
            {
                TempData["Mesg"] = "Tài khoản không tồn tại";
                return View();
            }
            var pwdHash = this.HashHMACSHA512WithKey(model.Password, user.PasswordSalt);
            if (!pwdHash.SequenceEqual(user.PasswordHash))
            {
                TempData["Mesg"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }

            var claims = new List<Claim> {
                           new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                           new Claim(ClaimTypes.Name, user.UserName),
                           new Claim(ClaimTypes.Email, user.Gmail),
                           new Claim(AppClaimType.PhoneNumber, user.PhoneNumber),
                           new Claim(AppClaimType.RoleName, user.RoleName),
                           new Claim(AppClaimType.RoleId, user.RoleId.ToString()),
                           new Claim(AppClaimType.Permissions, user.Permission),
                        };
            var claimsIdentity = new ClaimsIdentity(claims, AppConst.COOKIES_AUTH);
            var principal = new ClaimsPrincipal(claimsIdentity);
            var authenPropeties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(AppConst.LOGIN_TIMEOUT),
                IsPersistent = model.RemeberMe
            };
            await HttpContext.SignInAsync(AppConst.COOKIES_AUTH, principal, authenPropeties);
            SetSuccessMesg("Đăng nhập thành công");
            //CreateDirIfNotExist(model.Username);
            //var returnUrl = Request.Query["ReturnUrl"].ToString();
            //if (returnUrl.IsNullOrEmpty())
            //{
            //    return HomePage();
            //}
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AppConst.COOKIES_AUTH);
            SetSuccessMesg("Đã đăng xuất");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
