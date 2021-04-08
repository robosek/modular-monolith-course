using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    internal class HostNotFoundException : ConfabException
    {
        public HostNotFoundException(Guid id) : base($"Host with id {id} was not found.")
        {
        }
    }
}
