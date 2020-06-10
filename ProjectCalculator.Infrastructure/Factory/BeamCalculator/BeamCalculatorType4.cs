using ProjectCalculator.Core.Domain;
using ProjectCalculator.Infrastructure.Calculators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Factory.BeamCalculator
{
    public class BeamCalculatorType4 : IBeamCalculator
    {
        private InternalForces _internalForces;
        private FixedSupport _fixedSupport;
        private Beam _beam;

        public BeamCalculatorType4(Beam beam)
        {
            _beam = beam;
            _internalForces = new InternalForces();
            _fixedSupport = new FixedSupport();
        }

        public IBeamCalculator CalculateVa()
        {
            _fixedSupport.V = _beam.Q1 * _beam.L2 + _beam.Q2 * _beam.L3 + _beam.P;
            return this;
        }

        public IBeamCalculator CalculateHa()
        {
            _fixedSupport.H = 0;
            return this;
        }

        public IBeamCalculator CalculateMa()
        {
            //positive - stretches the upper fibers
            _fixedSupport.M = _beam.P * (_beam.L1 + _beam.L2 + _beam.L3) + _beam.Q1 * _beam.L2 * (0.5 * _beam.L2 + _beam.L1) + _beam.Q2 * _beam.L1 * 0.5 * _beam.L1;
            _internalForces.Moment = _fixedSupport.M;
            return this;
        }

        public InternalForces GetInternalForces()
        {
            return _internalForces;
        }
    }
}
