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
    public class ComentConfig : IEntityTypeConfiguration<Comemt>
    {
        public void Configure(EntityTypeBuilder<Comemt> builder)
        {
            builder.HasOne(m => m.lesson)
            .WithMany(m => m.comemts)
            .HasForeignKey(m => m.IdLesson);
        }
    }
}
