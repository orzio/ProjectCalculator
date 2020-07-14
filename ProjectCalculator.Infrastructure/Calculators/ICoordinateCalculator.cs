using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface ICoordinateCalculator
    {
        Dictionary<Char, Point> GetPoints();
    }
}