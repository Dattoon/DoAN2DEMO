using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data.Config
{
    public class PurchasedCourseConfig:IEntityTypeConfiguration<PurchasedCourse>
    {
        public void Configure(EntityTypeBuilder<PurchasedCourse> builder)
        {
            builder.HasOne(m => m.user)
            .WithMany(m => m.purchasedCourses)
            .HasForeignKey(m => m.IdUser);
            builder.HasOne(m => m.subject)
           .WithMany(m => m.purchasedCourses)
           .HasForeignKey(m => m.IdSub);
        }
    }
}
