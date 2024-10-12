using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.Chapter;
using WebHocTap.Web.Areas.Admin.ViewModels.Lesson;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class LessonController : BaseAdminController
    {
        public readonly IMapper _mapper;
        public LessonController(BaseReponsitory _repo,IMapper mapper):base(_repo)
        {
            _mapper = mapper;
        }
        [AppAuthorize(AuthConst.Lesson.VIEW_LIST)]
        public IActionResult Index(int id,int page=1,int size=10)
        {
            var data = _repo.GetAll <Lesson, ListLessonItemVM>(MapperConfig.LessonIndexConf)
                .Where(x=>x.IdChapter == id).ToPagedList(page, size);
            ViewBag.IdChapter = id;
            return View(data);
        }
        [AppAuthorize(AuthConst.Lesson.CREATE)]
        public IActionResult Create(int id)
        {
            var data = new AddorUpdateLessonVM();
            data.IdChapter = id;
            data.Id = 0;
            return View(data);
        }
        [HttpPost]
        [AppAuthorize(AuthConst.Lesson.CREATE)]
        public async Task<IActionResult> Create(AddorUpdateLessonVM model)
        {
            var data=_mapper.Map<Lesson>(model);
            data.Id = 0;
            await _repo.AddAsync(data);
            SetSuccessMesg("Thêm thành công");
            return RedirectToAction("Index",new {id=data.IdChapter});
        }
        [AppAuthorize(AuthConst.Lesson.UPDATE)]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _repo.GetOneAsync<Lesson, AddorUpdateLessonVM>(id, l => new AddorUpdateLessonVM
            {
                IdChapter = l.IdChapter,
                Content = l.Content,
                Id = l.Id,
                Video = l.Video,
            });
            return View(data);
        }
        [HttpPost]
        [AppAuthorize(AuthConst.Lesson.UPDATE)]
        public async Task<IActionResult> Update(AddorUpdateLessonVM model)
        {
            var data= await _repo.FindAsync<Lesson>(model.Id);
            if (data != null)
            {
                _mapper.Map<AddorUpdateLessonVM,Lesson>(model,data);
                await _repo.UpdateAsync(data);
                SetSuccessMesg("Cập nhật thành công");
            }
            return RedirectToAction("Index", new { id = data.IdChapter });
        }
        [AppAuthorize(AuthConst.Lesson.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            var data= await _repo.FindAsync<Lesson>(id);
            await _repo.DeleteAsync(data);
            SetSuccessMesg("Thêm thành công");
            return RedirectToAction("Index", new { id = data.IdChapter });
        }
    }
}
