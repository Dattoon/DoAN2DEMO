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
    public class NewsConfig : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasOne(m => m.categoryNew)
            .WithMany(m => m.news)
            .HasForeignKey(m => m.IdCategoryNew);
        }
    }
}
