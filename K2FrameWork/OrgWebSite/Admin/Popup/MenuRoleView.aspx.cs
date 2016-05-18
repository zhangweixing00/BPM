using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Model;

namespace Sohu.OA.Web.Manage.RoleManage
{
    /// <summary>
    /// create:康亚兵
    /// date：2011-09-08
    /// description：查看菜单下的角色信息
    /// </summary>
    public partial class MenuRoleView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoleMessage();
                LoadMenuMessage();
            }
        }

       
        /// <summary>
        /// 加载已添加的角色信息
        /// </summary>
        private void LoadRoleMessage()
        {
            string menuguid = Request.QueryString["menuguid"].ToString();

            if (!String.IsNullOrEmpty(menuguid))
            {
                RoleBLL bll = new RoleBLL();
                DataSet ds = bll.GetRoleByMenuID(menuguid);
                if (ds != null && ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
        }
        /// <summary>
        /// 加载菜单信息
        /// </summary>
        private void LoadMenuMessage()
        {
            MenuBLL bll = new MenuBLL();
            DataSet MenuInfo = bll.GetMenuInfo("", "", "");
            DataView dv = MenuInfo.Tables[0].DefaultView;
            dv.RowFilter = "MenuGuid='" + Request.QueryString["menuguid"].ToString() + "'";

            if (dv.ToTable() != null&&dv.ToTable().Rows.Count>0)
            {
                txtMenuName.Text = dv[0]["MenuName"].ToString();
                txtMenuType.Text = dv[0]["MenuType"].ToString();
            }
        }
    }
}