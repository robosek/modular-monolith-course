 using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Kernel.Types;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly ISpeakerRepository speakerRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker messageBroker;

        public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository, IDomainEventDispatcher domainEventDispatcher, IEventMapper eventMapper, IMessageBroker messageBroker)
        {
            this.submissionRepository = submissionRepository;
            this.speakerRepository = speakerRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _eventMapper = eventMapper;
            this.messageBroker = messageBroker;
        }

        public async Task HandleAsync(CreateSubmission command)
        {
            var speakerIds = command.SpeakerIds.Select(x => new AggregateId(x));
            var speakers = await speakerRepository.BrowserAsync(speakerIds);

            if(speakers.Count() != speakerIds.Count())
            {
                throw new InvalidSpakersNumberException(command.Id);
            }

            var submission = Submission.Create(
                command.Id,
                command.ConferenceId,
                command.Title,
                command.Description,
                command.Level,
                command.Tags,
                speakers.ToList());

            await submissionRepository.AddAsync(submission);
            await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());

            var events = _eventMapper.MapAll(submission.Events);
            await messageBroker.PublishAsync(events.ToArray());
        }
    }
}
