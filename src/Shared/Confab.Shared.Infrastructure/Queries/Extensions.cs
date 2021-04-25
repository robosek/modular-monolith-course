
using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            serviceCollection.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            serviceCollection.Scan(s => s.FromAssemblies(assemblies)
                                        .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime());


            return serviceCollection;
        }
    }
}
