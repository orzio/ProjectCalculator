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
        private readonly IRefreshService _refreshService;
        private readonly IJwtService _jwtService;
        private readonly IMemoryCache _cache;
        public LoginHandler(IUserService userService, IJwtService jwtService, IMemoryCache cache, IRefreshService refreshService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cache = cache;
            _refreshService = refreshService;
        }
        public async Task HandleAsync(LoginCommand command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtService.CreateToken(Guid.NewGuid(), user.Role);
            var refreshToken = _refreshService.GenerateRefreshToken();
            await _refreshService.UpdateTokenAsync(user.Id, jwt.Token, refreshToken);
        }
    }
}
