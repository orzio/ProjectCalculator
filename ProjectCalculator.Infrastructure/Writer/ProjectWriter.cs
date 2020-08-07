using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.DrawingScripts;
using ProjectCalculator.Infrastructure.Factory.BeamScriptor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectCalculator.Infrastructure.Writer
{
    public class ProjectWriter : IWriter
    {
        private IShapeScript _shapeScriptCreator;
        private IBeamScript _beamScriptCreator;
        private ParamFiz _paramFiz;
        private BendingMoment _bendingMoment;
        private InternalForces _internalForces;
        private TensionData _tensionData;
        private Dictionary<Char, Point> _furthestsPoints;
        private Contour _contour;
        private string _resultPage;
        private YieldPoint _yieldPoint;
        private readonly IBeamScriptorEquation _beamScriptorEquation;
        private readonly FixedSupport _fixedSupport;

        public ProjectWriter(ParamFiz paramFiz, FixedSupport fixedSupport, BendingMoment bendingMoment, InternalForces internalForces,
            TensionData tensionData, Dictionary<Char, Point> furthestsPoints, Contour contour,
            YieldPoint yieldPoint, IShapeScript shapeScriptCreator, IBeamScript beamScriptCreator, IBeamScriptorEquation beamScriptorEquation)
        {
            _shapeScriptCreator = shapeScriptCreator;
            _paramFiz = paramFiz;
            _bendingMoment = bendingMoment;
            _internalForces = internalForces;
            _tensionData = tensionData;
            _furthestsPoints = furthestsPoints;
            _contour = contour;
            _yieldPoint = yieldPoint;
            _beamScriptCreator = beamScriptCreator;
            _beamScriptorEquation = beamScriptorEquation;
            _fixedSupport = fixedSupport;
            _beamScriptorEquation.SetValues();
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
            _resultPage = _resultPage.Replace("scriptPlace", _shapeScriptCreator.GetScript());
            _resultPage = _resultPage.Replace("beamPlace", _beamScriptCreator.GetScript());
               
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
            _resultPage = _resultPage.Replace("VResult", _fixedSupport.V.ToString());
            _resultPage = _resultPage.Replace("Q1Force", _beamScriptorEquation.Q1.ToString());
            _resultPage = _resultPage.Replace("Q2Force", _beamScriptorEquation.Q2.ToString());
            _resultPage = _resultPage.Replace("Q1L", _beamScriptorEquation.Q1L.ToString());
            _resultPage = _resultPage.Replace("Q2L", _beamScriptorEquation.Q2L.ToString());
            _resultPage = _resultPage.Replace("Q1SumL", _beamScriptorEquation.Q1SumL.ToString());
            _resultPage = _resultPage.Replace("Q2SumL", _beamScriptorEquation.Q2SumL.ToString());
            _resultPage = _resultPage.Replace("PForce", _beamScriptorEquation.P.ToString());
            _resultPage = _resultPage.Replace("PSumL", _beamScriptorEquation.PSumL.ToString());

            return _resultPage;
        }

        public bool SaveFile(string path)
        {
            File.WriteAllText(path, _resultPage);
            return true;
        }
    }
}