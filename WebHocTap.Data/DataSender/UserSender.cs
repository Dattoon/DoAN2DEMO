using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.DataSender
{
    public static class UserSender
    {
        public static void SeedData(this EntityTypeBuilder<User> builder)
        {
            var now = new DateTime(year: 2021, month: 11, day: 10);

            // Tạo mật khẩu
            var defaultPassword = "1111";
            HMACSHA512 hmac = new();
            var pwByte = Encoding.UTF8.GetBytes(defaultPassword);
            var pwdHash = hmac.ComputeHash(pwByte);
            var pwdSalt = hmac.Key;

            // Tạo thông tin tài khoản admin
            builder.HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    PasswordHash = pwdHash,
                    PasswordSalt = pwdSalt,
                    Gmail = "admin_test@gmail.com",
                    PhoneNumber = "0928666158",
                    CreateBy = -1,
                    UpdateAt = now,
                    CreateAt = now,
                    IdRole = 1,              // Vai trò được tạo ở AppRoleSeeder

                    AvatarUrl = "default-avatar-url"
                }
            );
        }
    }
}
