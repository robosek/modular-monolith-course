using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Agendas.Api")]
namespace Confab.Modules.Agendas.Domain
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
               => services;
    }
}