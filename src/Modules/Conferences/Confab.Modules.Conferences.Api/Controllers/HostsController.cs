using System;
namespace Confab.Modules.Conferences.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Confab.Modules.Conferences.Core.DTO;
    using global::Confab.Modules.Conferences.Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace Confab.Modules.Conferences.Api.Controllers
    {
        [Authorize(Policy = Policy)]
        internal class HostsController : BaseController
        {
            private readonly IHostService _hostService;
            private const string Policy = "hosts";

            public HostsController(IHostService hostService)
            {
                _hostService = hostService;
            }

            [HttpGet("{id:guid}")]
            [ActionName("GetAsync")]
            [AllowAnonymous]
            public async Task<ActionResult<HostDetailsDto>> GetAsync(Guid id) =>
                 OkOrNotFound(await _hostService.GetAsync(id));

            [HttpGet]
            [AllowAnonymous]
            public async Task<ActionResult<IReadOnlyList<HostDto>>> BrowseAsync() =>
                Ok(await _hostService.BrowseAsync());

            [HttpPost]
            public async Task<ActionResult> AddAsync(HostDto dto)
             {
                await _hostService.AddAsync(dto);
                return CreatedAtAction(nameof(this.GetAsync), new { id = dto.Id }, null);
            }

            [HttpPut("{id:guid}")]
            public async Task<ActionResult> UpdateAsync(Guid id, HostDetailsDto dto)
            {
                dto.Id = id;
                await _hostService.UpdateAsync(dto);

                return NoContent();
            }


            [HttpDelete("{id:guid}")]
            public async Task<ActionResult> DeleteAsync(Guid id)
            {
                await _hostService.DeleteAsync(id);

                return NoContent();
            }
        }
    }

}
