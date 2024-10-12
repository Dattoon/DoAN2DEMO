using System.ComponentModel;

namespace WebHocTap.Web.Areas.Admin.ViewModels.Sub
{
    public class AddorUpdateSubVM
    {
        public int Id { get; set; }
        [DisplayName("Tên môn học")]
        public string NameSubject { get; set; }
        [DisplayName("Tên danh mục")]
        public int? IdCategorySub { get; set; }
    }
}
