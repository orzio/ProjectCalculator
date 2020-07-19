using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.ShapeCalculator
{
    public class ShapeCalculatorTypeC : IShapeCalculator
    {
        private ParamFiz _paramFiz;
        private Shape _shape;
        private Rectangle _rectangle;
        private Triangle _triangle;

        public ShapeCalculatorTypeC(Shape shape)
        {
            _shape = shape;
            _paramFiz = new ParamFiz();
            _rectangle = new Rectangle()
            {
                Height = _shape.H1,
                Width = _shape.B2
            };

            _triangle = new Triangle()
            {
                Height = _shape.H1 + _shape.H2,
                Width = _shape.B1
            };

            _paramFiz.Rectangle = _rectangle;
            _paramFiz.Triangle = _triangle;
        }


        public IShapeCalculator CalculateCenterOfGravity()
        {
            _paramFiz.RectangleCoordY = _rectangle.GetYCoordinate();
            _paramFiz.RectangleCoordZ = -_rectangle.GetZCoordinate();
            _paramFiz.TriangleCoordY = _triangle.GetYCoordinate();
            _paramFiz.TriangleCoordZ =_triangle.GetZCoordinate();
            _paramFiz.Sy = Math.Round(_rectangle.GetArea() * _paramFiz.RectangleCoordZ + _triangle.GetArea() * _paramFiz.TriangleCoordZ, 4);
            _paramFiz.Sz = Math.Round(_rectangle.GetArea() * _paramFiz.RectangleCoordY + _triangle.GetArea() * _paramFiz.TriangleCoordY, 4);
            _paramFiz.Area = Math.Round(_rectangle.GetArea() + _triangle.GetArea(), 3);

            _paramFiz.Zc = Math.Round(_paramFiz.Sy / _paramFiz.Area, 4);
            _paramFiz.Yc = Math.Round(_paramFiz.Sz / _paramFiz.Area, 4);

            return this;
        }

        public IShapeCalculator CalculateJz()
        {
            _paramFiz.Jz = Math.Round(_rectangle.GetJz() + _triangle.GetJz(), 4);
            return this;
        }

        public IShapeCalculator CalculateJy()
        {
            _paramFiz.Jy = Math.Round(_rectangle.GetJy() + _triangle.GetJy(), 4);
            return this;
        }

        public IShapeCalculator CalculateCentralMomentOfInteria()
        {
            _paramFiz.Jzc = Math.Round(_paramFiz.Jz - _paramFiz.Area * Math.Pow(_paramFiz.Yc, 2), 4);
            _paramFiz.Jyc = Math.Round(_paramFiz.Jy - _paramFiz.Area * Math.Pow(_paramFiz.Zc, 2), 4);
            return this;
        }

        public IShapeCalculator CalculateDeviantMoment()
        {
            _paramFiz.Jzy = Math.Round(-_rectangle.GetJzy() + _triangle.GetJzy(), 4);
            _paramFiz.Jzcyc = Math.Round(_paramFiz.Jzy - (_paramFiz.Area * _paramFiz.Yc * _paramFiz.Zc), 4);
            return this;
        }

        public IShapeCalculator CalculateMainCenteralMomentOfInteria()
        {
            _paramFiz.J1 = Math.Round(0.5 * (_paramFiz.Jzc + _paramFiz.Jyc) + 0.5 * Math.Sqrt(Math.Pow(_paramFiz.Jyc - _paramFiz.Jzc, 2) + 4 * Math.Pow(_paramFiz.Jzcyc, 2)), 4);
            _paramFiz.J2 = Math.Round(0.5 * (_paramFiz.Jzc + _paramFiz.Jyc) - 0.5 * Math.Sqrt(Math.Pow(_paramFiz.Jyc - _paramFiz.Jzc, 2) + 4 * Math.Pow(_paramFiz.Jzcyc, 2)), 4);
            _paramFiz.Je = _paramFiz.Jzc > _paramFiz.Jyc ? _paramFiz.J1 : _paramFiz.J2;
            _paramFiz.Jn = _paramFiz.Jyc > _paramFiz.Jzc ? _paramFiz.J1 : _paramFiz.J2;
            return this;
        }

        public IShapeCalculator CalculateTgFi()
        {
            _paramFiz.Tg2Fi = Math.Round(-2 * _paramFiz.Jzcyc / (_paramFiz.Jyc - _paramFiz.Jzc), 4);
            _paramFiz.TwoFi = Math.Round(Math.Atan(_paramFiz.Tg2Fi)*180.0d/Math.PI, 4);
            _paramFiz.Fi = Math.Round(_paramFiz.TwoFi / 2.0, 4);
            return this;
        }

        public IShapeCalculator CalculateCenterOfGravityInFirstQuarter()
        {
            var sy = Math.Round(_rectangle.GetArea() * (_rectangle.GetZCoordinate()) + _triangle.GetArea() * (_triangle.GetZCoordinate() + _rectangle.Width), 4);
            var sz = Math.Round(_rectangle.GetArea() * _rectangle.GetYCoordinate() + _triangle.GetArea() * _triangle.GetYCoordinate(), 4);
            _paramFiz.Area = Math.Round(_rectangle.GetArea() + _triangle.GetArea(), 3);

            _paramFiz.ZcFirstQuarter = Math.Round(sy / _paramFiz.Area, 4);
            _paramFiz.YcFirstQuarter = Math.Round(sz / _paramFiz.Area, 4);
                       
            return this;
        }

        public ParamFiz GetParameters()
        {
            return _paramFiz;

        }
    }
}
