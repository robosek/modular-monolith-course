using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;

namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient moduleClient;
        private readonly IAsyncMessageDispatcher asyncMessageDisptacher;
        private readonly MessagingOptions messagingOptions;

        public InMemoryMessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDisptacher, MessagingOptions messagingOptions)
        {
            this.moduleClient = moduleClient;
            this.asyncMessageDisptacher = asyncMessageDisptacher;
            this.messagingOptions = messagingOptions;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if(messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if(!messages.Any())
            {
                return;
            }

            var tasks = new List<Task>();

            foreach(var message in messages)
            {

                if(messagingOptions.UseBackgroundDisptacher)
                {
                    await asyncMessageDisptacher.PublishAsync(message);
                    continue;
                }

                tasks.Add(moduleClient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}
