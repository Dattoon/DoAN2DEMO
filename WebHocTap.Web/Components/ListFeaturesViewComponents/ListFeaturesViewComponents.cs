using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace WebHocTap.Web.Components.ListFeaturesViewComponents
{
    public class ListFeaturesViewComponents:ViewComponent
    {
        public readonly BaseReponsitory _repo;
        public ListFeaturesViewComponents(BaseReponsitory repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
