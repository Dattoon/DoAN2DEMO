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
    public class MstPerMissionConfig : IEntityTypeConfiguration<MstPerMission>
    {
        public void Configure(EntityTypeBuilder<MstPerMission> builder)
        {

            // Khóa chính
            builder.Property(m => m.Id).ValueGeneratedNever();

            // CODE là varchar & bắt buộc
            builder.Property(m => m.Code)
                .HasMaxLength(500)
                .IsUnicode(false)   // varchar (không chứa unicode)
                .IsRequired();

            builder.Property(m => m.Table)
                .HasMaxLength(500)
                .IsUnicode(false)   // varchar (không chứa unicode)
                .IsRequired();
            builder.Property(m => m.GroupName)
                .HasMaxLength(500)
                .IsUnicode()
                .IsRequired();
            builder.Property(m => m.Desc)
                .HasMaxLength(500)
                .IsUnicode()
                .IsRequired();
        }
    }
}
