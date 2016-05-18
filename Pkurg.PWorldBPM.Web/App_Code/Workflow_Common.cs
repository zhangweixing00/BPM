using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Common;

/// <summary>
///流程公共处理
/// </summary>
public class Workflow_Common : UControlBase
{
    /// <summary>
    /// 判断某一步骤某一人员是否为加签
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="currentUser"></param>
    /// <returns></returns>
    public bool IsAddSign(string sn, string currentUser)
    {
        try
        {
            string addApproversBy_Value = "";
            bool isOk = WorkflowHelper.GetActivityDataField(sn, "AddApproversBy", currentUser, out addApproversBy_Value);
            if (isOk && !string.IsNullOrEmpty(addApproversBy_Value))
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// 判断某一步骤某一人员是否为转签
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="currentUser"></param>
    /// <returns></returns>
    public bool IsChangeSign(string sn, string currentUser)
    {
        try
        {
            string changeApproversBy_Value = "";
            bool isOk = WorkflowHelper.GetActivityDataField(sn, "changeApproversBy", currentUser, out changeApproversBy_Value);
            if (isOk && !string.IsNullOrEmpty(changeApproversBy_Value))
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 是否为合法SQL
    /// </summary>
    /// <param name="queryConditions"></param>
    /// <returns></returns>
    public static bool ValidateQuery(string queryConditions)
    {
        //构造SQL的注入关键字符
        #region 字符
        string[] strBadChar = {"and"
            ,"exec"
            ,"insert"
            ,"select"
            ,"delete"
            ,"update"
            ,"count"
            ,"or"
            //,"*"
            ,"%"
            ,":"
            ,"\'"
            ,"\""
            ,"chr"
            ,"mid"
            ,"master"
            ,"truncate"
            ,"char"
            ,"declare"
            ,"SiteName"
            ,"net user"
            ,"xp_cmdshell"
            //,"/add"
            //,"exec master.dbo.xp_cmdshell"
            //,"net localgroup administrators"
                                  };
        #endregion
        //构造正则表达式
        string str_Regex = ".*(";
        for (int i = 0; i < strBadChar.Length - 1; i++)
        {
            str_Regex += strBadChar[i] + "|";
        }
        str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

        if (Regex.Matches(queryConditions, str_Regex, RegexOptions.IgnoreCase).Count > 0)
        {
            return false;
        }
        return true;
    }

    #region 用到pworld的相关方法
    /// <summary>
    /// 根据部门ID，角色获取部门列表
    /// </summary>
    /// <param name="deptId"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public static string GetRoleDepts(string deptId, string roleName)
    {
        StringBuilder dataInfos = new StringBuilder();
        BFCountersignRoleDepartment counterSignHelper = new BFCountersignRoleDepartment();
        DataTable dtDept = counterSignHelper.GetSelectCountersignDepartment(deptId, roleName);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("{0},", rowItem["DepartCode"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
    }

    /// <summary>
    /// 根据部门ID，角色获取部门列表
    /// </summary>
    /// <param name="deptId"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public static string GetRoleUsers(string deptId, string roleName)
    {
        string users = GetRoleUsersWithNoApproval(deptId, roleName);
        return FilterDataField(users);
    }

    #region op

    public static string GetRoleUsersWithNoApproval(string deptId, string roleName)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        StringBuilder dataInfos = new StringBuilder();
        DataTable dtDept = bfurd.GetSelectRoleUser(deptId, roleName);
        if (dtDept != null && dtDept.Rows.Count != 0)
        {
            foreach (DataRow rowItem in dtDept.Rows)
            {
                dataInfos.AppendFormat("K2:Founder\\{0},", rowItem["LoginName"].ToString());
            }
        }
        return dataInfos.ToString().Trim(',');
    }

    public static string FilterDataField(string dataField_old)
    {
        string dataField = dataField_old.Trim(',');
        if (string.IsNullOrEmpty(dataField))
        {
            dataField = "noapprovers";
        }
        return dataField;
    }

    private static string FilterDataField(StringBuilder dataField_old)
    {
        return FilterDataField(dataField_old.ToString().Trim(','));
    }
    
    #endregion

    #endregion


    /// <summary>
    /// 根据域名获取K2写入值
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static string GetK2UserByLoginName(string userName)
    {
        return String.Format("K2:{0}", GetFullLoginName(userName));
    }

    /// <summary>
    /// 根据域名获取完整域名，方便升级
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    private static string GetFullLoginName(string userName)
    {
        return String.Format("founder\\{0}", userName);
    }
}

