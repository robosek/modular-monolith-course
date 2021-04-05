using System;
namespace Confab.Shared.Abstractions.Exceptions
{
    public abstract class ConfabException : Exception
    {
        public ConfabException(string message) : base(message)
        {
        }
    }
}
