using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Extensions;
using WebHocTap.Web.Areas.Admin.ViewModels.Home;
using WebHocTap.Web.WebConfig;
using Microsoft.AspNetCore.Mvc;
using PayPal.v1.Invoices;
using X.PagedList;

namespace WebHocTap.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController(BaseReponsitory repo) : base(repo)
        {

        }
        public IActionResult Index()
        {
            var donHangBanDuocHomNay = _repo.GetAll<PurchasedCourse>().Where(x => x.CreateAt >= DateTime.Today).ToList();
            var donHangBanDuoc = _repo.GetAll<PurchasedCourse>().ToList();
            int tongdonHangBanDuoc = 0;
            var tongdonHangBanDuocHomNay = 0;
            int EarningsToday = 0;
            if (donHangBanDuocHomNay.Count > 0)
            {
                for (int i = 0; i < donHangBanDuocHomNay.Count; i++)
                {
                    tongdonHangBanDuocHomNay += 1;
                    EarningsToday += donHangBanDuocHomNay[i].Price.Value;
                }
            }
            if (donHangBanDuoc.Count > 0)
            {
                for (int i = 0; i < donHangBanDuoc.Count; i++)
                {
                    tongdonHangBanDuoc += donHangBanDuoc[i].Price.Value;
                }
            }
            StatisticalVM data = new StatisticalVM();
            data.DonHangBanDuocHomNay = tongdonHangBanDuocHomNay;
            data.EarningsToday = EarningsToday;
            data.totalrevenue = tongdonHangBanDuoc;

            return View(data);
        }
        public IActionResult GetPurchasedCourse(DateTime? date, int page = 1, int size = 10)
        {

            if (date == null)
            {
                var data = _repo.GetAll<PurchasedCourse, ListPurchasedCourseItem>(MapperConfig.PurchasedCourseIndexConf).ToPagedList(page, size);

                return View(data);
            }
            else
            {
                var stringdate = date.ToDMY();
                List<ListPurchasedCourseItem> datatemp = new List<ListPurchasedCourseItem>();
                var listdataa = _repo.GetAll<PurchasedCourse, ListPurchasedCourseItem>(MapperConfig.PurchasedCourseIndexConf).ToList();
                foreach (var item in listdataa)
                {
                    if (item.CreateAt.ToDMY() == stringdate)
                    {
                        datatemp.Add(item);
                    }
                }
                PagedList<ListPurchasedCourseItem> data = new PagedList<ListPurchasedCourseItem>(datatemp, page, size);
                return View(data);
            }
        }
    }
}
