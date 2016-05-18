using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Pkurg.PWorldBPM.Business.Sys;

/// <summary>
///CustomWorkflowDataProcess 的摘要说明
/// </summary>
public class CustomWorkflowDataProcess
{
    public CustomWorkflowDataProcess()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 获取当前实例的步骤（审批前）
    /// </summary>
    /// <param name="instanceId"></param>
    /// <returns></returns>
    public static string GetCurrentStepNameById(string instanceId,string defaultName)
    {
        string formId = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == instanceId).FormID;
        var info = DBContext.GetBizContext().OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == formId);
        if (info.CurrentStepId > 0)
        {
            var stepInfo = DBContext.GetSysContext().WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == info.CurrentStepId);
            return stepInfo == null ? defaultName : stepInfo.StepName;
        }
        return defaultName;
    }

    public static List<WF_Custom_InstanceItems> GetWorkItemsData(string formId)
    {
        ///流程未保存，取自session，用session数据驱动界面，保存时直接保存session,无需取客户端
        string sessionKey = string.Format("Steps_{0}", formId);
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = HttpContext.Current.Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;

        return itemInfos;
    }

    public static List<WF_Custom_InstanceItems> GetWorkItemsDataByInstanceId(string procID)
    {
        List<WF_Custom_InstanceItems> itemInfos = DBContext.GetSysContext().WF_Custom_InstanceItems.Where(x => x.InstancelD == procID).OrderBy(x => x.OrderId).ToList();
        return itemInfos;
    }

    public static void SaveWorkItemData(WF_Custom_InstanceItems itemInfo, string formId)
    {
        SaveSessionData(itemInfo, formId);
    }

    public static void SaveSessionData(WF_Custom_InstanceItems itemInfo, string formId)
    {
        string sessionKey = string.Format("Steps_{0}", formId);
        List<WF_Custom_InstanceItems> itemInfos = HttpContext.Current.Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;

        if (itemInfo.StepID <= 0)
        {//新增
            if (itemInfos == null)
            {
                itemInfos = new List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>();
                itemInfo.StepID = 1;
                itemInfo.OrderId = 1;
            }
            else
            {
                itemInfo.StepID = itemInfos.Max(x => x.StepID) + 1;
                itemInfo.OrderId = itemInfos.Max(x => x.OrderId) + 1;
            }

            itemInfos.Add(new Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems()
            {
                StepID = itemInfo.StepID,
                OrderId = itemInfo.OrderId,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StepName = itemInfo.StepName,
                PartUsers = itemInfo.PartUsers
            });
        }
        else
        {
            if (itemInfos != null)
            {
                var itemSessionInfo = itemInfos.FirstOrDefault(x => x.StepID == itemInfo.StepID);
                itemSessionInfo.LastUpdateTime = DateTime.Now;
                itemSessionInfo.StepName = itemInfo.StepName;
                itemSessionInfo.PartUsers = itemInfo.PartUsers;
            }
        }
        HttpContext.Current.Session[sessionKey] = itemInfos;
    }

    public static void SaveSessionDataToTemplation(string formId, string templateId)
    {
        SysDBDataContext context = DBContext.GetSysContext();
        List<WF_Custom_TemplationItems> itemInfos = context.WF_Custom_TemplationItems.Where(x => x.TemplD.ToString() == templateId).OrderBy(x => x.OrderId).ToList();
        List<WF_Custom_InstanceItems> instanceItemInfos = new List<WF_Custom_InstanceItems>();
        int currentIndex = 1;
        foreach (var itemInfo in itemInfos)
        {
            instanceItemInfos.Add(new Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems()
            {
                StepID =itemInfo.StepID,// context.WF_Custom_TemplationItems.Max(x => x.StepID) + 1,
                OrderId = itemInfo.OrderId,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StepName = itemInfo.StepName,
                PartUsers = itemInfo.PartUsers
            });
            currentIndex++;
        }

        string sessionKey = string.Format("Steps_{0}", formId);

        HttpContext.Current.Session[sessionKey] = instanceItemInfos;
        HttpContext.Current.Session[sessionKey + "Des"] = context.WF_Custom_Templation.FirstOrDefault(x => x.Id.ToString() == templateId).Des;
        HttpContext.Current.Session[sessionKey + "Name"] = context.WF_Custom_Templation.FirstOrDefault(x => x.Id.ToString() == templateId).Name;
    }

    private static void SaveDBData(WF_Custom_InstanceItems saveItemInfo, string procID)
    {
        SysDBDataContext context = DBContext.GetSysContext();

        var itemInfo = context.WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == saveItemInfo.StepID);
        if (itemInfo == null)
        {
            int? orderId = context.WF_Custom_InstanceItems.Where(x => x.InstancelD == procID).Max(x => x.OrderId);
            itemInfo = new Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems()
            {
                //StepID = context.WF_Custom_InstanceItems.Max(x=>x.StepID);
                OrderId = orderId.HasValue ? (orderId + 1) : 1,
                CreateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                StepName = saveItemInfo.StepName,
                PartUsers = saveItemInfo.PartUsers,
                InstancelD = procID
            };
            context.WF_Custom_InstanceItems.InsertOnSubmit(itemInfo);

        }
        else
        {
            itemInfo.LastUpdateTime = DateTime.Now;
            itemInfo.PartUsers = saveItemInfo.PartUsers;
            itemInfo.StepName = saveItemInfo.StepName;
            // context.WF_Custom_InstanceItems.Attach(itemInfo);
        }
        context.SubmitChanges();
    }

    public static string DisplayUserName(string partUsers)
    {
        var userInfos = partUsers.ToUserList();
        StringBuilder content = new StringBuilder();
        if (userInfos != null)
        {
            userInfos.ForEach(x => content.AppendFormat("{0},", x.UserInfo.EmployeeName));
        }
        return content.ToString().TrimEnd(',');
    }

    public static void LoadWorkItemsFromDBToSession(string FormId, string id)
    {
        SysDBDataContext context = DBContext.GetSysContext();
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = context.WF_Custom_InstanceItems.Where(x => x.InstancelD == id).ToList();

        string sessionKey = string.Format("Steps_{0}", FormId);
        HttpContext.Current.Session[sessionKey] = itemInfos;
    }


    #region  旧方法

    /// <summary>
    /// 获取当前步骤列表
    /// </summary>
    /// <param name="procID">为空表示未保存列表</param>
    /// <param name="formId"></param>
    /// <returns></returns>
    //[Obsolete("Old")]
    public static List<WF_Custom_InstanceItems> GetWorkItemsData(string procID, string formId)
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = null;
        if (string.IsNullOrWhiteSpace(procID))
        {
            ///流程未保存，取自session，用session数据驱动界面，保存时直接保存session,无需取客户端
            string sessionKey = string.Format("Steps_{0}", formId);
            itemInfos = HttpContext.Current.Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;
        }
        else
        {
            //取自数据库
            itemInfos = DBContext.GetSysContext().WF_Custom_InstanceItems.Where(x => x.InstancelD == procID).OrderBy(x => x.OrderId).ToList();
        }
        return itemInfos;
    }


    [Obsolete("Old")]
    public static void SaveWorkItemData(WF_Custom_InstanceItems itemInfo, string procID, string formId)
    {
        //List<WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(procID, formId);

        if (string.IsNullOrWhiteSpace(procID))
        {
            ///流程未保存，取自session，用session数据驱动界面，保存时直接保存session,无需取客户端
            SaveSessionData(itemInfo, formId);
        }
        else
        {
            SaveDBData(itemInfo, procID);
        }
    }

    #endregion


}

