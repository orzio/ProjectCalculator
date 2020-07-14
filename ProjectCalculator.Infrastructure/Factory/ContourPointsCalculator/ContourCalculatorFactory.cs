using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.ContourPointsCalculator
{
    public class ContourCalculatorFactory
    {
        public ICoordinateCalculator GetContourCalculator(BendingCommand command, ParamFiz paramFiz) { 
            ICoordinateCalculator contourCalculator = null;
            switch (command.ShapeType)
            {
                case 1:
                    contourCalculator = new ContourPointCalculatorTypeA(command.Shape, paramFiz);
                    break;
                case 2:
                    contourCalculator = new ContourPointCalculatorTypeB(command.Shape, paramFiz);
                    break;
                case 3:
                    contourCalculator = new ContourPointCalculatorTypeC(command.Shape, paramFiz);
                    break;
                case 4:
                    contourCalculator = new ContourPointCalculatorTypeD(command.Shape, paramFiz);
                    break;
            }
            return contourCalculator;
        }
    }
}
