using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.Chapter;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class ChapterController : BaseAdminController
    {
        public readonly IMapper _mapper;
        public ChapterController(BaseReponsitory _repo,IMapper mapper):base(_repo)
        {
            _mapper = mapper;
        }
        [AppAuthorize(AuthConst.Chapter.VIEW_LIST)]
        public IActionResult Index(int id, int page = 1, int size = 10)
        {
            var data = _repo.GetAll<Chapter, ListChapterItemVM>(MapperConfig.ChapterIndexConf)
                .Where(c => c.IdSubject == id).ToPagedList(page, size);
            ViewBag.IdSub = id;
            return View(data);
        }
        [AppAuthorize(AuthConst.Chapter.CREATE)]
        public IActionResult Create(int id) {
            var data = new AddorUpdateChappterVM();
            data.IdSubject = id;
            data.Id = 0;
            return View(data);
        }
        [AppAuthorize(AuthConst.Chapter.CREATE)]
        [HttpPost]
        public async Task<IActionResult> Create(AddorUpdateChappterVM model) 
        {
            try
            {
                model.Id = 0;
                var data = _mapper.Map<Chapter>(model);
                await _repo.AddAsync(data);
                SetSuccessMesg("Thêm chương mới thành công");
                return RedirectToAction("Index",new {id=model.IdSubject});
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        [AppAuthorize(AuthConst.Chapter.UPDATE)]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _repo.GetOneAsync<Chapter, AddorUpdateChappterVM>(id, c => new AddorUpdateChappterVM
            {
                Id = c.Id,
                IdSubject = c.IdSubject,
                NameChapter = c.NameChapter,
            });
            return View(data);
        }
        [AppAuthorize(AuthConst.Chapter.UPDATE)]
        [HttpPost]
        public async Task<IActionResult> Update(AddorUpdateChappterVM model)
        {
            try
            {
                var data = await _repo.FindAsync<Chapter>(model.Id);
                if (data != null)
                {
                    _mapper.Map<AddorUpdateChappterVM, Chapter>(model, data);
                    await _repo.UpdateAsync(data);
                    SetSuccessMesg("Cập nhật thành công");
                }
                return RedirectToAction("Index", new { id = data.IdSubject });
            }catch (Exception ex)
            {
                return View(ex);
            }
        }
        [AppAuthorize(AuthConst.Chapter.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            var data= await _repo.FindAsync<Chapter>(id);
            await _repo.DeleteAsync(data);
            SetSuccessMesg("Xóa thành công");
            return RedirectToAction("Index", new { id = data.IdSubject });
        }
    }
}
