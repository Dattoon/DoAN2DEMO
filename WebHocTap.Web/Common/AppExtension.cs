using WebHocTap.Share.Const;
using WebHocTap.Share.Extensions;
using WebHocTap.Web.WebConfig.Const;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace WebHocTap.Web.Common
{
    public static class AppExtension
    {
        public static string GetCurrentActionName(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["action"].ToString();
        }
        public static string GetCurrentControllerName(this ViewContext viewContext)
        {
            return viewContext.RouteData.Values["controller"].ToString();
        }
        public static bool IsInPermission(this ClaimsPrincipal user, int actionPermission)
        {
            var userPermission = user.FindFirstValue(AppClaimType.Permissions);
            if (userPermission.IsNullOrEmpty())
            {
                return false;
            }
            if (actionPermission == AuthConst.NO_PERMISSION)
            {
                return true;
            }
            return userPermission.Contains(actionPermission.ToString());
        }
        /// <summary>
        /// Kiểm tra xem cái date có phải là ngày trước đó của cái date2 không
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsBefore(this DateTime date, DateTime date2)
        {
            if (date < date2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra xem cái date có phải là ngày sau đó của cái date2 không
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsAfter(this DateTime date, DateTime date2)
        {
            if (date > date2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra xem cái dd/MM/yyyy của date có giống nhau với dd/MM/yyyy date2 không
        /// </summary>
        /// <param name="date"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsSameDate(this DateTime date, DateTime date2)
        {
            if (date.Date.Equals(date2.Date))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// kiếm tra xem ngày có phải là ngày nằm giữa của 2 ngày khác không
        /// </summary>
        /// <param name="date"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime date, DateTime d1, DateTime d2)
        {
            if (date.IsBefore(d2) && date.IsAfter(d1))
            {
                return true;
            }
            return false;
        }


        
            public static bool IsAjaxRequest(this HttpRequest request)
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.Headers != null)
                {
                    return request.Headers["X-Requested-With"] == "XMLHttpRequest";
                }

                return false;
            }
        

    }
}
