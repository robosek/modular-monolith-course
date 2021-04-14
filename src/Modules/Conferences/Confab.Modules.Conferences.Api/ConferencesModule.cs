using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Conferences.Api
{
    internal class ConferencesModule : IModule
    {
        public const string BasePath = "conferences-module";
        public string Name => "Conferences";
        public string Path => BasePath;
        public IEnumerable<string> Policies { get; } = new[] { "conferences", "hosts" };

        public void Register(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddCore(configuration);
        }

        public void Use(IApplicationBuilder app)
        {

        }
    }
}
