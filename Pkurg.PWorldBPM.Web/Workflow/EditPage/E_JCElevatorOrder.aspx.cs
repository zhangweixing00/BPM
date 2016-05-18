using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.BPM.Entities;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common.Log;

public partial class Workflow_EditPage_E_JCElevatorOrder : UPageBase
{
    //部门编号都写在web配置里，在这里需要调用
    string PKURGICode = System.Configuration.ConfigurationManager.AppSettings["PKURGICode"];
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];
    string YFSJDeptCode = System.Configuration.ConfigurationManager.AppSettings["YFSJDeptCode"];
    string XMYYDeptCode = System.Configuration.ConfigurationManager.AppSettings["XMYYDeptCode"];


    public string className = "Workflow_EditPage_E_JCElevatorOrder";

    JC_ElevatorOrder jc = new JC_ElevatorOrder();
    WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();
    Order order = new Order();

    protected override void OnPreRender(EventArgs e)
    {
        //防止二次提交
        ScriptManager.RegisterOnSubmitStatement(Page, typeof(Page), "Go_disabled", "disabledButton('lbSave');disabledButton('lbSubmit');disabledButton('lbClose');");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Clear_disabled", "enableButton('lbSave');enableButton('lbSubmit');enableButton('lbClose');", true);

        if (!IsPostBack)
        {
            Countersign1.SetDefault("合约审算部");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string methodName = "Page_Load";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["max"]))
            {
                txtTotalPrice.Text = Request.QueryString["max"];
            }

            InitDepartName();
            if (ddlDepartName.Items.Count < 1)
            {
                RunJs(this.Page, "alert('您没有发起该流程权限！请联系管理员');window.close();");
                return;
            }

            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                WorkFlowInstance info = new WF_WorkFlowInstance().GetWorkFlowInstanceById(Request.QueryString["id"]);
                if (info != null)
                {
                    ViewState["FormID"] = info.FormId;
                    BindFormData();
                }
            }
            else
            {
                tbNumber.Text = BPMHelp.GetSerialNumber("JC_"); ;
                ViewState["FormID"] = tbNumber.Text;
                tbPerson.Text = CurrentEmployee.EmployeeName;
                tbPhone.Text = CurrentEmployee.OfficePhone;
                UpdatedTextBox.Value = DateTime.Now.ToShortDateString();
                if (!string.IsNullOrEmpty(Request.QueryString["ORDERTYPE"]))
                {
                    tbOrderType.Text = Request.QueryString["ORDERTYPE"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ORDERID"]))
                {
                    tbOrderID.Text = Request.QueryString["ORDERID"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["URL"]))
                {
                    tbContent.Text = Request.QueryString["URL"];
                }
                if (!string.IsNullOrEmpty(Request.QueryString["TITLE"]))
                {
                    tbTitle.Text = Request.QueryString["TITLE"];
                }
            }
            string StartDeptId = ddlDepartName.SelectedItem.Value;
            Countersign1.CounterSignDeptId = StartDeptId;
        }
    }

    protected void InitDepartName()
    {
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(_BPMContext.CurrentUser.MainDeptId);
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        Pkurg.PWorld.Entities.TList<Department> deptInfo2 = bfurd.GetDeptListByEmployeeCodeAndRoleName(_BPMContext.CurrentPWordUser.EmployeeCode, "部门成员");

        foreach (Department deptItem in deptInfo2)
        {
            ListItem item = new ListItem()
             {
                 Text = deptItem.Remark,
                 Value = deptItem.DepartCode
             };
            ddlDepartName.Items.Add(item);
        }
        if (deptInfo2.Count == 0)
        {
            ddlDepartName.Items.Add(new ListItem()
            {
                Text = deptInfo.Remark,
                Value = deptInfo.DepartCode
            });
        }
    }

    private void BindFormData()
    {
        string methodName = "BindFormData";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        try
        {
            JC_ElevatorOrderInfo jcInfo = jc.GetElevatorOrder(ViewState["FormID"].ToString());
            cblSecurityLevel.SelectedValue = jcInfo.SecurityLevel.ToString();
            cblUrgentLevel.SelectedValue = jcInfo.UrgenLevel != null ? jcInfo.UrgenLevel.ToString() : "0";
            ListItem item = ddlDepartName.Items.FindByValue(jcInfo.StartDeptCode);
            if (item != null)
            {
                ddlDepartName.SelectedIndex = ddlDepartName.Items.IndexOf(item);
            }

            UpdatedTextBox.Value = ((DateTime)jcInfo.Date).ToString("yyyy-MM-dd");
            tbPerson.Text = jcInfo.UserName;
            tbPhone.Text = jcInfo.Mobile;
            tbTitle.Text = jcInfo.ReportTitle;
            tbOrderID.Text = jcInfo.OrderID.ToString();
            tbOrderType.Text = jcInfo.OrderType.ToString();
            tbNumber.Text = jcInfo.ReportCode;
            tbNote.Text = jcInfo.Note;
            //add 2015-01-14
            txtMaxCost.Text = jcInfo.MaxCost.HasValue ? jcInfo.MaxCost.Value.ToString() : "";

            tbContent.Text = jcInfo.Url;

            WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(jcInfo.FormID);
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.CounterSignDeptId = jcInfo.StartDeptCode;
            #region 审批意见框
            DeptManagerApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            RealateDeptApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            CityCompanyLeaderApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            JCFirstApprovalApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            DesignerApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            ProjectOperatorApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            JCReApprovalApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            PurchaserApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            COOApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            JCMakeOrderApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            JCFinalApprovalApproveOpinion.InstanceId = workFlowInstance.InstanceId;
            #endregion
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
    }

    private JC_ElevatorOrderInfo SaveJCElevatorOrder(string ID, string wfStatus)
    {
        string methodName = "SaveJCElevatorOrder";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);

        JC_ElevatorOrderInfo jcInfo = null;
        try
        {
            jcInfo = jc.GetElevatorOrder(ID);

            bool isEdit = false;
            if (jcInfo == null)
            {
                jcInfo = new JC_ElevatorOrderInfo();
                jcInfo.FormID = ViewState["FormID"].ToString();

                jcInfo.ReportCode = ViewState["FormID"].ToString();

                jcInfo.ApproveStatus = wfStatus;

                jcInfo.CreateByUserCode = CurrentEmployee.EmployeeCode;
                jcInfo.CreateAtTime = DateTime.Now;
                jcInfo.CreateByUserName = CurrentEmployee.EmployeeName;
            }
            else
            {
                isEdit = true;
                jcInfo.FormID = ViewState["FormID"].ToString();
                // jcInfo.OriginalFormId = ViewState["FormID"].ToString ();
                jcInfo.ApproveStatus = wfStatus;
            }
            jcInfo.UpdateByUserCode = CurrentEmployee.EmployeeCode;
            jcInfo.UpdateByUserName = CurrentEmployee.EmployeeName;
            if (cblSecurityLevel.SelectedIndex != -1)
            {
                jcInfo.SecurityLevel = short.Parse(cblSecurityLevel.SelectedValue);
            }
            if (cblUrgentLevel.SelectedIndex != -1)
            {
                jcInfo.UrgenLevel = short.Parse(cblUrgentLevel.SelectedValue);
            }
            DateTime date;
            bool flag1 = DateTime.TryParse(UpdatedTextBox.Value, out date);
            if (flag1)
            {
                jcInfo.Date = date;
            }

            jcInfo.UserName = tbPerson.Text;
            jcInfo.StartDeptCode = ddlDepartName.SelectedItem.Value.ToString();
            jcInfo.DeptName = ddlDepartName.SelectedItem.Text;
            jcInfo.Mobile = tbPhone.Text;
            jcInfo.ReportTitle = tbTitle.Text;
            jcInfo.Note = tbNote.Text;
            //add 2015-01-14
            jcInfo.MaxCost = Convert.ToDecimal(txtMaxCost.Text.Trim());
            jcInfo.OrderType = tbOrderType.Text.ToString();
            jcInfo.OrderID = tbOrderID.Text.ToString();
            if (tbContent.Text.ToString() != "")
            {
                jcInfo.Url = tbContent.Text.ToString();
            }
            else
            {
                jcInfo.Url = "";
            }

            if (!isEdit)
            {
                jc.InsertElevatorOrder(jcInfo);
            }
            else
            {
                jc.UpdateElevatorOrder(jcInfo);
            }

        }
        catch (Exception ex)
        {

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return jcInfo;
    }

    private bool SaveWorkFlowInstance(JC_ElevatorOrderInfo jcInfo, string WfStatus, DateTime? SumitTime, string WfInstanceId)
    {
        string methodName = "SaveWorkFlowInstance";
        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.IN);
        bool result = false;
        WorkFlowInstance workFlowInstance = null;
        try
        {
            workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(jcInfo.FormID);
            bool isEdit = false;
            if (workFlowInstance == null)
            {
                workFlowInstance = new WorkFlowInstance();
                workFlowInstance.InstanceId = Guid.NewGuid().ToString();
                workFlowInstance.CreateDeptCode = CurrentEmployee.DepartCode;
                workFlowInstance.CreateDeptName = CurrentEmployee.DepartName;
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.CreateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.CreateByUserName = CurrentEmployee.EmployeeName;
                workFlowInstance.AppId = "10106";
            }
            else
            {
                workFlowInstance.CreateAtTime = DateTime.Now;
                workFlowInstance.UpdateByUserCode = CurrentEmployee.EmployeeCode;
                workFlowInstance.UpdateByUserName = CurrentEmployee.EmployeeName;
                isEdit = true;
            }
            workFlowInstance.FormId = jcInfo.FormID;
            workFlowInstance.FormTitle = jcInfo.ReportTitle;
            workFlowInstance.WfStatus = WfStatus;
            if (SumitTime != null)
            {
                workFlowInstance.SumitTime = SumitTime;
            }

            if (WfInstanceId != "")
            {
                workFlowInstance.WfInstanceId = WfInstanceId;
            }

            if (!isEdit)
            {
                result = wf_WorkFlowInstance.AddWorkFlowInstance(workFlowInstance);
            }
            else
            {
                result = wf_WorkFlowInstance.UpdateWorkFlowInstance(workFlowInstance);
            }
            Countersign1.ProcId = workFlowInstance.InstanceId;
            Countersign1.SaveData(true);

        }
        catch (Exception ex)
        {

            Logger.Write(this.GetType(), EnumLogLevel.Fatal, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.Exception + ":" + string.Format("Exception={0}", ex));
            throw ex;
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, className + ":" + methodName + ":" + Pkurg.PWorldBPM.Common.Log.MessageType.OUT);
        return result;
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMaxCost.Text.Trim()))
        {
            txtMaxCost.Focus();
            Alert(Page, "目标成本限额 不能为空！");
            return;
        }

        decimal val = 0;
        bool f = decimal.TryParse(txtMaxCost.Text.Trim(), out val);
        if (!f)
        {
            txtMaxCost.Focus();
            Alert(Page, "目标成本限额 不是有效的数值！");
            return;
        }

        //合同总额已超目标成本限额
        if (!string.IsNullOrEmpty(txtTotalPrice.Text.Trim()))
        {
            if (Convert.ToDecimal(txtMaxCost.Text.Trim()) < Convert.ToDecimal(txtTotalPrice.Text))
            {
                if (string.IsNullOrEmpty(tbNote.Text.Trim()))
                {
                    tbNote.Focus();
                    Alert(Page, "订单合同总额已超目标成本限额，请在“备注”中写明理由！");
                    return;
                }
            }
        }
        

        string id = ViewState["FormID"].ToString();
        JC_ElevatorOrderInfo jcInfo = SaveJCElevatorOrder(id, "00");
        if (jcInfo != null)
        {
            if (SaveWorkFlowInstance(jcInfo, "0", null, ""))
            {
                Alert(Page, "保存成功！");
            }

        }
        else
        {
            Alert(Page, "保存失败");
        }
    }

    /// <summary>
    /// 设置流程参数
    /// </summary>
    /// <returns></returns>
    private NameValueCollection SetWFParams()
    {
        NameValueCollection dataFields = new NameValueCollection();

        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();

        //动态获取待定
        string startDeptId = ddlDepartName.SelectedItem.Value;
        Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(startDeptId);
        string CompanyCode = BPMHelp.GetCompanyCodeByDeptID(startDeptId);

        DataTable DeptManager = bfurd.GetSelectRoleUser(startDeptId, "部门负责人");
        DataTable CityCompanyLeader = bfurd.GetSelectRoleUser(CompanyCode, "集采城市公司负责人");
        DataTable JCFirstApproval = bfurd.GetSelectRoleUser(CGDeptCode, "集采初审员");
        DataTable Designer = bfurd.GetSelectRoleUser(YFSJDeptCode, "部门负责人");
        DataTable ProjectOperator = bfurd.GetSelectRoleUser(XMYYDeptCode, "部门负责人");
        DataTable JCReApproval = bfurd.GetSelectRoleUser(CGDeptCode, "集采复审员");
        DataTable Purchaser = bfurd.GetSelectRoleUser(CGDeptCode, "部门负责人");
        DataTable COO = bfurd.GetSelectRoleUser(XMYYDeptCode, "部门负责人");
        DataTable JCMakeOrder = bfurd.GetSelectRoleUser(CGDeptCode, "集采初审员");
        DataTable JCFinalApproval = bfurd.GetSelectRoleUser(CGDeptCode, "集采复审员");

        //绑定datafields
        bool flag = true;//标记datafields内的变量是否均赋值
        if (string.IsNullOrEmpty(tbOrderType.Text.ToString()))
        {
            flag = false;
            Alert(Page, "订单类型不可为空");
        }
        else
        {
            dataFields.Add("OrderType", tbOrderType.Text.ToString());
        }
        if (string.IsNullOrEmpty(tbOrderID.Text.ToString()))
        {
            flag = false;
            Alert(Page, "订单编号不可为空");
        }
        else
        {
            dataFields.Add("OrderID", tbOrderID.Text.ToString());
        }
        //城市公司部门负责人
        if (DeptManager != null && DeptManager.Rows.Count > 0)
        {
            dataFields.Add("DeptManager", "K2:Founder\\" + DeptManager.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "您所在部门负责人 尚未配置！");
        }

        if (string.IsNullOrEmpty(Countersign1.GetCounterSignUsers()))
        {
            flag = false;
            Alert(Page, "相关部门会签必须选择“合约审算部”！");
        }
        else
        {
            dataFields.Add("CounterSignUsers", Countersign1.GetCounterSignUsers());
        }
        //城市公司负责人
        if (CityCompanyLeader != null && CityCompanyLeader.Rows.Count > 0)
        {
            dataFields.Add("CityCompanyLeader", "K2:Founder\\" + CityCompanyLeader.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "您所在公司负责人 尚未配置！");
        }

        //集采初审
        if (JCFirstApproval != null && JCFirstApproval.Rows.Count > 0)
        {
            dataFields.Add("JCFirstApproval", "K2:Founder\\" + JCFirstApproval.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团采购管理部初审 尚未配置！");
        }

        //研发设计
        if (Designer != null && Designer.Rows.Count > 0)
        {
            dataFields.Add("Designer", "K2:Founder\\" + Designer.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团研发设计部负责人 尚未配置！");
        }

        //项目运营
        if (ProjectOperator != null && ProjectOperator.Rows.Count > 0)
        {
            dataFields.Add("ProjectOperator", "K2:Founder\\" + ProjectOperator.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团项目运营部负责人 尚未配置");
        }

        //集采复审
        if (JCReApproval != null && JCReApproval.Rows.Count > 0)
        {
            dataFields.Add("JCReApproval", "K2:Founder\\" + JCReApproval.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团采购管理部复审 尚未配置！");
        }

        //采购负责人
        if (Purchaser != null && Purchaser.Rows.Count > 0)
        {
            dataFields.Add("Purchaser", "K2:Founder\\" + Purchaser.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团采购管理部负责人 尚未配置！");
        }

        //COO
        if (COO != null && COO.Rows.Count > 0)
        {
            dataFields.Add("COO", "K2:Founder\\" + COO.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团COO 尚未配置！");
        }

        //集采下单
        if (JCMakeOrder != null && JCMakeOrder.Rows.Count > 0)
        {
            dataFields.Add("JCMakeOrder", "K2:Founder\\" + JCMakeOrder.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团采购管理部下单 尚未配置！");
        }

        //集采复核
        if (JCFinalApproval != null && JCFinalApproval.Rows.Count > 0)
        {
            dataFields.Add("JCFinalApproval", "K2:Founder\\" + JCFinalApproval.Rows[0]["LoginName"].ToString());
        }
        else
        {
            flag = false;
            Alert(Page, "集团采购管理部复审 尚未配置！");
        }
        if (!flag)
        {
            dataFields = null;
        }
        return dataFields;
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtMaxCost.Text.Trim()))
        {
            txtMaxCost.Focus();
            Alert(Page, "目标成本限额 不能为空！");
            return;
        }
        decimal val = 0;
        bool f = decimal.TryParse(txtMaxCost.Text.Trim(), out val);
        if (!f)
        {
            txtMaxCost.Focus();
            Alert(Page, "目标成本限额 不是有效的数值！");
            return;
        }

        //合同总额已超目标成本限额
        if (!string.IsNullOrEmpty(txtTotalPrice.Text.Trim()))
        {
            if (Convert.ToDecimal(txtMaxCost.Text.Trim()) < Convert.ToDecimal(txtTotalPrice.Text))
            {
                if (string.IsNullOrEmpty(tbNote.Text.Trim()))
                {
                    tbNote.Focus();
                    Alert(Page, "订单合同总额已超目标成本限额，请在“备注”中写明理由！");
                    return;
                }
            }
        }

        #region 工作流参数
        NameValueCollection dataFields = SetWFParams();
        if (dataFields == null)
        {
            return;
        }
        #endregion
        int wfInstanceId = 0; //process instance id
        string id = ViewState["FormID"].ToString();

        //会签数据保存 yanghechun
        //Countersign1.SaveData(true);

        JC_ElevatorOrderInfo jcInfo = SaveJCElevatorOrder(id, "02");
        if (jcInfo != null)
        {
            //Countersign1.SaveAndSubmit();//会签数据保存
            //开启流程
            WorkflowHelper.CurrentUser = "founder\\" + _BPMContext.CurrentUser.LoginId;

            WorkflowHelper.StartProcess(@"K2Workflow\JC_Lift", id, dataFields, ref wfInstanceId);
            if (wfInstanceId > 0)
            {
                if (SaveWorkFlowInstance(jcInfo, "1", DateTime.Now, wfInstanceId.ToString()))
                {

                    if (jc.UpdateStatus(id, "02"))
                    {
                        string Opinion = "";
                        string ApproveResult = "同意";
                        string OpinionType = "";
                        string IsSign = "0";
                        string DelegateUserName = "";
                        string DelegateUserCode = "";
                        WorkFlowInstance workFlowInstance = wf_WorkFlowInstance.GetWorkFlowInstanceByFormId(jcInfo.FormID);
                        //通知集采系统
                        string instanceID = workFlowInstance.InstanceId;
                        string url = "http://" + Request.Url.Authority + "/Workflow/ViewPage/V_JCElevatorOrder.aspx?ID=" + instanceID;

                        try
                        {
                            //和集采的接口
                            order.SubmitWorkFlow(url, Convert.ToInt16(tbOrderType.Text.ToString()), Convert.ToInt16(tbOrderID.Text.ToString()));
                        }
                        catch
                        {
                        }

                        var appRecord = new Pkurg.PWorldBPM.Business.Sys.WF_Approval_Record()
                        {
                             ApprovalID = Guid.NewGuid().ToString(),
                
                            FormID = id,
                            InstanceID = workFlowInstance.InstanceId,
                            Opinion = Opinion,
                            ApproveAtTime = DateTime.Now,
                            ApproveByUserCode = CurrentEmployee.EmployeeCode,
                            ApproveByUserName = CurrentEmployee.EmployeeName,
                            ApproveResult = ApproveResult,
                            OpinionType = OpinionType,
                            CurrentActiveName = "拟稿",
                            ISSign = IsSign,
                
                            DelegateUserName = DelegateUserName,
                            DelegateUserCode = DelegateUserCode,
                            CreateAtTime = DateTime.Now,
                            CreateByUserCode = CurrentEmployee.EmployeeCode,
                            CreateByUserName = CurrentEmployee.EmployeeName,
                            UpdateAtTime = DateTime.Now,
                            UpdateByUserCode = CurrentEmployee.EmployeeCode,
                            UpdateByUserName = CurrentEmployee.EmployeeName,
                            FinishedTime = DateTime.Now
                        };
                        BFApprovalRecord bfApproval = new BFApprovalRecord();
                        bfApproval.AddApprovalRecord(appRecord);
                    }
                }
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "1", "alert('提交成功');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.opener=null; window.open('', '_self', '');window.close();", true);
    }

    protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Countersign1.CounterSignDeptId = ddlDepartName.SelectedItem.Value;
        Countersign1.Refresh();
    }

    void Alert(Page page, object message)
    {
        StringBuilder sb = new StringBuilder();
        //改变鼠标的样式
        string js = string.Format(@"alert('{0}');", message) + sb.ToString();
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    /// <summary>
    /// 执行JS
    /// </summary>
    /// <param name="page"></param>
    /// <param name="js"></param>
    public static void RunJs(Page page, string js)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "ajax", js, true);
    }

    public string GetJCUrl()
    {
        return tbContent.Text.ToString();
    }
}
