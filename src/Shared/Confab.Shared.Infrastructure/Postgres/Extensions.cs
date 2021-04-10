using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Postgres
{
    public static class Extensions
    {
        internal static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<PostgresOptions>("postgres");
            services.AddSingleton(options);

            return services;
        }

        public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            var options = configuration.GetOptions<PostgresOptions>("postgres");
            services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

            return services;
        }
    }
}
