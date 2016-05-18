using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pkurg.PWorldBPM.Common.Info
{
    [XmlInclude(typeof(CounterSignDeptInfo))]
    public class CounterSignInfo
    {
        public CounterSignInfo()
        {
            Infos = new List<CounterSignDeptInfo>();
        }
        public List<CounterSignDeptInfo> Infos { get; set; }
    }
    [XmlInclude(typeof(DepartmentInfo))]
    public class CounterSignDeptInfo
    {
        public DepartmentInfo DeptInfo { get; set; }
        public bool IsChecked { get; set; }
        private bool isEnable = true;

        public bool IsEnable
        {
            get { return isEnable; }
            set { isEnable  = value; }
        }

    }
}
