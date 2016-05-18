using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace K2Utility
{
    public class MessageBox : System.Web.UI.Page
    {
        /// <summary>
        /// message box
        /// </summary>
        /// <param name="page">page which uses this method(this or this.Page)</param>
        /// <param name="msg">message to show</param>
        public static void Show(Page page, string msg)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "'});", true);
        }

        /// <summary>
        /// message box
        /// </summary>
        /// <param name="page">page which uses this method(this or this.Page)</param>
        /// <param name="msg">message to show</param>
        /// <param name="url">redirect url</param>
        public static void Show(Page page, string msg, string url)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');window.location.href='" + url + "';", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function(){window.location.href='" + url + "';}});", true);
        }

        /// <summary>
        /// message box,show alert and close,(IE6/7/8)
        /// </summary>
        /// <param name="page">page which uses this method(this or this.Page)</param>
        /// <param name="msg">message to show</param>
        public static void ShowAndClose(Page page, string msg)
        {
            //返回上一页。
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');\nif(window.opener!=null)\nwindow.opener.location.href=window.opener.location.href;window.top.opener = null;window.open(' ', '_self', ' ');window.close();", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function (){window.history.back();}})", true);
        }

        public static void ShowAndReload(Page page, string msg)
        {
            //提示成功后重新加载本页面
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');window.history.back();", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function(){window.location.reload();}});", true);
        }



        /// <summary>
        /// message box,show alert and goto homepage,(IE6/7/8)
        /// </summary>
        /// <param name="page">page which uses this method(this or this.Page)</param>
        /// <param name="msg">message to show</param>
        /// <param name="url">redirect url</param>
        public static void ShowAndPop(Page page, string msg, string url)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');window.location.href='" + url + "';", true);//modifyed by pccai on 2011-2-25
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function (){window.location.href='" + url + "';}});", true);
        }

        /// <summary>
        /// 不弹出框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url"></param>
        public static void ReturnPage(Page page, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "window.location.href='" + url + "'", true);
        }

        /// <summary>
        /// message box
        /// </summary>
        /// <param name="page">page which uses this method(this or this.Page)</param>
        /// <param name="msg">message to show</param>
        /// <param name="url">refresh parent page</param>
        public static void ShowAndClose(Page page, string msg, string url)
        {
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "alert('" + FormatMessage(@msg) + "');"
            //+ "\nif(window.opener!=null)\nwindow.opener.location.href='" + url + "';"
            //+ "\nwindow.close();", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function (){if(window.opener!=null)window.opener.location.href='" + url + "';window.close();}});", true);
        }

        private static string FormatMessage(string msg)
        {
            return msg.Replace("'", "\\'");
        }


        public static void OpenWin(Page page, string uri, string msg)
        {
            // page.ClientScript.RegisterStartupScript(page.GetType(), "openwin", "alert('" + FormatMessage(@msg) + "');\nif(window.opener!=null)\nwindow.opener.location.href=window.opener.location.href;window.top.opener = null;window.open(' ', '_self', ' ');window.close();window.open('" + uri + "');", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "ss", "top.window.ymPrompt.alert({title:'提示信息',message:'" + msg + "',handler:function (){if(window.opener!=null)window.opener.location.href=window.opener.location.href;window.top.opener = null;window.open(' ', '_self', ' ');window.close();window.open('" + uri + "');}});", true);
        }
    }
}
