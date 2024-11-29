using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.Areas.Admin.ViewModels.Test;
using WebHocTap.Web.Areas.Admin.ViewModels.TestandAnswer;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class TestController : BaseAdminController
    {
        private readonly IMapper _mapper;

        public TestController(BaseReponsitory repo, IMapper mapper) : base(repo)
        {
            _mapper = mapper;
        }

        public IActionResult Index(int id, int page = 1, int size = 10)
        {
            var data = _repo.GetAll<Test, ListTestItemVM>(MapperConfig.TestIndexConf)
                .Where(x => x.IdChapter == id)
                .ToPagedList(page, size);

            ViewBag.IdLesson = id;
            return View(data);
        }

        public IActionResult Create(int id)
        {
            var data = new AddorEditQAVM { IdChapter = id };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddorEditQAVM model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMesg("Dữ liệu không hợp lệ");
                return View(model);
            }

            var test = _mapper.Map<Test>(model);
            var answers = model.AnswerFail.Where(item => !string.IsNullOrEmpty(item))
                                           .Select(item => new Answer { Description = item, IsSucces = false })
                                           .ToList();

            var correctAnswer = new Answer { Description = model.AnswerSucces, IsSucces = true };
            answers.Add(correctAnswer);

            test.answers = answers;
            await _repo.AddAsync(test);

            SetSuccessMesg("Thêm câu hỏi thành công");
            return RedirectToAction(nameof(Index), new { id = test.IdChapter });
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _repo.GetOneAsync<Test, DetailTest>(id, t => new DetailTest
            {
                Id = t.Id,
                Question = t.Question,
            });

            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var test = await _repo.FindAsync<Test>(id);
            if (test != null)
            {
                var answers = _repo.GetAll<Answer>().Where(x => x.IdQues == test.Id).ToList();
                foreach (var answer in answers)
                {
                    await _repo.DeleteAsync(answer);
                }
                await _repo.DeleteAsync(test);
                SetSuccessMesg("Xóa thành công");
            }
            return RedirectToAction(nameof(Index), new { id = test?.IdChapter });
        }

        public IActionResult ListAnswer(int id)
        {
            var data = _repo.GetAll<Answer, ListAnswerItemVM>(MapperConfig.ListAnswerIndexConf)
                             .Where(a => a.IdQues == id)
                             .ToList();
            return Json(data);
        }

        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _repo.FindAsync<Answer>(id);
            if (answer != null)
            {
                await _repo.DeleteAsync(answer);
                SetSuccessMesg("Xóa câu trả lời thành công");
            }
            return RedirectToAction(nameof(Detail), new { id = answer?.IdQues });
        }

        public async Task<IActionResult> _UpdateAnswer(int id)
        {
            var data = await _repo.GetOneAsync<Answer, UpdateAnswerVM>(id, a => new UpdateAnswerVM
            {
                Id = a.Id,
                Description = a.Description,
            });
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> _UpdateAnswer(UpdateAnswerVM model)
        {
            if (!ModelState.IsValid)
            {
                SetErrorMesg("Dữ liệu không hợp lệ");
                return BadRequest(ModelState);
            }

            var answer = await _repo.FindAsync<Answer>(model.Id);
            if (answer != null)
            {
                _mapper.Map(model, answer);
                await _repo.UpdateAsync(answer);
                SetSuccessMesg("Cập nhật thành công");
            }
            return Ok(true);
        }
    }
}
