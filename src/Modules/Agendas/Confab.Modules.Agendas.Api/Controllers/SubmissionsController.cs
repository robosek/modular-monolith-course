using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Conferences.Api.Controllers;
using Confab.Shared.Abstractions.Commands;
using Confab.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : BaseController
    {
        private readonly ICommandDispatcher commandDisptacher;
        private readonly IQueryDispatcher queryDispatcher;

        public SubmissionsController(ICommandDispatcher commandDisptacher, IQueryDispatcher queryDispatcher)
        {
            this.commandDisptacher = commandDisptacher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SubmissionDto>> GetAsync(Guid id)
           =>
            OkOrNotFound(await queryDispatcher.QueryAsync(new GetSubmission() { Id = id }));

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await commandDisptacher.SendAsync(command);
            return CreatedAtAction("GetAsync", new { id = command.Id }, null);

        }

        [HttpPut("{id:guid}/approve")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await commandDisptacher.SendAsync(new ApproveSubmission(id));
            return NoContent();

        }

        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await commandDisptacher.SendAsync(new RejectSubmission(id));
            return NoContent();

        }
    }
}
