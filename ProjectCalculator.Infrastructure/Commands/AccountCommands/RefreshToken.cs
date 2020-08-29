using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Commands.AccountCommands
{
   public class RefreshToken:ICommand
    {
        public string ExpiredToken { get; set; }
        public string Refresh { get; set; }
    }
}
