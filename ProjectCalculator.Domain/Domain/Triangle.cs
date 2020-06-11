using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Core.Domain
{
    public class Triangle
    {
        public Triangle()
        {

        }

        public double Height { get; set; }
        public double Width { get; set; }

        public double GetArea()
        {
            return Math.Round(0.5* Height * Width,3);
        }

        public double GetZCoordinate()
        {
            return Math.Round(Width * 1.0/3.0,3);
        }

        public double GetYCoordinate()
        {
            return Math.Round(Height * 1.0 / 3.0,3);
        }

        public double GetJzc()
        {
            return Math.Round(Width * Math.Pow(Height, 3) / 36, 3);
        }


        public double GetJyc()
        {
            return Math.Round(Height * Math.Pow(Width, 3) / 36, 3);
        }


        public double GetJz()
        {
            return Math.Round(Width * Math.Pow(Height, 3) / 12, 3);
        }


        public double GetJy()
        {
            return Math.Round(Height * Math.Pow(Width, 3) / 12, 3);
        }


        public double GetJzcyz()
        {
            return Math.Round(Math.Pow(Width, 2) * Math.Pow(Height, 2) / 72, 2);
        }
        public double GetJzy()
        {
            return Math.Round(Math.Pow(Width, 2) * Math.Pow(Height, 2) / 24, 2);
        }
    }
}
