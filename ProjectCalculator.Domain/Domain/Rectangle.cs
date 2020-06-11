using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Core.Domain
{
    public class Rectangle
    {

        public double Height { get; set; }
        public double Width { get; set; }

        public Rectangle() { }

        public double GetArea()
        {
            return Height * Width;
        }

        public double GetZCoordinate()
        {
            return Width * 0.5;
        }

        public double GetYCoordinate()
        {
            return Height * 0.5;
        }

        public double GetJzc()
        {
            return Math.Round(Width*Math.Pow(Height, 3) / 12,4);
        }


        public double GetJyc()
        {
            return Math.Round(Height * Math.Pow(Width, 3) / 12,4);
        }


        public double GetJz()
        {
            return Math.Round(Width * Math.Pow(Height, 3) / 3, 4);
        }


        public double GetJy()
        {
            return Math.Round(Height * Math.Pow(Width, 3) / 3, 4);
        }

        public double GetJzcyz()
        {
            return 0;
        }
        public double GetJzy()
        {
            return Math.Round(Math.Pow(Width, 2) * Math.Pow(Height, 2)/ 4, 4);
        }
    }
}
