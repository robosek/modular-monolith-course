using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    internal sealed class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private readonly IMessageChannel messageChannel;

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
        {
            this.messageChannel = messageChannel;
        }

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
         => await messageChannel.Writer.WriteAsync(message);
    }
}
