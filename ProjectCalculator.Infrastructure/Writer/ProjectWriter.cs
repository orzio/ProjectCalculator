using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectCalculator.Infrastructure.Writer
{
    public class ProjectWriter : IWriter
    {
        private ParamFiz _paramFiz;
        private BendingMoment _bendingMoment;
        private InternalForces _internalForces;
        private TensionData _tensionData;
        private Dictionary<Char, Point> _furthestsPoints;
        private Contour _contour;
        private string _resultPage;
        private YieldPoint _yieldPoint;

        public ProjectWriter(ParamFiz paramFiz, BendingMoment bendingMoment, InternalForces internalForces,
            TensionData tensionData, Dictionary<Char, Point> furthestsPoints, Contour contour,
            YieldPoint yieldPoint)
        {
            _paramFiz = paramFiz;
            _bendingMoment = bendingMoment;
            _internalForces = internalForces;
            _tensionData = tensionData;
            _furthestsPoints = furthestsPoints;
            _contour = contour;
            _yieldPoint = yieldPoint;
        }

        public bool ReadHtmlTemplate(string path)
        {
            if (File.Exists(path))
            {
                _resultPage = File.ReadAllText(path);
                return true;
            }

            return false;
        }

        public string ReplaceHtmlTemplateWithValues()
        {
            _resultPage = _resultPage.Replace("scriptPlace", new ShapeScriptTypeD( _paramFiz,  _bendingMoment,  _internalForces,
             _tensionData, _furthestsPoints,  _contour).GetScript());
               
            _resultPage = _resultPage.Replace("rectArea", _paramFiz.Rectangle.GetArea().ToString());
            _resultPage = _resultPage.Replace("triangleArea", _paramFiz.Triangle.GetArea().ToString());
            _resultPage = _resultPage.Replace("YCoordinate", _paramFiz.Yc.ToString());
            _resultPage = _resultPage.Replace("ZCoordinate", _paramFiz.Zc.ToString());
            _resultPage = _resultPage.Replace("rectZCoorda",
                _paramFiz.RectangleCoordZ < 0
                    ? $"({_paramFiz.RectangleCoordZ.ToString()}a)"
                    : $"{_paramFiz.RectangleCoordZ.ToString()}a");
            _resultPage = _resultPage.Replace("rectYCoorda",
                _paramFiz.RectangleCoordY < 0
                    ? $"({_paramFiz.RectangleCoordY.ToString()}a)"
                    : $"{_paramFiz.RectangleCoordY.ToString()}a");
            _resultPage = _resultPage.Replace("triangleZCoord",
                _paramFiz.TriangleCoordZ < 0
                    ? $"({_paramFiz.TriangleCoordZ.ToString()})"
                    : _paramFiz.TriangleCoordZ.ToString());
            _resultPage = _resultPage.Replace("triangleYCoord", _paramFiz.TriangleCoordY < 0
                ? $"({_paramFiz.TriangleCoordY.ToString()})"
                : _paramFiz.TriangleCoordY.ToString());
            _resultPage = _resultPage.Replace("rectWidth", _paramFiz.Rectangle.Width.ToString());
            _resultPage = _resultPage.Replace("rectHeight", _paramFiz.Rectangle.Height.ToString());
            _resultPage = _resultPage.Replace("triangleWidth", _paramFiz.Triangle.Width.ToString());
            _resultPage = _resultPage.Replace("triangleHeight", _paramFiz.Triangle.Height.ToString());
            _resultPage = _resultPage.Replace("JzRes", _paramFiz.Jz.ToString());
            _resultPage = _resultPage.Replace("JyRes", _paramFiz.Jy.ToString());
            _resultPage = _resultPage.Replace("TotalArea", _paramFiz.Area.ToString());
            _resultPage = _resultPage.Replace("JzcRes", _paramFiz.Jzc.ToString());
            _resultPage = _resultPage.Replace("JycRes", _paramFiz.Jyc.ToString());
            _resultPage = _resultPage.Replace("JyczcRes", _paramFiz.Jzcyc.ToString());
            _resultPage = _resultPage.Replace("JksiRes", _paramFiz.Je.ToString());
            _resultPage = _resultPage.Replace("JethaRes", _paramFiz.Jn.ToString());
            _resultPage = _resultPage.Replace("tg2FiRes", _paramFiz.Tg2Fi.ToString());
            _resultPage = _resultPage.Replace("2FiRes", _paramFiz.TwoFi.ToString());
            _resultPage = _resultPage.Replace("FiRes", _paramFiz.Fi.ToString());
            _resultPage = _resultPage.Replace("JzyRes", _paramFiz.Jzy.ToString());
            _resultPage = _resultPage.Replace("MethaRes", _bendingMoment.Mn.ToString());
            _resultPage = _resultPage.Replace("MksiRes", _bendingMoment.Me.ToString());
            _resultPage = _resultPage.Replace("MRes", _internalForces.Moment.ToString());
            _resultPage = _resultPage.Replace("MethaJethaRslt", _tensionData.MnJn.ToString());
            _resultPage = _resultPage.Replace("MksiJksiRslt", _tensionData.MeJe.ToString());
            _resultPage = _resultPage.Replace("MeJeResOpposite", _tensionData.MeJeOpposite.ToString());
            _resultPage = _resultPage.Replace("rateRes", _tensionData.KsiRate.ToString());
            _resultPage = _resultPage.Replace("FirstPointName", _furthestsPoints.First().Key.ToString());
            _resultPage = _resultPage
                .Replace("FPZCoord",
                    _contour.ContourPoints.FirstOrDefault(point => point.Key.ToString()
                                                           == _furthestsPoints.First().Key.ToString())
                        .Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("FPYCoord", _contour.ContourPoints.FirstOrDefault(point => point.Key.ToString()
                                                                                                 == _furthestsPoints
                                                                                                     .First().Key
                                                                                                     .ToString())
                .Value.VerticalCoord.ToString());
            _resultPage = _resultPage.Replace("SecondPointName", _furthestsPoints.Last().Key.ToString());
            _resultPage = _resultPage.Replace("SPZCoord", _contour.ContourPoints.FirstOrDefault(point => point.Key.ToString()
                                                                                                 == _furthestsPoints
                                                                                                     .Last().Key
                                                                                                     .ToString())
                .Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("SPYCoord", _contour.ContourPoints.FirstOrDefault(point => point.Key.ToString()
                                                                                                 == _furthestsPoints
                                                                                                     .Last().Key
                                                                                                     .ToString())
                .Value.VerticalCoord.ToString());
            _resultPage = _resultPage.Replace("FirstksiCoordinateRes",
                _furthestsPoints.First().Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("FirstethaCoordinateRes",
                _furthestsPoints.First().Value.VerticalCoord.ToString());
            _resultPage = _resultPage.Replace("SecondksiCoordinateRes",
                _furthestsPoints.Last().Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("SecondethaCoordinateRes",
                _furthestsPoints.Last().Value.VerticalCoord.ToString());
            _resultPage = _resultPage.Replace("FirstPSigmaResult", _tensionData.FirstPointTension.ToString());
            _resultPage = _resultPage.Replace("SecondPSigmaResult", _tensionData.SecondPointTension.ToString());
            _resultPage = _resultPage.Replace("SigmaMaxRes", _tensionData.SigmaMax.ToString());
            _resultPage = _resultPage.Replace("SigmaMinRes", _tensionData.AbsSigmMin.ToString());
            _resultPage = _resultPage.Replace("krReskc", _yieldPoint.Kr.ToString());
            _resultPage = _resultPage.Replace("SMaxRes", _tensionData.ASigmaMax.ToString());
            _resultPage = _resultPage.Replace("SMinRes", _tensionData.ASigmaMin.ToString());
            _resultPage = _resultPage.Replace("ADimRes", _tensionData.CrossDimention.ToString());

            return _resultPage;
        }

        public bool SaveFile(string path)
        {
            File.WriteAllText(path, _resultPage);
            return true;
        }
    }
}