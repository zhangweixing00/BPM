using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3015")]
public partial class Workflow_EditPage_E_HR_EmployeeLeft
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbAgree');disabledButton('lbReject');disabledButton('lbSubmit');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbAgree');enableButton('lbReject');enableButton('lbSubmit');", true);

        if (!IsPostBack)
        {
            FormTitle = "员工离职\\调动转单";
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                //得到序列号（FormId）
                FormId = BPMHelp.GetSerialNumber("HR_EL_");
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle =info.FormTitle;
                //初始化表单数据
                InitFormData();

            }
        }
    }
    /// <summary>
    /// 初始化表单数据 使用linq
    /// </summary>
    protected override void InitFormData()
    {
        try
        {
            ///加载业务数据
            ///通过formid得到表单信息
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeLeft info = BizContext.HR_EmployeeLeft.FirstOrDefault(x=>x.FormID==FormId);
            if (info != null)
            {
                tbEmployeeName.Text = info.EmployeeName;
                tbEmployeeLoginName.Value = info.LoginName;
                tbDeptName.Text = info.DeptName;
                tbDeptID.Value = info.DeptID;
                tbPosition.Text = info.Position;
                tbLeftType.SelectedValue = info.LeftType;
                cbIsActiveLeft.Checked = bool.Parse(info.IsInitiativeLeft);
                cbEmployee.Checked = bool.Parse(info.IsEmployee);
                cbDeptManager.Checked = bool.Parse(info.IsStaffingDept);
                StartDeptId = info.DeptID;
                tbHandover.Text = info.Handover;
                tbRecipient.Text = info.Recipient;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 使用linq保存表单数据[根据formid来判断存储]
    /// </summary>
    protected override void SaveFormData()
    {
        var info = BizContext.HR_EmployeeLeft.FirstOrDefault(x=>x.FormID==FormId);
        if (info == null)
        {
            info = new HR_EmployeeLeft()
            {
               FormID = FormId,
                EmployeeName = tbEmployeeName.Text,
                LoginName = tbEmployeeLoginName.Value,
                DeptID = tbDeptID.Value,
                DeptName=tbDeptName.Text,
                Position = tbPosition.Text,
                LeftType = tbLeftType.SelectedValue,
                IsInitiativeLeft = cbIsActiveLeft.Checked.ToString(),
                IsEmployee = cbEmployee.Checked.ToString(),
                IsStaffingDept = cbDeptManager.Checked.ToString(),
                FormTitle="员工离职\\调动转单",
            };
            BizContext.HR_EmployeeLeft.InsertOnSubmit(info);
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,Employee,HRInterface,HRManager,IsPass,RelatedDeptInterface,RelatedDeptManager,StaffingDeptManager
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        var info = BizContext.HR_EmployeeLeft.FirstOrDefault(x => x.FormID == FormId);
        StartDeptId = info.DeptID;
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string HRDepartmentCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "人力资源部");
        string Employee = info.LoginName;
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,Employee,HRInterface,HRManager,IsPass,RelatedDeptInterface,RelatedDeptManager,StaffingDeptManager
        ///自动生成
        if (cbEmployee.Checked == true)
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = Employee, Name = "Employee" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "Employee"});
        }
        if (cbDeptManager.Checked == true)
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "StaffingDeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "StaffingDeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "离职流程相关部门接口人", Name = "RelatedDeptInterface" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "离职流程相关部门负责人", Name = "RelatedDeptManager" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "离职流程人力接口人", Name = "HRInterface" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDepartmentCode, RoleName = "部门负责人", Name = "HRManager" });
        return dfInfos;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        StartDeptId = tbDeptID.Value;
        Save();
        Alert("保存完成");
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        StartDeptId = tbDeptID.Value;
        Submit();
    }

    /// <summary>
    /// 终止
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        DelWorkflow();
    }

}
