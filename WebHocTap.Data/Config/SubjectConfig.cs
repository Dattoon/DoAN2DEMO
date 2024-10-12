using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHocTap.Data.Entites;

namespace WebHocTap.Data.Config
{
    public class SubjectConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasOne(m => m.categorySub)
            .WithMany(m => m.subjects)
            .HasForeignKey(m => m.IdCategorySub);
        }
    }
}
