using System;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
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
        
    }
}
