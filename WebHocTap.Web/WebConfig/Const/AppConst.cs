﻿namespace WebHocTap.Web.WebConfig.Const
{
    public class AppConst
    {

        // Phương thức xác thực người dùng
        public const string COOKIES_AUTH = "Cookies";

        // Tên trang web, xuất hiện ở góc trên trái của giao diện
        public const string APP_NAME = "Base Template";

        // Đường dẫn trang đăng nhập
        public const string LOGIN_PATH = "/Login";

        // đường dẫn trang đăng nhập admin
        public const string ADMIN_LOGIN_PATH = "/Admin/Login";

        // Thời gian đăng nhập tối đa
        public const byte LOGIN_TIMEOUT = 10; // 10 giờ

        // Đường dẫn cho hệ thống file
        public const string SYSTEM_FILE_PATH = "files";
        // Dung lượng file tối đa 5MB (user thường)
        public const int USER_MAX_SIZE_UPLOAD_IN_KB = 5120;
        // Dung lượng file tối đa 20MB (user có quyền quản lý hệ thống file
        public const int MANAGER_MAX_SIZE_UPLOAD_IN_KB = 20480;
        // Danh sách file bị cấm tải lên
        public static readonly string[] DENY_FILES = { /*"application",	*/				// .exe, .dll, .bat, .cmd, .php, .js, .json,.pdf,...
																	"text/html",					// .html
																	"text/css",						// .css
																};
        // key Random pass
        public const string UPPER_CASE = "QWERTYUIOPASDFGHJKLZXCVBNM";
        public const string LOWER_CASE = "qwertyuiopasdfghjklzxcvbnm";
        public const string DIGITS = "1234567890";

        // id role khách hàng
        public const short ROLE_CUSTOMER_ID = 1;
    }
}
