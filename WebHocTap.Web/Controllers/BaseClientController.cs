using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.Common;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace WebHocTap.Web.Controllers
{
    public class BaseClientController : Controller
    {
        protected readonly BaseReponsitory _repo;
        private readonly ILog _logger;
        public BaseClientController(BaseReponsitory repo)
        {
            _repo = repo;
            _logger = LogManager.GetLogger(typeof(BaseClientController));
        }
        protected void SetErrorMesg(string mesg, bool modelStateIsInvalid = false)
        {
            TempData["Err"] = mesg;
            if (modelStateIsInvalid)
            {
                // hiển thị tin nhắn lỗi ở file log
                var invalidMesg = string.Join("\n", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage));
                _logger.Error($"Model state is invalid: {invalidMesg}");
            }
        }

        protected void SetSuccessMesg(string mesg) => TempData["Messenger"] = mesg;
        protected HashResult HashHMACSHA512(string pwd)
        {
            var hashResult = new HashResult();
            HMACSHA512 hmac = new();
            var pwdByte = Encoding.UTF8.GetBytes(pwd);
            hashResult.Value = hmac.ComputeHash(pwdByte);
            hashResult.Key = hmac.Key;
            return hashResult;
        }
        protected byte[] HashHMACSHA512WithKey(string pwd, byte[] key)
        {
            HMACSHA512 hmac = new(key);
            var pwdByte = Encoding.UTF8.GetBytes(pwd);
            return hmac.ComputeHash(pwdByte);
        }
    }
}
