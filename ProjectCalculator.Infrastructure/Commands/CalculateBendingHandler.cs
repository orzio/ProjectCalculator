using Autofac;
using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Factory.BeamCalculator;
using ProjectCalculator.Infrastructure.Factory.Shape;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            var beamCalCulatorFactory = new BeamCalculatorFactory();
            var beamCalculator = beamCalCulatorFactory.GetBeamCalculator(command.BeamType);
            var internalForces = beamCalculator.Calculate(command.Beam);

            var shapeCalculatorFactory = new ShapeCalculatorFactory();
            var shapeCalculator = shapeCalculatorFactory.GetShapeCalculator(command.ShapeType);
            var paramFiz = shapeCalculator.Calculate(command.Shape);

            _bendingCalculator.Calculate(paramFiz,internalForces);



            return Task.FromResult(1);
        }
    }
}
