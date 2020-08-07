using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamScriptor
{
    public interface IBeamScriptorEquation
    {
        public void SetValues();
        string Q1 { get; set; }
        string Q1L { get; set; }
        string Q2 { get; set; }
        string Q2L { get; set; }
        string P { get; set; }
        string Q1SumL { get; set; }
        string Q2SumL { get; set; }
        string PSumL { get; set; }

    }

}
