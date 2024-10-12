using WebHocTap.Data.Reponsitory;
using WebHocTap.Share.Const;
using Microsoft.AspNetCore.Mvc;

namespace WebHocTap.Web.Areas.Admin.Components.MainNavBar
{
    public class MainNavBarViewComponent:ViewComponent
    {
        readonly BaseReponsitory repository;
        public MainNavBarViewComponent(BaseReponsitory _repository)
        {
            repository = _repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navBar = new NavBarViewModel();
            navBar.Items.AddRange(new MenuItem[]
            {
                 new MenuItem
                        {
                            Action = "Index",
                            Controller = "Home",
                            DisplayText = "Trang chủ",
                            Icon = "fa-home",
                            //Permission = AuthConst.C.VIEW_LIST
                        },
                 new MenuItem
                        {
                            Action = "Index",
                            Controller = "Role",
                            DisplayText = "Quản lý phân quyền",
                            Icon = "fa-home",
                            Permission = AuthConst.Role.VIEW_LIST
                        },
                 new MenuItem
                        {
                            Action = "Index",
                            Controller = "User",
                            DisplayText = "Quản lý tài khoản",
                            Icon = "fa-home",
                            Permission = AuthConst.User.VIEW_LIST
                        },
                  new MenuItem
                {
                            DisplayText = "Quản lý tin tức",
                            Icon = "fa-tasks",
                            ChildrenItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Action = "Index",
                                    Controller = "CategoryNew",
                                    DisplayText = "Thể loại tin tức",
                                    Icon = "fa-flag-checkered",
                                    Permission = AuthConst.CategoryNew.VIEW_LIST
                                },new MenuItem
                                {
                                    Action = "Index",
                                    Controller = "News",
                                    DisplayText = "Tin tức",
                                    Icon = "fa-flag-checkered",
                                    Permission = AuthConst.News.VIEW_LIST
                                }

                            }
                 },
                  new MenuItem
                {
                            DisplayText = "Quản lý môn học",
                            Icon = "fa-tasks",
                            ChildrenItems = new MenuItem[]
                            {
                                new MenuItem
                                {
                                    Action = "Index",
                                    Controller = "CategorySub",
                                    DisplayText = "Danh mục môn học",
                                    Icon = "fa-flag-checkered",
                                    Permission = AuthConst.CategorySub.VIEW_LIST
                                },
                                new MenuItem
                                {
                                    Action = "Index",
                                    Controller = "Sub",
                                    DisplayText = "Danh sách môn học",
                                    Icon = "fa-flag-checkered",
                                    Permission = AuthConst.Subject.VIEW_LIST
                                }
                            }
                 },
            });
            return View(navBar);
        }
    }
}
