using Microsoft.Extensions.Caching.Memory;
using ProjectCalculator.Infrastructure.Extensions;
using ProjectCalculator.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Commands.AccountCommands
{
    public class LoginHandler : ICommandHandler<LoginCommand>
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;
        public LoginHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }
        public async Task HandleAsync(LoginCommand command)
        {
             await _userService.LoginAsync(command.Email, command.Password);
             var user = await _userService.GetAsync(command.Email);
             var jwt = _jwtHandler.CreateToken(Guid.NewGuid(), "user");
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}
