using WebHocTap.Data.Config;
using WebHocTap.Data.DataSender;
using WebHocTap.Data.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WebHocTap.Data
{
    public class WebHocTapDbContext :DbContext
    {
        public WebHocTapDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Answer> answers { get; set; }
        public DbSet<CategoryNew> categoryNews { get; set; }
        public DbSet<CategorySub> categorySubs { get; set; }
        public DbSet<Chapter> chapters { get; set; }
        public DbSet<Comemt> comemts { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<MstPerMission> mstPerMissions { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Report> reports { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<RolePermission> rolePermissions { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Test> tests { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<PurchasedCourse> purchasedCourses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfig());
            modelBuilder.ApplyConfiguration(new ChapterConfig());
            modelBuilder.ApplyConfiguration(new ComentConfig());
            modelBuilder.ApplyConfiguration(new LessonConfig());
            modelBuilder.ApplyConfiguration(new NewsConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
            modelBuilder.ApplyConfiguration(new MstPerMissionConfig());
            modelBuilder.ApplyConfiguration(new SubjectConfig());
            modelBuilder.ApplyConfiguration(new TestConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new PurchasedCourseConfig());
            //tạo dữ liệu
            modelBuilder.Entity<MstPerMission>().SeedData();
            modelBuilder.Entity<Role>().SeedData();
            modelBuilder.Entity<User>().SeedData();
            modelBuilder.Entity<RolePermission>().SeedData();
        }
    }
}
