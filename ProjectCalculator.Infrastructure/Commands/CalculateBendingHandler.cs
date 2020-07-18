using Autofac;
using ProjectCalculator.Infrastructure.Calculators;
using ProjectCalculator.Infrastructure.Factory.BeamCalculator;
using ProjectCalculator.Infrastructure.Factory.ShapeCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCalculator.Infrastructure.Writer;
using ProjectCalculator.Infrastructure.Factory.ContourPointsCalculator;
using ProjectCalculator.Core.Domain;

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

            #region CALCULATE_PARAMFIZ
            var paramFiz = shapeCalculator
                .CalculateCenterOfGravity()
                .CalculateCenterOfGravityInFirstQuarter()
                .CalculateJz()
                .CalculateJy()
                .CalculateCentralMomentOfInteria()
                .CalculateDeviantMoment()
                .CalculateMainCenteralMomentOfInteria()
                .CalculateTgFi()
                .GetParameters();
            #endregion

            #region CALCULATE_INTERNAL_FORCES
            var internalForces = beamCalculator
                .CalculateHa()
                .CalculateVa()
                .CalculateMa()
                .GetInternalForces();
            #endregion

            var bendingMomentCalculator = new BendingMomentsCalculator();
            var bendingMoment = bendingMomentCalculator
                .CalculateM1(internalForces.Moment, paramFiz.Fi)
                .CalculateM2(internalForces.Moment, paramFiz.Fi)
                .GetBendingMoment();

            var contourPoointsCalculator = new ContourCalculatorFactory()
                    .GetContourCalculator(command, paramFiz);
            var contourPoints = contourPoointsCalculator.GetPoints();



            using (StreamWriter file =
             new StreamWriter(@$"E:\Własne\Projekty\testFiles\{command.ShapeType}_B1_{command.Shape.B1}_B2_{command.Shape.B2}_H1_{command.Shape.H1}_H2_{command.Shape.H2}.txt"))
            {
                file.WriteLine($"B1_{command.Shape.B1}_B2_{command.Shape.B2}_H1_{command.Shape.H1}_H2_{command.Shape.H2}");
                foreach (var item in contourPoints)
                {
                    {
                        file.WriteLine($"{item.Key}, {item.Value.HorizontalCoord}, {item.Value.VerticalCoord}");
                    }
                }
            }

            _bendingCalculator.Calculate(paramFiz, bendingMoment);
            _bendingCalculator.CalculateEthaRate();
            var tensionData = _bendingCalculator.GetData();
            var line = new Line
            {
                KsiRate = tensionData.KsiRate,
                EthaRate = -1,
                Rate = 0
            };
            var rotatedPoints = new PointRotator(contourPoints,paramFiz.Fi).RotatePoints().GetPoints();
            var furthestPoints = new DistanceCalculator(rotatedPoints,line).GetFurthestPoints();
            _bendingCalculator.CalculateTensionInFurthestsPoints(furthestPoints);
            _bendingCalculator.ChooseMinMaxTension();
            _bendingCalculator.CalculateDimensionA(command.YieldPoint.Kr);




            #region HTML_OUTPUT
            var writer = new ProjectWriter(paramFiz, bendingMoment, internalForces, 
                tensionData, furthestPoints, contourPoints,command.YieldPoint);
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"../../../Files/index.html"));
            writer.ReadHtmlTemplate(path);
            writer.ReplaceHtmlTemplateWithValues();
            writer.SaveFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"../../../Files/result.html")));
            #endregion

            //https://help.syncfusion.com/file-formats/pdf/create-pdf-file-in-asp-net-core pdf creator

            return Task.FromResult(1);
        }
    }
}