using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Factory.ShapeCalculator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public interface IShapeCalculator
    {
        IShapeCalculator CalculateCenterOfGravity();
        IShapeCalculator CalculateCentralMomentOfInteria();
        IShapeCalculator CalculateDeviantMoment();
        IShapeCalculator CalculateMainCenteralMomentOfInteria();
        IShapeCalculator CalculateTgFi();
        IShapeCalculator CalculateJz();
        IShapeCalculator CalculateJy();
        ParamFiz GetParameters();
    }
}
