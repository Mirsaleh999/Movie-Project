using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.BLL.Mapping;
using Movies.WebAdmin.Helper.Extensions;
using Movies.WebAdmin.Helper.FlluentExtensions;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using Movies.WebAdmin.Helper.LogExtensions;
using Movies.DAL.DbModel;
using Microsoft.AspNetCore.Identity;
using Movies.WebAdmin.Helper.IdentityExtensions;
using Movies.WebAdmin.Helper.CookieExtensions;
using Movies.WebAdmin.Provider;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

namespace Movies.WebAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 314572800; 
            });

            builder.Host.UseSerilog();

            builder.Services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
            });

            builder.Services.AddFluentServices();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddIdentityServices();

            builder.Services.AddCookieServices();

            builder.Services.AddImpotandLogServices();


            builder.Services.AddAutoMapper(typeof(CustomMapping));


            builder.Services.AddServices();


            builder.Services.AddRepositories();


            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 314572800; 
            });

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();





            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=LogIn}/{id?}");

            app.Run();
        }
    }
}
