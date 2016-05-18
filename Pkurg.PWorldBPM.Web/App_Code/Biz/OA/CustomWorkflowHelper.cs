using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Pkurg.PWorldBPM.Business.Sys;

/// <summary>
///CustomWorkflowHelper 的摘要说明
/// </summary>
public static class CustomWorkflowHelper
{
    public static string SuperNodeName = "审批节点";//超级节点

    public static List<CustomWorkflowUserInfo> ToUserList(this string xmlData)
    {
        if (string.IsNullOrEmpty(xmlData))
        {
            return new List<CustomWorkflowUserInfo>();
        }
        List<CustomWorkflowUserInfo> userInfos = new List<CustomWorkflowUserInfo>();
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomWorkflowUserInfo>));
            using (StringReader sr = new StringReader(xmlData))
            {
                userInfos = serializer.Deserialize(sr) as List<CustomWorkflowUserInfo>;
            }
        }
        catch (Exception ex)
        {
            LoggerR.logger.DebugFormat("CustomWorkflowHelper:ToUserList:{0}\r\n{1}", ex.Message, ex.StackTrace);
        }
        return userInfos;
    }

    public static string ToXml(this List<CustomWorkflowUserInfo> userInfos)
    {
        string xmlData = string.Empty;
        if (userInfos==null||userInfos.Count==0)
        {
            return xmlData;
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomWorkflowUserInfo>));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, userInfos);
                xmlData = sw.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggerR.logger.DebugFormat("CustomWorkflowHelper:ToXml:{0}\r\n{1}", ex.Message, ex.StackTrace);
        }
        return xmlData;
    }
    public static string GetUserNameByLoginId(string item)
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo uInfo = DBContext.GetSysContext().V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName.ToLower() == item.ToLower());
            if (uInfo != null)
            {
                return uInfo.EmployeeName;
            }
        }
        return "";
    }

    public static V_Pworld_UserInfo GetUserByLoginId(string item)
    {
        if (!string.IsNullOrWhiteSpace(item))
        {
            Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo uInfo = DBContext.GetSysContext().V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName.ToLower() == item.ToLower());
            if (uInfo != null)
            {
                return uInfo;
            }
        }
        return null;
    }
    /// <summary>
    /// 检验当前步骤用户是否依旧存在，返回不存在的用户
    /// </summary>
    /// <param name="userInfos"></param>
    /// <returns></returns>
    public static List<V_Pworld_UserInfo> CheckUserExsit(this List<V_Pworld_UserInfo> userInfos)
    {
        List<V_Pworld_UserInfo> NotExsitUsers = userInfos.Where(x =>
        {
            return string.IsNullOrWhiteSpace(GetUserNameByLoginId(x.LoginName));
        }).ToList();
        return NotExsitUsers;
    }

    /// <summary>
    /// 将步骤节点转换成流程可用的df
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static string ToWFC_XML(this  List<WF_Custom_InstanceItems> items)
    {
        List<CustomWorkflowDataField> dfs = new List<CustomWorkflowDataField>();
        if (items != null)
        {
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.PartUsers)||item.PartUsers.ToUserList().Count==0)
                {
                    continue;
                }
                dfs.Add(new CustomWorkflowDataField()
                {
                    ActivityID = item.StepID,
                    ActivityName = item.StepName,
                    IsFinished = false,
                    IsOpen = true,
                    OrderId = item.OrderId.Value,
                    Users = GetUserString(item.PartUsers.ToUserList())
                });
            }
        }
        string xmlData = string.Empty;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CustomWorkflowDataField>));

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, dfs)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ;
                xmlData = sw.ToString();
            }
        }
        catch (Exception ex)
        {
            LoggerR.logger.DebugFormat("CustomWorkflowHelper:ToXml:{0}\r\n{1}", ex.Message, ex.StackTrace);
        }
        return xmlData;
    }

    private static string GetUserString(List<CustomWorkflowUserInfo> list)
    {
        if (list.Count==0)
        {
            return "NoApproval";
        }
        StringBuilder content = new StringBuilder();
        foreach (var item in list)
        {
            content.AppendFormat("K2:founder\\{0},", item.UserInfo.LoginName);
        }
        return content.ToString().TrimEnd(',');
    }

    public static string ToHtmlString(this string src)
    {
        Dictionary<string, string> kvs = new Dictionary<string, string>();
        kvs.Add("\r\n", "<br/>");

        foreach (KeyValuePair<string,string> item in kvs)
        {
            src = src.Replace(item.Key, item.Value);
        }
        return src;
    }
}