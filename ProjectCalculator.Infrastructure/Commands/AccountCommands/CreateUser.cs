using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Commands.AccountCommands
{
    public class CreateUser:ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
