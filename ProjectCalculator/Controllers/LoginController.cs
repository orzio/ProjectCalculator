using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProjectCalculator.Api.Repositories;
using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.Commands.AccountCommands;
using ProjectCalculator.Infrastructure.Extensions;
using ProjectCalculator.Infrastructure.Services;

namespace ProjectCalculator.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IRefreshService _refreshService;
        private readonly IUserService _userService;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache, IRefreshService refreshService, IUserService userService)
        {
            _commandDispatcher = commandDispatcher;
            _refreshService = refreshService;
            _cache = cache;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            await _commandDispatcher.DispatchAsync(command);
            var userId = (await _userService.GetAsync(command.Email)).Id;
            var token = await _refreshService.GetToken(userId);
            return Ok(token);
        }
    }
}
