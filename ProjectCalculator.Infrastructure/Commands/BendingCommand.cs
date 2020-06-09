using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Commands
{
    public class BendingCommand:ICommand
    {
        public int BeamType { get; set; }
        public int ShapeType { get; set; }
        public Beam Beam { get; set; }
        public Shape Shape { get; set; }
    }
}
