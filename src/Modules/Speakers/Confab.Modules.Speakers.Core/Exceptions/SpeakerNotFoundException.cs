using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions
{
    internal class SpeakerNotFoundException : ConfabException
    {
        public SpeakerNotFoundException(Guid id) : base($"Speaker with id {id} was not found.")
        {
        }
    }
}
