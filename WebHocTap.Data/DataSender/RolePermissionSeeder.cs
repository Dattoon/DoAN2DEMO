using WebHocTap.Data.Entites;
using WebHocTap.Share.Const;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.DataSender
{
    public static class RolePermissionSeeder
    {
        public static void SeedData(this EntityTypeBuilder<RolePermission> builder)
        {
            var now = new DateTime(year: 2021, month: 12, day: 10);
            // Danh sách các class chứa permission
            Type[] classType = new Type[]
            {
                typeof(AuthConst.User),
                typeof(AuthConst.Answer),
                typeof(AuthConst.CategoryNew),
                typeof(AuthConst.CategorySub),
                typeof(AuthConst.Chapter),
                typeof(AuthConst.Lesson),
                typeof(AuthConst.Report),
                typeof(AuthConst.Subject),
                typeof(AuthConst.News),
                typeof(AuthConst.Test),
                typeof(AuthConst.Role),
            };


            // Cấp quyền cho vai trò
            var rolePermission = new List<RolePermission>();
            int i = 0;
            foreach (var type in classType)
            {
                var allPermission = GetConstants(type);
                foreach (var permission in allPermission)
                {
                    i++;
                    rolePermission.Add(new RolePermission
                    {
                        Id = i,
                        MStPermissionId = Convert.ToInt32(permission.GetRawConstantValue()),
                        UpdateAt = now,
                        CreateAt = now,
                        RoleId = 1,      // Vai trò được tạo ở AppRoleSeeder
                    });
                }
            }
            builder.HasData(rolePermission);
            // tạo vai trò khách hàng
            Type[] typeclient = new Type[]
            {
                 typeof(AuthConst.PurchasedCourse),
            };
            // Cấp quyền cho vai trò
            var roleclient = new List<RolePermission>();
            foreach (var type in typeclient)
            {
                var allPermission = GetConstants(type);
                foreach (var permission in allPermission)
                {
                    i++;
                    roleclient.Add(new RolePermission
                    {
                        Id = i,
                        MStPermissionId = Convert.ToInt32(permission.GetRawConstantValue()),
                        UpdateAt = now,
                        CreateAt = now,
                        RoleId = 2,      // Vai trò được tạo ở AppRoleSeeder
                    });
                }
            }
            builder.HasData(roleclient);
        }
        private static List<FieldInfo> GetConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                 BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }
    }
}
