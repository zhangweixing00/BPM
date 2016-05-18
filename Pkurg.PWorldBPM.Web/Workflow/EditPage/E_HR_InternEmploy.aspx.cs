using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3022")]
public partial class Workflow_EditPage_E_HR_InternEmploy
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormTitle = "实习生录用审批";
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("HR_IE_");
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                InitFormData();
            }
        }
    }
    /// <summary>
    /// 初始化表单数据，使用linq
    /// </summary>
    protected override void InitFormData()
    {
        try
        {
            ///加载业务数据
            //通过formid得到表单信息
            Pkurg.PWorldBPM.Business.BIZ.HR_InternEmploy info = BizContext.HR_InternEmploy.FirstOrDefault(x => x.FormID == FormId);
            StartDeptId = tbDeptCode.Value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //部门筛选
    //public string DeptFliter = "S363-S973,S363-S969";

    /// <summary>
    /// 使用linq保存表单数据[根据formid来判断存储]
    /// </summary>
    protected override void SaveFormData()
    {
        var info = BizContext.HR_InternEmploy.FirstOrDefault(x => x.FormID == FormId);
        if (info == null)
        {
            info = new HR_InternEmploy()
            {
               FormID = FormId,
                EmployeeName=tbEmployeeName.Text,
                Position=tbPosition.Text,
                InternDeptName=tbDept.Text,
                InternDeptCode = tbDeptCode.Value,
                InternReward=tbInternReward.Text,
                InternDeadline=tbInternDeadline.Text,
                FormTitle = "实习生录用审批"
            };
            BizContext.HR_InternEmploy.InsertOnSubmit(info);
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,HRManager,Interviewer,IsPass,StaffingDeptManager
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
        var info = BizContext.HR_InternEmploy.FirstOrDefault(x => x.FormID == FormId); 
        StartDeptId = info.InternDeptCode;
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;
        string HRDepartmentCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "人力资源部");
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,HRManager,Interviewer,IsPass,StaffingDeptManager
        ///自动生成
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDepartmentCode, RoleName = "面试负责人", Name = "Interviewer" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "StaffingDeptManager" });
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
        StartDeptId = tbDeptCode.Value;
        Save();
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        StartDeptId = tbDeptCode.Value;
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
