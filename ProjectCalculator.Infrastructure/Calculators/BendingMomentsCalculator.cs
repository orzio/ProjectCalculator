using System;
using ProjectCalculator.Core.Domain;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class BendingMomentsCalculator : IBendingMomentCalculator
    {
        private readonly BendingMoment _bendingMoment;

        public BendingMomentsCalculator()
        {
            _bendingMoment = new BendingMoment();
        }

        public IBendingMomentCalculator CalculateM1(double moment, double fi)
        {
            _bendingMoment.Mn = Math.Round(moment * Math.Sin(fi / 180.0d * Math.PI), 4);
            return this;
        }

        public IBendingMomentCalculator CalculateM2(double moment, double fi)
        {
            _bendingMoment.Mksi = Math.Round(moment * Math.Cos(fi / 180.0d * Math.PI), 4);
            return this;
        }

        public BendingMoment GetBendingMoment()
        {
            return _bendingMoment;
        }
    }
}