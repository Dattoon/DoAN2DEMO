namespace WebHocTap.Web.ViewModels
{
    public class UserDataForApp
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Permission { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }

        public string AvatarUrl { get; set; }
    }
}
