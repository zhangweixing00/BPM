using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;

public partial class Workflow_ViewPage_V_ERP_PaymentApplication : UPageBase
{
    PaymentApplication Payment = new PaymentApplication();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_ERP_PaymentApplication";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                ViewState["InstanceID"] = Request.QueryString["ID"];
                InintData();
            }
        }
    }

    private void InintData()
    {
        string methodName = "InintData";
        try
        {
            WorkFlowInstance Instance = wf_WorkFlowInstance.GetWorkFlowInstanceById(ViewState["InstanceID"].ToString());
            PaymentApplicationInfo item = PaymentApplication.GetPaymentApplicationInfoByInstanceId(ViewState["InstanceID"].ToString());
            if (item != null)
            {
                PaymentApplicationInfo info = PaymentApplication.GetPaymentApplicationInfo(Instance.FormId);
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.StartDeptId);
                ddlDepartName.Text = deptInfo.Remark;

                cblisoverCotract.Checked = info.IsOverContract == 1;
                cbChairman.Checked = info.IsCheckedChairman == 1;

                if (info.StartDeptId.Contains("S972"))
                {
                    lbPresident.Text = "总经理意见：";
                }
                else
                {
                    lbPresident.Text = "总裁意见：";
                }
                //LoadRelationPerson(info.StartDeptId);
                //cbRelatonUsers.Visible = cbPayer.Checked;
                //cbRelatonUsers.Enabled = false;
                //if (!string.IsNullOrEmpty(info.LeadersSelected))
                //{
                //    string[] cbDatas = info.LeadersSelected.Split(',');
                //    foreach (var cbItem in cbDatas)
                //    {
                //        ListItem listItem = cbRelatonUsers.Items.FindByValue(cbItem);
                //        listItem.Selected = true;
                //    }
                //}
            }

            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();

            ApproveOpinionUCDeptleader.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCRealateDept.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCLeader.InstanceId = ViewState["InstanceID"].ToString();
            Option_4.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC1.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC2.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC22.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC3.InstanceId = ViewState["InstanceID"].ToString();
            Option_0.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC4.InstanceId = ViewState["InstanceID"].ToString();
            Option_10.InstanceId = ViewState["InstanceID"].ToString();
            Option_11.InstanceId = ViewState["InstanceID"].ToString();
            Option_12.InstanceId = ViewState["InstanceID"].ToString();
            Option_13.InstanceId = ViewState["InstanceID"].ToString();
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private void LoadRelationPerson(string startDeptId)
    {
        //StringBuilder showUsers = new StringBuilder();
        //BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();

        //Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        //string companyCode = deptInfo.DepartCode.Substring(0, deptInfo.DepartCode.LastIndexOf('-'));
        //DataTable dtDept = bfurd.GetSelectRoleUser(companyCode, "付款申请工程师");
        //if (dtDept != null && dtDept.Rows.Count != 0)
        //{
        //    cbRelatonUsers.Items.Clear();
        //    foreach (DataRow rowItem in dtDept.Rows)//EmployeeCode
        //    {
        //        //showUsers.AppendFormat("{0},", rowItem["EmployeeName"].ToString());

        //        cbRelatonUsers.Items.Add(new ListItem()
        //        {
        //            Text = rowItem["EmployeeName"].ToString(),
        //            Value = rowItem["LoginName"].ToString()
        //        });
        //    }
        //    return;
        //}
        //else
        //{
        //    showUsers.Append("没有配置相关角色");
        //}

        //cbPayer.Text = showUsers.ToString();
    }
}
