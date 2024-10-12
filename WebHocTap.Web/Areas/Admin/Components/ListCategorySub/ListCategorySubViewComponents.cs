using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Components.ListCategorySub
{
    public class ListCategorySubViewComponents:ViewComponent
    {
        public readonly BaseReponsitory _repo;
        public ListCategorySubViewComponents(BaseReponsitory repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selected)
        {
            var data = await _repo.GetAll<CategorySub>().ToListAsync();
            if (selected != null)
            {
                ViewBag.selected = selected;
            }
            return View(data);
        }
    }
}
