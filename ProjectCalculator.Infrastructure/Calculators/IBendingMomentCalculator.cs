using ProjectCalculator.Core.Domain;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface IBendingMomentCalculator
    {
        public IBendingMomentCalculator CalculateM1(double moment,double fi);
        public IBendingMomentCalculator CalculateM2(double moment,double fi);
        public BendingMoment GetBendingMoment();
    }
}