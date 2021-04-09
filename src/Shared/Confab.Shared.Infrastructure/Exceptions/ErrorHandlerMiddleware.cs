using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception exception)
            {
                logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new { code = exception.GetType().Name, message = exception.Message };
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
