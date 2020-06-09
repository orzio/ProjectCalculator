using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.ShapeCalculator
{
    public class ShapeCalculatorFactory
    {
        public IShapeCalculator GetShapeCalculator(BendingCommand command)
        {
            IShapeCalculator shapeCalculator = null;
            switch (command.ShapeType)
            {
                case 1:
                    shapeCalculator = new ShapeCalculatorTypeA(command.Shape);
                    break;
            }
            return shapeCalculator;
        }
    }
}
