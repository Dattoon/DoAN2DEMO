using WebHocTap.Data.Entites;

namespace WebHocTap.Web.Components.ListNewViewComponents
{
    public class ListNewVM
    {
        public int Id { get; set; }
        public string NameCategoryNew { get; set; }
        public ICollection<News> news { get; set; }
    }
}
