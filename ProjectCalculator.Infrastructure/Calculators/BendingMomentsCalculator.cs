using System;
using ProjectCalculator.Core.Domain;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class BendingMomentsCalculator : IBendingMomentCalculator
    {
        private readonly BendingMoment _bendingMoment;
        private readonly double _moment;

        public BendingMomentsCalculator(double moment)
        {
            _bendingMoment = new BendingMoment();
            _bendingMoment.M = moment;
        }

        public IBendingMomentCalculator CalculateM1(double fi)
        {
            _bendingMoment.Mn = Math.Round(_bendingMoment.M * Math.Sin(fi / 180.0d * Math.PI), 4);
            return this;
        }

        public IBendingMomentCalculator CalculateM2(double fi)
        {
            _bendingMoment.Me = Math.Round(_bendingMoment.M * Math.Cos(fi / 180.0d * Math.PI), 4);
            return this;
        }
        public BendingMoment GetBendingMoment()
        {
            return _bendingMoment;
        }
    }
}