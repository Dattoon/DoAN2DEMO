using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Share.Const
{
    public static class AuthConst
    {
        public const int NO_PERMISSION = -1;
        public static class User
        {
            public const int VIEW_LIST = 1001;
            public const int VIEW_DETAIL = 1002;
            public const int CREATE = 1003;
            public const int UPDATE = 1004;
            public const int UPDATE_PWD = 1005;
            public const int BLOCK = 1006;
            public const int UNBLOCK = 1007;
            public const int DELETE = 1008;
        }
        public static class Answer
        {
            public const int VIEW_LIST = 2001;
            public const int VIEW_DETAIL = 2002;
            public const int CREATE = 2003;
            public const int UPDATE = 2004;
            public const int DELETE = 2008;
        }
        public static class CategoryNew
        {
            public const int VIEW_LIST = 3001;
            public const int CREATE = 3003;
            public const int UPDATE = 3004;
            public const int DELETE = 3008;
        }
        public static class CategorySub
        {
            public const int VIEW_LIST = 4001;
            public const int CREATE = 4003;
            public const int UPDATE = 4004;
            public const int DELETE = 4008;
        }
        public static class Chapter
        {
            public const int VIEW_DETAIL = 5002;
            public const int VIEW_LIST = 5001;
            public const int CREATE = 5003;
            public const int UPDATE = 5004;
            public const int DELETE = 5008;
        }
        public static class Lesson
        {
            public const int VIEW_DETAIL = 6002;
            public const int VIEW_LIST = 6001;
            public const int CREATE = 6003;
            public const int UPDATE = 6004;
            public const int DELETE = 6008;
        }
        public static class Report
        {
            public const int VIEW_LIST = 7001;
            public const int VIEW_DETAIL = 7002;
            public const int DELETE = 7008;
        }
        public static class Subject
        {
            public const int VIEW_DETAIL = 8002;
            public const int VIEW_LIST = 8001;
            public const int CREATE = 8003;
            public const int UPDATE = 8004;
            public const int DELETE = 8008;
        }
        public static class News
        {
            public const int VIEW_LIST = 9001;
            public const int VIEW_DETAIL = 9002;
            public const int CREATE = 9003;
            public const int UPDATE = 9004;
            public const int DELETE = 9008;
        }
        public static class Test
        {
            public const int VIEW_LIST = 9101;
            public const int VIEW_DETAIL = 9102;
            public const int CREATE = 9103;
            public const int UPDATE = 9104;
            public const int DELETE = 9108;
        }
        public static class Role
        {
            public const int VIEW_LIST = 9201;
            public const int VIEW_DETAIL = 9202;
            public const int CREATE = 9203;
            public const int UPDATE = 9204;
            public const int DELETE = 9208;
        }
        public static class PurchasedCourse
        {
            public const int VIEW_LIST = 9301;
            public const int VIEW_DETAIL = 9302;
            public const int CREATE = 9303;
        }



    }
}
