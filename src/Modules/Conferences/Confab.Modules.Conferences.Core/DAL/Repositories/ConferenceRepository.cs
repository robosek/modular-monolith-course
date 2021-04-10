 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly ConferencesDbContext conferencesDbContext;
        private readonly DbSet<Conference> conferences;

        public ConferenceRepository(ConferencesDbContext conferencesDbContext)
        {
            this.conferencesDbContext = conferencesDbContext;
            conferences = this.conferencesDbContext.Conferences;
        }

        public async Task AddAsync(Conference conference)
        {
            await conferences.AddAsync(conference);
            await conferencesDbContext.SaveChangesAsync();
        }


        public async Task<IReadOnlyList<Conference>> BrowseAsync() => await conferences.Include(x => x.Host).ToListAsync();


        public async Task DeleteAsync(Conference conference)
        {
            conferences.Remove(conference);
            await conferencesDbContext.SaveChangesAsync();
        }

        public Task<Conference> GetAsync(Guid id) => conferences.Include(x => x.Host).SingleOrDefaultAsync(x => x.Id == id);


        public async Task UpdateAsync(Conference conference)
        {
            conferences.Update(conference);
            await conferencesDbContext.SaveChangesAsync();
        }
    }
}
