using Autofac;
using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Factory.BeamCalculator;
using ProjectCalculator.Infrastructure.Factory.ShapeCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ProjectCalculator.Infrastructure.Writer;

namespace ProjectCalculator.Infrastructure.Commands
{
    public class CalculateBendingHandler : ICommandHandler<BendingCommand>
    {
        private readonly IBendingCalculator _bendingCalculator;

        public CalculateBendingHandler()
        {
            _bendingCalculator = new BendingCalculator();
        }

        public Task HandleAsync(BendingCommand command)
        {
            var beamCalculator = new BeamCalculatorFactory()
                .GetBeamCalculator(command);


            var shapeCalculator = new ShapeCalculatorFactory()
                .GetShapeCalculator(command);

            var paramFiz = shapeCalculator
                .CalculateCenterOfGravity()
                .CalculateJz()
                .CalculateJy()
                .CalculateCentralMomentOfInteria()
                .CalculateDeviantMoment()
                .CalculateMainCenteralMomentOfInteria()
                .CalculateTgFi()
                .GetParameters();

            var internalForces = beamCalculator
                .CalculateHa()
                .CalculateVa()
                .CalculateMa()
                .GetInternalForces();

            var bendingMomentCalculator = new BendingMomentsCalculator();
            var bendingMoment = bendingMomentCalculator
                .CalculateM1(internalForces.Moment, paramFiz.Fi)
                .CalculateM2(internalForces.Moment, paramFiz.Fi)
                .GetBendingMoment();



            _bendingCalculator.Calculate(paramFiz, bendingMoment);
            _bendingCalculator.CalculateEthaRate();
            var tensionData = _bendingCalculator.GetData();
            
            var writer = new ProjectWriter(paramFiz, bendingMoment, internalForces, tensionData);
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"../../../Files/index.html"));
            writer.ReadHtmlTemplate(path);
            writer.ReplaceHtmlTemplateWithValues();
            writer.SaveFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"../../../Files/result.html")));

            //https://help.syncfusion.com/file-formats/pdf/create-pdf-file-in-asp-net-core pdf creator

            return Task.FromResult(1);
        }
    }
}