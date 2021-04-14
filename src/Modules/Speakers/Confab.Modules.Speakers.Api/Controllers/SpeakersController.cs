using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Api.Controllers;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Confab.Modules.Speakers.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class SpeakersController : BaseController
    {
        private readonly ISpeakerService _speakerService;
        private const string Policy = "speakers";

        public SpeakersController(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetAsync")]
        [AllowAnonymous]
        public async Task<ActionResult<SpeakerDto>> GetAsync(Guid id) =>
             OkOrNotFound(await _speakerService.GetAsync(id));

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<SpeakerDto>>> BrowseAsync() =>
            Ok(await _speakerService.BrowseAsync());

        [HttpPost]
        public async Task<ActionResult> AddAsync(SpeakerDto dto)
        {
            await _speakerService.AddAsync(dto);
            return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, SpeakerDto dto)
        {
            dto.Id = id;
            await _speakerService.UpdateAsync(dto);

            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _speakerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
