using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamScriptor
{
    public class BeamEquationScriptorFactory
    {

        public IBeamScriptorEquation GetBeamScriptor(BendingCommand command)
        {
            IBeamScriptorEquation beamCalculator = null;
            switch (command.BeamType)
            {
                case 1:
                    beamCalculator = new BeamScriptorTypeA(command.Beam);
                    break;
                case 2:
                    beamCalculator = new BeamScriptorTypeB(command.Beam);
                    break;
                case 3:
                    beamCalculator = new BeamScriptorTypeC(command.Beam);
                    break;
                case 4:
                    beamCalculator = new BeamScriptorTypeD(command.Beam);
                    break;
            }

            return beamCalculator;
        }
    }
}
