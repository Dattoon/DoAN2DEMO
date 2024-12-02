using Microsoft.AspNetCore.Mvc;
using WebHocTap.Data.Reponsitory;
using System.Linq;
using System.Threading.Tasks;
using WebHocTap.Data.Entites;
using WebHocTap.Web.Components.ListNewViewComponents;
using WebHocTap.Web.WebConfig;

namespace WebHocTap.Web.Components
{
    public class NewsCategoriesViewComponent : ViewComponent
    {
        private readonly BaseReponsitory _repo;

        public NewsCategoriesViewComponent(BaseReponsitory repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _repo.GetAll<CategoryNew, ListNewVM>(MapperConfig.ListCateNewClIndexConf).ToList();
            return View(data);
        }
    }
}
