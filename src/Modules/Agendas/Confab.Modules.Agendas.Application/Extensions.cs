using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Agendas.Api")]
namespace Confab.Modules.Agendas.Application
{
    internal static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
               => services;
    }
}