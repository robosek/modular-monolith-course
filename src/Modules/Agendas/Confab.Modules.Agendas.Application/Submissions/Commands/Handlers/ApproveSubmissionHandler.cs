using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Application.Submissions.Services;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker messageBroker;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository, IDomainEventDispatcher domainEventDispatcher, IEventMapper eventMapper, IMessageBroker messageBroker)
        {
            this.submissionRepository = submissionRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _eventMapper = eventMapper;
            this.messageBroker = messageBroker;
        }

        public async Task HandleAsync(ApproveSubmission command)
        {
            var submission = await submissionRepository.GetAsync(command.Id);

            if(submission is null)
            {
                throw new SubmissionNotFoundException(command.Id);
            }

            submission.Approve();

            await submissionRepository.UpdateAsync(submission);
            await _domainEventDispatcher.DispatchAsync(submission.Events.ToArray());

            var events = _eventMapper.MapAll(submission.Events);
            await messageBroker.PublishAsync(events.ToArray());
        }
    }
}
