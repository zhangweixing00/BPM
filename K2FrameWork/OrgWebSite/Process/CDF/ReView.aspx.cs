using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using OrgWebSite.Process.CDF.WFChart;
using BLL;

namespace OrgWebSite.Process.CDF
{
    public partial class ReView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ProcInstID"]))
                {
                    CustomFlowBLL bll = new CustomFlowBLL();
                    //ViewState["Approval"] = new bll.GetApproveXMLByProcInsID(Request.QueryString["ProcInstID"]);
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["SN"]))
                {
                    string formId = string.Empty;
                    string activityName = string.Empty;
                    string processID = string.Empty;
                    string approveXML = string.Empty;
                    WorkflowHelper.GetProcessInfo(Request.QueryString["SN"], ref formId, ref processID, ref activityName, ref approveXML);
                    ViewState["Approval"] = approveXML;
                }
                CustomizeFlowChart cfc = new CustomizeFlowChart();
                hfjqFlowChart.Value = cfc.FlowChartXmlToJson(ViewState["Approval"].ToString(), true);
            }
        }
    }
}