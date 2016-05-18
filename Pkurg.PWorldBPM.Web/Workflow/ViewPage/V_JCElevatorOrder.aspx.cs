using System;
using System.Text.RegularExpressions;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_ViewPage_V_JCElevatorOrder : System.Web.UI.Page
{
    public JC_ElevatorOrder jc = new JC_ElevatorOrder();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_JCElevatorOrder";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                BindFormData();
            }
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            if (Instance == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            JC_ElevatorOrderInfo item = jc.GetElevatorOrder(Instance.FormId.ToString());
            if (item == null)
            {
                ExceptionHander.GoToErrorPage("记录不存在");
            }
            if (item != null)
            {
                cblSecurityLevel.SelectedValue = item.SecurityLevel.ToString();
                cblUrgentLevel.SelectedValue = item.UrgenLevel != null ? item.UrgenLevel.ToString() : "0";
                tbData.Text = ((DateTime)item.Date).ToString("yyyy-MM-dd");
                tbPerson.Text = item.UserName;
                tbDepartName.Text = item.DeptName;
                tbPhone.Text = item.Mobile;
                tbTitle.Text = item.ReportTitle;
                tbOrderType.Text = item.OrderType.ToString();
                tbOrderID.Text = item.OrderID.ToString();
                tbContent.Text = item.Url;
                tbNumber.Text = item.ReportCode;
                txtMaxCost.Text = item.MaxCost.HasValue ? FormatMoney(item.MaxCost.Value.ToString()) : "";
                tbNote.Text = item.Note;
                //add 2014-12-23
                Countersign1.CounterSignDeptId = item.StartDeptCode;
            }
            Countersign1.ProcId = ViewState["InstanceID"].ToString();

            #region 审批意见框

            DeptManagerApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            RealateDeptApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            CityCompanyLeaderApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            JCFirstApprovalApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            DesignerApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            ProjectOperatorApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            JCReApprovalApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            PurchaserApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            COOApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            JCMakeOrderApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            JCFinalApprovalApproveOpinion.InstanceId = ViewState["InstanceID"].ToString();
            #endregion

        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    public string GetJCUrl()
    {
        return tbContent.Text.ToString();
    }

    /// <summary>
    /// 格式化:给数字字符串加上千位逗号
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns>正则匹配,支持小数</returns>
    string FormatMoney(string num)
    {
        string str = "";

        if (!string.IsNullOrEmpty(num) && Convert.ToDecimal(num) < 0)
        {
            str = "-";
        }

        string newStr = string.Empty;
        Regex reg = new Regex(@"(\d+?)(\d{3})*(\.\d+|$)");
        Match m = reg.Match(num);
        newStr += m.Groups[1].Value;
        for (int i = 0; i < m.Groups[2].Captures.Count; i++)
        {
            newStr += "," + m.Groups[2].Captures[i].Value;
        }
        newStr += m.Groups[3].Value;
        return str + newStr;
    }
}