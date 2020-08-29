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
    public class RefreshTokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;
        public RefreshTokenController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        public async Task<IActionResult> RefreshToken(RefreshToken refreshTokenCommand)
        {
            _commandDispatcher.DispatchAsync(refreshTokenCommand);
            return Ok();
        }
    }
}
