using System;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            if(command is null)
            {
                return;
            }

            using var scope = serviceProvider.CreateScope();
            var hander = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await hander.HandleAsync(command);

        }
    }
}
