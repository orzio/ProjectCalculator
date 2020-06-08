using ProjectCalculator.Domain.Domain;
using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamCalculator
{
    public class BeamCalculatorType1 : IBeamCalculator
    {
        public InternalForces Calculate(Beam beam)
        {
            throw new NotImplementedException();
        }
    }
}
