using ProjectCalculator.Core.Domain;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface IBendingMomentCalculator
    {
        public IBendingMomentCalculator CalculateM1(double fi);
        public IBendingMomentCalculator CalculateM2(double fi);
        public BendingMoment GetBendingMoment();
    }
}