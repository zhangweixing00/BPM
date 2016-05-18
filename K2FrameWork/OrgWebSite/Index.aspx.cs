/**
 * 
 * 王红福
 * 
 * 2011-7-11
 * 
 * 首页菜单
 * 
 * **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

using System.Text;
using BLL;
using System.Data;
namespace Sohu.OA.Web
{
    public partial class Index : K2Utility.BasePage
    {
        public string LeftMenuString = string.Empty;
        public string menuIdListString = string.Empty;
        public string firstId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            LeftMenu();
        }

        #region 注销


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            string logOutUrl = string.Format("{0}logout?service=http://{1}/Login.aspx", ConfigurationManager.AppSettings["CASUrl"], ConfigurationManager.AppSettings["OutUrl"]);
            Response.Redirect(logOutUrl);
        }
        #endregion
        #region 加载左侧菜单

        /// <summary>
        /// 左侧 主菜单
        /// </summary>
        public void LeftMenu()
        {
            MenuBLL bll = new MenuBLL();
            DataSet ds = bll.GetMenuPermision(CurrentUser);

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "ParentMenuGuid = '' and MenuType='RIGHT' ";
            DataTable dt = dv.ToTable();

            StringBuilder sb = new StringBuilder();
            StringBuilder menuIdList = new StringBuilder();


            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<div class=\" ruzhiguanli\">  <div class=\"title\"> <p>");
                sb.Append(dr["MenuName"].ToString());
                sb.Append("</p></div><ul>");
                DataView dvSub = ds.Tables[0].DefaultView;
                string filter = "ParentMenuGuid='" + dr["MenuGuid"].ToString() + "' and MenuType='RIGHT' ";
                dvSub.RowFilter = filter;
                DataTable dtSub = dvSub.ToTable();
                LeftMenuSecond(dr["MenuGuid"].ToString(), dtSub, sb);
                sb.Append("</ul></div>");
                


            }
            LeftMenuString = sb.ToString();
        }

        /// <summary>
        /// 左侧 二级菜单
        /// </summary>
        /// <param name="menuId">主菜单的ID</param>
        /// <param name="listSecond">list</param>
        /// <returns></returns>
        void LeftMenuSecond(string menuId, DataTable dt, StringBuilder sb)
        {           
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("  <li><img src=\"pic/right_an2.jpg\"/> <a href=\"");
                sb.Append(dr["MenuURL"].ToString());
                sb.Append("\"><span>");
                sb.Append(dr["MenuName"].ToString());
                sb.Append("</span></a></li>");
            }
        }
        #endregion
    }

}