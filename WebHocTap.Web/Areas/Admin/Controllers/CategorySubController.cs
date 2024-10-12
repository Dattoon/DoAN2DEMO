using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.CategorySub;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class CategorySubController : BaseAdminController
    {
        public readonly IMapper _mapper;
        protected readonly IWebHostEnvironment Host;
        public CategorySubController(BaseReponsitory _repo,IMapper mapper, IWebHostEnvironment host) : base(_repo)
        {
            _mapper = mapper;
            Host = host;
        }
        [AppAuthorize(AuthConst.CategorySub.VIEW_LIST)]
        public IActionResult Index(int page=1,int size=10)
        {
            var data = _repo.GetAll<CategorySub,ListCategorySubItemVM>(MapperConfig.CategorySubIndexConf)
                .ToPagedList(page, size);
            return View(data);
        }
        [AppAuthorize(AuthConst.CategorySub.CREATE)]
        public IActionResult _Create()
        {
            return PartialView();
        }
        [HttpPost]
        [AppAuthorize(AuthConst.CategorySub.CREATE)]
        public async Task<IActionResult>_Create(AddorUpdateCategorySubVM model)
        {
            if (model.Image != null)
            {
                string image = UploadImgAndReturnPath(model.Image, "/img/categorysub/");
                image = image.Split('/').Last();
                model.pathImg = image;
            }
            var data =_mapper.Map<CategorySub>(model);
            data.CountView = 0;
            await _repo.AddAsync(data);
            SetSuccessMesg("Thêm danh mục bài học thành công");
            return Ok(true);
        }
        [AppAuthorize(AuthConst.CategorySub.UPDATE)]
        public async Task<IActionResult> _Update(int id)
        {
            var data = await _repo.GetOneAsync<CategorySub, AddorUpdateCategorySubVM>(id, s => new AddorUpdateCategorySubVM
            {
                Id = s.Id,
                NameCategorySub = s.NameCategorySub,
                Descripstion=s.Descripstion,
                Price=s.Price
            });
            return PartialView(data);
        }
        [HttpPost]
        [AppAuthorize(AuthConst.CategorySub.UPDATE)]
        public async Task<IActionResult> _Update(AddorUpdateCategorySubVM model)
        {
            var data= await _repo.FindAsync<CategorySub>(model.Id);
            model.pathImg= data.pathImg;
            if(data != null)
            {
                _mapper.Map<AddorUpdateCategorySubVM,CategorySub>(model, data);
                await _repo.UpdateAsync(data);
                SetSuccessMesg("Sửa danh mục bài học thành công");
            }
            return Ok(true);

        }
        [AppAuthorize(AuthConst.CategorySub.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            var data=await _repo.FindAsync<CategorySub>(id);
            await _repo.DeleteAsync(data);
            SetSuccessMesg("Xóa danh mục thành công");
            return RedirectToAction("Index");
        }
        string UploadImgAndReturnPath(IFormFile file, string childFolder = "/img/", bool saveInWwwRoot = true)
        {
            var y = Host.WebRootPath;
            var root = saveInWwwRoot ? Host.WebRootPath : Host.ContentRootPath;
            var filename = Path.GetFileNameWithoutExtension(file.FileName)
                            + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")
                            + Path.GetExtension(file.FileName);
            if (!Directory.Exists(root + childFolder))
            {
                Directory.CreateDirectory(root + childFolder);
            }
            var relativePath = childFolder + filename;
            var path = root + relativePath;
            var x = new FileStream(path, FileMode.Create);
            file.CopyTo(x);
            x.Dispose();
            GC.Collect();
            return relativePath;
        }
    }
}
