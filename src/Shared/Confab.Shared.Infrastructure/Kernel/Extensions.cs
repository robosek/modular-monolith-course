using System;
using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Kernel
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            serviceCollection.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            serviceCollection.Scan(s => s.FromAssemblies(assemblies)
                                        .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime());


            return serviceCollection;
        }
    }
}
