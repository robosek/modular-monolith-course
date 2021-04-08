using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Repositories
{
    internal interface IConferenceRepository
    {
        Task<Conference> GetAsync(Guid id);
        Task<IReadOnlyList<Conference>> BrowseAsync();
        Task AddAsync(Conference host);
        Task UpdateAsync(Conference host);
        Task DeleteAsync(Conference host);
    }
}
