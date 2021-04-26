using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Modules.Agendas.Infrastructure.EF.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Agendas.Api")]
namespace Confab.Modules.Agendas.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
               => services
                   .AddPostgres<AgendasDbContext>(configuration)
                   .AddScoped<ISpeakerRepository, SpeakerRepository>()
                   .AddScoped<ISubmissionRepository, SubmissionRepository>();
    }
}