using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.ViewModels;
using WebHocTap.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebHocTap.Web.WebConfig.Const;
using WebHocTap.Web.WebConfig;

namespace WebHocTap.Web.Controllers
{
    public class AccountController : BaseClientController
    {
        private readonly IMapper _mapper;
        private readonly BaseReponsitory _repo;

        public AccountController(BaseReponsitory repo, IMapper mapper) : base(repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            // Thiết lập giá trị mặc định cho AvatarUrl trước khi kiểm tra ModelState
            if (string.IsNullOrEmpty(model.AvatarUrl))
            {
                model.AvatarUrl = "default-avatar-url";
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // Log lỗi ra console hoặc log vào file
                }
                TempData["Mesg"] = "Dữ liệu không hợp lệ vui lòng kiểm tra lại!";
                return View(model);
            }

            model.UserName = model.UserName.ToLower();
            if (await _repo.AnyAsync<User>(x => x.UserName == model.UserName))
            {
                TempData["Mesg"] = "Tên đăng nhập đã tồn tại vui lòng kiểm tra lại!";
                return View(model);
            }

            var hashResult = HashHMACSHA512(model.Password);
            model.PasswordHash = hashResult.Value;
            model.PasswordSalt = hashResult.Key;

            try
            {
                var user = _mapper.Map<User>(model);
                user.IdRole = 2;
                await _repo.AddAsync(user);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return View(model);
            }
        }




        public IActionResult Login() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Login(LoginClientVM model)
        {
            var user = await _repo.GetOneAsync<User, UserDataForApp>(
                x => x.UserName == model.UserName.ToLower(),
                MapperConfig.LoginConf);

            if (user == null)
            {
                TempData["Mesg"] = "Tài khoản không tồn tại";
                return View();
            }

            var pwdHash = HashHMACSHA512WithKey(model.Password, user.PasswordSalt);
            if (!pwdHash.SequenceEqual(user.PasswordHash))
            {
                TempData["Mesg"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View();
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim(ClaimTypes.Email, user.Gmail ?? string.Empty),
        new Claim(AppClaimType.PhoneNumber, user.PhoneNumber ?? string.Empty),
        new Claim(AppClaimType.RoleName, user.RoleName ?? string.Empty),
        new Claim(AppClaimType.RoleId, user.RoleId.ToString()),
        new Claim(AppClaimType.Permissions, user.Permission ?? string.Empty),
        new Claim("AvatarUrl", user.AvatarUrl ?? string.Empty)
    };

            var claimsIdentity = new ClaimsIdentity(claims, AppConst.COOKIES_AUTH);
            var principal = new ClaimsPrincipal(claimsIdentity);
            var authenPropeties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddHours(AppConst.LOGIN_TIMEOUT),
                IsPersistent = model.RemeberMe
            };

            await HttpContext.SignInAsync(AppConst.COOKIES_AUTH, principal, authenPropeties);
            SetSuccessMesg("Đăng nhập thành công");
            return RedirectToAction(nameof(Index), "Home");
        }


        public IActionResult UpdateProfile(int id)
        {
            var user = _repo.GetOneAsync<User>(x => x.Id == id).Result;
            if (user == null) return NotFound();

            var model = new ProfileUpdateVM
            {
                UserId = user.Id,
                Gmail = user.Gmail,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mesg"] = "Dữ liệu không hợp lệ";
                return View(model);
            }

            var user = await _repo.GetOneAsync<User>(x => x.Id == model.UserId);
            if (user == null)
            {
                TempData["Mesg"] = "Người dùng không tồn tại";
                return View(model);
            }

            user.Gmail = model.Gmail;
            user.PhoneNumber = model.PhoneNumber;
            user.AvatarUrl = model.AvatarUrl;

            await _repo.UpdateAsync(user);

            TempData["Mesg"] = "Cập nhật hồ sơ thành công";
            return RedirectToAction("Profile", new { id = user.Id });
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AppConst.COOKIES_AUTH);
            SetSuccessMesg("Đã đăng xuất");
            return RedirectToAction("Index", "Home");
        }
    }
}
