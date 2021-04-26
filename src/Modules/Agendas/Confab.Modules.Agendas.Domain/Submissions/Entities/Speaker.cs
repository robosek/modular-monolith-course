using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public class Speaker : AgregateRoot
    {
        public string FullName { get; init; }

        public IEnumerable<Submission> Submissions => _submissions;

        private ICollection<Submission> _submissions;

        public Speaker(AggregateId id ,string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public static Speaker Create(Guid id, string fullName) =>
              new (new(id), fullName);
    }
}
