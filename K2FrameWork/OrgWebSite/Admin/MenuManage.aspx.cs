using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using K2Utility;
using BLL;
using System.Data;
using Utility;

namespace Sohu.OA.Web.Manage.RoleManage
{
    /// <summary>
    /// create：康亚兵
    /// date：2011-09-07
    /// description：菜单管理 （创建菜单、更新菜单、删除菜单、查询菜单）
    /// </summary>
    public partial class MenuManage : K2Utility.BasePage
    {
        protected DataSet MenuInfo
        {
            get
            {
                if (ViewState["MenuInfo"] == null)
                {
                    ViewState["MenuInfo"] = new DataSet();
                }
                return ViewState["MenuInfo"] as DataSet;
            }
            set { ViewState["MenuInfo"] = value; }
        }
        public int totalRecordCount = 0;  //总共多少条记录
        int intPageIndex = 0; //当前页索引
        int intPageNum = 20; //一页多少条数据
        int intPageSelectNum = 10;//从数据库查询多少条数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                StringBuilder sb = new StringBuilder("");
                LoadParentMenuGuid("" ,sb);
            }

        }
        /// <summary>
        /// 加载上级菜单选项
        /// </summary>
        private void LoadParentMenuGuid(string ParentMenuGuid ,StringBuilder sb)
        {
            if (MenuInfo != null && MenuInfo.Tables.Count > 0 && MenuInfo.Tables[0].Rows.Count > 0)
            {
                DataView dv = MenuInfo.Tables[0].DefaultView;
                dv.RowFilter = "ParentMenuGuid='" + ParentMenuGuid+"'"; 
                
                foreach (DataRow dr in dv.ToTable().Rows)
                {

                    ListItem menuitem = new ListItem();
                    menuitem.Text = sb.ToString() + dr["MenuName"].ToString();
                    menuitem.Value = dr["MenuGuid"].ToString();
                    this.ddlParentMenu.Items.Add(menuitem);
                    sb.Append("--");
                    LoadParentMenuGuid(dr["MenuGuid"].ToString(), sb);
                    sb.Remove(0, 2);
                }
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            MenuBLL bll = new MenuBLL();
            MenuInfo = bll.GetMenuInfo(this.txtMenuName.Text.Trim(), this.ddlParentMenu.SelectedValue.Trim(), this.ddlMenutype.SelectedValue.Trim());

            totalRecordCount = MenuInfo.Tables[0].Rows.Count;

            //this.GridView1.DataSource = ds.Tables[0];
            //this.GridView1.DataBind();
            Common.BindBoundControl(MenuInfo.Tables[0], this.GridView1, intPageIndex, intPageNum, totalRecordCount);
            
        }
        
        protected void btnSeach_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
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
        /// <summary>
        /// 翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelecctInfoByPageIndex(e.NewPageIndex);
        }
        /// <summary>
        /// 翻页方法
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
        /// <summary>
        /// 绑定总数据条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label RowsNum = (Label)e.Row.FindControl("lblRowAllCount");
                RowsNum.Text = "共" + totalRecordCount + "条";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string MenuName = ((Label)e.Row.FindControl("MenuName")).Text.ToString().Trim();
                e.Row.Cells[1].ToolTip = MenuName;
                string subMenuName = Common.GetSubString(MenuName, 20, true);
                ((Label)e.Row.FindControl("MenuName")).Text = subMenuName;

                string menutype = ((Label)e.Row.FindControl("MenuType")).Text.ToString().Trim();
                e.Row.Cells[2].ToolTip = menutype;

                string Menu_Url = ((Label)e.Row.FindControl("Menu_Url")).Text.ToString().Trim();
                e.Row.Cells[4].ToolTip = Menu_Url;
                string subMenu_Url = Common.GetSubString(Menu_Url, 40, true);
                ((Label)e.Row.FindControl("Menu_Url")).Text = subMenu_Url;
            }
        }
       
    }
}