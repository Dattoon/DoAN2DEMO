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
        [HttpGet]
        public IActionResult GetRevenueData()
        {
            var revenueData = _repo.GetAll<PurchasedCourse>()
                                   .AsEnumerable() // Chuyển về LINQ to Objects
                                   .GroupBy(x => x.CreateAt.Value.Date) // Sử dụng .Date trong LINQ to Objects
                                   .Select(g => new
                                   {
                                       Date = g.Key.ToString("yyyy-MM-dd"), // Chuyển ngày thành chuỗi
                                       TotalRevenue = g.Sum(x => x.Price ?? 0) // Tính tổng doanh thu
                                   })
                                   .OrderBy(x => x.Date)
                                   .ToList();

            return Json(revenueData);
        }
        [HttpGet]
        public IActionResult GetMonthlyRevenueData()
        {
            // Nhóm dữ liệu theo tháng và tính tổng doanh thu
            var monthlyRevenueData = _repo.GetAll<PurchasedCourse>()
                                          .Where(x => x.CreateAt.HasValue) // Loại bỏ null
                                          .GroupBy(x => new { x.CreateAt.Value.Year, x.CreateAt.Value.Month }) // Nhóm theo năm và tháng
                                          .Select(g => new
                                          {
                                              Year = g.Key.Year,
                                              Month = g.Key.Month,
                                              TotalRevenue = g.Sum(x => x.Price ?? 0) // Tổng doanh thu
                                          })
                                          .OrderBy(x => x.Year).ThenBy(x => x.Month) // Sắp xếp theo năm và tháng
                                          .ToList();

            // Thực hiện định dạng chuỗi sau khi lấy dữ liệu về
            var result = monthlyRevenueData.Select(x => new
            {
                Month = $"{x.Year}-{x.Month:D2}", // Định dạng yyyy-MM
                TotalRevenue = x.TotalRevenue
            }).ToList();

            return Json(result);
        }



    }
}
