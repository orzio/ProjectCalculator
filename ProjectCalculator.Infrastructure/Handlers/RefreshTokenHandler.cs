using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.Commands.AccountCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Services
{
    public class RefreshTokenHandler : ICommandHandler<RefreshToken>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IRefreshService _refreshService;

        public RefreshTokenHandler(IJwtService jwtService, IUserService userService, IRefreshService refreshService)
        {
            _jwtService = jwtService;
            _userService = userService;
            _refreshService = refreshService;
        }

        public Task HandleAsync(RefreshToken command)
        {
            return null;
        }
    }
}
