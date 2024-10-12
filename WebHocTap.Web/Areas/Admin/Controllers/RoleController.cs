using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.Role;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class RoleController :BaseAdminController
    {
        public RoleController(BaseReponsitory _repo):base(_repo)
        {
           
        }
        [AppAuthorize(AuthConst.Role.VIEW_LIST)]
        public IActionResult Index(int page=1,int size = 10)
        {
            var data = _repo.GetAll<Role, ListRoleItemVM>(MapperConfig.RoleIndexConf)
                .ToPagedList(page, size);
            return View(data);
        }
        [AppAuthorize(AuthConst.Role.CREATE)]
        public IActionResult AddRole() { 
            return View();
        }
        [HttpPost]
        [AppAuthorize(AuthConst.Role.CREATE)]
        public async Task<IActionResult> AddRole(AddRoleVM model)
        {
            if (model.PermissionIds == null)
            {
                SetErrorMesg("Có lỗi xãy ra");
                return View(model);
            }
            var arrIdPermission = model.PermissionIds.Split(',');

            var role = new Role
            {
                RoleName = model.RoleName,
                Description = model.Description
            };
            try
            {
                await _repo.AddAsync(role);
                foreach (var item in arrIdPermission)
                {
                    var idPer = Convert.ToInt32(item);
                    role.rolePermissions.Add(new RolePermission
                    {
                        MStPermissionId = idPer
                    });
                }
                await _repo.AddAsync(role.rolePermissions);
                SetSuccessMesg($"Thêm vai trò [{role.RoleName}] thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [AppAuthorize(AuthConst.Role.UPDATE)]
        public async Task<IActionResult> UpdateRole(int? id)
        {
            if (!id.HasValue)
            {
                SetErrorMesg("Id không được trống");
                return RedirectToAction(nameof(Index));
            }
            var data = await _repo.GetOneAsync<Role,EditRoleVM>(id.Value, r => new EditRoleVM
            {
                Id = r.Id,
                RoleName = r.RoleName,
                Description = r.Description,
                PermissionIds = string.Join(',', r.rolePermissions.Select(rp => rp.MStPermissionId)),
            });
            if (data == null)
            {
                SetErrorMesg("Không tìm thấy dữ liệu");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
    }
}
