using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectCalculator.Infrastructure.Calculators
{
    public class BendingCalculator:IBendingCalculator
    {
        private readonly TensionData _tensionData;
        public BendingCalculator()
        {
            _tensionData = new TensionData();
        }
        public void Calculate(ParamFiz paramFiz, BendingMoment moment)
        {
            _tensionData.MnJn = Math.Round(moment.Mn / paramFiz.Jn,4);
            _tensionData.MeJe = Math.Round(-moment.Me / paramFiz.Je,4);
            _tensionData.MeJeOpposite = -_tensionData.MeJe;
        }

        public void CalculateEthaRate()
        {
            _tensionData.KsiRate = Math.Round(_tensionData.MnJn / _tensionData.MeJeOpposite, 4);
        }

        public TensionData GetData()
        {
            return _tensionData;
        }

        public void CalculateTensionInFurthestsPoints(Dictionary<Char,Point> furthestsPoints)
        {
            _tensionData.FirstPointTension = Math.Round(_tensionData.MnJn*furthestsPoints.First().Value.HorizontalCoord 
                                 + _tensionData.MeJe*furthestsPoints.First().Value.VerticalCoord,4);
            _tensionData.SecondPointTension = Math.Round(_tensionData.MnJn*furthestsPoints.Last().Value.HorizontalCoord 
                                  + _tensionData.MeJe*furthestsPoints.Last().Value.VerticalCoord,4);
        }
        public void ChooseMinMaxTension()
        {
            _tensionData.SigmaMax = Math.Max(_tensionData.FirstPointTension, _tensionData.SecondPointTension);
            _tensionData.SigmMin = Math.Min(_tensionData.FirstPointTension, _tensionData.SecondPointTension);
            _tensionData.AbsSigmMin = Math.Abs(_tensionData.SigmMin);
        }

        public void CalculateDimensionA(double kr)
        {
            _tensionData.ASigmaMin = Math.Round(Math.Pow(_tensionData.AbsSigmMin, 1.0 / 3.0),4);
            _tensionData.ASigmaMax = Math.Round(Math.Pow(_tensionData.SigmaMax/kr, 1.0 / 3.0),4);
            _tensionData.CrossDimention = Math.Max(_tensionData.ASigmaMin, _tensionData.ASigmaMax);
        }
    }
}
