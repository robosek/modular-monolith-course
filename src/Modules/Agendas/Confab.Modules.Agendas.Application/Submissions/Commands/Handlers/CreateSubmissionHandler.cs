using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    public class CreateSubmissionHandler : ICommandHandler<CreateSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;
        private readonly ISpeakerRepository speakerRepository;


        public CreateSubmissionHandler(ISubmissionRepository submissionRepository, ISpeakerRepository speakerRepository)
        {
            this.submissionRepository = submissionRepository;
            this.speakerRepository = speakerRepository;
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
        }
    }
}
