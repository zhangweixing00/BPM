using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Utility
{
    public class BasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            //string formPath = Page.Request.ServerVariables["PATH_INFO"];

            //if (!DBManager.ContainFormPermission(Page.User.Identity.Name, formPath, "Admin"))
            //{
            //    Response.Write("NO Permission");
            //    Response.End();
            //}

            base.OnInit(e);
        }
    }
}
