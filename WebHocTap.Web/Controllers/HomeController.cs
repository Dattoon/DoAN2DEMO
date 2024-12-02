using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using WebHocTap.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using PayPal.Core;
using PayPal.v1.Payments;
using PayPal.v1.Orders;
using BraintreeHttp;
using WebHocTap.Web.Common;
using X.PagedList;
using WebHocTap.Web.Common.Mailer;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Claims;
using WebHocTap.Web.ViewModels;

namespace WebHocTap.Web.Controllers
{
    public class HomeController : BaseClientController
    {

        private readonly ILogger<HomeController> _logger;
        private readonly string _clientId;
        private readonly string _secretKey;
        public readonly int USD;
        private readonly AppMailConfiguration _mailConfig;
        private readonly IHostingEnvironment _env;
        public HomeController(ILogger<HomeController> logger,BaseReponsitory repo, IConfiguration configuration, AppMailConfiguration mailConfig, IHostingEnvironment env) : base(repo)
        {
            _logger = logger;
            _clientId = configuration["PaypalSetting:ClientID"];
            _secretKey = configuration["PaypalSetting:SecretKey"];
            USD = 23000;
            _mailConfig = mailConfig;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        // trag xem khóa học tra tiền để quyết định có mua hay k
        [AppAuthorize]
        public async Task<IActionResult> DetailSub(int id)
        {
            var data=await _repo.FindAsync<CategorySub>(id);
            return View(data);
        }


        public IActionResult Search(string query, int page = 1, int size = 10)
        { var newsResults = _repo.GetAll<News>().Where(n => n.Title.Contains(query) || n.Description.Contains(query)).ToPagedList(page, size); 
            var courseResults = _repo.GetAll<CategorySub>().Where(c => c.NameCategorySub.Contains(query) || c.Descripstion.Contains(query)).ToPagedList(page, size);
            var viewModel = new SearchViewModel
            {
                Query = query,
                NewsResults = newsResults, 
                CourseResults = courseResults
            };
            return View(viewModel); 
        }


        [AppAuthorize]
        public async Task<IActionResult> PaypalCheckOut(int id)
        {
            var categorysub=await _repo.FindAsync<CategorySub>(id);
            var environment = new SandboxEnvironment(_clientId, _secretKey);
            var client = new PayPalHttpClient(environment);
            var itemList = new ItemList()
            {
                Items = new List<PayPal.v1.Payments.Item>()
            };
            var priceUSD = Math.Round(Convert.ToDecimal(categorysub.Price / USD), 2);
            itemList.Items.Add(new PayPal.v1.Payments.Item()
            {
                Name = categorysub.NameCategorySub,
                Currency = "USD",
                Price = priceUSD.ToString(),
                Quantity = "1",
                Sku = "sku",
                Tax = "0"
            });




            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new PayPal.v1.Payments.Amount()
                        {
                            Total = priceUSD.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = priceUSD.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new PayPal.v1.Payments.RedirectUrls()
                {
                    CancelUrl = $"{hostname}/Home/CheckoutFail",
                    ReturnUrl = $"{hostname}/Home/CheckoutSuccess/?id={categorysub.Id}"
                },
                Payer = new PayPal.v1.Payments.Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
           
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    PayPal.v1.Payments.LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }
                
                return Redirect(paypalRedirectUrl);
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/Home/CheckoutFail");
            }
        }
        public async Task<IActionResult> CheckoutSuccess(int id)
        {
            var subject = await _repo.FindAsync<CategorySub>(id);
            var purchasedCourse = new PurchasedCourse
            {
                IdUser = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(ClaimTypes.NameIdentifier))?.Value),
                IdSub = id,
                Price = subject.Price
            };

