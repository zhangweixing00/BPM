using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.ERP;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId="2004")]
public partial class Workflow_EditPage_E_ERP_SupplementalAgreement
    : E_WorkflowFormBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string FWDeptCode = System.Configuration.ConfigurationManager.AppSettings["FWDeptCode"];


    //public new string AppID = "2004";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        { 
            InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("ERP_SA_");
                FormTitle = ContractApproval_Common.GetErpFormTitle(this);//设置标题
                StartDeptId = ddlDepartName.SelectedItem.Value;
                if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(Request["erpFormId"], ERP_WF_T_Name.ERP_SupplementalAgreement))
                {
                    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip_BPM + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;

                InitFormData();

                //Countersign1.CounterSignDeptId = StartDeptId;
            }
            //detailContract.InnerHtml = string.Format("<a href='{0}'>原合同详细信息</a>", SupplementalAgreement_Common.GetPoUrl());

        }
    }

    private void InitStartDeptment()
    {
        ddlDepartName.DataSource = GetStartDeptmentDataSource();
        ddlDepartName.DataTextField = "Remark";
        ddlDepartName.DataValueField = "DepartCode";
        ddlDepartName.DataBind();
    }

    protected override void InitFormData()
    {
        try
        {
            SupplementalAgreementInfo info = SupplementalAgreement.GetModel(this.FormId);
            if (info != null)
            {
                //if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(info.ErpFormId, ERP_WF_T_Name.ERP_ContractApproval))
                //{

                //    ERP_CallbackResultType resultType = new ContractApproval_Service().NotifyStartAdvance(Request["erpFormId"], true);
                //    new WF_WorkFlowInstance().DeleteWorkFlowInstance(_BPMContext.ProcID);
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('BPM已存在正在审批的该单据！'); window.opener=null;window.open('', '_self', '');window.close();", true);
                //    return false;
                //}


                ListItem selectedItem = ddlDepartName.Items.FindByValue(info.StartDeptId);
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }
                cbIsReportResource.Checked = info.IsReportToResource.Value == 1;
                cbIsReportFounder.Checked = info.IsReportToFounder.Value == 1;
                StartDeptId = info.StartDeptId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        SupplementalAgreementInfo info = null;
        try
        {
            info = SupplementalAgreement.GetModel(FormId);
            if (info == null)
            {
                info = new SupplementalAgreementInfo()
                {
                    CreateTime = DateTime.Now.ToString(),
                    IsReportToResource = cbIsReportResource.Checked ? 1 : 0,
                    IsReportToFounder = cbIsReportFounder.Checked ? 1 : 0,
                    StartDeptId = ddlDepartName.SelectedItem.Value,
                    FormID = FormId,
                    ErpFormId = Request["erpFormId"],
                    RelationContract  = Request["erpPoId"],
                    ErpFormType = "SUPPLMENT",
                    ApproveResult = ""
                };
                SupplementalAgreement.Add(info);
                //必须首次调用
                cbIsReportResource.SaveToDB(FormId,AppID);
            }
            else
            {

                info.IsReportToResource = cbIsReportResource.Checked ? 1 : 0;
                info.IsReportToFounder = cbIsReportFounder.Checked ? 1 : 0;
                info.StartDeptId = ddlDepartName.SelectedItem.Value;

                SupplementalAgreement.Update(info);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,Archiver,AssistLeaders,CEO,CityAssistLeaders,CityCEO,CityChairman,CityLawDeptManager,CityLawDeptManager2,CityViceLeaders,CounterSignNum,CounterSignUsers,CounterSignUsers_Group,DeptManager,GroupLeaders,IsOverContract,IsPass,IsReportToResource,LawDeptManager,LawLeaders,Stamper,ViceLeaders
        NameValueCollection dataFields = new NameValueCollection();
        dataFields.Add("IsPass", "1");
        dataFields.Add("IsReportToResource", cbIsReportResource.Checked ? "1" : "0");
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        //string groupId = "B04-D319";

        string FWDepartmentCode = BPMHelp.GetDeptIDByOtherIDAndName(StartDeptId, "法务部");
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(StartDeptId);
        //string startDeptId = _BPMContext.CurrentUser.MainDeptId;

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,,,,,,,,,CounterSignNum,CounterSignUsers,,,IsOverContract,IsPass,,,,
        ///自动生成
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "DeptManager", IsHaveToExsit = true });
        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers", RoleName = "部门负责人", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true, OrderId = 5 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDepartmentCode, RoleName = "部门负责人", Name = "CityLawDeptManager", IsHaveToExsit = true, IsRepeatIgnore = true, OrderId = 4 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign1.Result + string.Format(",{0},{1},", StartDeptId, FWDepartmentCode), RoleName = "主管助理总裁", Name = "CityAssistLeaders", IsHaveToExsit = false, IsRepeatIgnore = true, OrderId = 3 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign1.Result + string.Format(",{0},{1},", StartDeptId, FWDepartmentCode), RoleName = "主管副总裁", Name = "CityViceLeaders", IsHaveToExsit = false, IsRepeatIgnore = true, OrderId = 2 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总裁", Name = "CityCEO", IsHaveToExsit = true, OrderId = 1 });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "董事长", Name = "CityChairman", IsHaveToExsit = false });

        //StringBuilder deptsofGroup = new StringBuilder();

        //foreach (var item in (Countersign1.Result + string.Format("{0},", StartDeptId)).Split(',').ToList())
        //{
        //    string deptsofGroupTmp = Workflow_Common.GetRoleDepts(item, "集团主管部门");
        //    if (!deptsofGroup.ToString().Contains(deptsofGroupTmp))
        //    {
        //        deptsofGroup.AppendFormat("{0},", deptsofGroupTmp);
        //    }
        //}
        //string groupLawDept = "B04-D319-S496";

        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = deptsofGroup.ToString().Trim(','), RoleName = "部门负责人", Name = "GroupLeaders" ,  IsHaveToExsit = true});

        //dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers_Group", Result = Countersign_Group1.GetCounterSignUsers(), IsHaveToExsit = true });

        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers_Group", RoleName = "部门负责人", DeptCode = Countersign_Group1.Result, IsHaveToExsit = true });

        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDeptCode, RoleName = "总监", Name = "LawDeptManager" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管助理总裁", Name = "AssistLeaders" });


        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = "", RoleName = "部门负责人", Name = "LawLeaders" });//集团主管法务领导跳过，代码保留
        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = groupLawDept, RoleName = "部门负责人", Name = "LawLeaders"});//集团主管法务领导


        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = Countersign_Group1.Result, RoleName = "主管副总裁", Name = "ViceLeaders" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "总裁", Name = "CEO" });
        //dfInfos.Add(new K2_DataFieldInfo() { DeptCode = "", RoleName = "流程发起人", Name = "" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = FWDepartmentCode, RoleName = "部门负责人", Name = "CityLawDeptManager2" });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "公章管理员", Name = "Stamper", IsHaveToExsit = true });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "档案管理员", Name = "Archiver", IsHaveToExsit = true, });


        return dfInfos;
    }

    /// <summary>
    /// 流程发起前操作
    /// </summary>
    /// <returns></returns>
    protected override bool BeforeWorkflowStart()
    {
        //return true;
        string erpFormCode = Request["erpFormId"];
        if (!string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            erpFormCode =SupplementalAgreement.GetModelByInstId(_BPMContext.ProcID).ErpFormId;
        }
        ERP_CallbackResultType resultType = new CommonService(int.Parse(AppID)).NotifyStartAdvance(erpFormCode, false);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 流程成功启动后操作
    /// </summary>
    protected override bool AfterWorkflowStart(int wfInstanceId)
    {
        //return true;
        SupplementalAgreementInfo info = SupplementalAgreement.GetModel(FormId);
        ERP_CallbackResultType resultType = new CommonService(int.Parse(AppID)).NotifyStartAdvance(info.ErpFormId, true);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            //删除流程实例
            new WF_WorkFlowInstance().DeleteWorkFlowInstance(_BPMContext.ProcID);

            //撤销已发起的流程
            WorkflowManage.StopWorkflow(wfInstanceId);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }


    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
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

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }
}
