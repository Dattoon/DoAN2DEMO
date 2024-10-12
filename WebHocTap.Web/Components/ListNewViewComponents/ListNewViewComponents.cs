using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.Areas.Admin.Controllers;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;

namespace WebHocTap.Web.Components.ListNewViewComponents
{
    public class ListNewViewComponents:ViewComponent
    {
        public readonly BaseReponsitory _repo;
        public ListNewViewComponents(BaseReponsitory repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var data= _repo.GetAll<CategoryNew,ListNewVM>(MapperConfig.ListCateNewClIndexConf).ToList();
            return View(data);
        }
    }
}
