using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Commands;
using ProjectCalculator.Infrastructure.DrawingScripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.DrawingScript
{
    public class DrawingScriptFactory
    {
        public IShapeScript GetShapeScript(BendingCommand command, ParamFiz paramFiz, BendingMoment bendingMoment, InternalForces internalForces,
            TensionData tensionData, Dictionary<Char, Point> furthestsPoints, Contour contour)
        {
            IShapeScript shapeScript = null;
            switch (command.ShapeType)
            {
                case 1:
                    shapeScript = new ShapeScriptTypeA(paramFiz,  bendingMoment,  internalForces,
             tensionData, furthestsPoints, contour);
                    break;
                case 2:
                    shapeScript = new ShapeScriptTypeA(paramFiz, bendingMoment, internalForces,
             tensionData, furthestsPoints, contour);
                    break;
                case 3:
                    shapeScript = new ShapeScriptTypeD(paramFiz, bendingMoment, internalForces,
             tensionData, furthestsPoints, contour);
                    break;
                case 4:
                    shapeScript = new ShapeScriptTypeD(paramFiz, bendingMoment, internalForces,
             tensionData, furthestsPoints, contour);
                    break;
            }
            return shapeScript;
        }
    }
}
