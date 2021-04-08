using System;
namespace Confab.Modules.Conferences.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Confab.Modules.Conferences.Core.DTO;
    using global::Confab.Modules.Conferences.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace Confab.Modules.Conferences.Api.Controllers
    {
        internal class ConferencesController : BaseController
        {
            private readonly IConferenceService _conferenceService;

            public ConferencesController(IConferenceService conferenceService)
            {
                _conferenceService = conferenceService;
            }

            [HttpGet("{id:guid}")]
            [ActionName("GetAsync")]
            public async Task<ActionResult<ConferenceDetailsDto>> GetAsync(Guid id) =>
                 OkOrNotFound(await _conferenceService.GetAsync(id));

            [HttpGet]
            public async Task<ActionResult<IReadOnlyList<ConferenceDto>>> BrowseAsync() =>
                Ok(await _conferenceService.BrowseAsync());

            [HttpPost]
            public async Task<ActionResult> AddAsync(ConferenceDto dto)
            {
                await _conferenceService.AddAsync(dto);
                return CreatedAtAction(nameof(GetAsync), new { id = dto.Id }, null);
            }

            [HttpPut("{id:guid}")]
            public async Task<ActionResult> UpdateAsync(Guid id, ConferenceDetailsDto dto)
            {
                dto.Id = id;
                await _conferenceService.UpdateAsync(dto);

                return NoContent();
            }


            [HttpDelete("{id:guid}")]
            public async Task<ActionResult> DeleteAsync(Guid id)
            {
                await _conferenceService.DeleteAsync(id);

                return NoContent();
            }
        }
    }

}
