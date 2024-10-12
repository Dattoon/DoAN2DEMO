using WebHocTap.Share.Attributes;
using WebHocTap.Web.WebConfig.Const;

namespace WebHocTap.Web.Areas.Admin.ViewModels.Role
{
    public class EditRoleVM
    {
        public int Id { get; set; }
        [AppRequired]
        public string RoleName { get; set; }

        [AppRequired]
        public string Description { get; set; }

        // Chuỗi chứa permissionId, phân tách bởi dấu phẩy
        [AppRequired(ErrorMessage = VM.RoleVM.PERMISSION_IDS_REQUIRED_ERR_MESG)]
        [AppRegex(VM.RoleVM.PERMISSION_IDS_REGEX, ErrorMessage = VM.RoleVM.PERMISSION_IDS_REGEX_ERR_MESG)]
        public string PermissionIds { get; set; }
        public string? DeletedPermissionIds { get; set; }

        // Chuỗi chứa permissionId được thêm mới, phân tách bởi dấu phẩy
        public string? AddedPermissionIds { get; set; }
    }
}
