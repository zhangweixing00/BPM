using System;
using System.Linq;
using System.Reflection;

/// <summary>
///AppflowFactory 的摘要说明
/// </summary>
public class AppflowFactory
{
    public static AppFlowBase GetAppflow(string instanceId)
    {
        var info = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == instanceId);
        Type type = Assembly.GetExecutingAssembly().GetType("Appflow_" + info.AppID);
        if (type == null || !type.IsSubclassOf(typeof(AppFlowBase)))
        {
            return new AppFlowBase();
        }
        var instance = type.GetConstructors()[0].Invoke(null);
        return (AppFlowBase)instance;
    }
}