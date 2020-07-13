using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class ContourPointCalculatorTypeC
    {
        private Dictionary<Char, Point> _contourPoints;
        private readonly Shape _shape;
        private readonly ParamFiz _paramFiz;

        public ContourPointCalculatorTypeC(Shape shape, ParamFiz paramFiz)
        {
            _shape = shape;
            _contourPoints = new Dictionary<char, Point>();
            _paramFiz = paramFiz;
            SetPoints();
        }

        public Dictionary<Char,Point> GetPoints()
        {
            return _contourPoints;
        }

        private void SetPoints()
        {
            _contourPoints.Add('A', Point.CreatePoint((-_shape.B2 - Math.Abs(_paramFiz.Zc)), -_paramFiz.Yc));
            _contourPoints.Add('B', Point.CreatePoint((_shape.B1 + Math.Abs(_paramFiz.Zc)), -_paramFiz.Yc));
            _contourPoints.Add('C', Point.CreatePoint((-_shape.B2 - Math.Abs(_paramFiz.Zc)), _shape.H1 -_paramFiz.Yc));
            _contourPoints.Add('D', Point.CreatePoint(Math.Abs(_paramFiz.Zc), _shape.H1-_paramFiz.Yc));
            _contourPoints.Add('E', Point.CreatePoint(Math.Abs(_paramFiz.Zc),_shape.H1 + _shape.H2 -_paramFiz.Yc));
        }
    }
}

