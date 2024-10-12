using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebHocTap.Web.Areas.Admin.Components.FilterCategorySubViewComponents
{
    public class FilterCategorySubViewComponents:ViewComponent
    {
        public readonly BaseReponsitory _repo;
        public FilterCategorySubViewComponents(BaseReponsitory repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _repo.GetAll<CategorySub>().ToListAsync();
           
            return View(data);
        }
    }
}
