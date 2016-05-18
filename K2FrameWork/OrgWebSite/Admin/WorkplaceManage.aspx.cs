using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace OrgWebSite.Admin
{
    public partial class WorkplaceManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void Bind()
        {
            WorkPlaceBLL bll = new WorkPlaceBLL();
            gvWorkPlace.DataSource = bll.GetWorkPlace();
            gvWorkPlace.DataBind();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {

        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void gvWorkPlace_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}