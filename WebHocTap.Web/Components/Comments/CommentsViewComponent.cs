using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebHocTap.Data.Repositories;
using WebHocTap.Web.ViewModels;
using AutoMapper;
using System.Collections.Generic;

namespace WebHocTap.Web.Components.Comments
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly CommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentsViewComponent(CommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int lessonId)
        {
            var comments = await _commentRepo.GetCommentsByLessonIdAsync(lessonId);
            var commentVMs = _mapper.Map<IEnumerable<CommentVM>>(comments);

            // Chỉ định đường dẫn tùy chỉnh
            return View("~/Views/Shared/Components/Components/Default.cshtml", commentVMs);
        }
    }
}
