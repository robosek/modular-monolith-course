using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal class InMemoryConferenceRepository : IConferenceRepository
    {
        public Task AddAsync(Conference host)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Conference>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Conference host)
        {
            throw new NotImplementedException();
        }

        public Task<Conference> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Conference host)
        {
            throw new NotImplementedException();
        }
    }
}
