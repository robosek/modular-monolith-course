using Confab.Modules.Conferences.Api.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Confab.Modules.Agendas.Api.Controllers
{
    [Authorize(Policy = Policy)]
    internal class AgendasController : BaseController
    {
        private const string Policy = "conferences";
    }
}