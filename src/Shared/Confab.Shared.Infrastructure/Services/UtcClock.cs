using System;
using Confab.Shared.Abstractions;

namespace Confab.Shared.Infrastructure.Services
{
    public class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
 
    }
}
