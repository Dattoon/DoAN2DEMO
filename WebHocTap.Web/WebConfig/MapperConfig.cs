using AutoMapper;
using WebHocTap.Data.Entites;
using WebHocTap.Web.Areas.Admin.ViewModels.CategoryNew;
using WebHocTap.Web.Areas.Admin.ViewModels.CategorySub;
using WebHocTap.Web.Areas.Admin.ViewModels.Chapter;
using WebHocTap.Web.Areas.Admin.ViewModels.Home;
using WebHocTap.Web.Areas.Admin.ViewModels.Lesson;
using WebHocTap.Web.Areas.Admin.ViewModels.News;
using WebHocTap.Web.Areas.Admin.ViewModels.Role;
using WebHocTap.Web.Areas.Admin.ViewModels.Sub;
using WebHocTap.Web.Areas.Admin.ViewModels.Test;
using WebHocTap.Web.Areas.Admin.ViewModels.TestandAnswer;
using WebHocTap.Web.Areas.Admin.ViewModels.User;
using WebHocTap.Web.Components.ListNewViewComponents;
using WebHocTap.Web.ViewModels;
using WebHocTap.Web.ViewModels.Account;
using WebHocTap.Web.ViewModels.Subject;

namespace WebHocTap.Web.WebConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CategoryNew, AddorUpdateCategoryNewVM>().ReverseMap();
            CreateMap<News, AddorUpdateNewsVM>().ReverseMap();
            CreateMap<CategorySub, AddorUpdateCategorySubVM>().ReverseMap();
            CreateMap<Subject, AddorUpdateSubVM>().ReverseMap();
            CreateMap<Chapter, AddorUpdateChappterVM>().ReverseMap();
            CreateMap<Lesson, AddorUpdateLessonVM>().ReverseMap();
            CreateMap<Answer, UpdateAnswerVM>().ReverseMap();
            CreateMap<User, SignUpVM>().ReverseMap();
            CreateMap<User, ProfileUpdateVM>().ReverseMap();
        }

        public static MapperConfiguration RoleIndexConf = new(mapper =>
        {
            mapper.CreateMap<Role, ListRoleItemVM>();
        });

        public static MapperConfiguration UserIndexConf = new(mapper =>
        {
            mapper.CreateMap<User, ListUserItemVM>()
            .ForMember(uItem => uItem.RoleName,
            otps => otps.MapFrom(uE => uE.role.RoleName));
        });

        public static MapperConfiguration LoginConf = new(mapper =>
        {
            mapper.CreateMap<User, UserDataForApp>()
                .ForMember(uItem => uItem.RoleName, opts => opts.MapFrom(uEntity => uEntity.role == null ? "" : uEntity.role.RoleName))
                .ForMember(uItem => uItem.Permission, opts => opts.MapFrom
                (
                    uEntity => string.Join(',', uEntity.role
                                                    .rolePermissions
                                                    .Select(p => p.MStPermissionId))
                ));
        });

        public static MapperConfiguration CategoryNewIndexConf = new(mapper =>
        {
            mapper.CreateMap<CategoryNew, ListCategoryItemVM>();
        });

        public static MapperConfiguration NewsIndexConf = new(mapper =>
        {
            mapper.CreateMap<News, ListNewsItemVM>()
            .ForMember(uItem => uItem.IdCategoryNew,
            otps => otps.MapFrom(uE => uE.categoryNew.NameCategoryNew));
        });

        public static MapperConfiguration CategorySubIndexConf = new(mapper =>
        {
            mapper.CreateMap<CategorySub, ListCategorySubItemVM>();
        });

        public static MapperConfiguration SubIndexConf = new(mapper =>
        {
            mapper.CreateMap<Subject, ListSubItemVM>()
            .ForMember(uItem => uItem.CategorySub,
            otps => otps.MapFrom(uE => uE.categorySub.NameCategorySub));
        });

        public static MapperConfiguration ChapterIndexConf = new(mapper =>
        {
            mapper.CreateMap<Chapter, ListChapterItemVM>();
        });

        public static MapperConfiguration LessonIndexConf = new(mapper =>
        {
            mapper.CreateMap<Lesson, ListLessonItemVM>();
        });

        public static MapperConfiguration TestIndexConf = new(mapper =>
        {
            mapper.CreateMap<Test, ListTestItemVM>();
        });

        public static MapperConfiguration ListAnswerIndexConf = new(mapper =>
        {
            mapper.CreateMap<Answer, ListAnswerItemVM>();
        });

        public static MapperConfiguration ListTestAnswerIndexConf = new(mapper =>
        {
            mapper.CreateMap<Test, TestAnswerClientVM>();
        });

        public static MapperConfiguration ListCateNewClIndexConf = new(mapper =>
        {
            mapper.CreateMap<CategoryNew, ListNewVM>();
        });

        public static MapperConfiguration PurchasedCourseIndexConf = new(mapper =>
        {
            mapper.CreateMap<PurchasedCourse, ListPurchasedCourseItem>()
            .ForMember(uItem => uItem.UserName,
            otps => otps.MapFrom(uE => uE.user.UserName))
            .ForMember(uItem => uItem.SubName,
            otps => otps.MapFrom(uE => uE.subject.NameCategorySub));
        });
    }
}
