using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Areas.Admin.ViewModels.News;
using WebHocTap.Web.Common;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class NewsController : BaseAdminController
    {
        public readonly IMapper _mapper;
        protected readonly IWebHostEnvironment Host;
        public NewsController(BaseReponsitory _repo,IMapper mapper, IWebHostEnvironment host) : base(_repo)
        {
            _mapper = mapper;
            Host = host;
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
        [AppAuthorize(AuthConst.News.VIEW_LIST)]
        public IActionResult Index(int page =1,int size=10)
        {
            var data= _repo.GetAll<News,ListNewsItemVM>(MapperConfig.NewsIndexConf)
                .ToPagedList(page,size);
            return View(data);
        }
        [AppAuthorize(AuthConst.News.CREATE)]
        public IActionResult Create() {
            return View();
        }
        [AppAuthorize(AuthConst.News.CREATE)]
        [HttpPost]
        public async Task<IActionResult> Create(AddorUpdateNewsVM model)
        {
            if (model.Image != null)
            {
                string image = UploadImgAndReturnPath(model.Image, "/img/news/");
                image = image.Split('/').Last();
                model.pathImg = image;
            }
            var data=_mapper.Map<News>(model);
            await _repo.AddAsync(data);
            SetSuccessMesg("Thêm thông báo thành công");
            return RedirectToAction("Index");
        }
        [AppAuthorize(AuthConst.News.CREATE)]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _repo.GetOneAsync<News, AddorUpdateNewsVM>(id, n => new AddorUpdateNewsVM
            {
                Id = n.Id,
                Description = n.Description,
                IdCategoryNew = n.IdCategoryNew,
                pathImg = n.pathImg,
                Title = n.Title,
            });
            return View(data);
        }
        [AppAuthorize(AuthConst.News.CREATE)]
        [HttpPost]
        public async Task<IActionResult> Update(AddorUpdateNewsVM model)
        {
            var data = await _repo.FindAsync<News>(model.Id);
            if (data != null)
            {
                if(model.Image!= null)
                {
                    string image = UploadImgAndReturnPath(model.Image, "/img/news/");
                    image = image.Split('/').Last();
                    model.pathImg = image;
                }
                else
                {
                    model.pathImg=data.pathImg;
                }
                _mapper.Map<AddorUpdateNewsVM,News>(model,data);
                await _repo.UpdateAsync(data);
                SetSuccessMesg("Câp nhật thành công");
            }
            return RedirectToAction("Index");
        }
        [AppAuthorize(AuthConst.News.DELETE)]
        public async Task<IActionResult>Delete(int id)
        {
            var data= await _repo.FindAsync<News>(id);
            if (data != null)
            {
                await _repo.DeleteAsync(data);
                SetSuccessMesg("Xóa thành công");
            }
            return RedirectToAction("Index");
        }
    }
}
