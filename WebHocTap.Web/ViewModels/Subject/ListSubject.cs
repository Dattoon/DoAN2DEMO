using WebHocTap.Data.Entites;

namespace WebHocTap.Web.ViewModels.Subject
{
    public class ListSubject
    {
        public int Id { get; set; }
        public string NameCategorySub { get; set; }
        public List<WebHocTap.Data.Entites.Subject> subjects { get; set; }
        public List<Chapter> chapters { get; set; }
    }
}
