using System;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;
using Pkurg.PWorldBPM.Entites.BIZ.ERP;

public partial class Workflow_ViewPage_V_ERP_Instruction : UPageBase
{
    Instruction Payment = new Instruction();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    public string className = "Workflow_ViewPage_V_ERP_Instruction";

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
            InstructionInfo item = Instruction.GetInstructionInfoByInstanceId(ViewState["InstanceID"].ToString());
            if (item != null)
            {
                InstructionInfo info = Instruction.GetInstructionInfo(Instance.FormId);
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(info.StartDeptId);
                ddlDepartName.Text = deptInfo.Remark;
                cbChairman.Checked = info.IsCheckedChairman == 1;
            }

            FlowRelated1.ProcId = ViewState["InstanceID"].ToString();
            Countersign1.ProcId = ViewState["InstanceID"].ToString();
            UploadAttachments1.ProcId = ViewState["InstanceID"].ToString();

            ApproveOpinionUCDeptleader.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCRealateDept.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCLeader.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUCCEO.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC1.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC2.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC22.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC3.InstanceId = ViewState["InstanceID"].ToString();
            ApproveOpinionUC4.InstanceId = ViewState["InstanceID"].ToString();
            Option_10.InstanceId = ViewState["InstanceID"].ToString();
            Option_11.InstanceId = ViewState["InstanceID"].ToString();

        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }
}
