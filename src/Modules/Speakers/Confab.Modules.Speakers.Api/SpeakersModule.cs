using System.Runtime.CompilerServices;
using Confab.Modules.Conferences.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Speakers.Api
{
    internal class SpeakersModule : IModule
    {
        public const string BasePath = "speakers-module";
        public string Name => "Speakers";
        public string Path => BasePath;

        public void Register(IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            serviceCollection.AddCore(configuration);
        }

        public void Use(IApplicationBuilder app)
        {
           
        }
    }
}