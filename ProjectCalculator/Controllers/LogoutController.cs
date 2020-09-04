using Microsoft.AspNetCore.Mvc;
using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.Commands.AccountCommands;
using ProjectCalculator.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController:ControllerBase
    {
        private readonly IRefreshService _refreshService;
        public LogoutController(IRefreshService refreshService)
        {
            _refreshService = refreshService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RefreshToken command)
        {
            await _refreshService.DeleteTokens(command.UserId);
            return Ok();
        }
    }
}
