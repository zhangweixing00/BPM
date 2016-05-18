<%@ WebHandler Language="C#" Class="RemindHander"%>

using System.Web;

/// <summary>
/// 催办
/// </summary>
public class RemindHander : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "application/json";
        context.Response.ContentType = "text/plain";
        string responseText = "";
        string flowId = context.Request["flowId"];
        if (!string.IsNullOrEmpty(flowId))
        {
           ResponseInfo info= Reminder.Notify(flowId);
           responseText = info.Des;// IsSuccess ? "催办成功" : "催办失败";
        }
        context.Response.Write(responseText);
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
