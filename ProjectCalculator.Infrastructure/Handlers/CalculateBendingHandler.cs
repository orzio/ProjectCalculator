﻿using Autofac;
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
using ProjectCalculator.Infrastructure.Factory.DrawingScript;
using ProjectCalculator.Infrastructure.DrawingScripts;
using ProjectCalculator.Infrastructure.Factory.BeamScriptor;

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
            var supportForces = beamCalculator.GetSupportForces();

            var bendingMomentCalculator = new BendingMomentsCalculator(internalForces.Moment);
            var bendingMoment = bendingMomentCalculator
                .CalculateM1(paramFiz.Fi)
                .CalculateM2(paramFiz.Fi)
                .GetBendingMoment();

            var contourPoointsCalculator = new ContourCalculatorFactory()
                    .GetContourCalculator(command, paramFiz);
            var contour = new Contour();
            contour.ContourPoints = contourPoointsCalculator.GetPoints();


            #region Cooment
            //using (StreamWriter file =
            // new StreamWriter(@$"E:\Własne\Projekty\testFiles\{command.ShapeType}_B1_{command.Shape.B1}_B2_{command.Shape.B2}_H1_{command.Shape.H1}_H2_{command.Shape.H2}.txt"))
            //{
            //    file.WriteLine($"B1_{command.Shape.B1}_B2_{command.Shape.B2}_H1_{command.Shape.H1}_H2_{command.Shape.H2}");
            //    foreach (var item in contour.ContourPoints)
            //    {
            //        {
            //            file.WriteLine($"{item.Key}, {item.Value.HorizontalCoord}, {item.Value.VerticalCoord}");
            //        }
            //    }
            //}
            #endregion

            _bendingCalculator.Calculate(paramFiz, bendingMoment);
            _bendingCalculator.CalculateEthaRate();
            var tensionData = _bendingCalculator.GetData();
            var line = new Line
            {
                KsiRate = tensionData.KsiRate,
                EthaRate = -1,
                Rate = 0
            };

            contour.RotatedPoints = new PointRotator(contour.ContourPoints, paramFiz.Fi).RotatePoints().GetPoints();
            contour.FurthestsPoints = new DistanceCalculator(contour.RotatedPoints, line).GetFurthestPoints();
            contour.FurthestsPointsFirstQuarter = contour.ContourPoints.Where(x => contour.FurthestsPoints.Any(p => p.Key == x.Key)).ToDictionary(x => x.Key, y => y.Value);
            _bendingCalculator.CalculateTensionInFurthestsPoints(contour.FurthestsPoints);
            _bendingCalculator.ChooseMinMaxTension();
            _bendingCalculator.CalculateDimensionA(command.YieldPoint.Kr);




            #region HTML_OUTPUT
            var shapeScriptFactory = new DrawingScriptFactory();
            var shapeScriptCreator = shapeScriptFactory.GetShapeScript(command, paramFiz, bendingMoment, internalForces,
                tensionData, contour.FurthestsPoints, contour);

            var writer = new ProjectWriter(paramFiz, supportForces, bendingMoment, internalForces,
                tensionData, contour.FurthestsPoints, contour, command.YieldPoint, shapeScriptCreator, new BeamScriptFactory().GetShapeScript(command, internalForces),
                new BeamEquationScriptorFactory().GetBeamScriptor(command)) ;
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