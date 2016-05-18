using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utility;
using BLL;
using Model;


namespace Sohu.OA.Web
{
    /// <summary>
    /// create:康亚兵
    /// date：2011-08-31
    /// description：角色管理
    /// </summary>
    public partial class MenuRoleManage : System.Web.UI.Page
    {
        public int totalRecordCount = 0;  //总共多少条记录
        int intPageIndex = 0; //当前页索引
        int intPageNum = 20; //一页多少条数据
        int intPageSelectNum = 10;//从数据库查询多少条数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String pageIndex = Request.Params["PageIndex"];
                if (!String.IsNullOrEmpty(pageIndex))
                {
                    try
                    {
                        intPageIndex = Int32.Parse(pageIndex);
                    }
                    catch { }
                }

                //绑定角色数据
                BindData();
                //页码赋值
                if (totalRecordCount > 0)
                {
                    //页码赋值
                    TextBox tb = (TextBox)GridView1.BottomPagerRow.FindControl("txt_PageIndex");
                    tb.Text = (intPageIndex + 1).ToString();
                }
            }
        }
        /// <summary>
        /// 绑定角色信息
        /// </summary>
        private void BindData()
        {
            RoleBLL bll = new RoleBLL();
            IList<RoleInfo> roleList = bll.GetRoles(string.Empty);
            totalRecordCount = roleList.Count;
            Common.BindBoundControl(roleList, this.GridView1, intPageIndex, intPageNum, totalRecordCount);
        }
        /// <summary>
        /// 角色信息跳转页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "go")
            {
                TextBox tbPage = (TextBox)GridView1.BottomPagerRow.FindControl("txt_PageIndex");
                intPageIndex = Convert.ToInt32(tbPage.Text.ToString().Trim()) - 1;
                SelecctInfoByPageIndex(intPageIndex);
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeach_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }

        //private void LoadRoleType()
        //{  
        //    Biz.Biz_StringMap stringMap = new Biz.Biz_StringMap();
        //    IList<ListItem> lstitem = stringMap.DropDownList(enumDictCache.RoleType.ToString(), "", true);
        //    foreach (GridViewRow gvr in GridView1.Rows)
        //    {
        //        Label lbroleType = (Label)gvr.FindControl("RoleType");
        //        Label lbroleTypeDes = (Label)gvr.FindControl("RoleTypeDes");             
        //        ListItem li = lstitem.Where(c => c.Value == lbroleType.Text.ToString()).FirstOrDefault();
        //        if (li != null)
        //        {
        //            lbroleTypeDes.Text = li.Text;
        //        }
        //    }
        //}
        /// <summary>
        /// 角色信息页码change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelecctInfoByPageIndex(e.NewPageIndex);
        }
        /// <summary>
        /// 角色信息翻页执行方法
        /// </summary>
        /// <param name="PageIndex"></param>
        protected void SelecctInfoByPageIndex(int PageIndex)
        {
            intPageIndex = PageIndex;
            BindData();
            TextBox tbPage = (TextBox)GridView1.BottomPagerRow.FindControl("txt_PageIndex");
            tbPage.Text = (PageIndex + 1).ToString();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label RowsNum = (Label)e.Row.FindControl("lblRowAllCount");
                RowsNum.Text = "共" + totalRecordCount + "条";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lbSystemRole = (Label)e.Row.FindControl("SystemRole");
                //if (lbSystemRole.Text == "是")
                //{
                //    LinkButton lbRoleUser = (LinkButton)e.Row.FindControl("lbSelect");
                //    lbRoleUser.Enabled = false;
                //}

                string rolecode = ((Label)e.Row.FindControl("RoleCode")).Text.ToString().Trim();
                e.Row.Cells[2].ToolTip = rolecode;
                string subrolecode = Common.GetSubString(rolecode, 12, true);
                ((Label)e.Row.FindControl("RoleCode")).Text = subrolecode;

                string rolename = ((Label)e.Row.FindControl("RoleName")).Text.ToString().Trim();
                e.Row.Cells[3].ToolTip = rolename;
                string subrolename = Common.GetSubString(rolename, 26, true);
                ((Label)e.Row.FindControl("RoleName")).Text = subrolename;

                string roletype = ((Label)e.Row.FindControl("RoleTypeDes")).Text.ToString().Trim();
                e.Row.Cells[4].ToolTip = roletype;
                string subroletype = Common.GetSubString(roletype, 20, true);
                ((Label)e.Row.FindControl("RoleTypeDes")).Text = subroletype;

                //string systemrole = ((Label)e.Row.FindControl("SystemRole")).Text.ToString().Trim();
                //e.Row.Cells[6].ToolTip = systemrole;
            }
        }
    }
}