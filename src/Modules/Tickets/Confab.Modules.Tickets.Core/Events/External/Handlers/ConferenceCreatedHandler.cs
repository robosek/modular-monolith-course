using System;
using System.Threading.Tasks;
using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Tickets.Core.Events.External.Handlers
{
    internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
    {
        private readonly IConferenceRepository repository;
        private readonly ILogger<ConferenceCreatedHandler> logger;

        public ConferenceCreatedHandler(IConferenceRepository repository, ILogger<ConferenceCreatedHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task HandleAsync(ConferenceCreated @event)
        {
            var conference =  new Conference
            {
                Id = @event.Id,
                Name = @event.Name,
                ParticipantsLimit = @event.ParticipantsLimit,
                From = @event.From,
                To = @event.To
            };

            await repository.AddAsync(conference);
            logger.LogInformation($"Added a conference with ID: '{conference.Id}'.");
        }
    }
}
