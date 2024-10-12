namespace WebHocTap.Web.Areas.Admin.ViewModels.Home
{
    public class ListPurchasedCourseItem
    {
        public int Id { get; set; }
        public string SubName { get; set; }
        public string UserName { get; set; }
        public DateTime? CreateAt { get; set; }
        public int Price { get; set; }
    }
}
