using Autofac;
using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Factory.BeamCalculator;
using ProjectCalculator.Infrastructure.Factory.ShapeCalculator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Commands
{
    public class CalculateBendingHandler : ICommandHandler<BendingCommand>
    {
        private readonly IBendingCalculator _bendingCalculator;
        private readonly IService _shapeService;

        public CalculateBendingHandler()
        {
            _bendingCalculator = new BendingCalculator();
        }

        public Task HandleAsync(BendingCommand command)
        {
            var beamCalculator = new BeamCalculatorFactory()
                .GetBeamCalculator(command.BeamType);
            var internalForces = beamCalculator.Calculate(command.Beam);

            var shapeCalculator = new ShapeCalculatorFactory()
                .GetShapeCalculator(command);


            var paramFiz = shapeCalculator
                .CalculateCenterOfGravity()
                .CalculateCentralMomentOfInteria()
                .CalculateDeviantMoment()
                .CalculateMainCenteralMomentOfInteria()
                .CalculateTgFi()
                .GetParameters();

            //https://help.syncfusion.com/file-formats/pdf/create-pdf-file-in-asp-net-core pdf creator
            _bendingCalculator.Calculate(paramFiz, internalForces);
            return Task.FromResult(1);
        }
    }
}
