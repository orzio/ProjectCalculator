using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.ShapeCalculator
{
    public class ShapeCalculatorTypeA : IShapeCalculator
    {
        private ParamFiz _paramFiz;
        private Shape _shape;
        private Rectangle _rectangle;
        private Triangle _triangle;

        public ShapeCalculatorTypeA(Shape shape)
        {
            _shape = shape;
            _paramFiz = new ParamFiz();
            _rectangle = new Rectangle()
            {
                Height = _shape.H1 + _shape.H2,
                Width = _shape.B2
            };

            _triangle = new Triangle()
            {
                Height = _shape.H1,
                Width = _shape.B1
            };
        }


        public IShapeCalculator CalculateCenterOfGravity()
        {
            _paramFiz.Sy = _rectangle.GetArea() * (-_rectangle.GetZCoordinate()) + _triangle.GetArea() * _triangle.GetZCoordinate();
            _paramFiz.Sz = _rectangle.GetArea() * _rectangle.GetYCoordinate() + _triangle.GetArea() + _triangle.GetYCoordinate();
            _paramFiz.Area = Math.Round(_rectangle.GetArea() + _triangle.GetArea(), 2);

            _paramFiz.Zc = Math.Round(_paramFiz.Sy / _paramFiz.Area, 3);
            _paramFiz.Yc = Math.Round(_paramFiz.Sz / _paramFiz.Area, 3);

            return this;
        }

        public IShapeCalculator CalculateCentralMomentOfInteria()
        {
            _paramFiz.Jzc = Math.Round(_rectangle.GetJz() + _triangle.GetJz() - _paramFiz.Area * Math.Pow(_paramFiz.Yc, 2), 2);
            _paramFiz.Jyc = Math.Round(_rectangle.GetJy() + _triangle.GetJy() - _paramFiz.Area * Math.Pow(_paramFiz.Zc, 2));
            return this;
        }

        public IShapeCalculator CalculateDeviantMoment()
        {
            _paramFiz.Jzy = -_rectangle.GetJzy() + _triangle.GetJzy();
            _paramFiz.Jzcyc = _paramFiz.Jzy - _paramFiz.Area * (_paramFiz.Yc) * (-_paramFiz.Zc);
            return this;
        }

        public IShapeCalculator CalculateMainCenteralMomentOfInteria()
        {
            _paramFiz.J1 = Math.Round(0.5 * (_paramFiz.Jzc + _paramFiz.Jyc) + 0.5 * Math.Sqrt(Math.Pow(_paramFiz.Jyc - _paramFiz.Jzc, 2) + 4 * Math.Pow(_paramFiz.Jzcyc, 2)), 2);
            _paramFiz.J2 = Math.Round(0.5 * (_paramFiz.Jzc + _paramFiz.Jyc) - 0.5 * Math.Sqrt(Math.Pow(_paramFiz.Jyc - _paramFiz.Jzc, 2) + 4 * Math.Pow(_paramFiz.Jzcyc, 2)), 2);
            return this;
        }

        public IShapeCalculator CalculateTgFi()
        {
            _paramFiz.Tg2Fi = Math.Round(-2 * _paramFiz.Jzcyc / (_paramFiz.Jyc - _paramFiz.Jzc),2);
            _paramFiz.TwoFi = Math.Round(Math.Atan(_paramFiz.Tg2Fi), 2);
            _paramFiz.Fi = Math.Round(_paramFiz.TwoFi / 2.0, 2);
            return this;
        }



        public ParamFiz GetParameters()
        {
            return _paramFiz;
        }
    }
}
