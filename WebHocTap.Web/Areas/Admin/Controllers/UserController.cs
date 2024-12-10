using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.User;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Authentication;
using WebHocTap.Web.WebConfig.Const;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IMapper _mapper;
        private readonly BaseReponsitory _repo;

        public UserController(BaseReponsitory repo, IMapper mapper) : base(repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AppAuthorize(AuthConst.User.VIEW_LIST)]
        public IActionResult Index(int page = 1, int size = 10)
        {
            var data = _repo.GetAll<User, ListUserItemVM>(MapperConfig.UserIndexConf)
                            .ToPagedList(page, size);
            return View(data);
        }

        public IActionResult AddUser() => View();

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserVM model)
        {
            if (string.IsNullOrEmpty(model.AvatarUrl))
            {
                model.AvatarUrl = "default-avatar-url";
            }

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

            var hashResult = HashHMACSHA512(model.Password);
            model.PasswordHash = hashResult.Value;
            model.PasswordSalt = hashResult.Key;

            try
            {
                var user = _mapper.Map<User>(model);
                user.IdRole = model.IdRole; // Role can be set by admin
                await _repo.AddAsync(user);
                SetSuccessMesg($"Thêm tài khoản [{user.UserName}] thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log exception here
                return View(model);
            }
        }

        public async Task<IActionResult> EditUser(int id)

        {
            var user = await _repo.FindAsync<User>(id);
            if (user == null)
            {
                SetErrorMesg("Tài khoản không tồn tại");
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<EditUserVM>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMesg("Dữ liệu không hợp lệ vui lòng kiểm tra lại!");
                return View(model);

            }

            try
            {
                var user = await _repo.FindAsync<User>(model.Id);
                if (user == null)
                {
                    SetErrorMesg("Tài khoản không tồn tại");
                    return View(model);
                }


                user.Gmail = model.Gmail ?? user.Gmail;
                user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
                user.IdRole = model.IdRole;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var hashResult = HashHMACSHA512(model.Password);
                    user.PasswordHash = hashResult.Value;
                    user.PasswordSalt = hashResult.Key;
                }
                if (string.IsNullOrEmpty(model.AvatarUrl))
                {
                    model.AvatarUrl = "default-avatar-url";
                }

                await _repo.UpdateAsync(user);
                SetSuccessMesg($"Chỉnh sửa tài khoản [{user.UserName}] thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log exception here
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repo.FindAsync<User>(id);
            if (user == null)
            {
                SetErrorMesg("Tài khoản không tồn tại");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _repo.DeleteAsync(user);
                SetSuccessMesg($"Xóa tài khoản [{user.UserName}] thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                SetErrorMesg("Xóa tài khoản không thành công");
                Console.WriteLine(ex.Message); // Log exception here
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(AppConst.COOKIES_AUTH); SetSuccessMesg("Đã đăng xuất");
            return RedirectToAction("Index", "Home");

        }
    }
}