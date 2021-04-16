using System;
using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            serviceCollection.AddSingleton<IEventDispatcher, EventDispatcher>();
            serviceCollection.Scan(s => s.FromAssemblies(assemblies)
                                        .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime());
                                        
                                        
            return serviceCollection;
        }
    }
}
