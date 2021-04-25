﻿
using System.Collections.Generic;
using System.Reflection;
using Confab.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Commands
{
    internal static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection, IEnumerable<Assembly> assemblies)
        {
            serviceCollection.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.Scan(s => s.FromAssemblies(assemblies)
                                        .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime());


            return serviceCollection;
        }
    }
}
