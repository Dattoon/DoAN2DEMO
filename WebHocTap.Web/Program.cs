using AutoMapper;
using WebHocTap.Data;
using WebHocTap.Data.Reponsitory;
using WebHocTap.Web.Common.Mailer;
using WebHocTap.Web.WebConfig;
using WebHocTap.Web.WebConfig.Const;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebHocTapDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
// khai báo reponsitory
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BaseReponsitory>();
builder.Services.AddServiceRepositories();
//câu hình đăng nhập
builder.Services.AddAuthentication(AppConst.COOKIES_AUTH)
                .AddCookie(options =>
                {
                    options.LoginPath = AppConst.LOGIN_PATH;
                    options.ExpireTimeSpan = TimeSpan.FromHours(AppConst.LOGIN_TIMEOUT);
                    options.Cookie.HttpOnly = true;
                });
// cấu hình mapper
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MapperConfig());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Khởi tạo thông tin mail
AppMailConfiguration mailConfig = new();
mailConfig.LoadFromConfig(builder.Configuration);
builder.Services.AddSingleton(mailConfig);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
                name: "Client",
                pattern: AppConst.LOGIN_PATH,
                defaults: new
                {
                    controller = "Account",
                    action = "Login",

                });
    endpoints.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();
