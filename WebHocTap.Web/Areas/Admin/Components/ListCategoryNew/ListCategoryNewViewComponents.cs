using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Components.ListCategoryNew
{
    public class ListCategoryNewViewComponents :ViewComponent
    {
        readonly BaseReponsitory repository;
        public ListCategoryNewViewComponents(BaseReponsitory _repository)
        {
            repository = _repository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? selected)
        {
            var data = await repository.GetAll<CategoryNew>().ToListAsync();
            if (selected != null)
            {
                ViewBag.selected = selected;
            }
            return View(data);
        }
    }
}
