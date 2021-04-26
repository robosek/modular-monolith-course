using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories
{
    internal sealed class SubmissionRepository : ISubmissionRepository
    {
        private readonly AgendasDbContext context;
        private readonly DbSet<Submission> submissions;

        public SubmissionRepository(AgendasDbContext context)
        {
            this.context = context;
            this.submissions = context.Submissions;
        }

        public async Task AddAsync(Submission submission)
        {
            await submissions.AddAsync(submission);
            await context.SaveChangesAsync();
        }

        public async Task<Submission> GetAsync(AggregateId id)
         => await submissions
            .Include(x => x.Speakers)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task UpdateAsync(Submission submission)
        {
            context.Update(submission);
            await context.SaveChangesAsync();
        }
    }


}
