using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamCalculator
{
    public class BeamCalculatorFactory
    {
        public IBeamCalculator GetBeamCalculator(int beamType)
        {
            IBeamCalculator beamCalculator = null;
            switch (beamType)
            {
                case 1:
                    beamCalculator =  new BeamCalculatorType1();
                    break;

            }

            return beamCalculator;
        }
    }
}
