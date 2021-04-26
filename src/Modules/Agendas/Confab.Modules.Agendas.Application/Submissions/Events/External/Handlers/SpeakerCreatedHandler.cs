using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Events.Submission.External.Handlers
{
    internal sealed class SpeakerCreatedHandler : IEventHandler<SpeakerCreated>
    {
        private ISpeakerRepository speakerRepository;

        public SpeakerCreatedHandler(ISpeakerRepository speakerRepository)
        {
            this.speakerRepository = speakerRepository;
        }

        public async Task HandleAsync(SpeakerCreated @event)
        {
            if(await speakerRepository.ExistsAsync(@event.Id))
            {
                return;
            }

            var speaker = Speaker.Create(@event.Id, @event.FullName);
            await speakerRepository.AddAsync(speaker);
        }
    }
}
