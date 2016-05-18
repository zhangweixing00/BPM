using System;
using System.Web;
using System.Web.UI;

/// <summary>
/// JavaScript帮助类
/// </summary>
public partial class JsHelper
{
    /// <summary>
    /// 截取字符串
    /// 去掉域账号前缀
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="split">分隔符</param>
    /// <returns></returns>
    public static string TrimString(string str, string split = "\\")
    {
        if (str.Contains(split))
        {
            int index = str.LastIndexOf(split);
            return str.Substring(index + 1);
        }
        return str;
    }

    /// <summary>
    /// 执行JS
    /// </summary>
    /// <param name="page"></param>
    /// <param name="js"></param>
    public static void RunJs(Page page, string js)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 弹出小窗口(Ajax)
    /// </summary>
    /// <param name="page">当前页</param>
    /// <param name="message">窗口信息</param>
    public static void Alert(Page page, object message)
    {
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message);
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }
    /// <summary>
    /// 显示消息并导向到目标页面(Ajax)
    /// </summary>
    /// <param name="page">Page</param>
    /// <param name="str_Message">窗口信息</param>
    /// <param name="url">跳转的URL</param>
    public static void AlertAndRedirect(Page page, string message, string url)
    {
        string js = "alert('" + message + "');window.location.href='" + url + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public static void AlertOperationSuccess(Page page)
    {
        string message = "操作成功";
        string js = string.Format(@"alert('{0}');", message);
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public static void AlertOperationSuccessAndRedirect(Page page, string url)
    {
        string message = "操作成功";
        string js = "alert('" + message + "');window.location.href='" + url + "';" ;
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public static void AlertOperationFailure(Page page, Exception ex)
    {
        string message = "系统出错，请稍后再试！";
        string js = string.Format(@"alert('{0}');", message);
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public static void AlertOperationFailureAndRedirect(Page page, Exception ex, string url)
    {
        string message = "系统出错，请稍后再试！";
        string js = "alert('" + message + "');window.location.href='" + url + "';" ;
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 弹出定制页面大小的窗口
    /// </summary>
    /// <param name="url">Url地址</param>
    /// <param name="width">宽度</param>
    /// <param name="heigth">高度</param>
    /// <param name="top">顶部</param>
    /// <param name="left">左边</param>
    /// <returns></returns>
    public static void OpenWebFormSizeUrl(Page page, string url, int width, int heigth, int top, int left)
    {
        string js = @"window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no')";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public static string GetClientIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        return result;
    }
}

