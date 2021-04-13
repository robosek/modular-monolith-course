using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL.Repositories
{
    internal class SpeakerRepository : ISpeakerRepository
    {
        private readonly SpeakersDbContext speakersDbContext;
        private readonly DbSet<Speaker> speakers;

        public SpeakerRepository(SpeakersDbContext speakersDbContext)
        {
            this.speakersDbContext = speakersDbContext;
            speakers = speakersDbContext.Speakers;
        }

        public Task<Speaker> GetAsync(Guid id) => speakers.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Speaker>> BrowseAsync() => await speakers.ToListAsync();

        public async Task AddAsync(Speaker speaker)
        {
            await speakers.AddAsync(speaker);
            await speakersDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Speaker speaker)
        {
            speakers.Remove(speaker);
            await speakersDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Speaker speaker)
        {
            speakers.Update(speaker);
            await speakersDbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id) => speakers.AnyAsync(x => x.Id == id);
    }
}