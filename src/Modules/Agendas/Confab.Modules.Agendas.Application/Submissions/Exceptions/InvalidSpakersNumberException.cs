using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions
{
    public class InvalidSpakersNumberException : ConfabException
    {
        public Guid SubmissionId { get; }

        public InvalidSpakersNumberException(Guid sumibssionId)
            :base($"Numbero of speakers is not equal: {sumibssionId}")
        {
            SubmissionId = SubmissionId;
        }
    }
}
