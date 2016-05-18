using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using K2.Common;
using System.Xml;
using BLL;

namespace OrgWebSite.Process.CDF
{
    public partial class Submit : ProcessPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcControl = CDF1;
            IsStartPage = true;
            WorkflowID = 3;
            IsOnBoardSubmitPage = true;
            CDF1.CurrentEmpolyeeID = EmployeeID;
            CDF1.IsSign = "true";
        }

        public void SetValue()
        {
            ProcessTypeBLL bll = new ProcessTypeBLL();
            UCProcessAction1.ProcessName = bll.GetProcessNameByID(CDF1.ProcessID);
        }
    }
}