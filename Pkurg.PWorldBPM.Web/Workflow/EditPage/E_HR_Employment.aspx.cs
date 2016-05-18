using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3017")]
public partial class Workflow_EditPage_E_HR_Employment : E_WorkflowFormBase
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
                FormId = BPMHelp.GetSerialNumber("HR_E_");
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
            InitLeader();
        }
    }

    protected override void InitFormData()
    {
        try
        {
            Pkurg.PWorldBPM.Business.BIZ.HR_Employment info = BizContext.HR_Employment.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                InitApproveList2(info.IsGroup);
                tbReportCode.Text = info.FormID;
                tbUserName.Text = info.UserName;
                tbDeptName.Text = info.DeptName;
                tbDeptCode.Value = info.DeptCode;
                tbGoalPost.Text = info.GoalPost;
                tbPostLevel.Text = info.PostLevel;
                tbSalary.Text = info.Salary;
                tbRatio.Text = info.Ratio;
                tbAnnualSalary.Text = info.AnnualSalary;
                cblIsLabourContract.SelectedValue = info.IsLabourContract;
                tbLabourContractStart.Value = info.LabourContractStart;
                tbLabourContractEnd.Value = info.LabourContractEnd;
                cblIsProbationPeriod.SelectedValue = info.IsProbationPeriod;
                tbProbationPeriodStart.Value = info.ProbationPeriodStart;
                tbProbationPeriodEnd.Value = info.ProbationPeriodEnd;
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
        var info = BizContext.HR_Employment.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new HR_Employment()
            {
               FormID = FormId,
                UserName = tbUserName.Text,
                DeptName = tbDeptName.Text,
                DeptCode = tbDeptCode.Value,
                GoalPost = tbGoalPost.Text,
                PostLevel = tbPostLevel.Text,
                Salary = tbSalary.Text,
                Ratio = tbRatio.Text,
                AnnualSalary = tbAnnualSalary.Text,
                IsLabourContract = cblIsLabourContract.SelectedValue,
                LabourContractStart = tbLabourContractStart.Value,
                LabourContractEnd = tbLabourContractEnd.Value,
                IsProbationPeriod = cblIsProbationPeriod.SelectedValue,
                ProbationPeriodStart = tbProbationPeriodStart.Value,
                ProbationPeriodEnd = tbProbationPeriodEnd.Value,
                Remark = tbRemark.Text,
                IsGroup = hfIsGroup.Value
            };
            BizContext.HR_Employment.InsertOnSubmit(info);
        }
        else
        {
            info.DeptName = tbDeptName.Text;
            info.GoalPost = tbGoalPost.Text;
            info.PostLevel = tbPostLevel.Text;
            info.Salary = tbSalary.Text;
            info.Ratio = tbRatio.Text;
            info.AnnualSalary = tbAnnualSalary.Text;
            info.IsLabourContract = cblIsLabourContract.SelectedValue;
            info.LabourContractStart = tbLabourContractStart.Value;
            info.LabourContractEnd = tbLabourContractEnd.Value;
            info.IsProbationPeriod = cblIsProbationPeriod.SelectedValue;
            info.ProbationPeriodStart = tbProbationPeriodStart.Value;
            info.ProbationPeriodEnd = tbProbationPeriodEnd.Value;
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
        dataFields.Add("Employee", "noapprovers");
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

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,Director,EDeptManager,Employee,HRDeptManager,HRDeptManager2,IsPass,President,RDeptManager
        ///自动生成
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = tbDeptCode.Value, RoleName = "部门负责人", Name = "EDeptManager" });
        if (tbDeptName.Text.Contains("人力资源部"))
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "HRDeptManager" });
        }
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = HRDeptCode, RoleName = "部门负责人", Name = "HRDeptManager" });
        }
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = tbDeptCode.Value, RoleName = "主管助理总裁,主管副总裁", Name = "Director" });
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
        FormTitle = tbUserName.Text + "--员工录用审批";
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
        FormTitle = tbUserName.Text + "--员工录用审批";
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
    private void InitLeader()
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        DataTable dtDeptManager = bfurd.GetSelectRoleUser(tbDeptCode.Value, "部门负责人");
        DataTable dtHRDeptManager = bfurd.GetSelectRoleUser(HRDeptCode, "部门负责人");
        DataTable dtDirector = bfurd.GetSelectRoleUser(tbDeptCode.Value, "主管副总裁");
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
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitLeader();
    }
}
