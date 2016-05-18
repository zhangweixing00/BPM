using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "3024")]
public partial class Workflow_EditPage_E_OA_ContractOfWY
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化起始部门
            InitDepartName();

            //合同类型
            ddlContractType1.DataSource = ContractTypeInfosHelper.GetFirstContractTypeInfos1();
            ddlContractType1.DataTextField = "value";
            ddlContractType1.DataValueField = "key";
            ddlContractType1.DataBind();

            //合同主体
            ddlContractSubject.DataSource = ContractSubjectHelper.GetContractSubjectInfos1();
            ddlContractSubject.DataTextField = "value";
            ddlContractSubject.DataValueField = "key";
            ddlContractSubject.DataBind();

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;


            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("OA_WYHT_");
                FormTitle = SysContext.WF_AppDict.FirstOrDefault(x => x.AppId == AppID).AppName;

                StartDeptId = ddlDepartName.SelectedItem.Value;
                tbReportCode.Text = FormId;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                tbUserName.Text = CurrentEmployee.EmployeeName;
                tbMobile.Text = CurrentEmployee.MobilePhone;
                
                cbAP.Checked = true;
                cbVP.Checked = true;

                if (ddlContractType1.Items.Count != 0)
                {
                    ddlContractType1.SelectedIndex = 0;
                    ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos1(ddlContractType1.SelectedItem.Value);
                    ddlContractType2.DataTextField = "value";
                    ddlContractType2.DataValueField = "key";
                    ddlContractType2.DataBind();
                    if (ddlContractType2.Items.Count != 0)
                    {
                        ddlContractType2.SelectedIndex = 0;
                        ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos1(ddlContractType2.SelectedItem.Value);
                        ddlContractType3.DataTextField = "value";
                        ddlContractType3.DataValueField = "key";
                        ddlContractType3.DataBind();
                    }
                }
                //InitCheckBoxList();
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
               
                //初始化表单数据
                InitFormData();
            }
            //判断分公司和物业集团显示
            if (StartDeptId.Contains("S366-S976"))
            {
                Company.Visible = false;
                Group.Visible = true;
                Group1.Visible = false;
                IsReportToWY.Visible = false;
                //cblIsReportToWY.Visible = false;
               
            }
            else if (!StartDeptId.Contains("S366-S976"))
            {
                Company.Visible = true;
                Company1.Visible = true;
                Group.Visible = true;
                IsReportToGroup.Visible = false;
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
            Pkurg.PWorldBPM.Business.BIZ.OA_ContractOfWY info = BizContext.OA_ContractOfWY.FirstOrDefault(x => x.FormID == FormId);

            lbIsReport.Text = string.IsNullOrEmpty(info.RelatedFormID) ? "0" : "1";
            if (lbIsReport.Text == "0")
            {
                ListItem item = ddlDepartName.Items.FindByValue(info.DeptCode);
                if (item != null)
                {
                    ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
                }
            }
            else
            {
                ddlDepartName.Items.Clear();
                string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
                Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(CompanyCode);

                ListItem item = new ListItem { Text = deptInfo.DepartName, Value = CompanyCode };
                ddlDepartName.Items.Add(item);
                ddlDepartName.Enabled = false;
            }

            if (info != null)
            {

                ListItem selectedItem = ddlDepartName.Items.FindByValue(info.DeptCode);
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }
                StartDeptId = info.DeptCode;


                //读取已保存的数据
                tbReportCode.Text = info.FormID;
                cblSecurityLevel.SelectedIndex = int.Parse(info.SecurityLevel.ToString());
                cblUrgenLevel.SelectedIndex = int.Parse(info.UrgenLevel.ToString());
                ddlDepartName.SelectedValue = info.DeptName;
                tbDateTime.Text = info.DateTime;
                UpdatedTextBox.Value = info.DateTime;
                tbUserName.Text = info.UserName;
                tbMobile.Text = info.Mobile;
                cblIsReportToWY.SelectedValue = info.IsReportToWY;
                cblIsReportToGroup.SelectedValue= info.IsReportToGroup;
                //合同类型
                ListItem li1 = ddlContractType1.Items.FindByValue(info.ContractType1);
                if (li1 != null)
                {
                    li1.Selected = true;
                }
                ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos1(ddlContractType1.SelectedItem.Value);
                ddlContractType2.DataTextField = "value";
                ddlContractType2.DataValueField = "key";
                ddlContractType2.DataBind();

                ListItem li2 = ddlContractType2.Items.FindByValue(info.ContractType2);
                if (li2 != null)
                {
                    li2.Selected = true;
                }
                if (ddlContractType2.SelectedItem != null)
                {
                    ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos1(ddlContractType2.SelectedItem.Value);
                    ddlContractType3.DataTextField = "value";
                    ddlContractType3.DataValueField = "key";
                    ddlContractType3.DataBind();
                    ListItem li3 = ddlContractType3.Items.FindByValue(info.ContractType3);
                    if (li3 != null)
                    {
                        li3.Selected = true;
                    }
                }

                tbContractSum.Text = info.ContractSum;
                cblIsSupplementProtocol.SelectedValue = info.IsSupplementProtocol;
                tbSupplementProtocol.Text = info.IsSupplementProtocolText;
                cblIsFormatContract.SelectedValue = info.IsFormatContract;
                cblIsNormText.SelectedValue = info.IsNormText;
                cblIsBidding.SelectedValue = info.IsBidding;
                //合同主体
                ddlContractSubject.SelectedValue = info.ContractSubject;
                tbContractSubject1.Text = info.ContractSubjectName2;
                tbContractSubject2.Text = info.ContractSubjectName3;
                tbContractSubject3.Text = info.ContractSubjectName4;

                tbContractTitle.Text = info.ContractTitle;
                tbContractContent.Text = info.ContractContent;

                InitCheckBoxList(info.LeadersSelected);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 使用linq保存表单数据【根据formid判断来存储】
    /// </summary>
    protected override void SaveFormData()
    {
        var info = BizContext.OA_ContractOfWY.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new OA_ContractOfWY()
            {
               FormID = FormId,
                SecurityLevel = cblSecurityLevel.SelectedIndex.ToString(),
                UrgenLevel = cblUrgenLevel.SelectedIndex.ToString(),

                DeptCode = ddlDepartName.SelectedItem.Value.ToString(),
                DeptName = ddlDepartName.SelectedItem.Text,
                DateTime = DateTime.Now.ToString(),
                UserName = tbUserName.Text,
                Mobile = tbMobile.Text,

                IsReportToWY=cblIsReportToWY.SelectedIndex.ToString(),
                IsReportToGroup=cblIsReportToGroup.SelectedIndex.ToString(),
                //新添加的存储数据
                //合同类型
                ContractType1 = ddlContractType1.SelectedItem.Value,
                ContractType2 = ddlContractType2.SelectedItem != null ? ddlContractType2.SelectedItem.Value : "",
                ContractType3 = ddlContractType3.SelectedItem != null ? ddlContractType3.SelectedItem.Value : "",

                ContractTypeName1 = ddlContractType1.SelectedItem.Text,
                ContractTypeName2 = ddlContractType2.SelectedItem != null ? ddlContractType2.SelectedItem.Text : "",
                ContractTypeName3 = ddlContractType3.SelectedItem != null ? ddlContractType3.SelectedItem.Text : "",

                ContractSum = tbContractSum.Text,

                IsSupplementProtocol = cblIsSupplementProtocol.SelectedValue.ToString(),
                IsSupplementProtocolText = tbSupplementProtocol.Text,
                IsFormatContract = cblIsFormatContract.SelectedValue.ToString(),
                IsNormText = cblIsNormText.SelectedValue.ToString(),
                IsBidding = cblIsBidding.SelectedValue.ToString(),
                //合同主体
                ContractSubject = ddlContractSubject.SelectedItem.Value,
                ContractSubjectName = ddlContractSubject.SelectedItem.Text,
                ContractSubjectName2 = tbContractSubject1.Text,
                ContractSubjectName3 = tbContractSubject2.Text,
                ContractSubjectName4 = tbContractSubject3.Text,

                ContractTitle = tbContractTitle.Text,
                ContractContent = tbContractContent.Text,
                LeadersSelected = SaveLeadersSelected(),
                RelatedFormID = string.Empty,
            };
            BizContext.OA_ContractOfWY.InsertOnSubmit(info);
        }
        BizContext.SubmitChanges();
    }

    protected override string GetFormTitle()
    {
        return tbContractTitle.Text;
    }
    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        if (StartDeptId.Contains("S366-S976"))
        {
            dataFields.Add("IsReportToGroup", cblIsReportToGroup.SelectedItem.Value);
            dataFields.Add("IsReportToWY", "0");
        }
        else
        {
            dataFields.Add("IsReportToGroup", "0");
            dataFields.Add("IsReportToWY", cblIsReportToWY.SelectedItem.Value);
        }
        
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        string WYGroupCode = System.Configuration.ConfigurationManager.AppSettings["WYGroupCode"];

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        //分公司
        if (!StartDeptId.Contains("S366-S976"))
        {
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "CityDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "部门负责人", Name = "CityLawDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管副总经理", Name = "ViceLeader" });
            //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "主管副总经理", Name = "ViceLeader" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总裁", Name = "CityPresident" });

            if (cblIsReportToWY.SelectedItem.Value == "0")
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "DeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "CounterSignUsers_Group", IsRepeatIgnore = true });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "LawDeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管助理总裁", Name = "AssiPresident" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管副总裁", Name = "VicePresident" });
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "总裁", Name = "President" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "DeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers_Group", DeptCode = Countersign_Group1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "部门负责人", Name = "LawDeptManager" });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管助理总裁", Name = "AssiPresident" });
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管副总裁", Name = "VicePresident" }); 
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "总裁", Name = "President" });
            }

            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "合同法务复核员", Name = "LawAuditOpinion" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "公章管理员", Name = "SealManager" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "档案管理员", Name = "FileManager" });
        }
            //物业集团
        else
        {
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "CityDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "CounterSignUsers", IsRepeatIgnore = true });
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "CityLawDeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管副总经理", Name = "ViceLeader" });
            dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "总裁", Name = "CityPresident" });

            //物业公司
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "DeptManager" });
            dfInfos.Add(new K2_DataFieldInfo() { RoleName = "部门负责人", Name = "CounterSignUsers_Group", DeptCode = Countersign_Group1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "部门负责人", Name = "LawDeptManager" });
            if (cbAP.Checked)
            {
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管助理总裁", Name = "AssiPresident" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "AssiPresident" });
            }
            if (cbVP.Checked)
            {
                dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + Countersign1.Result, RoleName = "主管副总裁", Name = "VicePresident" });
            }
            else
            {
                dfInfos.Add(new K2_DataFieldInfo() { Result = "noapprovers", Name = "VicePresident" });
            }
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "总裁", Name = "President" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "合同法务复核员", Name = "LawAuditOpinion" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "公章管理员", Name = "SealManager" });
            dfInfos.Add(new K2_DataFieldInfo() { DeptCode = WYGroupCode, RoleName = "档案管理员", Name = "FileManager" });
        }
        return dfInfos;
    }

    #region 自定义函数

    /// <summary>
    /// 初始化起始部门
    /// </summary>
    private void InitDepartName()
    {
        //得到起始部门的数据源
        ddlDepartName.DataSource = GetStartDeptmentDataSource();
        ddlDepartName.DataTextField = "Remark";
        ddlDepartName.DataValueField = "DepartCode";
        ddlDepartName.DataBind();
    }
    /// <summary>
    /// 初始化选择框列表控件
    /// </summary>
    /// <param name="cblCheck"></param>
    private void InitCheckBoxList(string cblCheck)
    {
        if (!string.IsNullOrEmpty(cblCheck))
        {
            string[] array = cblCheck.Split(',');
            cbAP.Checked = array[0].Substring(0, 1) == "0" ? false : true;
            cbVP.Checked = array[1].Substring(0, 1) == "0" ? false : true;
        }
    }
    /// <summary>
    /// 保存勾选框数据
    /// </summary>
    /// <returns></returns>
    private string SaveLeadersSelected()
    {
        string strLeadersSelected = "";
        strLeadersSelected = (cbAP.Checked ? "1" : "0") + ":AP";
        strLeadersSelected += "," + (cbVP.Checked ? "1" : "0") + ":VP";
        return strLeadersSelected;
    }
    /// <summary>
    /// 发起部门更改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }
    /// <summary>
    /// 点击事件1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlContractType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContractType2.DataSource = ContractTypeInfosHelper.GetSecondContractTypeInfos1(ddlContractType1.SelectedItem.Value);
        ddlContractType2.DataTextField = "value";
        ddlContractType2.DataValueField = "key";
        ddlContractType2.DataBind();
        if (ddlContractType2.Items.Count != 0)
        {
            ddlContractType2.SelectedIndex = 0;
            ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos1(ddlContractType2.SelectedItem.Value);
            ddlContractType3.DataTextField = "value";
            ddlContractType3.DataValueField = "key";
            ddlContractType3.DataBind();
        }
    }
    /// <summary>
    /// 点击事件2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlContractType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlContractType3.DataSource = ContractTypeInfosHelper.GetThirdContractTypeInfos1(ddlContractType2.SelectedItem.Value);
        ddlContractType3.DataTextField = "value";
        ddlContractType3.DataValueField = "key";
        ddlContractType3.DataBind();
    }
    #endregion
    
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        Save();
    }

    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
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
