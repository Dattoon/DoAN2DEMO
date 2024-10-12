using WebHocTap.Data.Entites;
using WebHocTap.Data.Reponsitory;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using X.PagedList;

namespace WebHocTap.Web.Components.ListCategorySubPaymentViewComponents
{
    public class ListCategorySubPaymentViewComponents:ViewComponent
    {
        public readonly BaseReponsitory _repo;
        public ListCategorySubPaymentViewComponents(BaseReponsitory repo)
        {
            _repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync(int iduser)
        {

            if (iduser != 0)
            {
                var key = _repo.GetAll<PurchasedCourse>(x => x.IdUser == iduser).ToList();
                var categorysub = _repo.GetAll<CategorySub>(x => x.Price != null).ToList();
                var data = new List<CategorySub>();
                foreach (var c in categorysub)
                {
                    var check=false;
                    foreach (var k in key)
                    {
                      if(c.Id==k.IdSub)
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
                // lọc lại những phần tử trùng nhau ở vòng lặp ở trên
               
                var data1 = data.Take(8).ToList();
                return View(data1);
            }
            else
            {
                var data = _repo.GetAll<CategorySub>(x => x.Price != null).Take(8).ToList();
                return View(data);
            }


            //var data = _repo.GetAll<CategorySub>(x=>x.Price!=null).Take(8).ToList();
            //return View(data);
        }
    }
}
