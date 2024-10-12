using WebHocTap.Data.Entites;
using WebHocTap.Share.Const;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.DataSender
{
    public static class MstPerMissionSender
    {
        public static void SeedData(this EntityTypeBuilder<MstPerMission> builder)
        {
            var now = new DateTime(year: 2021, month: 11, day: 10);
            var groupName = "";
            #region Data liên quan đến bảng Answer
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý câu hỏi";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Answer.CREATE,
                    Code = "CREATE",
                    Table = NameTable.AnswerTable,
                    GroupName = groupName,
                    Desc = "Thêm câu hỏi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Answer.DELETE,
                    Code = "DELETE",
                    Table = NameTable.AnswerTable,
                    GroupName = groupName,
                    Desc = "Xóa câu hỏi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Answer.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.AnswerTable,
                    GroupName = groupName,
                    Desc = "Sửa câu hỏi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Answer.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.AnswerTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết câu hỏi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Answer.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.AnswerTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách câu hỏi",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng CategoryNews
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý thể loại tin tức";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.CategoryNew.CREATE,
                    Code = "CREATE",
                    Table = NameTable.CategoryNewTable,
                    GroupName = groupName,
                    Desc = "Thêm thể loại tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategoryNew.DELETE,
                    Code = "DELETE",
                    Table = NameTable.CategoryNewTable,
                    GroupName = groupName,
                    Desc = "Xóa thể loại tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategoryNew.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.CategoryNewTable,
                    GroupName = groupName,
                    Desc = "Sửa thể loại tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategoryNew.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.CategoryNewTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách thể loại tin tức",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng CategorySub
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý danh mục";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.CategorySub.CREATE,
                    Code = "CREATE",
                    Table = NameTable.CategorySubTable,
                    GroupName = groupName,
                    Desc = "Thêm danh mục",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategorySub.DELETE,
                    Code = "DELETE",
                    Table = NameTable.CategorySubTable,
                    GroupName = groupName,
                    Desc = "Xóa danh mục",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategorySub.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.CategorySubTable,
                    GroupName = groupName,
                    Desc = "Sửa danh mục",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.CategorySub.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.CategorySubTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách danh mục",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Chapter
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý chương của bài học";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Chapter.CREATE,
                    Code = "CREATE",
                    Table = NameTable.ChapterTable,
                    GroupName = groupName,
                    Desc = "Thêm chương của bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Chapter.DELETE,
                    Code = "DELETE",
                    Table = NameTable.ChapterTable,
                    GroupName = groupName,
                    Desc = "Xóa chương của bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Chapter.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.ChapterTable,
                    GroupName = groupName,
                    Desc = "Sửa chương của bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Chapter.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.ChapterTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết chương của bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Chapter.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.ChapterTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách chương của bài học",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Lesson
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý bài học";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Lesson.CREATE,
                    Code = "CREATE",
                    Table = NameTable.LessonTable,
                    GroupName = groupName,
                    Desc = "Thêm bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Lesson.DELETE,
                    Code = "DELETE",
                    Table = NameTable.LessonTable,
                    GroupName = groupName,
                    Desc = "Xóa bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Lesson.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.LessonTable,
                    GroupName = groupName,
                    Desc = "Sửa bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Lesson.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.LessonTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết bài học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Lesson.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.LessonTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách bài học",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng News
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý tin tức";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.News.CREATE,
                    Code = "CREATE",
                    Table = NameTable.NewsTable,
                    GroupName = groupName,
                    Desc = "Thêm tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.News.DELETE,
                    Code = "DELETE",
                    Table = NameTable.NewsTable,
                    GroupName = groupName,
                    Desc = "Xóa tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.News.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.NewsTable,
                    GroupName = groupName,
                    Desc = "Sửa tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.News.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.NewsTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết tin tức",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.News.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.NewsTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách tin tức",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Report
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý phản hồi";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Report.DELETE,
                    Code = "DELETE",
                    Table = NameTable.ReportTable,
                    GroupName = groupName,
                    Desc = "Xóa phản hồi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Report.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.ReportTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết phản hồi",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Report.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.ReportTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách phản hồi",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Role
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý phân quyền";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Role.CREATE,
                    Code = "CREATE",
                    Table = NameTable.RoleTable,
                    GroupName = groupName,
                    Desc = "Thêm quyền",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Role.DELETE,
                    Code = "DELETE",
                    Table = NameTable.RoleTable,
                    GroupName = groupName,
                    Desc = "Xóa quyền",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Role.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.RoleTable,
                    GroupName = groupName,
                    Desc = "Sửa quyền",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Role.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.RoleTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết quyền",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Role.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.RoleTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách quyền",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Subject
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý môn học ";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Subject.CREATE,
                    Code = "CREATE",
                    Table = NameTable.SubjectTable,
                    GroupName = groupName,
                    Desc = "Thêm môn học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Subject.DELETE,
                    Code = "DELETE",
                    Table = NameTable.SubjectTable,
                    GroupName = groupName,
                    Desc = "Xóa môn học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Subject.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.SubjectTable,
                    GroupName = groupName,
                    Desc = "Sửa môn học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Subject.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.SubjectTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết môn học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Subject.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.SubjectTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách môn học",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng Test
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý bài kiểm tra";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.Test.CREATE,
                    Code = "CREATE",
                    Table = NameTable.TestTable,
                    GroupName = groupName,
                    Desc = "Thêm bài kiểm tra",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Test.DELETE,
                    Code = "DELETE",
                    Table = NameTable.TestTable,
                    GroupName = groupName,
                    Desc = "Xóa bài kiểm tra",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Test.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.TestTable,
                    GroupName = groupName,
                    Desc = "Sửa bài kiểm tra",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Test.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.TestTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết bài kiểm tra",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.Test.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.TestTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách bài kiểm tra",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng User
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý tài khoản";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.User.CREATE,
                    Code = "CREATE",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Thêm tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.DELETE,
                    Code = "DELETE",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Xóa tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.UPDATE,
                    Code = "UPDATE",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Sửa tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.BLOCK,
                    Code = "VIEW_LIST",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "khóa tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.UNBLOCK,
                    Code = "VIEW_LIST",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Mở khóa tài khoản",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.User.UPDATE_PWD,
                    Code = "VIEW_LIST",
                    Table = NameTable.UserTable,
                    GroupName = groupName,
                    Desc = "Đổi mật khẩu tài khoản",
                    CreateAt = now
                }
            );
            #endregion
            #region Data liên quan đến bảng User
            // Permission liên quan đến bảng AppRole
            groupName = "Quản lý mua khóa học";
            builder.HasData(
                new MstPerMission
                {
                    Id = AuthConst.PurchasedCourse.CREATE,
                    Code = "CREATE",
                    Table = NameTable.PurchasedCourseTable,
                    GroupName = groupName,
                    Desc = "Mua khóa học",
                    CreateAt = now
                },
               
                new MstPerMission
                {
                    Id = AuthConst.PurchasedCourse.VIEW_DETAIL,
                    Code = "VIEW_DETAIL",
                    Table = NameTable.PurchasedCourseTable,
                    GroupName = groupName,
                    Desc = "Xem chi tiết khóa học",
                    CreateAt = now
                },
                new MstPerMission
                {
                    Id = AuthConst.PurchasedCourse.VIEW_LIST,
                    Code = "VIEW_LIST",
                    Table = NameTable.PurchasedCourseTable,
                    GroupName = groupName,
                    Desc = "Xem danh sách khóa học",
                    CreateAt = now
                }
            );
            #endregion
        }
    }
}