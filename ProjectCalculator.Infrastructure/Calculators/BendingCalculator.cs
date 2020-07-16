﻿using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
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
            _tensionData.M1J1 = Math.Round(moment.Mn / paramFiz.Jn,4);
            _tensionData.M2J2 = Math.Round(moment.Mksi / paramFiz.Je,4);
        }

        public void CalculateEthaRate()
        {
            _tensionData.KsiRate = Math.Round(_tensionData.M1J1 / _tensionData.M2J2, 4);
        }

        public TensionData GetData()
        {
            return _tensionData;
        }
    }
}
