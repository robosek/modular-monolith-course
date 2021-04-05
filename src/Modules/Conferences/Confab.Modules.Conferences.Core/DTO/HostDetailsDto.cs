using System.Collections.Generic;

namespace Confab.Modules.Conferences.Core.DTO
{
    public class HostDetailsDto : HostDto
    {
        public List<ConferenceDto> Conferences { get; set; }
    }
}
 