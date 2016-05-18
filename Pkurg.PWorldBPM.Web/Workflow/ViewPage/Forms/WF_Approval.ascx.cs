using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using Pkurg.PWorld.Business.Permission;

public partial class Workflow_Forms_WF_Approval : UWorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ProcName = tbTitle.Text;
        //if (!IsPostBack)
        //{
        //    tbDeptName.Text = _BPMContext.CurrentPWordUser.DepartName;
        //    tbUser.Text = _BPMContext.CurrentPWordUser.EmployeeName;

        //}
        ApprovalControls = new List<UserControl>();
        ApprovalControls.Add(ApprovalBox1);
        ApprovalControls.Add(ApprovalBox2);
        ApprovalControls.Add(ApprovalBox3);
        ApprovalControls.Add(ApprovalBox4);
    }

 

    public override void SetParams()
    {
        base.SetParams();
        Countersign1.Submit();

        //FlowParams.SetParams("leaders", @"founder\zhangweixing,founder\zybpmadmin");//分管领导
        //return;
        string counterSignDeptIds = Countersign1.Result;
        StringBuilder leaders = new StringBuilder();
        if (!string.IsNullOrEmpty(counterSignDeptIds))
        {
            string[] deptIds = counterSignDeptIds.Split(',');
            foreach (var item in deptIds)
            {
                BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
                DataTable dt = bfurd.GetSelectRoleUser(item, "主管领导");
                foreach (DataRow dr in dt.Rows)
                {
                    leaders.AppendFormat(@"Founder\{0},", dt.Rows[0]["LoginName"].ToString());
                }
            }
        }

        FlowParams.SetParams("leaders", leaders.ToString().Trim(','));//分管领导


    }

    public override void SaveFormData()
    {
        Countersign1.SaveData();
        UploadAttachments1.SaveAttachment(SerialNumber);
        base.SaveFormData();
    }
}