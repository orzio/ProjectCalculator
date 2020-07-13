using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.ContourPointsCalculator
{
    public class ContourPointCalculatorTypeA
    {
        private Dictionary<Char, Point> _contourPoints;
        private readonly Shape _shape;
        private readonly ParamFiz _paramFiz;

        public ContourPointCalculatorTypeA(Shape shape, ParamFiz paramFiz)
        {
            _shape = shape;
            _contourPoints = new Dictionary<char, Point>();
            _paramFiz = paramFiz;
            SetPoints();
        }

        public Dictionary<Char, Point> GetPoints()
        {
            return _contourPoints;
        }

        private void SetPoints()
        {
            _contourPoints.Add('A', Point.CreatePoint(-_paramFiz.ZcFirstQuarter, -_paramFiz.YcFirstQuarter));
            _contourPoints.Add('B', Point.CreatePoint(_shape.B1 + _shape.B2 - _paramFiz.ZcFirstQuarter, -_paramFiz.YcFirstQuarter));
            _contourPoints.Add('C', Point.CreatePoint(-_paramFiz.ZcFirstQuarter, _shape.H1 + _shape.H2 - _paramFiz.YcFirstQuarter));
            _contourPoints.Add('D', Point.CreatePoint(_paramFiz.ZcFirstQuarter > _shape.B2 
                                                     ? _shape.B2 - _paramFiz.ZcFirstQuarter 
                                                     :_paramFiz.ZcFirstQuarter - _shape.B2,

                                                                                            _paramFiz.YcFirstQuarter > _shape.H1 ?
                                                                                                        _shape.H1 - _paramFiz.YcFirstQuarter
                                                                                                        : _paramFiz.YcFirstQuarter - _shape.H1));

            _contourPoints.Add('E', Point.CreatePoint(_paramFiz.ZcFirstQuarter > _shape.B2
                                                     ? _shape.B2 - _paramFiz.ZcFirstQuarter
                                                     : _paramFiz.ZcFirstQuarter - _shape.B2, _paramFiz.YcFirstQuarter > _shape.H1 ?
                                                                                                        _shape.H1 - _paramFiz.YcFirstQuarter 
                                                                                                        : _paramFiz.YcFirstQuarter - _shape.H1));
        }
    }
}
