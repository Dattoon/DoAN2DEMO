namespace WebHocTap.Web.Areas.Admin.ViewModels.Lesson
{
    public class AddorUpdateLessonVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? Video { get; set; }
        public int? IdChapter { get; set; }
    }
}
