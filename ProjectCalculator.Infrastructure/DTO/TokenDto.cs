using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.DTO
{
    public class TokenDto
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
