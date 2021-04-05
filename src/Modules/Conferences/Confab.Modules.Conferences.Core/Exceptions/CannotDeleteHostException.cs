using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions
{
    public class CannotDeleteHostException : ConfabException
    {
        

        public CannotDeleteHostException(Guid id) : base($"Host with ID: '{id}' cannot be deleted.")
        {
            
        }
    }
}
