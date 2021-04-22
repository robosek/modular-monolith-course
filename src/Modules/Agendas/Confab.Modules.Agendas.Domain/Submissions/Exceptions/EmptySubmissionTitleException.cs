using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class EmptySubmissionTitleException : ConfabException
    {
        public Guid SubmissionId { get; }

        public EmptySubmissionTitleException(Guid submissionId)
            : base($"Submission with id {submissionId} defines empty title.")
        {
            SubmissionId = submissionId;
        }
    }
}
