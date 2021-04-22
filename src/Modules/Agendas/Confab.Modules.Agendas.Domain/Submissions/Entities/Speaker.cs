using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public class Speaker : AgregateRoot
    {
        public string FullName { get; init; }

        public Speaker(AggregateId id ,string fullName)
        {
            FullName = fullName;
        }

        public static Speaker Create(Guid id, string fullName) =>
              new (new(id), fullName);
    }
}
