using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Factory.BeamCalculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
   public interface IBeamCalculator
    {
        IBeamCalculator CalculateVa();
        IBeamCalculator CalculateHa();
        IBeamCalculator CalculateMa();
        InternalForces GetInternalForces();
        FixedSupport GetSupportForces();

    }
}
