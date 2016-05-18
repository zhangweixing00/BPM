using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.BIZ;

[BPM(AppId = "3014")]
public partial class Workflow_EditPage_E_OA_ITSupport
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("OA_IT_");
                FormTitle = SysContext.WF_AppDict.FirstOrDefault(x => x.AppId == AppID).AppName;
                StartDeptId = _BPMContext.CurrentPWordUser.DepartCode;

            }
            else
            {
                Pkurg.PWorldBPM.Business.Sys.WF_WorkFlowInstance info = SysContext.WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == instId);
                FormId = info.FormID;
                FormTitle = info.FormTitle;
            }

            InitFormData();

            tbPhone.Text = _BPMContext.CurrentPWordUser.MobilePhone;
            tbPerson.Text = _BPMContext.CurrentPWordUser.EmployeeName;
            tbDate.Text = DateTime.Now.ToShortDateString();
            tbCompany.Text = _BPMContext.CurrentPWordUser.CompanyName;
            tbEmail.Text = _BPMContext.CurrentPWordUser.Email;
            tbDepartName.Text = _BPMContext.CurrentPWordUser.DepartName;
        }
        else
        {

        }

    }

    //private void InitStartDeptment()
    //{
    //    ddlDepartName.DataSource = GetStartDeptmentDataSource();
    //    ddlDepartName.DataTextField = "Remark";
    //    ddlDepartName.DataValueField = "DepartCode";
    //    ddlDepartName.DataBind();
    //}

    protected override void InitFormData()
    {
        try
        {
            /**/
            LoadQuestions();
            LoadTypes();

            var info = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbContent.Text = info.ContentTxt;

                //ListItem li = ddlDepartName.Items.FindByValue(info.DeptCode);
                //if (li != null)
                //{
                //    li.Selected = true;
                //}
                if (info.QuestionId.HasValue)
                {
                    ListItem li = ddlQuestions.Items.FindByValue(info.QuestionId.ToString());
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }
                if (info.STypeId.HasValue)
                {
                    ListItem li = ddlTypes.Items.FindByValue(info.STypeId.ToString());
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }
            }
            else
            {
                string defaultTypeString = Request["type"];
                //int defaultType;
                if (!string.IsNullOrWhiteSpace(defaultTypeString))// && int.TryParse(defaultTypeString, out defaultType))
                {
                    ListItem li = ddlTypes.Items.FindByValue(defaultTypeString);
                    if (li != null)
                    {
                        li.Selected = true;
                    }
                }
            }



            //if (info != null && info.STypeId.HasValue)
            //{
            //    ListItem li = ddlQuestions.Items.FindByValue(info.STypeId.ToString());
            //    if (li != null)
            //    {
            //        li.Selected = true;
            //    }
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void SaveFormData()
    {
        try
        {
            var info = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
            if (info == null)
            {
                info = new OA_ITSupport_Form()
                {
                    FormID = FormId,
                    CompanyName = tbCompany.Text,
                    Email = tbEmail.Text,
                    DateTime = DateTime.Now,
                    DeptCode = _BPMContext.CurrentPWordUser.DepartCode,
                    DeptName = _BPMContext.CurrentPWordUser.DepartName,
                    Mobile = tbPhone.Text,
                    UserName = _BPMContext.CurrentPWordUser.EmployeeName
                };
                BizContext.OA_ITSupport_Form.InsertOnSubmit(info);
            }

            info.STypeId = int.Parse(ddlTypes.SelectedValue);
            info.QuestionId = int.Parse(ddlQuestions.SelectedValue);
            info.ContentTxt = tbContent.Text;
            info.Title = GetFormTitle();


            BizContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected override string GetFormTitle()
    {
        string typeString = "";
        string titleFormat = "{0}提出了{1}问题";
        if (ddlQuestions.SelectedIndex > 0)
        {
            typeString = ddlQuestions.SelectedItem.Text;
        }
        else if (ddlTypes.SelectedIndex > 0)//
        {
            typeString = ddlTypes.SelectedItem.Text;
        }

        return string.Format(titleFormat, _BPMContext.CurrentPWordUser.EmployeeName, typeString);
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
        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        dfInfos.Add(new K2_DataFieldInfo()
        {
            Result = ITSupportCommon.GetK2DataFieldByType(ddlTypes.SelectedValue),
            Name = "GroupUsers"
        });
        return dfInfos;
    }

    /// <summary>
    /// 流程发起前操作
    /// </summary>
    /// <returns></returns>
    protected override bool BeforeWorkflowStart()
    {
        return true;
    }

    /// <summary>
    /// 流程成功启动后操作
    /// </summary>
    protected override bool AfterWorkflowStart(int wfInstanceId)
    {
        string stepId = Guid.NewGuid().ToString();
        BizContext.OA_ITSupport_Step.InsertOnSubmit(new OA_ITSupport_Step()
        {
            Id = stepId,
           FormID = FormId,
            InstanceId = _BPMContext.ProcID,
            StartTime = DateTime.Now,
            StartType = (int)ITSupportStatus.待领取,
            OrderId = 1,
            ProcessGroupId = BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id.ToString() == ddlTypes.SelectedValue).GroupId.Value.ToString()
        });
        foreach (var item in ITSupportCommon.GetUserListByType(ddlTypes.SelectedValue))
        {
            BizContext.OA_ITSupport_Step_Users.InsertOnSubmit(new OA_ITSupport_Step_Users()
            {
                StepId = stepId,
                UserCode = item.EmployeeCode,
                LoginName = item.LoginName
            });
            NotifyToUser(item.LoginName);
        }
        var formInfo = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == FormId);
        if (formInfo != null)
        {
            formInfo.CurrentStepId = stepId;
            formInfo.ProcessGroupId = BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id.ToString() == ddlTypes.SelectedValue).GroupId.Value.ToString();
        }

        BizContext.SubmitChanges();
        return true;
    }

    private void NotifyToUser(string user)
    {
        try
        {
            string notifyString = string.Format("有支持单待领取：{0}", GetFormTitle());
            new WebServiceInvokeHelper_IT().Invoke("http://172.25.20.43:3553/Service.asmx", "Notify", "Service",
                new string[] { user, notifyString });
        }
        catch (Exception)
        {


        }
    }

    #region 一个临时类WebServiceInvokeHelper_IT，appcode更新后可以替换

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "AgentServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class WebServiceInvokeHelper_IT : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        public WebServiceInvokeHelper_IT(string url = "")
        {
            this.Url = string.IsNullOrEmpty(url) ? "http://172.25.20.43:5001/AgentService.asmx" : url;
        }

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Invoke", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object[] Invoke(string url, string methodName, string className, object[] args)
        {
            try
            {
                return this.Invoke("Invoke", new object[] { url, methodName, className, args });
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    #endregion

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Save_Click(object sender, EventArgs e)
    {
        if (ddlTypes.SelectedIndex <= 0)
        {
            Alert("请选择系统名称");
            return;
        }
        Save();

        Alert("保存完成");
    }

    /// <summary>
    /// 每次保存后都会触发
    /// </summary>
    protected override void AfterSaveInstance()
    {
    }
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (ddlTypes.SelectedIndex <= 0)
        {
            Alert("请选择系统名称");
            return;
        }

        var instanceInfo = SysContext.WF_WorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
        if (instanceInfo != null && instanceInfo.WFStatus != "0")
        {
            Alert("流程已发起,请关闭页面");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ajax", " window.close();", true);
            return;
        }

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

    //protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    StartDeptId = ddlDepartName.SelectedItem.Value;
    //}


    protected void ddlQuestions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQuestions.SelectedIndex == -1)
            return;
        var qInfo = BizContext.V_ITSupport_Question.FirstOrDefault(x => x.Id.ToString() == ddlQuestions.SelectedValue);
        var tInfo = BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id == qInfo.CataId.Value);
        if (tInfo != null)
        {
            ListItem tListItem = ddlTypes.Items.FindByValue(tInfo.Id.ToString());
            ddlTypes.SelectedIndex = ddlTypes.Items.IndexOf(tListItem);
        }
    }

    private void LoadQuestions()
    {
        var qList = BizContext.V_ITSupport_Question.ToList();
        qList.Insert(0, new V_ITSupport_Question()
        {
            Id = -1,
            QuestionKey = "--请选择--"
        });

        ddlQuestions.DataSource = qList;
        ddlQuestions.DataTextField = "QuestionKey";
        ddlQuestions.DataValueField = "Id";
        ddlQuestions.DataBind();

    }

    private void LoadTypes()
    {
        List<Pkurg.PWorldBPM.Business.BIZ.V_ITSupport_Catalog> tList = new List<V_ITSupport_Catalog>();
        tList = BizContext.V_ITSupport_Catalog.ToList();
        tList.Insert(0, new V_ITSupport_Catalog()
        {
            Id = -1,
            Name = "--请选择--"
        });

        //else
        //{
        //    tList = BizContext.V_ITSupport_Catalog.Where(x => x.Id.ToString() == ddlQuestions.SelectedValue).ToList();
        //}
        ddlTypes.DataSource = tList;
        ddlTypes.DataTextField = "Name";
        ddlTypes.DataValueField = "Id";
        ddlTypes.DataBind();


    }
    protected void ddlTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlQuestions.SelectedIndex = -1;
    }


}
