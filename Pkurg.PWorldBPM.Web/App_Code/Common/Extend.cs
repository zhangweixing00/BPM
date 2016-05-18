using System.Collections.Generic;
using System.Text;

/// <summary>
///Extend 的摘要说明
/// </summary>
/// 
public static class Extend
{
    public static Pkurg.PWorldBPM.Common.Info.ContextProcInst ToContextInfo(this Pkurg.BPM.Entities.WorkFlowInstance instance)
    {
        return new Pkurg.PWorldBPM.Common.Info.ContextProcInst()
        {
            ProcId = instance.InstanceId,
            ProcName = instance.FormTitle,
            AppCode = instance.AppId,
            WorkflowId = instance.WfInstanceId,
            FormData = instance.FormData,
            Status = instance.WfStatus,
            EndTime = instance.FinishedTime,
            StartTime = instance.SumitTime,
            FormId = instance.FormId,
            StartDeptCode = instance.CreateDeptCode
        };
    }
    public static Pkurg.BPM.Entities.WorkFlowInstance ToDBInfo(this Pkurg.PWorldBPM.Common.Info.ContextProcInst instance)
    {
        IList<Pkurg.BPM.Entities.WorkFlowInstance> infos = new Pkurg.BPM.Services.WorkFlowInstanceService().Find(string.Format("InstanceId='{0}'", instance.ProcId));
        if (infos.Count == 0)
        {
            return null;
        }
        if (string.IsNullOrEmpty(infos[0].WfInstanceId))
        {
            infos[0].WfInstanceId = instance.WorkflowId;
        }

        infos[0].WfStatus = instance.Status;
        infos[0].SumitTime = instance.StartTime;
        infos[0].FinishedTime = instance.EndTime;
        infos[0].FormData = instance.FormData;
        infos[0].AppId = instance.AppCode;
        infos[0].FormTitle = instance.ProcName;
        infos[0].FormId = instance.FormId;

        return infos[0];
    }

    public static string ListToString(this List<string> items)
    {
        StringBuilder content = new StringBuilder();
        if (items != null)
        {
            items.ForEach(x => content.AppendFormat("{0},", x));
        }
        return content.ToString().Trim(',');
    }
}