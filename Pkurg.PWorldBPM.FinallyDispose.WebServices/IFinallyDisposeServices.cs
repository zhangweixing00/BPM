using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace Pkurg.PWorldBPM.FinallyDisposeServices
{
    public interface IFinallyDisposeServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="k2_workflowId">流程对应K2ID</param>
        /// <param name="dataFields">参数键值对</param>
        /// <returns></returns>
        ExecuteResultInfo DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields);
    }
}
