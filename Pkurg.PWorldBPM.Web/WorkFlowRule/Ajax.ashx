<%@ WebHandler Language="C#" Class="Ajax" %>

using System;
using System.Linq;
using System.Web;
using Pkurg.PWorldBPM.WorkFlowRule;

public class Ajax : IHttpHandler
{
    Focus db = new Focus();
    Permission permission = new Permission();
    IdentityUser identityUser = new IdentityUser();

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

        string action = context.Request.QueryString["action"].ToLower();

        switch (action)
        {
            case "checkoa":
                CheckOA(context);
                break;
            case "checkpermission":
                CheckPermission(context);
                break;
            case "addfocus":
                AddFocus(context);
                break;
            case "checkisfocus":
                CheckIsFocus(context);
                break;
            case "checkisadmin":
                CheckIsAdmin(context);
                break;
            default:
                context.Response.Write("Debug");
                break;
        }

    }

   

    /// <summary>
    /// 判断OA流程的权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public void CheckOA(HttpContext context)
    {
        //default values
        int flag = 0;
        string flowId = "0", code = "", name = "", error = "";
        code = HttpContext.Current.User.Identity.Name;

        //2015-5-18
        IdentityUser identityUser = new IdentityUser();
        Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = identityUser.GetEmployee();
        code = userInfo.FounderLoginId;


        if (!string.IsNullOrEmpty(context.Request.QueryString["flowId"]))
        {
            flowId = context.Request.QueryString["flowId"];
        }

        try
        {
            if (!string.IsNullOrEmpty(code))
            {
                name = DataAccess.GetUserName(code);
            }
        }
        catch (Exception ex)
        {
            error = "【GetUserName exception is】:" + ex.Message;
        }

        //Test Code
        /* 
        flowId = "100";
        code = "founder\\xupc";
        */

        if (!string.IsNullOrEmpty(code))
        {
            try
            {
                flag = CheckOAPermission(code, flowId);
            }
            catch (Exception ex)
            {
                error = error + "【CheckPermission exception is】:" + ex.Message;
            }
        }

        string format = "\"code\":\"{0}\",\"name\":\"{1}\",\"flag\":\"{2}\",\"error\":\"{3}\"";

        string shortCode = code;
        if (code.IndexOf("\\") > -1)
        {
            shortCode = shortCode.Substring(code.IndexOf("\\") + 1);
        }

        context.Response.Write("{" + string.Format(format, shortCode, name.Replace("\\", ""), flag, error) + "}");

    }

    /// <summary>
    /// 判断发起BPM流程的权限
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public void CheckPermission(HttpContext context)
    {
        string flowId = context.Request.QueryString["flowId"];
        Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = identityUser.GetEmployee();
        if (userInfo != null)
        {
            string userCode = userInfo.LoginId;
            string deptCode = userInfo.MainDeptId;
            bool flag = permission.CheckPermission(flowId, userCode, deptCode);
            context.Response.Write(flag ? "1" : "0");
        }
        else
        {
            context.Response.Write("0");
        }

    }

    /// <summary>
    /// 添加收藏
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public void AddFocus(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        int id = Convert.ToInt16(context.Request.QueryString["id"]);
        string createdBy = context.Request.QueryString["createdBy"];
        string createdByName = context.Request.QueryString["createdByName"];

        WR_Focus model = new WR_Focus();
        model.Created_By = createdBy;
        model.Created_By_Name = createdByName;
        model.Created_On = DateTime.Now;
        model.Rule_ID = id;
        model.Focus_ID = Guid.NewGuid();

        db.Insert(model);

        context.Response.Write("1");
    }

    /// <summary>
    /// 取消收藏
    /// </summary>
    /// <param name="type"></param>
    /// <param name="id"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public void RemoveFocus(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        int id = Convert.ToInt16(context.Request.QueryString["id"]);
        string createdBy = context.Request.QueryString["createdBy"];
        string createdByName = context.Request.QueryString["createdByName"];

        db.Delete(type, id, createdBy);
        context.Response.Write("1");
    }

    public void CheckIsFocus(HttpContext context)
    {
        string type = context.Request.QueryString["type"];
        int id = Convert.ToInt16(context.Request.QueryString["id"]);
        string createdBy = context.Request.QueryString["createdBy"];
        bool flag = db.CheckIsFocus(type, id, createdBy);
        context.Response.Write(flag ? "1" : "0");
    }


    /// <summary>
    /// 判断是否显示制度管理链接
    /// </summary>
    /// <param name="context"></param>
    public void CheckIsAdmin(HttpContext context)
    {
        bool flag = false;
        //全局管理员
        Pkurg.PWorldBPM.WorkFlowRule.Setting db = new Pkurg.PWorldBPM.WorkFlowRule.Setting();
        string admin = db.GetValueByName("Rule_Admin");

        Pkurg.PWorldBPM.Common.Info.UserInfo userInfo = identityUser.GetEmployee();
        string userCode = userInfo.LoginId;

        flag = admin.Split(',').Contains(userCode);

        if (!flag)
        {
            //流程分类和管理员
            Pkurg.PWorldBPM.WorkFlowRule.Rule rule = new Pkurg.PWorldBPM.WorkFlowRule.Rule();
            flag = rule.CheckIsCagegoryAdmin(userCode);
        }
        context.Response.Write(flag ? "1" : "0");
    }

    #region CheckOAPermission

    /// <summary>
    /// 判断用户是否对流程ID有权限
    /// </summary>
    /// <param name="code"></param>
    /// <param name="flowId"></param>
    /// <returns></returns>
    int CheckOAPermission(string code, string flowId)
    {
        //flag是否有权限，1：有权限，0：没权限
        int flag = 0;

        /*------------1-判断是否是超级管理员，不受控------------*/
        flag = DataAccess.IsSuperAdmin(code) ? 1 : 0;
        if (flag == 1)
        {
            return flag;
        }

        System.Data.DataSet ds = DataAccess.GetViews();

        /*------------2-判断是否流程ID在所有人都可以发起的流程集合------------*/
        if (flag == 0)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                System.Data.DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Data.DataRow[] rows = dt.Select("AppCode=" + flowId);
                    if (rows.Length > 0)
                    {
                        flag = 1;
                    }
                }
            }
        }

        /*------------3-判断用户和流程的关联关系------------*/
        if (flag == 0)
        {
            if (ds != null && ds.Tables.Count > 1)
            {
                System.Data.DataTable dt = ds.Tables[1];
                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Data.DataRow[] rows = dt.Select("LoginID='" + code.ToLower() + "'");
                    if (rows.Length > 0)
                    {
                        for (int i = 0; i < rows.Length; i++)
                        {
                            string appCodes = rows[i]["Context"].ToString() + ",";
                            int index = appCodes.IndexOf(flowId.ToLower() + ",");
                            if (index > -1)
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }
                }
            }
        }
        return flag;
    }

    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}