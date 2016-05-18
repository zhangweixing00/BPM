using Pkurg.PWorldBPM.Common.Info;

namespace Pkurg.PWorldBPM.Common.IServices
{
    public interface IProcService
    {
        bool UpdateProcInst(ContextProcInst procInst);

        bool UpdateProcInst(ContextProcInst procInst,bool isUpDataAll);

        ContextProcInst GetInfo(string ProcID);

        ContextProcInst GetInfoByWFId(string workflowId);

        //ContextProcInst GetProcInstByWorkflowId(string workflowId);

       // ContextProcInst Save(ContextProcInst contextProcInst);
    }
}
