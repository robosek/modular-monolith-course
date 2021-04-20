using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Agendas.Api
{
    internal class AgendasModule : IModule
    {
        private const string BasePath = "agendas-module";

        public string Name => "Agendas";

        public string Path => BasePath;

        public void Register(IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            serviceCollection.AddApplication();
            serviceCollection.AddDomain();
            serviceCollection.AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
