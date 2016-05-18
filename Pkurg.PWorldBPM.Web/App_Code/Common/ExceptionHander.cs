using System;
using System.Web;

/// <summary>
///ExceptionHander 的摘要说明
/// </summary>
public class ExceptionHander
{
    public static void GoToErrorPage()
    {
        //yanghechun 2015-01-27 修改错误页面的跳转
        HttpContext.Current.Response.Redirect("~/Error/General.aspx");
    }

    public static void GoToErrorPage(string msg, Exception ex)
    {
        Pkurg.PWorldBPM.Common.Log.Logger.Write(typeof(ExceptionHander), Pkurg.PWorldBPM.Common.Log.EnumLogLevel.Error, ex==null?new Exception(msg):ex);
        HttpContext.Current.Response.Redirect("~/Error/General.aspx?error=" + HttpContext.Current.Server.HtmlEncode(msg));
    }

    public static void GoToErrorPage(string msg)
    {
        //yanghechun 2015-01-27 修改错误页面的跳转
        HttpContext.Current.Response.Redirect("~/Error/General.aspx?error=" + HttpContext.Current.Server.HtmlEncode(msg));
    }
}