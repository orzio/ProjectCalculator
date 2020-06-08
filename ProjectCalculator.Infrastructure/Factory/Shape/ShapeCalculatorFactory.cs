using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.Shape
{
    public class ShapeCalculatorFactory
    {
        public IShapeCalculator GetShapeCalculator(int shapeType)
        {
            IShapeCalculator shapeCalculator = null;
            switch (shapeType)
            {
                case 1:
                    shapeCalculator = new ShapeCalculatorType1();
                    break;
            }
            return shapeCalculator;
        }
    }
}
