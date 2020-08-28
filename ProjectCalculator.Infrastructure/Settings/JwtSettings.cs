using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Settings
{
   public class JwtSettings
    {
        public string Key { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
