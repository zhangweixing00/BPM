using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;

namespace OrgWebSite.Process.CDF
{
    public partial class ReSubmit : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = CDF1;
            IsReWorkPage = true;
            WorkflowID = 3;
            CDF1.CurrentEmpolyeeID = EmployeeID;
            if (IsPostBack)
            {
                if (Request.Params[1] == "UCProcessAction1_btnCancel")
                {
                    (this.FindControl("UCProcessAction1") as OrgWebSite.Process.Controls.UCProcessAction).btnCancel_Click(null, null);
                }
            }
        }
    }
}