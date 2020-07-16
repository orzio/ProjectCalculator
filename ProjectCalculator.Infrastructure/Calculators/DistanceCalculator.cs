using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class DistanceCalculator : IDistanceCalculator
    {
        private readonly Dictionary<Char, Point> _contourPoints;
        private readonly Line _line;

        public DistanceCalculator(Dictionary<Char, Point> contourPoints, Line line)
        {
            _contourPoints = contourPoints;
            _line = line;

        }

        public Dictionary<char, Point> GetFurthestPoints()
        {
            var distances = new Dictionary<Char, double>();
            foreach (var item in _contourPoints)
            {
                distances.Add(item.Key, Math.Abs(_line.KsiRate * item.Value.HorizontalCoord + _line.EthaRate * item.Value.VerticalCoord + _line.Rate)/(Math.Sqrt(Math.Pow(_line.EthaRate,2) + Math.Pow(_line.KsiRate,2))));
            }

            distances = distances.OrderByDescending(x => x.Value).Take(2).ToDictionary(x => x.Key, y => y.Value);

            return _contourPoints.Where(x => distances.Keys.Any(p => p.Equals(x.Key))).ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
