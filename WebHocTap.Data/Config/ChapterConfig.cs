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
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasOne(m => m.subject)
            .WithMany(m => m.chapters)
            .HasForeignKey(m => m.IdSubject);
        }
    }
}
