namespace WebHocTap.Web.ViewModels
{
    public class CommentVM
    {
        public string Content { get; set; }
        public int IdLesson { get; set; }
        public int? IdUser { get; set; }
        public string UserName { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
