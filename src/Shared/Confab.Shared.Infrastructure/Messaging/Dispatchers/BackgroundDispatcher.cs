using System;
using System.Threading;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal sealed class BackgroundDispatcher : BackgroundService
    {
        private readonly IMessageChannel messageChannel;
        private readonly IModuleClient moduleClient;
        private readonly ILogger<BackgroundDispatcher> logger;

        public BackgroundDispatcher(IMessageChannel messageChannel
            , IModuleClient moduleClient, ILogger<BackgroundDispatcher> logger)
        {
            this.messageChannel = messageChannel;
            this.moduleClient = moduleClient;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Running background dispatcher");

            await foreach(var message in messageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await moduleClient.PublishAsync(message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex,ex.Message);
                }
            }

            logger.LogInformation("Finished background disptacher");
        }
    }
}
