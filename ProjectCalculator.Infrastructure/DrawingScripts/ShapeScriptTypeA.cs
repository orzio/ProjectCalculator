using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectCalculator.Infrastructure.DrawingScripts
{
    public class ShapeScriptTypeA : IShapeScript
    {

        private ParamFiz _paramFiz;
        private BendingMoment _bendingMoment;
        private InternalForces _internalForces;
        private TensionData _tensionData;
        private Dictionary<Char, Point> _furthestsPoints;
        private Contour _contour;
        private string _resultPage;
        private YieldPoint _yieldPoint;

        public ShapeScriptTypeA(ParamFiz paramFiz, BendingMoment bendingMoment, InternalForces internalForces,
            TensionData tensionData, Dictionary<Char, Point> furthestsPoints, Contour contour)
        {
            _paramFiz = paramFiz;
            _bendingMoment = bendingMoment;
            _internalForces = internalForces;
            _tensionData = tensionData;
            _furthestsPoints = furthestsPoints;
            _contour = contour;
        }

        public string GetScript()
        {
            var script =
              "<scr" + "ipt>" +
                 "var scale = 80;" +
                 "var offset = 500;" +

                 $"var dimensionOffset = {Math.Max(_paramFiz.Triangle.Height, _paramFiz.Rectangle.Height)} * scale;" +
                 $"var axisLength = {Math.Max(_paramFiz.Triangle.Width, _paramFiz.Rectangle.Width)} * scale;" +

                 "var rWidth = rectWidth*scale;" +
                 "var rHigh = rectHeight*scale;" +
                 "var tWidth = triangleWidth*scale;" +
                 "var tHigh = triangleHeight*scale;" +
                 "var totalWidth = rWidth*scale;" +
                 "var totalHeight = rHigh + tHigh ;" +
                 "var c = document.getElementById(\"myCanvas\");" +

                 "var ctx = c.getContext(\"2d\");" +

                 GetVerticalDimensions() +
                 GetHorizontalDimension()+
                    //draw gravity center dimentions
                    $"ctx.translate(offset,(totalHeight*1.2+offset));" +
                     "ctx.beginPath();" +

                     $"ctx.moveTo(0,0);" +
                      $"ctx.moveTo(0,-5);" +
                  $"ctx.lineTo(0,5);" +
                   $"ctx.moveTo(0,0);" +
                     $"ctx.lineTo({_paramFiz.Zc}*scale,0);" +
                      $"ctx.moveTo({_paramFiz.Zc}*scale,-5);" +
                  $"ctx.lineTo({_paramFiz.Zc}*scale,5);" +
                     $"ctx.fillText('{Math.Abs(_paramFiz.Zc)}a',scale*{_paramFiz.Zc * 0.4} ,-10);" +
                     "ctx.lineWidth = 1.5;" +
                     "ctx.stroke();" +
                   $"ctx.translate(-offset,-(totalHeight*1.2+offset));" +

                 //draw gravity vertival
                 GetGravityVerticatDimension()+
                

                 //draw shape
                 "ctx.beginPath();" +
                  $"ctx.translate(offset,offset);" +
                 "ctx.rect(0,0,rWidth,rHigh);" +
                 "ctx.moveTo(0,rHigh);" +
                 "ctx.lineTo(tWidth,rHigh);" +
                 "ctx.lineTo(0,rHigh+tHigh);" +
                 "ctx.lineTo(0,rHigh);" +
                 "ctx.lineWidth = 2.5;" +

                  "ctx.stroke();" +
                  "ctx.lineWidth = 1;" +

                 "ctx.beginPath();" +
                 "ctx.lineWidth = 1;" +
                 //draw y axis
                 "ctx.moveTo(rWidth,0);" +
                 "ctx.lineTo(rWidth,axisLength*1.25);" +
                 "ctx.lineTo(rWidth-10,axisLength*1.25-10);" +
                 "ctx.moveTo(rWidth,axisLength*1.25);" +
                 "ctx.lineTo(rWidth+10,axisLength*1.25-10);" +
                 "ctx.font = '25px serif';" +
                 "ctx.fillText('y',rWidth+10,axisLength*1.25);" +


                 //draw z axis
                 "ctx.moveTo(rWidth,0);" +
                 "ctx.lineTo(axisLength*1.85,0);" +
                 "ctx.lineTo(axisLength*1.85-10,-10);" +
                 "ctx.moveTo(axisLength*1.85,0);" +
                 "ctx.lineTo(axisLength*1.85-10,10);" +
                 "ctx.font = '25px serif';" +
                 "ctx.fillText('z',axisLength*1.85,-10);" +
                 "ctx.stroke();" +

                  //draw yc axis
                  "ctx.beginPath();" +
                 $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,0);" +
                 $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale,axisLength*1.25);" +
                 $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale-10,axisLength*1.25-10);" +
                 $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,axisLength*1.25);" +
                 $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale+10,axisLength*1.25-10);" +
                 "ctx.font = '25px serif';" +
                 $"ctx.fillText('yc',{_paramFiz.ZcFirstQuarter}*scale+10,axisLength*1.25);" +

                 //draw zc axis
                 $"ctx.moveTo(0,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.lineTo(axisLength*1.85,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.lineTo(axisLength*1.85-10,{_paramFiz.YcFirstQuarter}*scale-10);" +
                 $"ctx.moveTo(axisLength*1.85,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.lineTo(axisLength*1.85-10,{_paramFiz.YcFirstQuarter}*scale+10);" +
                 $"ctx.fillText('zc',axisLength*1.85,{_paramFiz.YcFirstQuarter}*scale-10);" +
                  "ctx.strokeStyle='green';" +
                  "ctx.stroke();" +
                 //draw M
                 "ctx.beginPath();" +
                  "ctx.lineWidth = 1.5;" +
                  "ctx.strokeStyle='blue';" +
                 $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale);" +
                 GetMomentArrows() +

                 $"ctx.fillText('M',{_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M} -10,{_paramFiz.YcFirstQuarter}*scale-10);" +
                 "ctx.stroke();" +

                 //draw os obojetna
                 "ctx.beginPath();" +
                  "ctx.strokeStyle='orange';" +
                   $"ctx.translate({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.rotate({-_paramFiz.Fi} * Math.PI / 180);" +
                  $"ctx.moveTo(-rWidth,{-_tensionData.KsiRate}*rWidth);" +
                  $"ctx.lineTo(rWidth,{_tensionData.KsiRate}*rWidth);" +


                  $"ctx.fillStyle = 'black';" +
                  $"ctx.fillText('oś obojętna',-rWidth,{-_tensionData.KsiRate}*rWidth+20);" +

                  "ctx.stroke();" +
                    $"ctx.rotate({_paramFiz.Fi} * Math.PI / 180);" +
                   $"ctx.translate({-_paramFiz.ZcFirstQuarter}*scale,{-_paramFiz.YcFirstQuarter}*scale);" +
                   $"ctx.fillStyle = 'black';" +

                 ////draw ksi
                 "ctx.beginPath();" +
                  "ctx.strokeStyle='red';" +
                 $"ctx.translate({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.rotate({-_paramFiz.Fi} * Math.PI / 180);" +
                 $"ctx.moveTo(-axisLength,0);" +
                 $"ctx.lineTo(axisLength,0);" +
                 $"ctx.lineTo(axisLength-10,-10);" +
                 $"ctx.moveTo(axisLength,0);" +
                 $"ctx.lineTo(axisLength-10,10);" +
                 $"ctx.fillText('ξ',axisLength,-10);" +
                 "ctx.stroke();" +
                  //draw arrow
                  "ctx.beginPath();" +
                  "ctx.lineWidth = 1.5;" +
                  "ctx.strokeStyle='blue';" +
                 $"ctx.moveTo(0,0);" + GetKsiArrows() +


                 $"ctx.fillText('Mξ',{_bendingMoment.Me}-50,30);" +
                 $"ctx.translate({-_paramFiz.ZcFirstQuarter}*scale,{-_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.setTransform(1,0,0,1,0,0);" +
                 "ctx.stroke();" +

                  //draw etha
                  "ctx.beginPath();" +
                  "ctx.strokeStyle='red';" +
                 $"ctx.translate(offset,offset);" +
                 $"ctx.translate({_paramFiz.ZcFirstQuarter}*scale,{_paramFiz.YcFirstQuarter}*scale);" +
                 $"ctx.rotate({-_paramFiz.Fi} * Math.PI / 180);" +
                 $"ctx.moveTo(0,-axisLength);" +
                 $"ctx.lineTo(0,axisLength);" +
                 $"ctx.lineTo(-10,axisLength-10);" +
                 $"ctx.moveTo(0,axisLength);" +
                 $"ctx.lineTo(10,axisLength-10);" +
                 $"ctx.fillText('η',10,axisLength-10);" +
                 "ctx.stroke();" +
                 //draw arrow
                 "ctx.beginPath();" +
                 "ctx.lineWidth = 1.5;" +
                  "ctx.strokeStyle='blue';" +
                 $"ctx.moveTo(0,0);" +

                 GetEthaArrows() +

                 $"ctx.fillText('Mη',-50,{_bendingMoment.Mn});" +
                 "ctx.stroke();" +

                 //draw gravitycenter
                 "ctx.beginPath();" +
                 $"ctx.rotate({_paramFiz.Fi} * Math.PI / 180);" +
                 $"ctx.moveTo(0,0);" +
                 $"ctx.arc(0, 0, 5, 0, Math.PI * 2, true);" +
                 $"ctx.fillText('C',5,20);" +
                 "ctx.stroke();" +


                 //draw last point
                 "ctx.beginPath();" +
                 "ctx.strokeStyle='black';" +
                 $"ctx.translate({_contour.FurthestsPointsFirstQuarter.Last().Value.HorizontalCoord}*scale,{_contour.FurthestsPointsFirstQuarter.Last().Value.VerticalCoord}*scale);" +
                 $"ctx.arc(0, 0, 5, 0, Math.PI * 2, false);" +
                 $"ctx.fillText('{_contour.FurthestsPointsFirstQuarter.Last().Key} ({_contour.FurthestsPointsFirstQuarter.Last().Value.HorizontalCoord}a; {_contour.FurthestsPointsFirstQuarter.Last().Value.VerticalCoord}a)',5,20);" +
                 "ctx.stroke();" +

                 ////draw first point
                 "ctx.beginPath();" +
                 $"ctx.translate({-_contour.FurthestsPointsFirstQuarter.Last().Value.HorizontalCoord}*scale,{-_contour.FurthestsPointsFirstQuarter.Last().Value.VerticalCoord}*scale);" +
                 $"ctx.translate({_contour.FurthestsPointsFirstQuarter.First().Value.HorizontalCoord}*scale,{_contour.FurthestsPointsFirstQuarter.First().Value.VerticalCoord}*scale);" +
                 $"ctx.arc(0, 0, 5, 0, Math.PI * 2, false);" +
                 $"ctx.fillText('{_contour.FurthestsPointsFirstQuarter.First().Key} ({_contour.FurthestsPointsFirstQuarter.First().Value.HorizontalCoord}a; {_contour.FurthestsPointsFirstQuarter.First().Value.VerticalCoord}a)',5,-20);" +
                 "ctx.stroke();" +
                 "</scr" + "ipt>";

            return script;
        }




        private string GetGravityDimensionSmootherTriangle()
        {
            return "ctx.beginPath();" +
                  $"ctx.translate(rWidth*1.3 + offset,offset+rHigh);" +
                  "ctx.moveTo(-5,0);" +
                  "ctx.lineTo(5,0);" +
                  "ctx.moveTo(0,0);" +

                  $"ctx.lineTo(0,{_paramFiz.Yc}*scale);" +
                  $"ctx.moveTo(-5,{_paramFiz.Yc}*scale);" +
                  $"ctx.lineTo(5,{_paramFiz.Yc}*scale);" +
                  $"ctx.moveTo(0,{_paramFiz.Yc}*scale);" +
                  $"ctx.fillText('{Math.Abs(_paramFiz.Yc)}a',5,{_paramFiz.Yc / 2}*scale);" +

                   "ctx.stroke();" +
                   $"ctx.translate(-rWidth*1.3 - offset,-offset-rHigh);";
        }

        private string GetGravityDimensionWiderTriangle()
        {
            return "ctx.beginPath();" +
                  $"ctx.translate(tWidth*1.3 + offset,offset+rHigh);" +
                  "ctx.moveTo(-5,0);" +
                  "ctx.lineTo(5,0);" +
                  "ctx.moveTo(0,0);" +

                  $"ctx.lineTo(0,{_paramFiz.Yc}*scale);" +
                  $"ctx.moveTo(-5,{_paramFiz.Yc}*scale);" +
                  $"ctx.lineTo(5,{_paramFiz.Yc}*scale);" +
                  $"ctx.moveTo(0,{_paramFiz.Yc}*scale);" +
                  $"ctx.fillText('{Math.Abs(_paramFiz.Yc)}a',5,{_paramFiz.Yc / 2}*scale);" +

                   "ctx.stroke();" +
                   $"ctx.translate(-tWidth*1.3 - offset,-offset-rHigh);";
        }

        private string GetGravityVerticatDimension()
        {
            if (_paramFiz.Triangle.Width > _paramFiz.Rectangle.Width)
                return GetGravityDimensionWiderTriangle();
            return GetGravityDimensionSmootherTriangle();
        }

        private string GetHorizontalDimension()
        {
            if (_paramFiz.Triangle.Width> _paramFiz.Rectangle.Width)
                return WiderTriangle();
            return SmootherTriangle();
        }

        private string SmootherTriangle()
        {
            return
            "ctx.beginPath();" +
                    $"ctx.translate(offset,totalHeight*1.35+offset);" +
                    //draw horizontal dimentions
                    "ctx.moveTo(0,0);" +
                    "ctx.moveTo(0,-5);" +
                   "ctx.lineTo(0,5);" +
                   "ctx.moveTo(0,0);" +
                   "ctx.lineTo(tWidth,0);" +
                    "ctx.moveTo(tWidth,-5);" +
                   "ctx.lineTo(tWidth,5);" +
                   "ctx.moveTo(tWidth,0);" +
                   "ctx.lineTo(rWidth,0);" +
                    "ctx.moveTo(rWidth,0);" +
                    "ctx.moveTo(rWidth,-5);" +
                   "ctx.lineTo(rWidth,5);" +
                    $"ctx.fillText('{_paramFiz.Triangle.Width}a',tWidth/2,-10);" +
                    $"ctx.fillText('{_paramFiz.Rectangle.Width - _paramFiz.Triangle.Width}a',tWidth + (rWidth - tWidth)/2,-10);" +

                    "ctx.stroke();" +
                    $"ctx.translate(-offset,-(totalHeight*1.35+offset));";
        }

        private string WiderTriangle()
        {
            return
            "ctx.beginPath();" +
                   $"ctx.translate(offset,totalHeight*1.35+offset);" +
                   //draw horizontal dimentions
                   "ctx.moveTo(0,0);" +
                   "ctx.moveTo(0,-5);" +
                  "ctx.lineTo(0,5);" +
                  "ctx.moveTo(0,0);" +
                  "ctx.lineTo(rWidth,0);" +
                   "ctx.moveTo(rWidth,-5);" +
                  "ctx.lineTo(rWidth,5);" +
                  "ctx.moveTo(rWidth,0);" +
                  "ctx.lineTo(tWidth,0);" +
                   "ctx.moveTo(tWidth,0);" +
                   "ctx.moveTo(tWidth,-5);" +
                  "ctx.lineTo(tWidth,5);" +
                   $"ctx.fillText('{_paramFiz.Rectangle.Width}a',rWidth/2,-10);" +
                   $"ctx.fillText('{_paramFiz.Triangle.Width - _paramFiz.Rectangle.Width}a',(tWidth-rWidth)/2+rWidth,-10);" +

                   "ctx.stroke();" +
                   $"ctx.translate(-offset,-(totalHeight*1.35+offset));";
        }

        private string GetEthaArrows()
        {
            if (_bendingMoment.Mn > 0)
                return GetPositiveEtha();
            return GetNegativeEtha();
        }

        private string GetPositiveEtha()
        {
            return
               $"ctx.lineTo(0,{_bendingMoment.Mn});" +
               $"ctx.lineTo(-10,{_bendingMoment.Mn}-10);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn});" +
               $"ctx.lineTo(10,{_bendingMoment.Mn}-10);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn}-10);" +
               $"ctx.lineTo(-10,{_bendingMoment.Mn}-20);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn}-10);" +
               $"ctx.lineTo(10,{_bendingMoment.Mn}-20);";

        }

        private string GetNegativeEtha()
        {
            return
               $"ctx.lineTo(0,{_bendingMoment.Mn});" +
               $"ctx.lineTo(-10,{_bendingMoment.Mn}+10);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn});" +
               $"ctx.lineTo(10,{_bendingMoment.Mn}+10);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn}+10);" +
               $"ctx.lineTo(-10,{_bendingMoment.Mn}+20);" +
               $"ctx.moveTo(0,{_bendingMoment.Mn}+10);" +
               $"ctx.lineTo(10,{_bendingMoment.Mn}+20);"; ;
        }

        private string GetKsiArrows()
        {
            if (_bendingMoment.Me > 0)
                return GetPositiveKsi();
            return GetNegativeKsi();
        }

        private string GetPositiveKsi()
        {
            return
               $"ctx.lineTo({_bendingMoment.Me},0);" +
               $"ctx.lineTo({_bendingMoment.Me}-10,-10);" +
               $"ctx.moveTo({_bendingMoment.Me},0);" +
               $"ctx.lineTo({_bendingMoment.Me}-10,10);" +
               $"ctx.moveTo({_bendingMoment.Me}-10,0);" +
               $"ctx.lineTo({_bendingMoment.Me}-20,-10);" +
               $"ctx.moveTo({_bendingMoment.Me}-10,0);" +
               $"ctx.lineTo({_bendingMoment.Me}-20,10);";
            ;
        }

        private string GetNegativeKsi()
        {
            return
              $"ctx.lineTo({_bendingMoment.Me},0);" +
               $"ctx.lineTo({_bendingMoment.Me}+10,-10);" +
               $"ctx.moveTo({_bendingMoment.Me},0);" +
               $"ctx.lineTo({_bendingMoment.Me}+10,10);" +
               $"ctx.moveTo({_bendingMoment.Me}+10,0);" +
               $"ctx.lineTo({_bendingMoment.Me}+20,-10);" +
               $"ctx.moveTo({_bendingMoment.Me}+10,0);" +
               $"ctx.lineTo({_bendingMoment.Me}+20,10);";
            ;
        }



        private string GetMomentArrows()
        {
            if (_bendingMoment.M > 0)
                return GetPositiveMoment();
            return GetNegativeMoment();
        }

        private string GetPositiveMoment()
        {
            return $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale-10);" +
                  $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale);" +
                  $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale+10);" +
                  $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale);" +
                  $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-20,{_paramFiz.YcFirstQuarter}*scale-10);" +
                  $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-10,{_paramFiz.YcFirstQuarter}*scale);" +
                  $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}-20,{_paramFiz.YcFirstQuarter}*scale+10);";
        }


        private string GetNegativeMoment()
        {
            return $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+10,{_paramFiz.YcFirstQuarter}*scale+10);" +
               $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M},{_paramFiz.YcFirstQuarter}*scale);" +
               $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+10,{_paramFiz.YcFirstQuarter}*scale-10);" +
               $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+10,{_paramFiz.YcFirstQuarter}*scale);" +
               $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+20,{_paramFiz.YcFirstQuarter}*scale+10);" +
               $"ctx.moveTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+10,{_paramFiz.YcFirstQuarter}*scale);" +
               $"ctx.lineTo({_paramFiz.ZcFirstQuarter}*scale + {_bendingMoment.M}+20,{_paramFiz.YcFirstQuarter}*scale-10);";

        }

        private string GetVerticalDimensions()
        {
            return
                 "ctx.beginPath();   " +
                  $"ctx.translate(rWidth*1.65 + offset,offset);" +
                  "ctx.moveTo(-5,0);" +
                  "ctx.lineTo(5,0);" +
                  "ctx.moveTo(0,0);" +

                  "ctx.lineTo(0,rHigh);" +
                  "ctx.font = '20px Arial';" +
                  $"ctx.fillText('{_paramFiz.Rectangle.Height}a',5,rHigh/2);" +

                  "ctx.moveTo(-5,rHigh);" +
                  "ctx.lineTo(5,rHigh);" +
                  "ctx.moveTo(0,rHigh);" +

                  "ctx.lineTo(0,rHigh + tHigh);" +

                   "ctx.moveTo(-5,rHigh + tHigh);" +
                  "ctx.lineTo(5,rHigh + tHigh);" +
                  "ctx.moveTo(0,rHigh + tHigh);" +
                   $"ctx.fillText('{_paramFiz.Triangle.Height}a',5,(rHigh) + tHigh/2);" +
                   "ctx.lineWidth = 1.5;" +
                   "ctx.stroke();" +
             $"ctx.translate(-rWidth*1.65 -offset,-offset);";
        }
    }
}
