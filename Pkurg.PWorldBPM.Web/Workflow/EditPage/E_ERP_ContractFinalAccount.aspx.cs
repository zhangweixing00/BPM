using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.ERP;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.BIZ.ERP;
using Pkurg.PWorldBPM.Business.Workflow;

[BPM(AppId = "10111")]
public partial class Workflow_EditPage_E_ERP_ContractFinalAccount
    : E_WorkflowFormBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    //项目运营部
    string XMYYDeptCode = System.Configuration.ConfigurationManager.AppSettings["XMYYDeptCode"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //初始化起始部门 
            InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                //得到序列号（FormId）
                FormId = BPMHelp.GetSerialNumber("ERP_JS_");
                //设置标题
                FormTitle = ContractApproval_Common.GetErpFormTitle(this);
                StartDeptId = ddlDepartName.SelectedItem.Value;
                //判断该流程是否存在【根据erpFormId判断】
                if (Pkurg.PWorldBPM.Business.BIZ.ERP.ERP_Common.IsExsitRunFlow(Request["erpFormId"], ERP_WF_T_Name.ERP_ContractFinalAccount))
                {
                    ERP_CallbackResultType resultType = new ERP_ContractFinalAccount_Service().NotifyStartAdvance(Request["erpFormId"], true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
                    return;
                }
            }
            else
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(instId);
                FormId = info.FormId;
                FormTitle = info.FormTitle;
                //初始化表单数据
                InitFormData();
            }
        }
    }
    /// <summary>
    /// 初始化起始部门
    /// </summary>
    private void InitStartDeptment()
    {
        //得到起始部门的数据源
        ddlDepartName.DataSource = GetStartDeptmentDataSource();
        ddlDepartName.DataTextField = "Remark";
        ddlDepartName.DataValueField = "DepartCode";
        ddlDepartName.DataBind();
    }
    /// <summary>
    /// 使用linq，初始化表单数据
    /// </summary>
    protected override void InitFormData()
    {
        try
        {
            ///加载业务数据
            //通过formid得到表单信息
            Pkurg.PWorldBPM.Business.BIZ.ERP_ContractFinalAccount info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                ListItem selectedItem = ddlDepartName.Items.FindByValue(info.StartDeptId);
                if (selectedItem != null)
                {
                    selectedItem.Selected = true;
                }
                StartDeptId = info.StartDeptId;
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
        var info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);

        if (info == null)
        {
            info = new ERP_ContractFinalAccount()
            {
               FormID = FormId,
                CreateTime = DateTime.Now.ToString(),
                StartDeptId = ddlDepartName.SelectedItem.Value,
                ErpFormId = Request["erpFormId"],
                RelationContract = Request["erpPoId"],
                ErpFormType = "POJS",
                ApproveResult = "",
                ERPFormTitle = Request["FormTitle"],
                ERPApproveLev = int.Parse(Request["erpApproveLev"])
            };
            BizContext.ERP_ContractFinalAccount.InsertOnSubmit(info);
        }
        else
        {
            info.StartDeptId = ddlDepartName.SelectedItem.Value;
        }
        BizContext.SubmitChanges();
    }

    /// <summary>
    /// 设置常量型DataField
    /// </summary>
    /// <returns></returns>
    protected override NameValueCollection LoadConstDataField()
    {
        //所有DataField：ActJumped,BusinessDocking,CEO,CityAssistLeaders,CityCEO,CityViceLeaders,CounterSignNum,CounterSignUsers,DeptManager,GroupViceLeaders,IsPass,ProjectOperationsDirector,ProjectOperationsVicePresident
        NameValueCollection dataFields = new NameValueCollection();
        //var info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);
        dataFields.Add("IsPass", "1");
        //dataFields.Add("ERPApproveLev", info.ERPApproveLev.ToString());
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

        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        ///已存在dataField：ActJumped,BusinessDocking,CEO,CityAssistLeaders,CityCEO,CityViceLeaders,CounterSignNum,CounterSignUsers,DeptManager,GroupViceLeaders,IsPass,ProjectOperationsDirector,ProjectOperationsVicePresident
        ///部门负责人
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId, RoleName = "部门负责人", Name = "CityDeptManager", IsHaveToExsit = true });
        //相关部门
        dfInfos.Add(new K2_DataFieldInfo() { Name = "CounterSignUsers", RoleName = "部门负责人", DeptCode = Countersign1.Result, IsRepeatIgnore = true, IsHaveToExsit = true });
        //分管领导意见
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + CompanyCode, RoleName = "主管助理总裁", Name = "CityAssistLeaders", IsHaveToExsit = false });
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = StartDeptId + "," + CompanyCode, RoleName = "主管副总裁", Name = "CityViceLeaders", IsHaveToExsit = false });
        //城市公司总裁意见
        dfInfos.Add(new K2_DataFieldInfo() { DeptCode = CompanyCode, RoleName = "总裁", Name = "CityPresident", IsHaveToExsit = true });


        var info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);

        //如果审批层次为1，则城市公司总裁之后不审批，审批人为空，审批层次为2，3，4时，项目运营部审批
        //项目运营部
        //dfInfos.Add(info.ERPApproveLev < 2 ?
        //    new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门成员", Name = "BusinessDocking", IsHaveToExsit = false }
        //    : new K2_DataFieldInfo() { DeptCode = XMYYDeptCode, RoleName = "部门成员", Name = "BusinessDocking", IsHaveToExsit = true }
        //    );
        dfInfos.Add(info.ERPApproveLev < 2 ?
            new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门副总经理", Name = "GroupVicePresident", IsHaveToExsit = false }
            : new K2_DataFieldInfo() { DeptCode = XMYYDeptCode, RoleName = "部门副总经理", Name = "GroupVicePresident", IsHaveToExsit = true }
            );
        dfInfos.Add(info.ERPApproveLev < 2 ?
           new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "部门负责人", Name = "GroupDeptManager", IsHaveToExsit = false }
           : new K2_DataFieldInfo() { DeptCode = XMYYDeptCode, RoleName = "部门负责人", Name = "GroupDeptManager", IsHaveToExsit = true }
            );
        //如果审批层次为3，4，集团分管领导审批
        //集团分管领导审核
        dfInfos.Add(info.ERPApproveLev < 3 ?
            new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "主管副总裁", Name = "GroupViceLeaders", IsHaveToExsit = false }
            : new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "主管副总裁", Name = "GroupViceLeaders", IsHaveToExsit = true });
        //如果审批层次为4，集团CEO审批
        //集团总裁
        dfInfos.Add(info.ERPApproveLev < 4 ?
            new K2_DataFieldInfo() { Result = "noapprovers", RoleName = "总裁", Name = "GroupPresident", IsHaveToExsit = false }
            : new K2_DataFieldInfo() { DeptCode = PKURGICode, RoleName = "总裁", Name = "GroupPresident", IsHaveToExsit = true });
        return dfInfos;
    }
    //测试时先注释掉
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
            string id = _BPMContext.ProcID;
            WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
            var inst = DBContext.GetSysContext().WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == id);
            var contractfinalaccountinfo = DBContext.GetBizContext().ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == inst.FormID);
            erpFormCode = contractfinalaccountinfo.ErpFormId;
        }
        ERP_CallbackResultType resultType = new CommonService(int.Parse(AppID)).NotifyStartAdvance(erpFormCode, false);
        if (resultType != ERP_CallbackResultType.调用成功)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", "alert('" + ConstString.RepeatAlertTip + "'); window.opener=null;window.open('', '_self', '');window.close();", true);
            return false;
        }
        return true;
    }

    //测试时先注释掉
    /// <summary>
    /// 流程成功启动后操作
    /// </summary>
    protected override bool AfterWorkflowStart(int wfInstanceId)
    {
        var info = BizContext.ERP_ContractFinalAccount.FirstOrDefault(x => x.FormID == FormId);
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
        try
        {
            //保存嵌套的表单数据，下载该表单
            IFrameHelper.DownloadLocalFileUrl(_BPMContext.ProcID);
        }
        catch (Exception)
        {

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
}
