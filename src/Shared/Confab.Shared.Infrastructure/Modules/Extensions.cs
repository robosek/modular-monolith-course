using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Confab.Shared.Abstractions.Modules;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Abstractions.Events;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {

        internal static IServiceCollection AddModuleInfo(this IServiceCollection serviceCollection, IList<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInfos = modules.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies ?? Enumerable.Empty<string>())) ?? Enumerable.Empty<ModuleInfo>();
            moduleInfoProvider.Modules.AddRange(moduleInfos);

            serviceCollection.AddSingleton(moduleInfoProvider);

            return serviceCollection;
        }

        internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            }); 
        }

        internal static IHostBuilder ConfigureModules(this IHostBuilder hostBuilder)
            => hostBuilder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach(var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern) =>
                  Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);

            });

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services, IList<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

            return services;
        }

        private static void AddModuleRegistry(this IServiceCollection services, IList<Assembly> assemblies)
        {
            var moduleRegistry = new ModuleRegistry();

            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();
            var eventTypes = types.Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                                    .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();

                foreach(var type in eventTypes)
                {
                    moduleRegistry.AddBroadcastAction(type, @event =>
                    
                    (Task)eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                                                    ?.MakeGenericMethod(type)
                                                    .Invoke(eventDispatcher, new[] { @event }));
                    
                }


                return moduleRegistry;
            });
            
        }
        
    }
}
