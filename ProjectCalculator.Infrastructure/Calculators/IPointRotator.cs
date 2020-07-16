using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
   public interface IPointRotator
    {
        Dictionary<Char, Point> GetPoints();
        IPointRotator RotatePoints();
    }
}
