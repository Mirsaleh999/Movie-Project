using Movies.DAL.Repostory.Interfaces;
using Movies.DAL.Repostory;

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
            return services;

        }
    }
}
