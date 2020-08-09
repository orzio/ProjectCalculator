using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.DrawingScripts
{
    public class BeamScriptTypeD : IBeamScript
    {
        private InternalForces _internalForces;
        private Beam _beam;
        private double forceOffset = 0.1d;

        public BeamScriptTypeD(InternalForces internalForces, Beam beam)
        {
            _internalForces = internalForces;
            _beam = beam;
        }
        public string GetScript()
        {
            var script =
              "<scr" + "ipt>" +
                 "var scale = 80;" +
                   "var offset = 120;" +
                 "var c = document.getElementById(\"myBeam\");" +
                 "var ctx = c.getContext(\"2d\");" +
                  $"ctx.translate(offset,offset);" +
                  "ctx.beginPath();   " +


                  DrawBeam() +
                  DrawSupport() +
                  DrawHorizontalDimensions() +
                  DrawSupportForces() +
                  DrawQForce(_beam.L1, _beam.L2, _beam.Q1) +
                  DrawQForce(forceOffset, _beam.L1 - forceOffset, _beam.Q2) +
                  DrawPForce(_beam.L1 + _beam.L2 + _beam.L3, _beam.P) +

                  "ctx.font = '20px Arial';" +
            "</scr" + "ipt>";
            return script;
        }

        private string DrawBeam()
        {
            return "ctx.moveTo(0,0);" +
                  $"ctx.lineTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,0);" +
                 "ctx.stroke();";
        }

        private string DrawPForce(double forcePoint, double forceValue)
        {
            var scale = 80;
            var forceLength = 45;
            return
                DrawPArrow(forcePoint * scale, forceLength) +
                $"ctx.fillText('{forceValue}qL',{forcePoint * scale}+5,{forceLength});";

        }

        private string DrawQForce(double firstPoint, double lastPoint, double qForce)
        {
            var forceHeight = 5 * qForce;
            var scale = 80;
            var startPoint = firstPoint * scale;
            var endPoint = (firstPoint + lastPoint) * scale;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
            $"ctx.moveTo({startPoint},{-forceHeight});" +
            $"ctx.lineTo({endPoint},{-forceHeight});");



            var distance = startPoint;

            while (distance <= endPoint)
            {
                stringBuilder.Append(DrawArrow(distance, forceHeight));
                distance += 0.5 * scale;
            }
            if (firstPoint == forceOffset)
                stringBuilder.Append(DrawArrow(_beam.L1 * scale, forceHeight));

            stringBuilder.Append($"ctx.fillText('{qForce}q',{endPoint}+5,{-forceHeight});");
            stringBuilder.Append("ctx.stroke();");
            return stringBuilder.ToString();
        }

        private string DrawArrow(double xCoordinate, double force)
        {
            return $"ctx.moveTo({xCoordinate},-{force});" +
                 $"ctx.lineTo({xCoordinate},-5);" +
                 $"ctx.lineTo({xCoordinate}-5,-15);" +
                 $"ctx.moveTo({xCoordinate},-5);" +
                 $"ctx.lineTo({xCoordinate}+5,-15);" +
                 "ctx.stroke();";
        }

        private string DrawPArrow(double xCoordinate, double force)
        {
            return $"ctx.moveTo({xCoordinate},5);" +
                 $"ctx.lineTo({xCoordinate},{force});" +
                 $"ctx.lineTo({xCoordinate}-5,{force}-15);" +
                 $"ctx.moveTo({xCoordinate},{force});" +
                 $"ctx.lineTo({xCoordinate}+5,{force}-15);" +
                 "ctx.stroke();";
        }



        private string DrawSupportForces()
        {
            return "ctx.moveTo(0,25);" +
                "ctx.lineTo(0,65);" +
                "ctx.moveTo(0,25);" +
                "ctx.lineTo(-5,35);" +
                "ctx.moveTo(0,25);" +
                "ctx.lineTo(5,35);" +
                 $"ctx.fillText('Va',5 ,55);" +

                 "ctx.moveTo(-10,0);" +
                "ctx.lineTo(-45,0);" +
                "ctx.moveTo(-10,0);" +
                "ctx.lineTo(-20,-5);" +
                "ctx.moveTo(-10,0);" +
                "ctx.lineTo(-20,5);" +
                 $"ctx.fillText('Ha',-40 ,20);" +
                  "ctx.stroke();" +

                "ctx.beginPath();   " +
                "ctx.moveTo(-50,30);" +
                "ctx.arc(-50,0, 30, (Math.PI) / 2, Math.PI + (Math.PI) / 2, false);" +

                "ctx.moveTo(-50,30);" +
                "ctx.lineTo(-60,20);" +
                "ctx.moveTo(-50,30);" +
                "ctx.lineTo(-60,35);" +
                 $"ctx.fillText('Ma',-105,0);" +

                "ctx.stroke();";


        }


        private string DrawSupport()
        {
            return "ctx.moveTo(0,0);" +
                   "ctx.moveTo(0,-20);" +
                   "ctx.lineTo(0,20);" +

                   "ctx.moveTo(0,-20);" +
                   "ctx.lineTo(-5,-15);" +
                   "ctx.moveTo(0,-15);" +
                   "ctx.lineTo(-5,-10);" +
                   "ctx.moveTo(0,-10);" +
                   "ctx.lineTo(-5,-5);" +
                   "ctx.moveTo(0,-5);" +
                   "ctx.lineTo(-5,0);" +
                   "ctx.moveTo(0,0);" +
                   "ctx.lineTo(-5,5);" +
                    "ctx.moveTo(0,5);" +
                   "ctx.lineTo(-5,10);" +
                    "ctx.moveTo(0,10);" +
                   "ctx.lineTo(-5,15);" +
                    "ctx.moveTo(0,15);" +
                    "ctx.lineTo(-5,20);" +
                     "ctx.moveTo(0,0);" +
                   "ctx.stroke();";
        }

        private string DrawHorizontalDimensions()
        {
            return "ctx.moveTo(0,1.2*scale);" +
                 $"ctx.lineTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale);" +

                 //draw first
                 "ctx.moveTo(0,1.2*scale);" +
                 "ctx.moveTo(0,1.2*scale-5);" +
                 "ctx.lineTo(0,1.2*scale+5);" +
                 "ctx.moveTo(0,1.2*scale);" +
                 "ctx.font = '15px Arial';" +
                 //draw second
                 $"ctx.moveTo({_beam.L1}*scale,1.2*scale);" +
                 $"ctx.moveTo({_beam.L1}*scale,1.2*scale-5);" +
                 $"ctx.lineTo({_beam.L1}*scale,1.2*scale+5);" +
                 $"ctx.moveTo({_beam.L1}*scale,1.2*scale);" +
                  $"ctx.fillText('{_beam.L1}L',scale*{_beam.L1 / 2} ,1.2*scale-5);" +

                 //draw third
                 $"ctx.moveTo({_beam.L1 + _beam.L2}*scale,1.2*scale);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2}*scale,1.2*scale-5);" +
                 $"ctx.lineTo({_beam.L1 + _beam.L2}*scale,1.2*scale+5);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2}*scale,1.2*scale);" +
                  $"ctx.fillText('{_beam.L2}L',scale*{_beam.L1 + _beam.L2 / 2} ,1.2*scale-5);" +


                 //draw fourth
                 $"ctx.moveTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale-5);" +
                 $"ctx.lineTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale+5);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale);" +
                 $"ctx.fillText('{_beam.L3}L',scale*{_beam.L1 + _beam.L2 + _beam.L3 / 2} ,1.2*scale-5);" +
                 "ctx.stroke();";
        }
    }
}
