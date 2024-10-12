using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.CategoryNew;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class CategoryNewController : BaseAdminController
    {
        public readonly IMapper _mapper;
        public CategoryNewController(BaseReponsitory _repo,IMapper mapper):base(_repo)
        {
            _mapper = mapper;
        }
        [AppAuthorize(AuthConst.CategoryNew.VIEW_LIST)]
        public IActionResult Index(int page=1,int size=10)
        {
            var data=_repo.GetAll<CategoryNew,ListCategoryItemVM>(MapperConfig.CategoryNewIndexConf)
                .ToPagedList(page,size);
            return View(data);
        }
        [AppAuthorize(AuthConst.CategoryNew.CREATE)]
        public IActionResult _Create()
        {
            return PartialView();
        }
        [HttpPost]
        [AppAuthorize(AuthConst.CategoryNew.CREATE)]
        public async Task<IActionResult> _Create(AddorUpdateCategoryNewVM model)
        {
            try
            {
                var data = _mapper.Map<CategoryNew>(model);
                await _repo.AddAsync(data);
                SetSuccessMesg("Thêm thành công");
                return Ok(true);
            }catch (Exception ex)
            {
                return Ok(false);
            }
        }
        [AppAuthorize(AuthConst.CategoryNew.UPDATE)]
        public async Task<IActionResult> _Update(int id)
        {
            var data= await _repo.GetOneAsync<CategoryNew,AddorUpdateCategoryNewVM>(id,r=>new AddorUpdateCategoryNewVM
            {
                Id=r.Id,
                NameCategoryNew=r.NameCategoryNew,
            });
            return PartialView(data);
        }
        [HttpPost]
        [AppAuthorize(AuthConst.CategoryNew.UPDATE)]
        public async Task<IActionResult> _Update(AddorUpdateCategoryNewVM model)
        {
            var data= await _repo.FindAsync<CategoryNew>(model.Id);
            if (data != null)
            {
                _mapper.Map<AddorUpdateCategoryNewVM,CategoryNew>(model,data);
                await _repo.UpdateAsync(data);
                SetSuccessMesg("Sửa dữ liệu thành công");
            }
            return Ok(true);
        }
        [AppAuthorize(AuthConst.CategoryNew.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            var data=await _repo.FindAsync<CategoryNew>(id);
            var news= _repo.GetAll<News>().Where(news=>news.Id==data.Id).ToList();
            for(int i = 0; i < news.Count; i++)
            {
                news[i].IdCategoryNew=null;
                await _repo.UpdateAsync(news[i]);
            }
            await _repo.DeleteAsync(data);
            SetSuccessMesg("Xóa thành công");
            return RedirectToAction("Index");
        }
    }
}
