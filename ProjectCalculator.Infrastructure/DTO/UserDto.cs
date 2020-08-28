using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
    }
}
