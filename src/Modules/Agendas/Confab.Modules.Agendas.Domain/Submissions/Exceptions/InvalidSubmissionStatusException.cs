using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Exceptions
{
    public class InvalidSubmissionStatusException : ConfabException
    {
        public Guid SubmissionId { get; }

        public InvalidSubmissionStatusException(Guid submissionId)
            : base($"Submission with id {submissionId} defines invalid status.")
        {
            SubmissionId = submissionId;
        }
    }
}
