using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BLL;

namespace Sohu.OA.Web.JavaScript
{
    /// <summary>
    /// DelRoles 的摘要说明
    /// </summary>
    public class DelRoles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DelRoles = context.Request["roleids"].ToString();
            string ResultText = "";
            RoleBLL bll = new RoleBLL();
            if ( bll.DeleteRoles(DelRoles))
            {
                ResultText = "true";
            }
            else
            {
                ResultText = "false";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(ResultText);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}