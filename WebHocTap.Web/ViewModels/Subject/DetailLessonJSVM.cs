using WebHocTap.Data.Entites;

namespace WebHocTap.Web.ViewModels.Subject
{
    public class DetailLessonJSVM
    {
        public List<Lesson> lessons { get; set; }
        public bool IsTest { get; set; }
        public int? IdChapter { get; set; }
    }
}
