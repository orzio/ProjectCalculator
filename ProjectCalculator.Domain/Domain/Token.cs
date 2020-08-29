using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Core.Domain
{
    public class Token
    {
        public Token()
        {

        }
        public Guid TokenId { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
