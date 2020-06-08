using ProjectCalculator.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface IBendingCalculator
    {
        public void Calculate(ParamFiz paramFiz, InternalForces internalForces);
    }
}
