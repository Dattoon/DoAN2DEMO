using System.ComponentModel.DataAnnotations;

namespace WebHocTap.Web.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required][DataType(DataType.Password)][Display(Name = "Mật khẩu hiện tại")] public string CurrentPassword { get; set; }
        [Required][StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự.", MinimumLength = 4)][DataType(DataType.Password)][Display(Name = "Mật khẩu mới")] public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword
        {
            get; set;
        }
    }
}
