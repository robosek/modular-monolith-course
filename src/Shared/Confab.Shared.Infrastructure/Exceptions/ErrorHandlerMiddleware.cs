using System;
using System.Net;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToResponseMapper responseMapper;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(IExceptionToResponseMapper responseMapper, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.responseMapper = responseMapper;
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
            var errorResponse = responseMapper.Map(exception);
            context.Response.StatusCode = (int) (errorResponse?.StatusCode ?? HttpStatusCode.BadRequest);
            var response = errorResponse.Response;

            if(response is null)
            {
                return;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
