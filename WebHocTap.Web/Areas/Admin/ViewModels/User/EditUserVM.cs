using WebHocTap.Share.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebHocTap.Web.Areas.Admin.ViewModels.User
{
    public class EditUserVM
    {
        public int Id { get; set; }

        [AppRequired]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ Gmail không hợp lệ.")]
        public string Gmail { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string PhoneNumber { get; set; }

        public int IdRole { get; set; }

        // Nếu bạn muốn cho phép chỉnh sửa mật khẩu, thêm các trường dưới đây
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Mật khẩu không khớp")]
        public string ComformPassword { get; set; }

        public byte[]? PasswordHash { get; internal set; }
        public byte[]? PasswordSalt { get; internal set; }
    }
}
