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
            return 0.5* Height * Width;
        }

        public double GetZCoordinate()
        {
            return Width * 1.0/3.0;
        }

        public double GetYCoordinate()
        {
            return Height * 1.0 / 3.0;
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
