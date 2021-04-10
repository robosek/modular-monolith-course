using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Conferences.Api
{
    internal static  class Extensions
    {
        public static IServiceCollection AddConferences(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore(configuration);

            return services;
        }
        
    }
}
