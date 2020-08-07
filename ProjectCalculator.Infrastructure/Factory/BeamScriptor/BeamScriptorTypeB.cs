using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamScriptor
{
    public class BeamScriptorTypeB : IBeamScriptorEquation
    {

        private readonly Beam _beam;
        public BeamScriptorTypeB(Beam beam)
        {
            _beam = beam;
            SetValues();
        }

        public string Q1 { get; set; }
        public string Q1L { get; set; }
        public string Q2 { get; set; }
        public string Q2L { get; set; }
        public string P { get; set; }
        public string Q1SumL { get; set; }
        public string Q2SumL { get; set; }
        public string PSumL { get; set; }

        public void SetValues()
        {
            Q1 = _beam.Q1.ToString();
            Q2 = _beam.Q2.ToString();
            Q1L = _beam.L2.ToString();
            Q2L = _beam.L3.ToString();
            P = _beam.P.ToString();
            Q1SumL = $"(0.5*{Q1L}L+{_beam.L1}L)";
            Q2SumL = $"(0.5*{Q2L}L+{_beam.L2}L+{_beam.L1}L)";
            PSumL = $"({_beam.L1}L+{_beam.L2}L)";
        }
    }
}
