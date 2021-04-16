using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Events
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            using var scope = serviceProvider.CreateScope();
            var eventHandlers = scope.ServiceProvider.GetServices<IEventHandler<IEvent>>();
            var tasks = eventHandlers.Select(x => x.HandleAsync(@event));

            await Task.WhenAll(tasks);
        }

    }
}
