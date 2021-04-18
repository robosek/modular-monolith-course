using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Events
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private List<ModuleBroadcastRegistration> _broadcastRegistrations = new();

        public void AddBroadcastAction(Type requestType, Func<object, Task> action)
        {
            if(string.IsNullOrEmpty(requestType.Namespace))
            {
                throw new InvalidOperationException("Missing namespace");
            }

            var moduleBroadcast = new ModuleBroadcastRegistration(requestType, action);
            _broadcastRegistrations.Add(moduleBroadcast);
        }

        public IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key)
         => _broadcastRegistrations.Where(x => x.Key == key);
    }
}
 