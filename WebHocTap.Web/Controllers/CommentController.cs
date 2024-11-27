using AutoMapper;
using WebHocTap.Data.Repositories;
using WebHocTap.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebHocTap.Data.Entites;

namespace WebHocTap.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentController(CommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        // Hiển thị danh sách bình luận (sử dụng ViewComponent CommentsViewComponent)
        public IActionResult GetComments(int lessonId)
        {
            return ViewComponent("Comments", new { lessonId });
        }

        // Thêm bình luận mới
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _mapper.Map<Comemt>(model);
            comment.CreateAt = DateTime.Now;
            await _commentRepo.AddCommentAsync(comment);

            // Trả về ViewComponent AddComment để render lại giao diện
            return RedirectToAction("Lesson", "SubjectClient", new { id = model.IdLesson });
        }
    }
}
