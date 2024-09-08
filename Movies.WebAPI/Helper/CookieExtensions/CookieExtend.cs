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

                opts.Cookie = cookieBuilder;
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);
            });

            return services;
        }
    }
}
