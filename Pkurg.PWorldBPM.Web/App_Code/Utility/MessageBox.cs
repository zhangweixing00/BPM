/*===============================================================================
 *创建者：
 *创建时间：
 *最后修改时间：2014-04-24
 *最后修改人：阴丽宁
 *功能描述：注册客户端JS方法。
 *
 ***********************修改履历*******************************
 *  1.
 *  修改时间：2014-04-24
 *  修改人：阴丽宁
 *  修改内容：
 *      (1).添加重载方法Show(Page page, Type pageType, string message)
 *      (2).添加重载方法Show(Page page, Type pageType, string message, string redirectUrl)
 
 **************************************************************
 *
 *版本：v0.2
 *==============================================================================*/

using System;
using System.Web.UI;

public class MessageBox
{
    /// <summary>
    /// 显示消息(Ajax)
    /// </summary>
    /// <param name="Control">注册该客户端脚本的控件</param>
    /// <param name="type">客户端脚本块的类型</param>
    /// <param name="message">消息</param>
    public static void Show(UpdatePanel control, Type type, string message)
    {
        ScriptManager.RegisterClientScriptBlock(control, type,
            Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12),
            string.Format(Resources.Message.ScriptAlter, message), true);
    }

    /// <summary>
    /// 显示消息(Ajax)
    /// </summary>
    /// <param name="Control">UpdatePannel控件</param>
    /// <param name="type">客户端脚本块的类型</param>
    /// <param name="message">消息</param>
    /// <param name="url">确认后的跳转地址</param>
    public static void Show(UpdatePanel control, Type type, string message, string redirectUrl)
    {
        ScriptManager.RegisterClientScriptBlock(control, type,
            Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12),
            string.Format(Resources.Message.ScriptAlter + Resources.Message.ScriptHerfRedirect, message, redirectUrl), true);
    }

    /// <summary>
    /// 弹出提示信息
    /// </summary>
    /// <param name="page">WebPage对象</param>
    /// <param name="message">提示消息的内容</param>
    public static void Show(Page page, Type pageType, string message)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(),
            Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12),
            string.Format(Resources.Message.ScriptAlter, message), true);
    }

    /// <summary>
    /// 弹出提示信息
    /// </summary>
    /// <param name="page">WebPage对象</param>
    /// <param name="message">提示消息的内容</param>
    public static void Show(Page page, Type pageType, string message, string redirectUrl)
    {
        //page.ClientScript.RegisterStartupScript(page.GetType(),
        //    Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12),
        //    string.Format(Resources.Message.JsAlter + Resources.Message.JsHerfRedirect, message, redirectUrl), true);

        page.ClientScript.RegisterStartupScript(page.GetType(),
            Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12),
            string.Format(Resources.Message.ScriptAlter, message) + string.Format(Resources.Message.ScriptHerfRedirect, redirectUrl), true);
    }
}
