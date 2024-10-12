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
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasOne(m => m.chapter)
            .WithMany(m => m.lessons)
            .HasForeignKey(m => m.IdChapter);
        }
    }
}
