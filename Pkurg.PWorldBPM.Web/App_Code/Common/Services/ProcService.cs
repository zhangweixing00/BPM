using System;
using System.Collections.Generic;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Common.Info;
using Pkurg.PWorldBPM.Common.IServices;

namespace Pkurg.PWorldBPM.Common.Services
{
    public class ProcService : IProcService
    {
        public bool UpdateProcInst(ContextProcInst procInst)
        {
            IList<ProcInst> infos = new Pkurg.BPM.Services.ProcInstService().Find(string.Format("ProcId='{0}'", procInst.ProcId));
            if (infos.Count == 0)
            {
                return false;
            }
            infos[0].Status = byte.Parse(procInst.Status);
            infos[0].FormData = procInst.FormData;
            infos[0].WorkflowId = int.Parse(procInst.WorkflowId);
            infos[0].ProcName = procInst.ProcName;

            return new Pkurg.BPM.Services.ProcInstService().Update(infos[0]);
        }


        public ContextProcInst GetInfo(string ProcID)
        {
            IList<ProcInst> infos = new Pkurg.BPM.Services.ProcInstService().Find(string.Format("ProcId='{0}'", ProcID));
            if (infos.Count == 0)
            {
                return null;
            }
            return new ContextProcInst()
            {
                ProcId = infos[0].ProcId,
                ProcName = infos[0].ProcName,
                FormData = infos[0].FormData,
                WorkflowId = infos[0].WorkflowId.ToString(),
                Status = infos[0].Status.Value.ToString(),
                AppCode = infos[0].AppCode,
                StartDeptCode = infos[0].CreatorDeptId
            };
        }
        public ContextProcInst GetInfoByWFId(string workflowId)
        {
            IList<ProcInst> infos = new Pkurg.BPM.Services.ProcInstService().Find(string.Format("workflowId='{0}'", workflowId));
            if (infos.Count == 0)
            {
                return null;
            }
            return new ContextProcInst()
            {
                ProcId = infos[0].ProcId,
                ProcName = infos[0].ProcName,
                FormData = infos[0].FormData,
                WorkflowId = infos[0].WorkflowId.ToString(),
                Status = infos[0].Status.Value.ToString(),
                AppCode = infos[0].AppCode,
                StartDeptCode = infos[0].CreatorDeptId
            };
        }

        public ContextProcInst Save(ContextProcInst contextProcInst)
        {
            new Pkurg.BPM.Services.ProcInstService().Save(new ProcInst()
            {
                ProcId = contextProcInst.ProcId,
                ProcName = contextProcInst.ProcName,
                Status = byte.Parse(contextProcInst.Status),
                AppCode = contextProcInst.AppCode,
                CreatorDeptId = contextProcInst.StartDeptCode
            });
            return contextProcInst;
        }


        public bool UpdateProcInst(ContextProcInst procInst, bool isUpDataAll)
        {
            throw new NotImplementedException();
        }
    }
}
