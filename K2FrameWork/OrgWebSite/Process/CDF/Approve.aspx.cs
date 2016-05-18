using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;

namespace OrgWebSite.Process.CDF
{
    public partial class Approve : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = this.CDF1;
            IsApprove = true;
            WorkflowID = 3;
            CDF1.CurrentEmpolyeeID = EmployeeID;
        }
    }
}