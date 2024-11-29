using WebHocTap.Data.Entites;

namespace WebHocTap.Web.ViewModels.Subject
{
    public class LessonDetailVM
    {
        public bool IsTest { get; set; }
        public WebHocTap.Data.Entites.Subject subject { get; set; }
        public List<Chapter> chapters { get; set; }
        public List<Lesson> lessons { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
