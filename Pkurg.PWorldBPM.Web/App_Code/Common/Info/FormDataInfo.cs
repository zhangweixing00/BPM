using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pkurg.PWorldBPM.Common.Info
{
    [XmlInclude(typeof(ComplexData))]
    public class FormDataInfo
    {
        //public string ParamName { get; set; }
        //public string BindKey { get; set; }
        //public string BindValue { get; set; }
        //public bool Checked { get; set; }
        public FormDataInfo()
        {
            complexDatas = new List<ComplexData>();
        }
        public string ParamName { get; set; }
        public string TxtValue { get; set; }
        public List<string> Ids { get; set; }
        public string ControlType { get; set; }
        public List<ComplexData> complexDatas { get; set; }
    }
    
    public class ComplexData
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Checked { get; set; }
    }
    

}
