using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Events
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        void AddBroadcastAction(Type requestType, Func<object, Task> action);
    }
}
