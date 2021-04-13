using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerAlreadyExistsException : ConfabException
    {
        public SpeakerAlreadyExistsException(Guid id) : base($"Speaker with id {id} alreday exists.")
        {
        }
    }
}
