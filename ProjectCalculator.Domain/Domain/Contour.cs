using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Core.Domain
{
    public class Contour
    {
        public Dictionary<Char, Point> ContourPoints { get; set; }
        public Dictionary<Char, Point> RotatedPoints { get; set; }
        public Dictionary<Char, Point> FurthestsPoints { get; set; }
        public Dictionary<Char, Point> FurthestsPointsFirstQuarter { get; set; }
    }
}
