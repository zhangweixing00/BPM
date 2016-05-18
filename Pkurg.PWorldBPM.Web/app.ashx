<%@ WebHandler Language="C#" Class="app" %>

using System;
using System.Web;

public class app : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string callback = context.Request["callback"];
        string json = "";

        //action:数据请求的类型（分支）
        string action = context.Request["action"];
        //当前APP登录的用户代码(加密)
        string usercode = context.Request["userencode"];
        //列表分页，第1页从0开始计算
        int pageIndex = Convert.ToInt16(context.Request["pageindex"]);
        //列表分页，分页的页面的大小
        int pagesize = Convert.ToInt16(context.Request["pagesize"]);
        //流程实例ID
        string instanceId = context.Request["id"];
        //SN
        string sn = context.Request["sn"];
        //审批(加签)意见
        string remark = context.Request["remark"];
        switch (action)
        {
            case "login"://登录                
                string account = context.Request["account"];
                string password = context.Request["password"];
                json = Handle.Login(account, password);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "gettodolist"://获取待办（分页）                               
                json = Handle.GetToDoList(usercode, pageIndex);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "getdonelist"://获取已办（分页）                               
                json = Handle.GetDoneList(usercode, pageIndex);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "getarchivelist"://获取归档（分页）                
                json = Handle.GetArchiveList(usercode, pageIndex);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "getworkflowinfo"://获取审批表单详情                
                json = Handle.GetWorkFlowInfo(instanceId);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "approve"://审批动作                                
                //同意：Y，不同意：N
                string result = context.Request["result"];                
                json = Handle.Approve(usercode, sn, instanceId, result, remark);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "addsign"://加签操作                
                //被加签人
                string tousercode = context.Request["tousercode"];
                json = Handle.AddSign(usercode, tousercode, sn, instanceId, remark);
                context.Response.Write(callback + "(" + json + ")");
                break;
            case "getuserlist"://获取用户列表                
                //查询关键字
                string keyword = context.Request["keyword"];
                json = Handle.GetUserList(keyword, pageIndex, pagesize);
                context.Response.Write(callback + "(" + json + ")");
                break;
            default:
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}