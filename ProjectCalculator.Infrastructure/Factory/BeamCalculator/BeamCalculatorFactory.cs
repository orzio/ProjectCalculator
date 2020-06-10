using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamCalculator
{
    public class BeamCalculatorFactory
    {
        public IBeamCalculator GetBeamCalculator(BendingCommand command)
        {
            IBeamCalculator beamCalculator = null;
            switch (command.BeamType)
            {
                case 1:
                    beamCalculator = new BeamCalculatorType1(command.Beam);
                    break;
                case 2:
                    beamCalculator = new BeamCalculatorType2(command.Beam);
                    break;
                case 3:
                    beamCalculator = new BeamCalculatorType3(command.Beam);
                    break;
                case 4:
                    beamCalculator =  new BeamCalculatorType4(command.Beam);
                    break;
            }

            return beamCalculator;
        }
    }
}
