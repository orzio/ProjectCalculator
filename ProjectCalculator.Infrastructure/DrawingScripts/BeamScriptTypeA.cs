using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.DrawingScripts
{
    public class BeamScriptTypeA: IBeamScript
    {
        private InternalForces _internalForces;
        private Beam _beam; 

        public BeamScriptTypeA(InternalForces internalForces,Beam beam )
        {
            _internalForces = internalForces;
            _beam = beam;
        }
        public string GetScript()
        {
            var script =
              "<scr" + "ipt>" +
                 "var scale = 80;" +
                 "var offset = 500;" +
                 "var c = document.getElementById(\"myBeam\");" +
                 "var ctx = c.getContext(\"2d\");" +
                  $"ctx.translate(offset,offset);" +
                  "ctx.beginPath();   " +


                  DrawBeam() +
                  DrawSupport()+
                  DrawHorizontalDimensions()+
                  DrawSupportForces()+
                  "ctx.font = '20px Arial';" +
            //$"ctx.fillText('{_paramFiz.Rectangle.Height}a',5,rHigh/2);";
            "</scr" + "ipt>";
            return script;
        }

        private string DrawBeam()
        {
            return "ctx.moveTo(0,0);" +
                  $"ctx.lineTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,0);" +
                 "ctx.stroke();";
        }

        private string DrawSupportForces()
        {
            return "ctx.moveTo(0,25);" +
                "ctx.lineTo(0,65);"+
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
                  "ctx.stroke();"+

                "ctx.beginPath();   " +
                "ctx.moveTo(-50,30);" +
                "ctx.arc(-50,0, 30, (Math.PI) / 2, Math.PI + (Math.PI) / 2, false);" +

                "ctx.moveTo(-50,-30);" +
                "ctx.lineTo(-60,-25);" +
                "ctx.moveTo(-50,-30);" +
                "ctx.lineTo(-60,-35);" +
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
                    "ctx.moveTo(0,15);"+
                    "ctx.lineTo(-5,20);" +
                     "ctx.moveTo(0,0);" +
                   "ctx.stroke();";
        }

        private string DrawHorizontalDimensions()
        {
            return "ctx.moveTo(0,1.2*scale);" +
                 $"ctx.lineTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale);"+

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
                  $"ctx.fillText('{_beam.L1}a',scale*{_beam.L1/2} ,1.2*scale-5);" +

                 //draw third
                 $"ctx.moveTo({_beam.L1+_beam.L2}*scale,1.2*scale);" +
                 $"ctx.moveTo({_beam.L1+_beam.L2}*scale,1.2*scale-5);" +
                 $"ctx.lineTo({_beam.L1+_beam.L2}*scale,1.2*scale+5);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2}*scale,1.2*scale);" +
                  $"ctx.fillText('{_beam.L2}a',scale*{_beam.L1 + _beam.L2/2} ,1.2*scale-5);" +


                 //draw fourth
                 $"ctx.moveTo({_beam.L1+_beam.L2+_beam.L3}*scale,1.2*scale);" +
                 $"ctx.moveTo({_beam.L1+_beam.L2+_beam.L3}*scale,1.2*scale-5);" +
                 $"ctx.lineTo({_beam.L1+_beam.L2+_beam.L3}*scale,1.2*scale+5);" +
                 $"ctx.moveTo({_beam.L1 + _beam.L2 + _beam.L3}*scale,1.2*scale);" +
                 $"ctx.fillText('{_beam.L3}a',scale*{_beam.L1 + _beam.L2 + _beam.L3/2} ,1.2*scale-5);" +
                 "ctx.stroke();";
        }
    }
}
