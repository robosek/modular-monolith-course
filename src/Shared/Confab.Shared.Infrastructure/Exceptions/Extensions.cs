using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal static class Extensions
    {
        public static IServiceCollection AddErrorHandler(this IServiceCollection services) =>
               services.AddSingleton<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app) =>
               app.UseMiddleware<ErrorHandlerMiddleware>();

    }
}
