using AutoMapper;
using WebHocTap.Data.Repositories;
using WebHocTap.Web.ViewModels.Subject;
using Microsoft.AspNetCore.Mvc;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.WebConfig;
using WebHocTap.Data.Entites;

namespace WebHocTap.Web.Controllers
{
    public class SubjectClientController : BaseClientController
    {
        private readonly CommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public SubjectClientController(BaseReponsitory repo, CommentRepository commentRepository, IMapper mapper) : base(repo)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> DetailsSubject(int id)
        {
            var data = await _repo.FindAsync<CategorySub>(id);
            var purchasedCourse = _repo.GetAll<PurchasedCourse>(x => x.IdSub == data.Id).ToList();
            if (data.Price != null)
            {
                var iduser = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(System.Security.Claims.ClaimTypes.NameIdentifier))?.Value);
                bool check = purchasedCourse.Any(claim => claim.IdUser == iduser);
                if (!check)
                {
                    return RedirectToAction("DetailSub", "Home", new { id = data.Id });
                }
            }

            data.CountView += 1;
            ListSubject model = new ListSubject
            {
                NameCategorySub = data.NameCategorySub,
                Id = data.Id,
                subjects = _repo.GetAll<Subject>(x => x.IdCategorySub == id).ToList(),
                chapters = _repo.GetAll<Chapter>().OrderBy(x => x.Id).ToList()
            };

            await _repo.UpdateAsync(data);
            return View(model);
        }

        public async Task<IActionResult> Lesson(int id)
        {
            var data = new LessonDetailVM
            {
                lessons = _repo.GetAll<Lesson>(x => x.IdChapter == id).OrderBy(x => x.Id).ToList(),
            };

            var chapter = await _repo.FindAsync<Chapter>(id);
            data.chapters = _repo.GetAll<Chapter>(x => x.IdSubject == chapter.IdSubject).OrderBy(x => x.Id).ToList();

            int idsub = Convert.ToInt32(chapter.IdSubject);
            data.subject = await _repo.FindAsync<Subject>(idsub);

            var test = _repo.GetAll<Test>(x => x.IdChapter == id).ToList();
            data.IsTest = test.Count > 0;

            // Lấy danh sách bình luận
            data.Comments = _mapper.Map<List<CommentViewModel>>(await _commentRepository.GetCommentsByLessonIdAsync(id));

            // Cung cấp URL video YouTube từ dữ liệu bài học
            var lesson = data.lessons.FirstOrDefault();
            if (lesson != null)
            {
                data.VideoUrl = lesson.Video;
            }

            ViewBag.IdChapter = id;
            ViewBag.LessonId = data.lessons.FirstOrDefault()?.Id;

            return View(data);
        }





        public IActionResult DetailLesson(int id)
        {
            var data = new DetailLessonJSVM();
            var lesson = _repo.GetAll<Lesson>().Where(x => x.IdChapter == id).ToList();
            data.lessons = lesson;
            var test = _repo.GetAll<Test>(x => x.IdChapter == id).ToList();
            if (test.Count > 0)
            {
                data.IsTest = true;
            }
            else
            {
                data.IsTest = false;
            }
            data.IdChapter = id;
            return new JsonResult(data);
        }

        public IActionResult TestClient(int id)
        {
            var data = _repo.GetAll<Test, TestAnswerClientVM>(MapperConfig.ListTestAnswerIndexConf)
                .Where(x => x.IdChapter == id).ToList();
            return PartialView(data);
        }
    }
}
