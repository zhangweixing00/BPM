using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pkurg.PWorldBPM.Common.Info
{
    [Serializable]
    [XmlRoot]
    [XmlInclude(typeof(CounterSignInfo))]
    [XmlInclude(typeof(FormDataInfo))]
    public class ProcInstDataInfo
    {
        public List<FormDataInfo> DataInfo;

        public CounterSignInfo _CountsignInfo { get; set; }

        public CounterSignInfo GroupCountsignInfo { get; set; }

        public ProcInstDataInfo()
        {
            DataInfo = new List<FormDataInfo>();
            _CountsignInfo = new CounterSignInfo();
            GroupCountsignInfo = new CounterSignInfo();
        }
    }


}
