using ProjectCalculator.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
   public interface IBeamCalculator
    {
        public InternalForces Calculate(Beam beam);
    }
}
