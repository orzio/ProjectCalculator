using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
   public interface IDistanceCalculator
    {
        Dictionary<Char, Point> GetFurthestPoints();
    }
}
