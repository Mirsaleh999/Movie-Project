using Movies.DAL.Repostory.Interfaces;
using Movies.DAL.Repostory;
using Movies.BLL.Services.Interfaces;
using Movies.BLL.Services;

namespace Movies.WebAdmin.Helper.Extensions
{
    public static class RepositoryExtend
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            return services;

        }
    }
}
