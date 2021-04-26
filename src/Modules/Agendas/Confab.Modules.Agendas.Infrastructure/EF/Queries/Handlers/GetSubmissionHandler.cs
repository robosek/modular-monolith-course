using System;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Queries.Handlers
{
    internal sealed class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
    {
        private readonly DbSet<Submission> submissions;

        public GetSubmissionHandler(AgendasDbContext context)
        {
            this.submissions = context.Submissions;
        }

        public async Task<SubmissionDto> HandleAsync(GetSubmission query)
        {
            return await submissions
                 .AsNoTracking()
                 .Where(x => x.Id.Equals(query.Id))
                 .Include(x => x.Speakers)
                 .Select(x => Map(x))
                 .SingleOrDefaultAsync();
                 
        }

        private SubmissionDto Map(Submission submission)
        {
            return new SubmissionDto
            {
                Id = submission.Id,
                ConferenceId = submission.ConferenceId,
                Title = submission.Title,
                Description = submission.Description,
                Level = submission.Level,
                Status = submission.Status,
                Tags = submission.Tags,
                Speakers = submission.Speakers.Select(submissionSpeaker => new SpeakerDto
                {
                    Id = submissionSpeaker.Id,
                    FullName = submissionSpeaker.FullName
                })
            };
        }
    }
}
