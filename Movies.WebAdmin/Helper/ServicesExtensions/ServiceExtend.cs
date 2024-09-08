using Movies.BLL.Services.Interfaces;
using Movies.BLL.Services;

namespace Movies.WebAdmin.Helper.Extensions
{
    public static class ServiceExtend
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddScoped<IMoviesService, MoviesService>();

            return services;

        }
    }
}
