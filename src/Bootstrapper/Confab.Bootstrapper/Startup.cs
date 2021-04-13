using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Confab.Bootstrapper
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private IList<Assembly> _assemblies;
        private IList<IModule> _modules;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
            _assemblies = ModuleLoader.LoadAssemblies(configuration );
            _modules = ModuleLoader.LoadModules(_assemblies);
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(configuration);
            foreach(var module in _modules)
            {
                module.Register(services,configuration);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseInfrastructure();   
            foreach(var module in _modules)
            {
                module.Use(app);
            }

            logger.LogInformation($"Modules {string.Join(", ", _modules.Select(x => x.Name))}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Confab!");
                });
            });

            _assemblies.Clear();
            _modules.Clear();
        }

    }
}