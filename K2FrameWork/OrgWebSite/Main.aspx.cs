using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using Utility;
using System.Data;
using System.Text;
using System.Configuration;
using BLL;

namespace OrgWebSite
{
    public partial class Main : K2Utility.BasePage
    {
        public string LeftMenuString = string.Empty;   
        public string menuIdListString = string.Empty;
        public string firstId = string.Empty;
        private string myDraft = ConfigurationManager.AppSettings["MyDraft"].ToString();    //我的草稿GUID
        private string myJoined = ConfigurationManager.AppSettings["MyJoined"].ToString();  //我的参与GUID
        private string myStarted = ConfigurationManager.AppSettings["MyStarted"].ToString();//我的发起GUID
        private string myWorklist = ConfigurationManager.AppSettings["MyWorklist"].ToString();  //我的任务GUID
        private string myDelegation = ConfigurationManager.AppSettings["MyDelegation"].ToString();  //我的代理GUID

       
       
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Label1.Text = base.CHName;
           
            LeftMenu();

         
        }

        #region 注销
        
       
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
           // DotNetCasClient.CasAuthentication.SingleSignOut();
            SignOut();

            
            string logOutUrl = string.Format("{0}logout?service={1}", ConfigurationManager.AppSettings["CASUrl"],"http://" + Request.Url.Authority + ConfigurationManager.AppSettings["OutUrl"]);
            Response.Redirect(logOutUrl);
            //FormsAuthentication.RedirectToLoginPage();
        }


        #endregion
        #region 加载左侧菜单
        
       /// <summary>
       /// 左侧 主菜单
       /// </summary>
        public  void LeftMenu()
        {
            MenuBLL bll = new MenuBLL();
            DataSet ds = bll.GetMenuPermision(CurrentUser);

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "ParentMenuGuid = '' and MenuType='left' ";
            DataTable dt=dv.ToTable();

            StringBuilder sb = new StringBuilder();
            StringBuilder menuIdList = new StringBuilder();

            bool isFirst = true; ;
            foreach (DataRow dr in dt.Rows)
            {

                sb.Append("<h2  onclick=\" TraggleShow('");
                sb.Append(dr["MenuGuid"].ToString());
                sb.Append("')\">");
                if (isFirst)
                {
                    sb.Append("<img src=\"../pic/comput.png\" />");
                    sb.Append(dr["MenuName"].ToString());
                    sb.Append("</h2>");
                }
                else
                {
                    sb.Append("<img src=\"../pic/set.png\" />");
                    sb.Append(dr["MenuName"].ToString());
                    sb.Append("</h2>");
                }
                sb.Append(" <div id=\"");
                sb.Append(dr["MenuGuid"].ToString());
                sb.Append("\"  class=\"left_menu_list\">");

                menuIdList.Append("#");
                menuIdList.Append(dr["MenuGuid"].ToString());
                menuIdList.Append(",");

                //二级菜单 
                DataView dvSub = ds.Tables[0].DefaultView;
                string filter = "ParentMenuGuid='" + dr["MenuGuid"].ToString() + "' and MenuType='left' ";
                dvSub.RowFilter = filter;
                DataTable dtSub = dvSub.ToTable();
                LeftMenuSecond(dr["MenuGuid"].ToString(), dtSub, sb);

                //菜单结尾
                sb.Append(" </div>  ");
                isFirst = false;
            }
            LeftMenuString = sb.ToString();
            menuIdListString = menuIdList.ToString();
            if (menuIdListString.Length > 0)
            {
                menuIdListString = menuIdListString.Substring(0, menuIdListString.Length - 1);
                firstId = menuIdListString.Split(',')[0];
            }
            
        }

        /// <summary>
        /// 左侧 二级菜单
        /// </summary>
        /// <param name="menuId">主菜单的ID</param>
        /// <param name="listSecond">list</param>
        /// <returns></returns>
        void LeftMenuSecond(string menuId, DataTable dt, StringBuilder sb)
        {

            bool isFirst = true;

            string taskCount = "-1";
            string taskId = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                //if (item.MenuGuid == myDraft)
                //{
                //    取得Draft数量
                //    ds = K2.BDAdmin.DBManager.GetDraftByUser(EmployeeCode, "1", "1", "", "", "", "");
                //    if (ds != null && ds.Tables.Count >= 1)
                //    {
                //        taskCount = ds.Tables[1].Rows[0]["TotalNum"].ToString();
                //        taskId = "mydraft";
                //    }
                //    else
                //    {
                //        taskCount = "0";
                //        taskId = "";
                //    }
                //}
                //else if (item.MenuGuid == myJoined)
                //{
                //    取得参与数量
                //    ds = K2.BDAdmin.DBManager.GetMyDoc(CurrentUserAdaccoutWithK2Lable, "1", "1", "", "", "", "", "", "", "");
                //    if (ds != null && ds.Tables.Count >= 3)
                //    {
                //        taskCount = ds.Tables[2].Rows[0]["TotalNum"].ToString();
                //        taskId = "myjoined";
                //    }
                //    else
                //    {
                //        taskCount = "0";
                //        taskId = "";
                //    }
                //}
                //else if (item.MenuGuid == myStarted)
                //{
                //    取得发起数量
                //    ds = K2.BDAdmin.DBManager.GetMyStartedProcess(CurrentUserAdaccoutWithK2Lable, "1", "1", "", "", "", "", "");
                //    if (ds != null && ds.Tables.Count >= 3)
                //    {
                //        taskCount = ds.Tables[2].Rows[0]["TotalNum"].ToString();
                //        taskId = "mystarted";
                //    }
                //    else
                //    {
                //        taskCount = "0";
                //        taskId = "";
                //    }
                //}
                //if (string.Compare(dr["MenuGuid"].ToString(),myWorklist,true))
                //{
                //    //取得任务数量
                //    ds = DBManager.GetMyWorklist(CurrentUserAdaccoutWithK2Lable, "1", "12", "", "", "''", "", "", "", "", "");
                //    if (ds != null && ds.Tables.Count >= 3)
                //    {
                //        taskCount = ds.Tables[2].Rows[0]["TotalNum"].ToString();
                //        taskId = "myworklist";
                //    }
                //    else
                //    {
                //        taskCount = "0";
                //        taskId = "";
                //    }
                //}
                //else if (item.MenuGuid == myDelegation)
                //{
                //    Biz_MyDelegation deletaion = new Biz_MyDelegation();
                //    List<T_MyDeligation> dtDelegation = deletaion.GetMydelegation(GetAdAccoutByEmail(Page.User.Identity.Name));
                //    if (dtDelegation != null)
                //    {
                //        taskCount = dtDelegation.Count.ToString();
                //        taskId = "mydelegation";
                //    }
                //    else
                //    {
                //        taskCount = "0";
                //        taskId = "";
                //    }
                //}
                //else
                //{
                //    taskCount = "-1";
                //}

                if (isFirst)
                {
                    if (taskCount == "-1")
                    {
                        sb.Append(string.Format(" <ul > <li class=\"left_img\"><img src=\"pic/tree_book.png\" /></li> <li style='width:120px;'><a  href=\"javascript:TaskRedirect('frameContent','{0}');\">{1}</a></li></ul>", dr["MenuURL"].ToString(), dr["MenuName"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format(" <ul > <li class=\"left_img\"><img src=\"pic/tree_book.png\" /></li> <li style='width:120px;'><a id='{3}' href=\"javascript:TaskRedirect('frameContent','{0}');\">{1}<strong>({2})</strong></a></li></ul>", dr["MenuURL"].ToString(), dr["MenuName"].ToString(), taskCount, taskId));
                    }
                }
                else
                {
                    if (taskCount == "-1")
                    {
                        sb.Append(string.Format(" <ul> <li class=\"left_img\"><img src=\"pic/tree_book.png\" /></li> <li style='width:120px;'><a  href=\"javascript:TaskRedirect('frameContent','{0}');\"> {1}</a></li></ul>", dr["MenuURL"].ToString(), dr["MenuName"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format(" <ul > <li class=\"left_img\"><img src=\"pic/tree_book.png\" /></li> <li style='width:120px;'><a id='{3}' href=\"javascript:TaskRedirect('frameContent','{0}');\">{1}<strong>({2})</strong></a></li></ul>", dr["MenuURL"].ToString(), dr["MenuName"].ToString(), taskCount, taskId));
                    }
                }
                isFirst = false;
            }


            //sb.Append(string.Format(" <ul > <li class=\"left_img\"><img src=\"pic/tree_book.png\" /></li> <li style='width:120px;'><a href=\"javascript:TaskRedirect('frameContent','{0}');\"><strong>({1})</strong></a></li></ul>", "Manage/TelNOManage/TelAreaManage.aspx", "分机区域管理"));
        }
        #endregion
    }
}