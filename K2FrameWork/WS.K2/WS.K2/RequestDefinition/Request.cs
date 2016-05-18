using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.K2.RequestDefinition
{
    [Serializable]
    [XmlType("Request")]
    public class Request
    {
        [XmlElement("Requestor")]
        public string Requestor { get; set; }
        [XmlElement("RequestorAD")]
        public string RequestorAD { get; set; }
        [XmlElement("Department")]
        public string Department { get; set; }
        [XmlElement("DepartmentName")]
        public string DepartmentName { get; set; }
        [XmlElement("WorkSpace")]
        public string WorkSpace { get; set; }
        [XmlElement("RequestorEmail")]
        public string RequestorEmail { get; set; }
        [XmlElement("RequestorTel")]
        public string RequestorTel { get; set; }
        [XmlElement("Operator")]
        public string Operator { get; set; }
        [XmlElement("OperatorAD")]
        public string OperatorAD { get; set; }
        [XmlElement("OperatorEmail")]
        public string OperatorEmail { get; set; }
        [XmlElement("OperatorTel")]
        public string OperatorTel { get; set; }
        [XmlElement("Data")]
        public List<RequestData> RequestDatas
        {
            get;
            set;
        }
        [XmlElement("Keys")]
        public List<RequestKeys> RequestKeys
        {
            get;
            set;
        }

        public Request()
        {
            Requestor = string.Empty;
            RequestorAD = string.Empty;
            //Key = string.Empty;
            Department = string.Empty;
            DepartmentName = string.Empty;
            WorkSpace = string.Empty;
            RequestorEmail = string.Empty;
            RequestorTel = string.Empty;
            Operator= string.Empty;
            OperatorAD = string.Empty;
            OperatorEmail = string.Empty;
            OperatorTel = string.Empty;
        }

        public void AddData(RequestData requestData)
        {
            if (RequestDatas == null)
            {
                RequestDatas = new List<RequestData>();
            }

            RequestDatas.Add(requestData);
        }

        public void AddData(string requestData)
        {
            if (RequestDatas == null)
            {
                RequestDatas = new List<RequestData>();
            }

            RequestDatas.Add(SerializationHelper.Deserialize<RequestData>(requestData));
        }

        public void AddKeys(RequestKeys requestKey)
        {
            if (RequestKeys == null)
            {
                RequestKeys = new List<RequestKeys>();
            }

            RequestKeys.Add(requestKey);
        }

        public void AddKeys(string requestKey)
        {
            if (RequestKeys == null)
            {
                RequestKeys = new List<RequestKeys>();
            }

            RequestKeys.Add(SerializationHelper.Deserialize<RequestKeys>(requestKey));
        }
    }
}