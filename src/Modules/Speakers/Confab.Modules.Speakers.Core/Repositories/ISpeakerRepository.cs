using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;

namespace Confab.Modules.Speakers.Core.Repositories
{
    internal interface ISpeakerRepository
    {
        Task<Speaker> GetAsync(Guid id);
        Task<IReadOnlyList<Speaker>> BrowseAsync();
        Task AddAsync(Speaker speaker);
        Task UpdateAsync(Speaker speaker);
        Task DeleteAsync(Speaker speaker);
        Task<bool> ExistsAsync(Guid id);
    }
}