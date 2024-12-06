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
using System.Security.Cryptography;
using WebHocTap.Web.Common.Mailer;
using WebHocTap.Web.Common;

namespace WebHocTap.Web.Controllers
{
    public class AccountController : BaseClientController
    {
        private readonly IMapper _mapper;
        private readonly BaseReponsitory _repo;
        private readonly IWebHostEnvironment _host;
        private readonly AppMailer _mailService;


        public AccountController(BaseReponsitory repo, IMapper mapper, IWebHostEnvironment host, IConfiguration configuration) : base(repo)
        {
            _repo = repo;
            _mapper = mapper;
            _host = host;
            var mailConfig = new AppMailConfiguration();
            mailConfig.LoadFromConfig(configuration);
            _mailService = new AppMailer(mailConfig);

        }

        // Hiển thị form quên mật khẩu
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        // Xử lý yêu cầu quên mật khẩu
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            var user = await _repo.GetOneAsync<User>(x => x.Gmail == model.Email);
            if (user == null)
            {
                TempData["Mesg"] = "Email không tồn tại trong hệ thống.";
                return View();
            }

            var token = GenerateResetToken();
            var resetLink = Url.Action("ResetPassword", "Account", new { token, email = user.Gmail }, Request.Scheme);

            var sender = new AppMailSender
            {
                Name = "Admin",
                Subject = "Password Reset Request",
                Content = $"Click the link to reset your password: <a href='{resetLink}'>Reset Password</a>"
            };

            var receiver = new AppMailReciver
            {
                Name = user.UserName,
                Email = user.Gmail
            };

            await _mailService.SendEmailAsync(sender, receiver);

            TempData["Mesg"] = "Email đặt lại mật khẩu đã được gửi.";
            return RedirectToAction("Login");
        }

        // Hiển thị form đặt lại mật khẩu
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        // Xử lý yêu cầu đặt lại mật khẩu
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await _repo.GetOneAsync<User>(x => x.Gmail == model.Email);
            if (user == null || !VerifyResetToken(model.Token))
            {
                TempData["Mesg"] = "Yêu cầu không hợp lệ.";
                return View(model);
            }

            var passwordHash = CreatePasswordHash(model.NewPassword);
            user.PasswordHash = passwordHash.Hash;
            user.PasswordSalt = passwordHash.Salt;

            await _repo.UpdateAsync(user);
            TempData["Mesg"] = "Mật khẩu đã được đặt lại thành công.";
            return RedirectToAction("Login");
        }

        private string GenerateResetToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        private bool VerifyResetToken(string token)
        {
            return true; // Cần thêm logic để xác thực token nếu cần
        }

       
        private (byte[] Hash, byte[] Salt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (hash, salt);
            }
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
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine(ex.Message);
                return View(model);
            }
        }




        public IActionResult Login() => View();

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

            // Thêm xử lý cho các giá trị null
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



        private string UploadImgAndReturnPath(IFormFile file, string childFolder = "/img/", bool saveInWwwRoot = true)
        {
            var root = saveInWwwRoot ? _host.WebRootPath : _host.ContentRootPath;
            var filename = Path.GetFileNameWithoutExtension(file.FileName)
                            + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
                            + Path.GetExtension(file.FileName);

            if (!Directory.Exists(root + childFolder))
            {
                Directory.CreateDirectory(root + childFolder);
            }

            var relativePath = childFolder + filename;
            var path = root + relativePath;
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            return relativePath;
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

            if (model.AvatarUrl != null)
            {
                string avatarPath = UploadImgAndReturnPath(model.Avatar, "/img/avatars/");
                avatarPath = avatarPath.Split('/').Last(); // Simplifying the path
                model.AvatarUrl = avatarPath;
            }
            else
            {
                model.AvatarUrl = user.AvatarUrl; // Retain the existing avatar if no new file is uploaded
            }

            _mapper.Map(model, user);
            await _repo.UpdateAsync(user);
            TempData["Mesg"] = "Cập nhật hồ sơ thành công";
            return RedirectToAction(nameof(Index), "Home");
        }



        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(ClaimTypes.NameIdentifier))?.Value);
            var user = await _repo.FindAsync<User>(userId);

            if (user == null || !VerifyPasswordHash(model.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu hiện tại không chính xác.");
                return View(model);
            }

            var passwordHash = CreatePasswordHash(model.NewPassword);
            user.PasswordHash = passwordHash.Hash;
            user.PasswordSalt = passwordHash.Salt;

            await _repo.UpdateAsync(user);
            SetSuccessMesg("Đổi mật khẩu thành công.");

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

       





        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AppConst.COOKIES_AUTH);
            SetSuccessMesg("Đã đăng xuất");
            return RedirectToAction("Index", "Home");
        }




    }
}
