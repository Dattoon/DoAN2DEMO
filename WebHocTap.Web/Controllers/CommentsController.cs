using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebHocTap.Data.Repositories;
using WebHocTap.Web.ViewModels.Subject;
using WebHocTap.Data.Entites;
using Microsoft.Extensions.Logging;

namespace WebHocTap.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(CommentRepository commentRepository, IMapper mapper, ILogger<CommentsController> logger)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentViewModel model)
        {
            _logger.LogInformation("Received request to add comment");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state: {ModelStateErrors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Model is valid. Content: {Content}, LessonId: {LessonId}", model.Content, model.LessonId);

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var userName = User.Identity.Name; // Lấy UserName từ User.Identity

                var comment = _mapper.Map<Comemt>(model);
                comment.IdUser = userId;
                comment.CreateAt = DateTime.UtcNow;

                await _commentRepository.AddCommentAsync(comment);

                // Thiết lập UserName trong ViewModel để trả về cho client
                var commentViewModel = _mapper.Map<CommentViewModel>(comment);
                commentViewModel.UserName = userName;

                _logger.LogInformation("Comment added successfully");
                return Ok(commentViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment");
                return StatusCode(500, new { Message = "Đã xảy ra lỗi, vui lòng thử lại sau." });
            }
        }

        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetComments(int lessonId)
        {
            var comments = await _commentRepository.GetCommentsByLessonIdAsync(lessonId);
            var commentViewModels = _mapper.Map<List<CommentViewModel>>(comments);

            // Log dữ liệu trả về để kiểm tra
            _logger.LogInformation("Returning comments: {CommentsCount}", commentViewModels.Count);
            foreach (var comment in commentViewModels)
            {
                _logger.LogInformation("Comment: {Content} by {UserName} at {CreatedAt}", comment.Content, comment.UserName, comment.CreatedAt);
            }

            return Ok(commentViewModels);
        }
    }
}
