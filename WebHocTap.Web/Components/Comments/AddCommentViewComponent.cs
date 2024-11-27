using Microsoft.AspNetCore.Mvc;
using WebHocTap.Web.ViewModels;

namespace WebHocTap.Web.Components.Comments
{
    public class AddCommentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int lessonId)
        {
            var model = new CommentVM
            {
                IdLesson = lessonId
            };

            // Chỉ định đường dẫn tùy chỉnh
            return View("~/Views/Shared/Components/Components/AddCommentViewComponent.cshtml", model);
        }
    }
}
