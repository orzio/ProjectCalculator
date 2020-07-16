using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
   public class PointRotator : IPointRotator
    {
        private Dictionary<Char, Point> _rotatedPoints = new Dictionary<char, Point>();
        private readonly double _fi;
        private readonly Dictionary<Char, Point> _basePoints = new Dictionary<char, Point>();

        public Dictionary<Char, Point> GetPoints()
        {
            return _rotatedPoints;
        }

        public PointRotator(Dictionary<Char, Point> basePoints, double fi)
        {
            _basePoints = basePoints;
            _fi = fi;
        }

        public IPointRotator RotatePoints()
        {
            foreach(var point in _basePoints)
            {
                _rotatedPoints.Add(point.Key, Point.CreatePoint(Math.Round(-point.Value.VerticalCoord * Math.Sin(_fi / 180.0d * Math.PI) + point.Value.HorizontalCoord * Math.Cos(_fi / 180.0d * Math.PI),4), 
                   Math.Round( point.Value.VerticalCoord * Math.Cos(_fi / 180.0d * Math.PI) + point.Value.HorizontalCoord * Math.Sin(_fi / 180.0d * Math.PI),4)));
            }
            return this;
        }
    }
}
