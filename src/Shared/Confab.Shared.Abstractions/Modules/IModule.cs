using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Abstractions.Modules
{
    public interface IModule
    {
        public string Name { get; }
        public string Path { get; }
        void Register(IServiceCollection serviceCollection, IConfiguration configuration = null);
        void Use(IApplicationBuilder app);
    }
}
