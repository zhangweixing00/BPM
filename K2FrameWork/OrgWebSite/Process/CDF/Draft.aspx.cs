using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;

namespace OrgWebSite.Process.CDF
{
    public partial class Draft : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = CDF1;
            IsDraftPage = true;
            WorkflowID = 3;
            IsOnBoardSubmitPage = true;
            CDF1.CurrentEmpolyeeID = EmployeeID;
        }
    }
}