using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Web.Services;
namespace Pkurg.PWorldBPM.FinallyDisposeServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FinallyDisposeServiceBase : System.Web.Services.WebService
    {

        public SerializableDictionary<string, string> dataFields { get; set; }


        public void SetValues(string XmlData)
        {
            dataFields = new SerializableDictionary<string, string>();
            if (!string.IsNullOrEmpty(XmlData))
            {
                string[] items = XmlData.Split('$');
                foreach (var item in items)
                {
                    string[] subs = item.Split('|');
                    dataFields.Add(subs[0], subs[1]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k2_workflowId">流程对应K2ID</param>
        /// <param name="dataFields">参数键值对</param>
        /// <returns></returns>
        public virtual ExecuteResultInfo DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields)
        {

            return new ExecuteResultInfo()
            {
                ExecException = "没有实现"
            };
        }
        protected virtual bool BeforeDoServiceEvent(int k2_workflowId)
        {
            return true;
        }

        [WebMethod]
        public ExecuteResultInfo InvokeService(int k2_workflowId, string XmlData)
        {
            SetValues(XmlData);
            BeforeDoServiceEvent(k2_workflowId);
            return DoServiceEvent(k2_workflowId, dataFields);
        }
    }
}
