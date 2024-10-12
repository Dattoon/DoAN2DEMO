using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.DataSender
{
    public static class RoleSender
    {
        public static void SeedData(this EntityTypeBuilder<Role> builder)
        {
            var now = new DateTime(year: 2021, month: 12, day: 10);

            // Tạo vai trò
            var roleAdmin = new Role
            {
                Id = 1,
                RoleName = "Quản trị hệ thống",
                Description = "Quản trị toàn bộ hệ thống",
                CreateAt = now,
                UpdateAt = now,
            };
            var client = new Role
            {
                Id = 2,
                RoleName = "Khách hàng",
                Description = "Tài khoản do khách hàng tạo ngoài trang client",
                CreateAt = now,
                UpdateAt = now,
            };
            builder.HasData(roleAdmin);
            builder.HasData(client);
        }
    }
}
