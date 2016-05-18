using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using BLL;

namespace OrgWebSite.Admin.Popup
{
    public partial class SelectRequestNode : BasePage
    {
        public string ProcessID
        {
            get
            {
                return Request.QueryString["ProcessID"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNode();
            }
        }

        /// <summary>
        /// 绑定节点
        /// </summary>
        private void BindNode()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            gvApproveNode.DataSource = bll.GetRequestNodeByProcessID(ProcessID);
            gvApproveNode.DataBind();
        }
    }
}