namespace WebHocTap.Web.ViewModels.Subject
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int LessonId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
