using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Exceptions;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands.Handlers
{
    public sealed class ApproveSubmissionHandler : ICommandHandler<ApproveSubmission>
    {
        private readonly ISubmissionRepository submissionRepository;

        public ApproveSubmissionHandler(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
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
        }
    }
}