            // Nội dung email bằng HTML
            var emailContent = string.Format(@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            .email-container {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                max-width: 600px;
                margin: 0 auto;
                border: 1px solid #ddd;
                padding: 20px;
                box-shadow: 0 0 10px rgba(0,0,0,0.1);
            }}
            .header {{
                background-color: #007bff;
                padding: 10px;
                color: white;
                text-align: center;
            }}
            .content {{
                padding: 20px;
            }}
            .footer {{
                background-color: #f4f4f4;
                padding: 10px;
                text-align: center;
                font-size: 12px;
                color: #666;
            }}
            .button {{
                display: inline-block;
                padding: 10px 20px;
                margin-top: 10px;
                background-color: #007bff;
                color: #fff;
                text-decoration: none;
                border-radius: 5px;
            }}
            .highlight {{
                color: #007bff;
            }}
        </style>
    </head>
    <body>
        <div class=""email-container"">
            <div class=""header"">
                <h1>Cảm ơn bạn đã mua khóa học!</h1>
            </div>
            <div class=""content"">
                <p>Xin chào <strong>{0}</strong>,</p>
                <p>Chúc mừng bạn đã đăng ký thành công khóa học <strong class=""highlight"">{1}</strong>!</p>
                <p>Chi tiết khóa học:</p>
                <ul>
                    <li>Tên khóa học: <strong>{1}</strong></li>
                    <li>Giá: <strong>{2}</strong> VND</li>
                    <li>Ngày mua: <strong>{3}</strong></li>
                </ul>
                <p>Bạn có thể truy cập khóa học của mình bất cứ lúc nào bằng cách đăng nhập vào tài khoản của bạn. Chúng tôi hi vọng rằng bạn sẽ có trải nghiệm học tập tuyệt vời.</p>
                <a href=""/Account/Login"" class=""button"">Truy cập khóa học</a>
            </div>
            <div class=""footer"">
                <p>Xin cảm ơn,<br>Đội ngũ hỗ trợ của chúng tôi</p>
                <p><a href=""https://www.example.com"" class=""highlight"">Trang chủ</a> | <a href=""https://www.example.com/contact"" class=""highlight"">Liên hệ</a></p>
            </div>
        </div>
    </body>
    </html>",
            User.FindFirstValue(ClaimTypes.Name),
            subject.NameCategorySub,
            subject.Price?.ToString("#,##0"),
            DateTime.Now.ToString("dd/MM/yyyy"));

            var appMailSendercumstumner = new AppMailSender
            {
                Name = "Đội ngũ hỗ trợ",
                Subject = "Cảm ơn bạn đã mua khóa học",
                Content = emailContent
            };

            var appMailRecivercustommer = new AppMailReciver
            {
                Email = User.FindFirstValue(ClaimTypes.Email),
                Name = User.FindFirstValue(ClaimTypes.Name)
            };

            var emailService = new AppMailer(_mailConfig)
            {
                Sender = appMailSendercumstumner,
                Reciver = appMailRecivercustommer
            };

            emailService.Send();

            await _repo.AddAsync(purchasedCourse);

            return View();
        }

        public IActionResult CheckoutFail()
        {
            return View();
        }
        public IActionResult ListCateSubFree(int page=1,int size=10)
        {
            var data=_repo.GetAll<CategorySub>(x=>x.Price==null).ToPagedList(page,size);
            return View(data);
        }
        public IActionResult ListCateSubPay(int page = 1, int size = 10)
        {
            var iduser = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(System.Security.Claims.ClaimTypes.NameIdentifier))?.Value);
            if (iduser != 0 ) {
                var key = _repo.GetAll<PurchasedCourse>(x => x.IdUser == iduser).ToList();
                var categorysub = _repo.GetAll<CategorySub>(x => x.Price != null).ToPagedList(page, size);
                var data = new List<CategorySub>();
                foreach (var c in categorysub)
                {
                    var check = false;
                    foreach (var k in key)
                    {
                        if (c.Id == k.IdSub)
                        {
                            check = true;
                            break;
                        }
                    }
                    if (!check)
                    {
                        data.Add(c);
                    }
                }
                var data1 = data.ToPagedList(page, size);
                return View(data1);
            }
            else
            {
                var data = _repo.GetAll<CategorySub>(x => x.Price != null).ToPagedList(page, size);
                return View(data);
            }



        }
        public IActionResult ListSubUserBuy() { 
            var categorysub= _repo.GetAll<CategorySub>().ToList();
            var iduser = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type.Contains(System.Security.Claims.ClaimTypes.NameIdentifier))?.Value);
            var key = _repo.GetAll<PurchasedCourse>(x => x.IdUser == iduser).ToList();
            var data = new List<CategorySub>();
            foreach (var c in categorysub)
            {
                foreach (var k in key)
                {
                    if (c.Id == k.IdSub)
                    {
                        data.Add(c);
                    }
                }
            }
            return View(data);
        }


        public IActionResult ChoosePaymentMethod(int id)
        {
            var model = new PaymentMethodViewModel { CategorySubId = id };
            return View(model);
        }

        [HttpPost]
        public IActionResult ProcessPayment(PaymentMethodViewModel model)
        {
            switch (model.SelectedMethod)
            {
                case "PayPal":
                    return RedirectToAction("PaypalCheckOut", new { id = model.CategorySubId });
                case "CreditCard":
                    // Xử lý thanh toán bằng thẻ tín dụng ở đây
                    return RedirectToAction("CreditCardPayment", new { id = model.CategorySubId });
                default:
                    return RedirectToAction("ChoosePaymentMethod", new { id = model.CategorySubId });
            }
        }
        public IActionResult GetNewsCategories() { var categories = _repo.GetAll<CategoryNew>().Select(c => new { c.Id, c.NameCategoryNew }).ToList(); return Json(categories); }

    }
}