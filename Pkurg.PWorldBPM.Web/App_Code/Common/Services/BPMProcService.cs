using System.Collections.Generic;
using Pkurg.PWorldBPM.Common.IServices;

namespace Pkurg.PWorldBPM.Common.Services
{
    public class BPMProcService : IProcService
    {
        public Pkurg.PWorldBPM.Common.Info.ContextProcInst GetProcInstByWorkflowId(string workflowId)
        {
            return new Info.ContextProcInst()
            {
                ProcName = "关于XXX的申请",
                AppType = ""
            };
        }

        public bool UpdateProcInst(Info.ContextProcInst procInst)
        {
            return new Pkurg.BPM.Services.WorkFlowInstanceService().Update(procInst.ToDBInfo());
        }

        public bool UpdateProcInst(Info.ContextProcInst procInst, bool isUpDataAll)
        {
            return new Pkurg.BPM.Services.WorkFlowInstanceService().Update(procInst.ToDBInfo());
        }

        Info.ContextProcInst IProcService.GetInfo(string ProcID)
        {
            IList<Pkurg.BPM.Entities.WorkFlowInstance> infos = new Pkurg.BPM.Services.WorkFlowInstanceService().Find(string.Format("InstanceId='{0}'", ProcID));
            if (infos.Count == 0)
            {
                return null;
            }
            return infos[0].ToContextInfo();
        }

        Info.ContextProcInst IProcService.GetInfoByWFId(string workflowId)
        {
            IList<Pkurg.BPM.Entities.WorkFlowInstance> infos = new Pkurg.BPM.Services.WorkFlowInstanceService().Find(string.Format("WFInstanceId='{0}'", workflowId));
            if (infos.Count == 0)
            {
                return null;
            }
            return infos[0].ToContextInfo();
        }
    }
}
