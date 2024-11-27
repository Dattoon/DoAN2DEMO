using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.ViewModels.Subject;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;

namespace WebHocTap.Web.Controllers
{
    public class SubjectClientController : BaseClientController
    {
        public SubjectClientController(BaseReponsitory repo) : base(repo)
        {

        }
        public async Task<IActionResult> DetailsSubject(int id)
        {
            var data = await _repo.FindAsync<CategorySub>(id);
            var purchasedCourse =  _repo.GetAll<PurchasedCourse>(x => x.IdSub == data.Id).ToList();
            if(data.Price!=null)
            {
                var iduser = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(System.Security.Claims.ClaimTypes.NameIdentifier))?.Value);
                bool check = false;
                foreach (var claim in purchasedCourse)
                {
                    if (claim.IdUser == iduser)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) {
                    return RedirectToAction("DetailSub", "Home", new { id = data.Id });
                }
            }
            data.CountView += 1;
            ListSubject model = new ListSubject();
            model.NameCategorySub = data.NameCategorySub;
            model.Id = data.Id;
            // tất cả  môn học của khóa đó
            var subjeck = _repo.GetAll<Subject>(x => x.IdCategorySub == id).ToList();

            model.subjects = subjeck;
            // tìm tất cả các chương rồi ra view lọc lại 
            var chapter =_repo.GetAll<Chapter>().OrderBy(x=>x.Id).ToList();
            model.chapters = chapter;
            await _repo.UpdateAsync(data);
            return View(model);
        }
        public async Task<IActionResult> Lesson(int id)
        {
            var data = new LessonDetailVM();
            // tìm nội dung bài học
            var lesson = _repo.GetAll<Lesson>(x => x.IdChapter == id).OrderBy(x => x.Id).ToList();
            data.lessons = lesson;
            // tìm ra chương của bài học để get tất cả các chương chung 1 môn
            var chapter = await _repo.FindAsync<Chapter>(id);
            var listchapter = _repo.GetAll<Chapter>(x => x.IdSubject == chapter.IdSubject).OrderBy(x => x.Id).ToList();
            data.chapters = listchapter;
            // tìm tên môn học
            int idsub = Convert.ToInt32(chapter.IdSubject);
            var sub = await _repo.FindAsync<Subject>(idsub);
            data.subject = sub;
            var test = _repo.GetAll<Test>(x => x.IdChapter == id).ToList();
            data.IsTest = test.Count > 0;
            ViewBag.IdChapter = id;
            ViewBag.LessonId = lesson.FirstOrDefault()?.Id; // Thiết lập LessonId
            return View(data);
        }

        public IActionResult DetailLesson(int id)
        { 
            var data=new DetailLessonJSVM();
            var lesson = _repo.GetAll<Lesson>().Where(x=>x.IdChapter==id).ToList();
            data.lessons = lesson;
            var test=  _repo.GetAll<Test>(x=>x.IdChapter==id).ToList();
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
        public IActionResult TestClient(int id) { 
            var data= _repo.GetAll<Test,TestAnswerClientVM>(MapperConfig.ListTestAnswerIndexConf).
                Where(x=>x.IdChapter==id).ToList();
            return PartialView(data);
        }
    }
}
