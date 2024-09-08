using FluentValidation.AspNetCore;
using FluentValidation;
using Movies.BLL.Validations;

namespace Movies.WebAdmin.Helper.CookieExtensions
{
    public static class CookieExtend
    {
        public static IServiceCollection AddCookieServices(this IServiceCollection services)
        {
            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "Movie";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SameSite = SameSiteMode.Lax;
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            services.ConfigureApplicationCookie(opts =>
            {

                opts.LoginPath = new PathString("/Home/SignIn");
                opts.LogoutPath = new PathString("/Home/LogOut");
                opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                opts.AccessDeniedPath = new PathString("/Home/AccessDenied");
               
            });

            return services;
        }
    }
}
