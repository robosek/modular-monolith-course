using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories
{
    internal class HostRepository : IHostRepository
    {
        private readonly ConferencesDbContext conferencesDbContext;
        private readonly DbSet<Host> hosts;

        public HostRepository(ConferencesDbContext conferencesDbContext)
        {
            this.conferencesDbContext = conferencesDbContext;
            hosts = this.conferencesDbContext.Hosts;
        }

        public async Task AddAsync(Host host)
        {
            await hosts.AddAsync(host);
            await conferencesDbContext.SaveChangesAsync();
        }


        public async Task<IReadOnlyList<Host>> BrowseAsync() => await hosts.Include(x => x.Conferences). ToListAsync();


        public async Task DeleteAsync(Host host)
        {
            hosts.Remove(host);
            await conferencesDbContext.SaveChangesAsync();
        }

        public Task<Host> GetAsync(Guid id) => hosts.Include(x => x.Conferences).SingleOrDefaultAsync(x => x.Id == id);


        public async Task UpdateAsync(Host host)
        {
            hosts.Update(host);
            await conferencesDbContext.SaveChangesAsync();
        }
    }
}
