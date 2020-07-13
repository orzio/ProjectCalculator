using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ProjectCalculator.Core.Domain
{
    public class Point
    {
        protected Point(double x, double y)
        {
            HorizontalCoord = x;
            VerticalCoord = y;
        }

        public double HorizontalCoord { get; private set; }
        public double VerticalCoord { get; private set; }

        public static Point CreatePoint(double x, double y)
            => new Point(x, y);
    }
}