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
        public readonly IMapper _mapper;
        public TestController(BaseReponsitory _repo,IMapper mapper):base(_repo)
        {
            _mapper = mapper;
        }
        public IActionResult Index(int id,int page=1,int size=10)
        {
            var data= _repo.GetAll<Test,ListTestItemVM>(MapperConfig.TestIndexConf)
                .Where(x => x.IdChapter == id).ToPagedList(page,size);
            ViewBag.IdLesson= id;
            return View(data);
        }
        public IActionResult Create(int id)
        {
            var data = new AddorEditQAVM();
            data.IdChapter = id;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddorEditQAVM model)
        {
            var data = new Test();
            data.Question = model.Question;
            data.IdChapter =model.IdChapter;
            List<Answer> answers =new List<Answer>();
            foreach(var item in model.AnswerFail)
            {
                if (item!=null)
                {
                    var answer = new Answer();
                    answer.IsSucces = false;
                    answer.Description = item;
                    answers.Add(answer);
                }
            }
            var answersusces = new Answer();
            answersusces.IsSucces = true;
            answersusces.Description = model.AnswerSucces;
            answers.Add(answersusces);


            data.answers = answers;
            await _repo.AddAsync(data);
            SetSuccessMesg("Thêm câu hỏi thành công");
            return RedirectToAction("Index", new { id = data.IdChapter });
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
            var data=await _repo.FindAsync<Test>(id);
            if (data != null)
            {
                var listanswer= _repo.GetAll<Answer>().Where(x=>x.IdQues==data.Id).ToList();
                foreach(var item in listanswer)
                {
                    await _repo.DeleteAsync(item);
                }
                await _repo.DeleteAsync(data);
                SetSuccessMesg("Xóa thành công");
            }
            return RedirectToAction("Index", new { id = data.IdChapter });
        }
        public IActionResult ListAnswer(int id)
        {
            var data=_repo.GetAll<Answer,ListAnswerItemVM>(MapperConfig.ListAnswerIndexConf)
                .Where(a=>a.IdQues==id).ToList();
            return new JsonResult(data);
        }
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var data=await _repo.FindAsync<Answer>(id);
            await _repo.DeleteAsync(data);
            SetSuccessMesg("Xóa câu trả lời thành công");
            return RedirectToAction("Detail", new { id = data.IdQues });
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
            var data= await _repo.FindAsync<Answer>(model.Id);
            if(data != null)
            {
                _mapper.Map<UpdateAnswerVM,Answer>(model,data);
                await _repo.UpdateAsync(data);
                SetSuccessMesg("Cập nhật thành công");
            }
            return Ok(true);
        }
    }
}
