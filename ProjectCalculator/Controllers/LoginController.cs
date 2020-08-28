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

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache)
        {
            _commandDispatcher = commandDispatcher;
            _cache = cache;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            command.TokenId = Guid.NewGuid();
            await _commandDispatcher.DispatchAsync(command);
            var jwt = _cache.GetJwt(command.TokenId);
            return Ok(jwt);
        }
    }
}
