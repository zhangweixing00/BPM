using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3019")]
public partial class Workflow_EditPage_E_HR_EmployeeTransfer : E_WorkflowFormBase
{
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string HRDeptCode = System.Configuration.ConfigurationManager.AppSettings["HRDeptCode"];

    #region 重载函数
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitApproveList();
            StartDeptId = _BPMContext.CurrentUser.MainDeptId;
            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("HR_ET_");
                //StartDeptId = ddlDeptName.SelectedItem.Value;
                tbReportCode.Text = FormId;
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                InitFormData();
            }
            InitLeader("");
        }
    }

    protected override void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_EmployeeTransfer info = BizContext.HR_EmployeeTransfer.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);
                tbReportCode.Text = info.FormID;
                tbUserName.Text = info.UserName;
                tbLoginID.Value = info.LoginID;
                tbSex.Text = info.Sex;
                tbEntryTime.Value = info.EntryTime;
                tbGraduation.Text = info.Graduation;
                tbEducation.Text = info.Education;
                tbFounderTime.Value = info.FounderTime;
                tbDeptName.Text = info.DeptName;
                tbDeptCode.Value = info.DeptCode;
                tbPost.Text = info.Post;
                tbPostLevel.Text = info.PostLevel;
                tbToDeptName.Text = info.ToDeptName;
                tbToDeptCode.Value = info.ToDeptCode;
                tbToPost.Text = info.ToPost;
                tbToPostLevel.Text = info.ToPostLevel;
                tbLabourContractStart.Value = info.LabourContractStart;
                tbLabourContractEnd.Value = info.LabourContractEnd;
                tbToLabourContractStart.Value = info.ToLabourContractStart;
                tbToLabourContractEnd.Value = info.ToLabourContractEnd;
                tbSalary.Text = info.Salary;
                tbRatio.Text = info.Ratio;
                tbAnnualSalary.Text = info.AnnualSalary;
                tbToSalary.Text = info.ToSalary;
                tbToRatio.Text = info.ToRatio;
                tbToAnnualSalary.Text = info.ToAnnualSalary;
                cblTransferReason.SelectedValue = info.TransferReason;
                tbRemark.Text = info.Remark;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        var info = BizContext.HR_EmployeeTransfer.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new HR_EmployeeTransfer()
            {
               FormID = FormId,
                UserName = tbUserName.Text,
                LoginID=tbLoginID.Value,
                Sex=tbSex.Text,
                EntryTime = tbEntryTime.Value,
                Graduation=tbGraduation.Text,
                Education=tbEducation.Text,
                FounderTime = tbFounderTime.Value,
                DeptName = tbDeptName.Text,
                DeptCode = tbDeptCode.Value,
                Post = tbPost.Text,
                PostLevel = tbPostLevel.Text,
                ToDeptName = tbToDeptName.Text,
                ToDeptCode = tbToDeptCode.Value,
                ToPost = tbToPost.Text,
                ToPostLevel = tbToPostLevel.Text,
                LabourContractStart = tbLabourContractStart.Value,
                LabourContractEnd = tbLabourContractEnd.Value,
                ToLabourContractStart = tbToLabourContractStart.Value,
                ToLabourContractEnd = tbToLabourContractEnd.Value,
                Salary = tbSalary.Text,
                Ratio = tbRatio.Text,
                AnnualSalary = tbAnnualSalary.Text,
                ToSalary = tbToSalary.Text,
                ToRatio = tbToRatio.Text,
                ToAnnualSalary = tbToAnnualSalary.Text,
                TransferReason=cblTransferReason.SelectedValue,
                Remark = tbRemark.Text,
                IsGroup = hfIsGroup.Value
            };
            BizContext.HR_EmployeeTransfer.InsertOnSubmit(info);
        }
        else
        {
                info.UserName = tbUserName.Text;
                info.LoginID=tbLoginID.Value;
                info.Sex=tbSex.Text;
                info.EntryTime = tbEntryTime.Value;
                info.Graduation=tbGraduation.Text;
                info.Education=tbEducation.Text;
                info.FounderTime = tbFounderTime.Value;
                info.DeptName = tbDeptName.Text;
                info.DeptCode = tbDeptCode.Value;
                info.Post = tbPost.Text;
                info.PostLevel = tbPostLevel.Text;
                info.ToDeptName = tbToDeptName.Text;
                info.ToDeptCode = tbToDeptCode.Value;
                info.ToPost = tbToPost.Text;
                info.ToPostLevel = tbToPostLevel.Text;
                info.LabourContractStart = tbLabourContractStart.Value;
                info.LabourContractEnd = tbLabourContractEnd.Value;
                info.ToLabourContractStart = tbToLabourContractStart.Value;
                info.ToLabourContractEnd = tbToLabourContractEnd.Value;
                info.Salary = tbSalary.Text;
                info.Ratio = tbRatio.Text;
                info.AnnualSalary = tbAnnualSalary.Text;
                info.ToSalary = tbToSalary.Text;
                info.ToRatio = tbToRatio.Text;
                info.ToAnnualSalary = tbToAnnualSalary.Text;
                info.TransferReason=cblTransferReason.SelectedValue;
                info.Remark = tbRemark.Text;
                info.IsGroup = hfIsGroup.Value;
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,Director,EDeptManager,Employee,HRDeptManager,HRDeptManager2,IsPass,President,RDeptManager
        NameValueCollection dataFields = new NameValueCollection();
        if (tbToOrFrom.Value == "调入")
        {
            dataFields.Add("Employee", tbLoginID.Value);
        }
        else
        { 
            dataFields.Add("Employee", "noapprovers");
        }
        
        dataFields.Add("RDeptManager", "noapprovers");
        dataFields.Add("HRDeptManager2", "noapprovers");
        dataFields.Add("IsPass", "1");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;
        string DeptCode = "";
        string strDeptName = "";
        if (tbToOrFrom.Value == "调入")
        {
            DeptCode = tbDeptCode.Value;
            strDeptName = tbDeptName.Text;
        }
        else if (tbToOrFrom.Value == "调出")
        {
            DeptCode = tbToDeptCode.Value;
            strDeptName = tbToDeptName.Text;
        }
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,Director,EDeptManager,Employee,HRDeptManager,HRDeptManager2,IsPass,President,RDeptManager
        ///自动生成
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = DeptCode, RoleName = "部门负责人", Name = "EDeptManager" });
        if (strDeptName.Contains("人力资源部"))
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "HRDeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDeptCode, RoleName = "部门负责人", Name = "HRDeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = DeptCode, RoleName = "主管助理总裁,主管副总裁", Name = "Director" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "董事长", Name = "President" });

        return dfInfos;
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        FormTitle = tbUserName.Text + "--员工流动审批";
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
        FormTitle = tbUserName.Text + "--员工流动审批";
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
    #endregion

    #region 自定义函数

    /// <summary>
    /// 初始化审批栏
    /// </summary>
    private void InitApproveList()
    {
        string IsGroup = Request.QueryString["ref"];
        if (string.IsNullOrEmpty(IsGroup))
        {
            lbDirector.Text = "相关董事意见：";
            lbPresident.Text = "董事长意见：";
            hfIsGroup.Value = "0";
            lbTitle.Text = "Peking University Resources Group Investment Company Limited";
        }
        else
        {
            lbDirector.Text = "用人部门分管领导意见：";
            lbPresident.Text = "CEO意见：";
            hfIsGroup.Value = "1";
        }
    }

    /// <summary>
    /// 初始化审批栏2
    /// </summary>
    private void InitApproveList2(string IsGroup)
    {
        lbDirector.Text = "相关董事意见：";
        lbPresident.Text = "董事长意见：";
        lbTitle.Text = "Peking University Resources Group Investment Company Limited";
        if (!string.IsNullOrEmpty(IsGroup))
        {
            if (IsGroup == "1")
            {
                lbDirector.Text = "用人部门分管领导意见：";
                lbPresident.Text = "CEO意见：";
                lbTitle.Text = "";
            }
        }
    }

    /// <summary>
    /// 发起部门更改方法
    /// </summary>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //StartDeptId = ddlDeptName.SelectedItem.Value;
    }

    /// <summary>
    /// 初始化领导名称
    /// </summary>
    private void InitLeader(string DeptCode)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(DeptCode, "部门负责人");
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtDirector = bfurd.GetSelectRoleUser(DeptCode, "主管副总裁");
        DataTable dtPresident = bfurd.GetSelectRoleUser(PKURGICode, "董事长");

        if (dtDeptManager.Rows.Count != 0)
        {
            lbDeptManager2.Text = "(" + dtDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbDeptManager2.Text = "";
        }
        if (dtHRDeptManager.Rows.Count != 0)
        {
            lbHRDeptManager2.Text = "(" + dtHRDeptManager.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        if (dtDirector.Rows.Count != 0)
        {
            lbDirector2.Text = "(" + dtDirector.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
        else
        {
            lbDirector2.Text = "";
        }
        if (dtPresident.Rows.Count != 0)
        {
            lbPresident2.Text = "(" + dtPresident.Rows[0]["EmployeeName"].ToString() + ")审批";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitLeader(tbDeptCode.Value);
        tbToOrFrom.Value = "调入";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        InitLeader(tbToDeptCode.Value);
        tbToOrFrom.Value = "调出";
    }
    #endregion
}
