using Confab.Modules.Conferences.Core.DAL;
using Confab.Modules.Conferences.Core.DAL.Repositories;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgres<ConferencesDbContext>(configuration);

            services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
            services.AddScoped<IHostRepository, HostRepository>();
            services.AddScoped<IHostService, HostService>();

            services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IConferenceService, ConferenceService>();

            return services;
        }
    }
}
