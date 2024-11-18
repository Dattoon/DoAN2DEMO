using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Controllers
{
    public class NewsClientController : BaseClientController
    {
        public NewsClientController(BaseReponsitory repo) : base(repo)
        {
        }

        public IActionResult Index(string query, int page = 1, int size = 10)
        {
            IQueryable<News> data;
            if (!string.IsNullOrEmpty(query))
            {
                data = _repo.GetAll<News>()
                            .Where(n => n.Title.Contains(query) || n.Description.Contains(query));
            }
            else
            {
                data = _repo.GetAll<News>();
            }

            var pagedList = data.ToPagedList(page, size);
            ViewBag.Query = query; // Để hiển thị lại từ khóa tìm kiếm trong view nếu cần
            return View(pagedList);
        }

        public async Task<IActionResult> DetailNews(int id)
        {
            var data = await _repo.FindAsync<News>(id);
            return View(data);
        }
    }
}
