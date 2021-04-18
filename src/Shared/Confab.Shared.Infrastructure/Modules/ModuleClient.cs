using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Events;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry moduleRegistry;
        private readonly IModuleSerializer serializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer serializer)
        {
            this.moduleRegistry = moduleRegistry;
            this.serializer = serializer;
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = moduleRegistry.GetBroadcastRegistrations(key);

            var tasks = new List<Task>();
            foreach(var registration in registrations)
            {
                var translatedMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(registration.Action(translatedMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object obj, Type type)
            => serializer.Deserialize(serializer.Serialize(obj), type);
        
    }
}
