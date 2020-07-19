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
        private Dictionary<Char, Point> _contourPoints;
        private string _resultPage;
        private YieldPoint _yieldPoint;

        public ProjectWriter(ParamFiz paramFiz, BendingMoment bendingMoment, InternalForces internalForces,
            TensionData tensionData, Dictionary<Char, Point> furthestsPoints, Dictionary<Char, Point> contourPoints,
            YieldPoint yieldPoint)
        {
            _paramFiz = paramFiz;
            _bendingMoment = bendingMoment;
            _internalForces = internalForces;
            _tensionData = tensionData;
            _furthestsPoints = furthestsPoints;
            _contourPoints = contourPoints;
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
            _resultPage = _resultPage.Replace("scriptPlace",
                "<scr" + "ipt>" +
                "var scale = 50;" +
                "var rWidth = rectWidth*scale;" +
                "var rHigh = rectHeight*scale;" +
                "var tWidth = triangleWidth*scale;" +
                "var tHigh = triangleHeight*scale;" +
                "var totalWidth = rWidth + tWidth;" +
                "var c = document.getElementById(\"myCanvas\");" +
                "var ctx = c.getContext(\"2d\");" +
                "ctx.beginPath();   " +
                $"ctx.translate(scale,scale);" +
                "ctx.rect(0,0,rWidth,rHigh);" +
                "ctx.moveTo(rWidth,0);" +
                "ctx.lineTo(rWidth+tWidth,0);" +
                "ctx.lineTo(rWidth,tHigh);" +

                //draw y axis
                "ctx.moveTo(rWidth,0);" +
                "ctx.lineTo(rWidth,rHigh*1.5);" +
                "ctx.lineTo(rWidth-10,rHigh*1.5-10);" +
                "ctx.moveTo(rWidth,rHigh*1.5);" +
                "ctx.lineTo(rWidth+10,rHigh*1.5-10);" +
                "ctx.font = '25px serif';" +
                "ctx.fillText('y',rWidth+10,rHigh*1.5);" +


                //draw z axis
                "ctx.moveTo(0,0);" +
                "ctx.lineTo(totalWidth+rHigh*0.5,0);" +
                "ctx.lineTo(totalWidth+rHigh*0.5-10,-10);" +
                "ctx.moveTo(totalWidth+rHigh*0.5,0);" +
                "ctx.lineTo(totalWidth+rHigh*0.5-10,10);" +
                "ctx.font = '25px serif';" +
                "ctx.fillText('z',totalWidth+rHigh*0.5,-10);" +

                //draw yc axis
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,0);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale,rHigh*1.5);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale-10,rHigh*1.5-10);" +
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,rHigh*1.5);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale+10,rHigh*1.5-10);" +
                "ctx.font = '25px serif';" +
                $"ctx.fillText('yc',{_paramFiz.ZcFirstQuarter}*scale+10,rHigh*1.5);" +

                //draw zc axis
                $"ctx.moveTo(0,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo(rHigh*1.5,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo(rHigh*1.5-10,{_paramFiz.YcFirstQuarter}*scale-10);" +
                $"ctx.moveTo(rHigh*1.5,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo(rHigh*1.5-10,{_paramFiz.YcFirstQuarter}*scale+10);" +
                $"ctx.fillText('zc',rHigh*1.5,{_paramFiz.YcFirstQuarter}*scale-10);" +
                //draw M
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale-10);" +
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale+10);" +
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-20,{_paramFiz.YcFirstQuarter}*scale-10);" +
                $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-20,{_paramFiz.YcFirstQuarter}*scale+10);" +
                $"ctx.fillText('M',{_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale-10);" +


                //draw ksi
                $"ctx.translate({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.rotate({-_paramFiz.Fi} * Math.PI / 180);" +
                $"ctx.moveTo(-rWidth,0);" +
                $"ctx.lineTo(totalWidth,0);" +
                $"ctx.lineTo(totalWidth-10,-10);" +
                $"ctx.moveTo(totalWidth,0);" +
                $"ctx.lineTo(totalWidth-10,10);" +
                $"ctx.fillText('ξ',totalWidth,-10);" +
                //draw arrow
                $"ctx.moveTo(0,0);" +
                $"ctx.lineTo({_bendingMoment.Me},0);" +
                $"ctx.lineTo({_bendingMoment.Me}-10,-10);" +
                $"ctx.moveTo({_bendingMoment.Me},0);" +
                $"ctx.lineTo({_bendingMoment.Me}-10,10);" +
                $"ctx.moveTo({_bendingMoment.Me}-10,0);" +
                $"ctx.lineTo({_bendingMoment.Me}-20,-10);" +
                $"ctx.moveTo({_bendingMoment.Me}-10,0);" +
                $"ctx.lineTo({_bendingMoment.Me}-20,10);" +
                 $"ctx.fillText('Mξ',{_bendingMoment.Me}-50,30);" +
                $"ctx.translate({-_paramFiz.ZcFirstQuarter},{-_paramFiz.YcFirstQuarter});" +
                $"ctx.setTransform(1,0,0,1,0,0);" +


                //drow etha
                $"ctx.translate(scale,scale);" +
                $"ctx.translate({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                $"ctx.rotate({-_paramFiz.Fi} * Math.PI / 180);" +
                $"ctx.moveTo(0,-rHigh);" +
                $"ctx.lineTo(0,rHigh);" +
                $"ctx.lineTo(-10,rHigh-10);" +
                $"ctx.moveTo(0,rHigh);" +
                $"ctx.lineTo(10,rHigh-10);" +
                $"ctx.fillText('η',10,rHigh-10);" +
                //draw arrow
                $"ctx.moveTo(0,0);" +
                $"ctx.lineTo(0,{_bendingMoment.Mn});" +
                $"ctx.lineTo(-10,{_bendingMoment.Mn}+10);" + 
                $"ctx.moveTo(0,{_bendingMoment.Mn});" +
                $"ctx.lineTo(10,{_bendingMoment.Mn}+10);" +
                $"ctx.moveTo(0,{_bendingMoment.Mn}+10);" +
                $"ctx.lineTo(-10,{_bendingMoment.Mn}+20);" + 
                $"ctx.moveTo(0,{_bendingMoment.Mn}+10);" +
                $"ctx.lineTo(10,{_bendingMoment.Mn}+20);" +
                 $"ctx.fillText('Mη',-50,{_bendingMoment.Mn});" +

                $"ctx.translate({-_paramFiz.ZcFirstQuarter},{-_paramFiz.YcFirstQuarter});" +
                $"ctx.setTransform(1,0,0,1,0,0);" +



                "ctx.stroke();" +
                "</scr" + "ipt>") + ";";
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
                    _contourPoints.FirstOrDefault(point => point.Key.ToString()
                                                           == _furthestsPoints.First().Key.ToString())
                        .Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("FPYCoord", _contourPoints.FirstOrDefault(point => point.Key.ToString()
                                                                                                 == _furthestsPoints
                                                                                                     .First().Key
                                                                                                     .ToString())
                .Value.VerticalCoord.ToString());
            _resultPage = _resultPage.Replace("SecondPointName", _furthestsPoints.Last().Key.ToString());
            _resultPage = _resultPage.Replace("SPZCoord", _contourPoints.FirstOrDefault(point => point.Key.ToString()
                                                                                                 == _furthestsPoints
                                                                                                     .Last().Key
                                                                                                     .ToString())
                .Value.HorizontalCoord.ToString());
            _resultPage = _resultPage.Replace("SPYCoord", _contourPoints.FirstOrDefault(point => point.Key.ToString()
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