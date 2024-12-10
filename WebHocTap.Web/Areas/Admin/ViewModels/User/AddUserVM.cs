using WebHocTap.Share.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebHocTap.Web.Areas.Admin.ViewModels.User
{
    public class AddUserVM
    {
        [AppRequired]
        public string UserName { get; set; }
        [AppRequired]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Mật khẩu không khớp")]
        public string ComformPassword { get; set; }
        [AppRequired]
        public string Gmail { get; set; }
        [AppRequired]
        public string PhoneNumber { get; set; }
        public int IdRole { get; set; }
        public bool? IsSubmit { get; set; }
        public byte[]? PasswordHash { get; internal set; }
        public byte[]? PasswordSalt { get; internal set; }
        public string AvatarUrl { get; set; } = "default-avatar-url";
    }
}
