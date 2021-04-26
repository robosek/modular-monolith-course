using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class SpeakerRepository : ISpeakerRepository
    {
        private readonly AgendasDbContext _context;
        private readonly DbSet<Speaker> speakers;

        public SpeakerRepository(AgendasDbContext context)
        {
            _context = context;
            speakers = context.Speakers;
        }

        public async Task<IEnumerable<Speaker>> BrowserAsync(IEnumerable<AggregateId> ids)
         => await speakers.Where(x => ids.Contains(x.Id)).ToListAsync();
            

        public async Task AddAsync(Speaker speaker)
        {
            await speakers.AddAsync(speaker);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(AggregateId id)
        => await speakers.AnyAsync(x => x.Id.Equals(id));
    }
}
