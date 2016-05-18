using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
///ITSupportCommon 的摘要说明
/// </summary>
public class ITSupportCommon
{
    public ITSupportCommon()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static string GetK2DataFieldByType(string typeId)
    {
        var bizContext = DBContext.GetBizContext();
        var catalogInfo = bizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id.ToString() == typeId);
        return GetK2DataFieldByGroupId(catalogInfo.GroupId.Value);
    }

    public static string GetK2DataFieldByGroupId(int groupId)
    {
        var bizContext = DBContext.GetBizContext();
        var groupInfo = bizContext.V_ITSupport_ITGroup.FirstOrDefault(x => x.Id == groupId);
        if (String.IsNullOrWhiteSpace(groupInfo.UserList))
        {
            throw new Exception("组内没有设置IT顾问");
        }
        StringBuilder content = new StringBuilder();
        foreach (var item in groupInfo.UserList.Split(',').ToList())
        {
            content.AppendFormat("{0},",Workflow_Common.GetK2UserByLoginName(item));
        }
        return content.ToString().TrimEnd(',');
    }




    public static List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> GetUserListByType(string typeId)
    {
        var bizContext = DBContext.GetBizContext();
        var catalogInfo = bizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id.ToString() == typeId);
        return GetUserListByGroupId(catalogInfo.GroupId.Value);
    }

    public static List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> GetUserListByGroupId(int groupId)
    {
        var bizContext = DBContext.GetBizContext();
        var groupInfo = bizContext.V_ITSupport_ITGroup.FirstOrDefault(x => x.Id == groupId);
        if (String.IsNullOrWhiteSpace(groupInfo.UserList))
        {
            throw new Exception("组内没有设置IT顾问");
        }
        var sysContext=DBContext.GetSysContext();
        List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> userList = new List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo>();
        foreach (var item in groupInfo.UserList.Split(',').ToList())
        {
            var userInfo = sysContext.V_Pworld_UserInfo.FirstOrDefault(x => item==x.LoginName);
            if (userInfo!=null)
            {
                userList.Add(userInfo);
            }
        }
        return userList;
    }
}

public enum ITSupportStatus:int
{
    待领取,
    处理,
    加签处理,
    转签处理,
    已转出,
    结束
}