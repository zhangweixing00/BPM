using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using BLL;

namespace OrgWebSite.Process.Controls
{
    public partial class ProcessLog : WorkflowControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProcessLog();
            }

        }


        #region load process log
        private void LoadProcessLog()
        {
            ProcessLogBLL bll = new ProcessLogBLL();
            this.GridView1.DataSource = bll.GetProcessLogList(TaskPage.FormID);
            this.GridView1.DataBind();
        }
        #endregion
    }
}