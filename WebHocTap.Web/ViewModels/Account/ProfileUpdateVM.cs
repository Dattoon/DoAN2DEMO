using System.ComponentModel.DataAnnotations;

namespace WebHocTap.Web.ViewModels.Account
{
    public class ProfileUpdateVM
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Gmail { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public IFormFile Avatar { get; set; }
        public string AvatarUrl { get; set; }
    }
}
