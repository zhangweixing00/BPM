using System;
using System.Data;
using System.Text;
using Pkurg.PWorld.Business.Permission;

public partial class Workflow_Forms_WF_Approval : UWorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbDeptName.Text = _BPMContext.CurrentPWordUser.DepartName;
            tbUser.Text = _BPMContext.CurrentPWordUser.EmployeeName;
            
        }
    }

    public override void SetParams()
    {
        base.SetParams();

        FlowParams.SetParams("WFM", @"founder\tangsheng");//流程审核人
        FlowParams.SetParams("DeptManager", GetUserLoginName("部门负责人"));//部门负责人
        FlowParams.SetParams("CEO", GetUserLoginName("CEO"));//CEO
        FlowParams.SetParams("StartUser",_BPMContext.CurrentUser.LoginId);//发起人
        //FlowParams.SetParams("CounterSignUsers", @"founder\yanghechun,founder\zhangweixing");//会签部门


        ///提交前所有字段需传入
        Countersign1.Submit();
    }

    public string GetUserLoginName(string roleName)
    {
        StringBuilder content = new StringBuilder();
        DataTable dt = new BFPmsUserRoleDepartment().GetSelectRoleUser(_BPMContext.CurrentUser.MainDeptId, roleName);
        foreach (DataRow dr in dt.Rows)
        {
            content.AppendFormat(@"Founder\{0},", dt.Rows[0]["LoginName"].ToString());
        }
        return content.ToString().Trim(',');
    }

    public override void SaveFormData()
    {
        Countersign1.SaveData();
        UploadAttachments1.SaveAttachment(SerialNumber);
        ProcName = tbTitle.Text;

        base.SaveFormData();
    }
}