namespace WebHocTap.Web.Areas.Admin.ViewModels.CategorySub
{
    public class AddorUpdateCategorySubVM
    {
        public int Id { get; set; }
        public string NameCategorySub { get; set; }
        public int? Price { get; set; }
        public string Descripstion { get; set; }
        public IFormFile Image { get; set; }
        public string pathImg { get; set; }
    }
}
