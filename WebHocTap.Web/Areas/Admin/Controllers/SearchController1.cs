using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.ViewModels.Subject;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly SearchRepository _searchRepo;

        public SearchController(SearchRepository searchRepo)
        {
            _searchRepo = searchRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                model.Results = _searchRepo.SearchCategoryNew(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.NameCategoryNew,
                        DetailsUrl = Url.Action("Index", "CategoryNew", new { area = "Admin", id = x.Id }) // URL chi tiết đến CategoryNewController
                    })
                    .ToList();

                model.Results.AddRange(_searchRepo.SearchCategorySub(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.NameCategorySub,
                        DetailsUrl = Url.Action("Index", "CategorySub", new { area = "Admin", id = x.Id }) // URL chi tiết đến CategorySubController
                    })
                    .ToList());

                model.Results.AddRange(_searchRepo.SearchChapter(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.NameChapter,
                        DetailsUrl = Url.Action("Index", "Chapter", new { area = "Admin", id = x.Id }) // URL chi tiết đến ChapterController
                    })
                    .ToList());

                

                model.Results.AddRange(_searchRepo.SearchNews(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.Title,
                        DetailsUrl = Url.Action("Index", "News", new { area = "Admin", id = x.Id }) // URL chi tiết đến NewsController
                    })
                    .ToList());

                model.Results.AddRange(_searchRepo.SearchRole(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.RoleName,
                        DetailsUrl = Url.Action("Index", "Role", new { area = "Admin", id = x.Id }) // URL chi tiết đến RoleController
                    })
                    .ToList());

                model.Results.AddRange(_searchRepo.SearchSubject(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.NameSubject,
                        DetailsUrl = Url.Action("Index", "Subject", new { area = "Admin", id = x.Id }) // URL chi tiết đến SubjectController
                    })
                    .ToList());

                model.Results.AddRange(_searchRepo.SearchUser(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        DetailsUrl = Url.Action("Index", "User", new { area = "Admin", id = x.Id }) // URL chi tiết đến UserController
                    })
                    .ToList());

                model.Results.AddRange(_searchRepo.SearchTest(model.Keyword)
                    .Select(x => new SearchResultViewModel
                    {
                        Id = x.Id,
                        Name = x.Question,
                        DetailsUrl = Url.Action("Index", "Test", new { area = "Admin", id = x.Id }) // URL chi tiết đến TestController
                    })
                    .ToList());
            }
            else
            {
                model.Results = new List<SearchResultViewModel>();
            }

            return View("Index", model);
        }
    }
}
