using System.ComponentModel;

namespace WebHocTap.Web.Areas.Admin.ViewModels.News
{
    public class AddorUpdateNewsVM
    {
        
        public int Id { get; set; }
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Nội dung")]
        public string Description { get; set; }
        public string pathImg { get; set; }
        [DisplayName("Thể loại tin tức")]
        public int? IdCategoryNew { get; set; }
        [DisplayName("Hình ảnh")]
        public IFormFile? Image { get; set; }
    }
}
