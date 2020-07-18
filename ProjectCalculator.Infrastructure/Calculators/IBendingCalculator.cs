using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface IBendingCalculator
    {
        void Calculate(ParamFiz paramFiz, BendingMoment internalForces);
        TensionData GetData();
        void CalculateEthaRate();
        void CalculateTensionInFurthestsPoints(Dictionary<Char, Point> furthestsPoints);
        void ChooseMinMaxTension();
        void CalculateDimensionA(double kr);
    }
}
