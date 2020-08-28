using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.Commands.AccountCommands;
using ProjectCalculator.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Handlers
{
    public class CreateUserHandler:ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
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
