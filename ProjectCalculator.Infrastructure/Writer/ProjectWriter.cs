using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Writer
{
    public class ProjectWriter : IWriter
    {

        private ParamFiz _paramFiz;
        public ProjectWriter(ParamFiz paramFiz)
        {
            _paramFiz = paramFiz;
        }




    }
}
