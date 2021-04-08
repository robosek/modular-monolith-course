using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class ConferenceNotFoundException : ConfabException
    {
        public ConferenceNotFoundException(Guid id) : base($"Conference with id {id} was not found.")
        {
        }
    }
}
