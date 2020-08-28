using ProjectCalculator.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Commands.AccountCommands
{
    public class UserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;
        public UserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email,
                command.FirstName,command.LastName, command.Password, command.Role);
        }
    }
}
