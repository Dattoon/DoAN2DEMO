using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Controllers
{
    public class NewsClientController : BaseClientController
    {
        public NewsClientController(BaseReponsitory repo):base(repo) 
        {

        }
        public IActionResult Index(int page=1,int size=10)
        {
            var data = _repo.GetAll<News>().ToPagedList(page, size);
            return View(data);
        }
        public async Task< IActionResult> DetailNews(int id)
        {
            var data =await _repo.FindAsync<News>(id);
            return View(data);
        }
    }
}
