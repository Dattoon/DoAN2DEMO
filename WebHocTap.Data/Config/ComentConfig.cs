using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebHocTap.Data.Entites;
using WebHocTap.Share.Const;

namespace WebHocTap.Data.Config
{
    public class ComentConfig : IEntityTypeConfiguration<Comemt>
    {
        public void Configure(EntityTypeBuilder<Comemt> builder)
        {
            builder.ToTable(NameTable.ComentTable);

            // Cấu hình quan hệ với Lesson
            builder.HasOne(m => m.Lesson)
                   .WithMany(m => m.comemts)
                   .HasForeignKey(m => m.IdLesson);

            // Cấu hình quan hệ với User
            builder.HasOne(m => m.User)
                   .WithMany(u => u.Comemts)
                   .HasForeignKey(m => m.IdUser);
        }
    }
}
