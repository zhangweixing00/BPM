using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace Sohu.OA.Web.Manage.RoleManage
{
    /// <summary>
    /// create:康亚兵
    /// date：2011-09-07
    /// description：删除菜单处理页面
    /// </summary>
    public class DeleteMenus : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string MenuGuids = context.Request["menuguids"].ToString();
            string ResultFlag = "";
            if (!String.IsNullOrEmpty(MenuGuids))
            {
                //MenuGuids = "'" + MenuGuids.Replace(",", "','") + "'";
                MenuBLL bll = new MenuBLL();
                if (bll.UpdateMenuInfo(MenuGuids))
                {
                    ResultFlag = "True";
                }
                else
                {
                    ResultFlag = "False";
                }
            }
            else
            {
                ResultFlag = "False";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(ResultFlag);
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