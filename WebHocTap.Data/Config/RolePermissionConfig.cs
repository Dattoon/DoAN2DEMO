using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Config
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {

            // Khóa ngoại
            builder.HasOne(m => m.role)
                 .WithMany(m => m.rolePermissions)
                 .HasForeignKey(m => m.RoleId);
            //.OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.mstPerMission)
                .WithMany(m => m.rolePermissions)
                .HasForeignKey(m => m.MStPermissionId);
        }
    }
}
