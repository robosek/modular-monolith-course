using System;
using System.Threading.Tasks;
using Confab.Modules.Agendas.Application.Submissions.Commands;
using Confab.Modules.Conferences.Api.Controllers;
using Confab.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Agendas.Api.Controllers
{
    internal class SubmissionsController : BaseController
    {
        private readonly ICommandDispatcher commandDisptacher;

        public SubmissionsController(ICommandDispatcher commandDisptacher)
        {
            this.commandDisptacher = commandDisptacher;
        }


        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateSubmission command)
        {
            await commandDisptacher.SendAsync(command);
            return CreatedAtAction("Get", new { id = command.Id }, null);

        }

        [HttpPut("{id:guid}/approve}")]
        public async Task<ActionResult> ApproveAsync(Guid id)
        {
            await commandDisptacher.SendAsync(new ApproveSubmission(id));
            return NoContent();

        }

        [HttpPut("{id:guid}/reject}")]
        public async Task<ActionResult> RejectAsync(Guid id)
        {
            await commandDisptacher.SendAsync(new RejectSubmission(id));
            return NoContent();

        }

       
    }
}
