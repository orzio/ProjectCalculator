using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.DrawingScripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.DrawingScript
{
   public class BeamScriptFactory
    {
        public IBeamScript GetShapeScript(BendingCommand command, InternalForces internalForces)
        {
            IBeamScript shapeScript = null;
            switch (command.BeamType)
            {
                case 1:
                    shapeScript = new BeamScriptTypeA(internalForces, command.Beam);
                    break;
                case 2:
                    shapeScript = new BeamScriptTypeB(internalForces, command.Beam);
                    break;                                            
                case 3:                                               
                    shapeScript = new BeamScriptTypeC(internalForces, command.Beam);
                    break;                                           
                case 4:                                              
                    shapeScript = new BeamScriptTypeD(internalForces, command.Beam);
                    break;
            }
            return shapeScript;
        }
    }
}
