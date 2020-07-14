using System;
using ProjectCalculator.Core.Domain;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class CoordinateCalculator
    {
        private Point _rotatedPoint;

        public CoordinateCalculator CalculateCoordinate(Action<Point> calculator)
        {
            calculator.Invoke(_rotatedPoint);
            return this;
        }

        public Point GetPoint()
        {
            return _rotatedPoint;
        }
        
    }
}