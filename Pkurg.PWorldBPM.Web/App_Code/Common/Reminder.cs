using System.Collections.Generic;
using Pkurg.PWorldBPM.Business.Controls;

/// <summary>
///催办
/// </summary>
public class Reminder
{
    public static bool IsShow(string instId)
    {
        IList<Pkurg.BPM.Entities.WorkFlowInstance> infos = new Pkurg.BPM.Services.WorkFlowInstanceService().Find(string.Format("InstanceId='{0}'", instId));
        if (infos.Count == 0)
        {
            return false;
        }
        string instanceCreateUserCode = string.IsNullOrEmpty(infos[0].CreateByUserCode) ? "" : infos[0].CreateByUserCode;
        return new IdentityUser().GetEmployee().PWordUser.EmployeeCode.ToLower() == instanceCreateUserCode.ToLower();
    }
    public static ResponseInfo Notify(string instId)
    {
        if (!IsShow(instId))
        {
            return new ResponseInfo()
            {
                IsSuccess = false,
                Des = "只有发起人才能催办"
            };
        }
        try
        {
            List<string> users = WF_Process.ReSendEmailAndGetToDoUsers(instId);

            OperationLog.Log(new IdentityUser().GetEmployee().Name,
                string.Format("催办：实例ID：{0}，用户：{1}", instId, users.ListToString()), 1);
            return new ResponseInfo()
            {
                IsSuccess = true,
                Des = "催办成功"
            };
        }
        catch
        {
            return new ResponseInfo()
            {
                IsSuccess = false,
                Des = "催办失败"
            };
        }


    }

}