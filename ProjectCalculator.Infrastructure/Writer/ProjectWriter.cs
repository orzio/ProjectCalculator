using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectCalculator.Infrastructure.Writer
{
    public class ProjectWriter : IWriter
    {
        private ParamFiz _paramFiz;
        private BendingMoment _bendingMoment;
        private InternalForces _internalForces;
        private TensionData _tensionData;
        private string _resultPage;

        public ProjectWriter(ParamFiz paramFiz, BendingMoment bendingMoment, InternalForces internalForces, TensionData tensionData)
        {
            _paramFiz = paramFiz;
            _bendingMoment = bendingMoment;
            _internalForces = internalForces;
            _tensionData = tensionData;
        }

        public bool ReadHtmlTemplate(string path)
        {
            if (File.Exists(path))
            {
                _resultPage = File.ReadAllText(path);
                return true;
            }
            return false;
        }

        public string ReplaceHtmlTemplateWithValues()
        {
            _resultPage = _resultPage.Replace("rectArea", _paramFiz.Rectangle.GetArea().ToString());
            _resultPage = _resultPage.Replace("triangleArea", _paramFiz.Triangle.GetArea().ToString());
            _resultPage = _resultPage.Replace("YCoordinate", _paramFiz.Yc.ToString());
            _resultPage = _resultPage.Replace("ZCoordinate", _paramFiz.Zc.ToString());
            _resultPage = _resultPage.Replace("rectZCoord", _paramFiz.Rectangle.GetZCoordinate().ToString());
            _resultPage = _resultPage.Replace("rectYCoord", _paramFiz.Rectangle.GetYCoordinate().ToString());
            _resultPage = _resultPage.Replace("triangleZCoord", _paramFiz.Triangle.GetZCoordinate().ToString());
            _resultPage = _resultPage.Replace("triangleYCoord", _paramFiz.Triangle.GetYCoordinate().ToString());
            _resultPage = _resultPage.Replace("rectWidth", _paramFiz.Rectangle.Width.ToString());
            _resultPage = _resultPage.Replace("rectHeight", _paramFiz.Rectangle.Height.ToString());
            _resultPage = _resultPage.Replace("triangleWidth", _paramFiz.Triangle.Width.ToString());
            _resultPage = _resultPage.Replace("triangleHeight", _paramFiz.Triangle.Height.ToString());
            _resultPage = _resultPage.Replace("JzRes", _paramFiz.Jz.ToString());
            _resultPage = _resultPage.Replace("JyRes", _paramFiz.Jy.ToString());
            _resultPage = _resultPage.Replace("TotalArea", _paramFiz.Area.ToString());
            _resultPage = _resultPage.Replace("JzcRes", _paramFiz.Jzc.ToString());
            _resultPage = _resultPage.Replace("JycRes", _paramFiz.Jyc.ToString());
            _resultPage = _resultPage.Replace("JyczcRes", _paramFiz.Jzcyc .ToString());
            _resultPage = _resultPage.Replace("J1Res", _paramFiz.J1.ToString());
            _resultPage = _resultPage.Replace("J2Res", _paramFiz.J2.ToString());
            _resultPage = _resultPage.Replace("tg2FiRes", _paramFiz.Tg2Fi.ToString());
            _resultPage = _resultPage.Replace("2FiRes", _paramFiz.TwoFi.ToString());
            _resultPage = _resultPage.Replace("FiRes", _paramFiz.Fi.ToString());
            _resultPage = _resultPage.Replace("JzyRes", _paramFiz.Jzy.ToString());
            _resultPage = _resultPage.Replace("M1Res", _bendingMoment.M1.ToString());
            _resultPage = _resultPage.Replace("M2Res", _bendingMoment.M2.ToString());
            _resultPage = _resultPage.Replace("MRes", _internalForces.Moment.ToString());
            _resultPage = _resultPage.Replace("M1J1Rslt", _tensionData.M1J1.ToString());
            _resultPage = _resultPage.Replace("M2J2Rslt", _tensionData.M2J2.ToString());
            _resultPage = _resultPage.Replace("rateRes", _tensionData.ethaRate.ToString());
            return _resultPage;
        }

        public bool SaveFile(string path)
        {
            File.WriteAllText(path, _resultPage);
            return true;
        }
    }
}