using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Sys;

[BPM(AppId = "3011")]
public partial class Workflow_EditPage_E_OA_CustomWorkflow
    : E_WorkflowFormBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InitStartDeptment();

            string instId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(instId))
            {
                FormId = BPMHelp.GetSerialNumber("OA_Custom_");
                FormTitle = SysContext.WF_AppDict.FirstOrDefault(x => x.AppId == AppID).AppName;
                StartDeptId = ddlDepartName.SelectedItem.Value;

            }
            else
            {
                Pkurg.PWorldBPM.Business.Sys.WF_WorkFlowInstance info = SysContext.WF_WorkFlowInstance.FirstOrDefault(x => x.InstanceID == instId);
                FormId = info.FormID;
                FormTitle = info.FormTitle;

                InitFormData();

            }

            tbPhone.Text = _BPMContext.CurrentPWordUser.MobilePhone;
            tbPerson.Text = _BPMContext.CurrentPWordUser.EmployeeName;
            tbDate.Text = DateTime.Now.ToShortDateString();

        }
        else
        {

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
            /**/
            CustomWorkflowDataProcess.LoadWorkItemsFromDBToSession(FormId, _BPMContext.ProcID);
            LoadWorkItemsData();
            var info = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
            if (info != null)
            {
                tbTheme.Text = info.Title;
                tbContent.Text = info.ContentTxt;

                ListItem li = ddlDepartName.Items.FindByValue(info.DeptCode);
                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void LoadWorkItemsData()
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(FormId);
        if (itemInfos != null)
        {
            rptList.DataSource = itemInfos;
            rptList.DataBind();
            Div_NoStepsTip.Visible = false;
        }
        else
        {
            Div_NoStepsTip.Visible = true;
        }
    }

    private bool SaveWorkItemsData(string instId)
    {
        string sessionKey = string.Format("Steps_{0}", FormId);
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;
        if (itemInfos != null)
        {
            foreach (var item in itemInfos)
            {
                item.InstancelD = instId;
            }
            SysContext.WF_Custom_InstanceItems.DeleteAllOnSubmit(SysContext.WF_Custom_InstanceItems.Where(x => x.InstancelD == instId));
            SysContext.WF_Custom_InstanceItems.InsertAllOnSubmit(itemInfos);
            SysContext.SubmitChanges();

            //失效
            //Session[sessionKey] = null;
            //LoadWorkItemsData();
            return true;
        }
        return false;

    }

    //[Obsolete]
    //private void DeleteWorkItemData(long id)
    //{
    //    List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = null;

    //    if (string.IsNullOrWhiteSpace(_BPMContext.ProcID))
    //    {
    //        ///流程未保存，取自session，用session数据驱动界面，保存时直接保存session,无需取客户端
    //        string sessionKey = string.Format("Steps_{0}", FormId);
    //        itemInfos = Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;
    //        var itemInfo = itemInfos.FirstOrDefault(x => x.StepID == id);
    //        if (itemInfo != null)
    //        {
    //            itemInfos.Remove(itemInfo);
    //        }
    //    }
    //    else
    //    {
    //        //取自数据库
    //        var itemInfo = SysContext.WF_Custom_InstanceItems.FirstOrDefault(x => x.StepID == id);
    //        if (itemInfo != null)
    //        {
    //            SysContext.WF_Custom_InstanceItems.DeleteOnSubmit(itemInfo);
    //            SysContext.SubmitChanges();
    //        }
    //    }
    //    LoadWorkItemsData();
    //}

    private void DeleteWorkItemData(long id)
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = null;

        string sessionKey = string.Format("Steps_{0}", FormId);
        itemInfos = Session[sessionKey] as List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems>;
        var itemInfo = itemInfos.FirstOrDefault(x => x.StepID == id);
        if (itemInfo != null)
        {
            itemInfos.Remove(itemInfo);
        }

        LoadWorkItemsData();
    }
    protected override void SaveFormData()
    {
        FormTitle = tbTheme.Text;
        try
        {
            var info = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
            string TempTitle = "";
            if (!string.IsNullOrEmpty(lbFormTitle.Text))
            {
                TempTitle = lbFormTitle.Text;
            }
            else
            {
                TempTitle = tbTheme.Text;
            }
            if (info == null)
            {
                info = new OA_CustomWorkFlowInstance()
                {
                    FormID = FormId,
                    Title = tbTheme.Text,
                    ContentTxt = tbContent.Text,
                    CurrentStepId = 1,
                    DateTime = DateTime.Now,
                    DeptCode = ddlDepartName.SelectedItem.Value,
                    DeptName = ddlDepartName.SelectedItem.Text,
                    Mobile = _BPMContext.CurrentPWordUser.MobilePhone,
                    UserName = _BPMContext.CurrentPWordUser.EmployeeName,     
                    SecurityLevel=TempTitle
                };
                BizContext.OA_CustomWorkFlowInstance.InsertOnSubmit(info);
            }
            else
            {

                info.ContentTxt = tbContent.Text;
                info.Title = tbTheme.Text;
            }

            BizContext.SubmitChanges();
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


        List<WF_Custom_InstanceItems> steps = SysContext.WF_Custom_InstanceItems.Where(x => x.InstancelD == _BPMContext.ProcID).ToList();
        dataFields.Add("Steps", steps.ToWFC_XML());

        return dataFields;
    }
    /// <summary>
    /// 设置用户DataField
    /// </summary>
    /// <returns></returns>
    protected override List<K2_DataFieldInfo> LoadUserDataField()
    {
        List<K2_DataFieldInfo> dfInfos = new List<K2_DataFieldInfo>();
        return dfInfos;
    }

    /// <summary>
    /// 流程发起前操作
    /// </summary>
    /// <returns></returns>
    protected override bool BeforeWorkflowStart()
    {
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(FormId);

        if (itemInfos == null || itemInfos.Count == 0)
        {
            Alert("没有设置流程步骤！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 流程成功启动后操作
    /// </summary>
    protected override bool AfterWorkflowStart(int wfInstanceId)
    {
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
    protected override void AfterSaveInstance()
    {
        bool isSaveSuccess = SaveWorkItemsData(_BPMContext.ProcID);
        if (!isSaveSuccess)
        {
            Alert("没有步骤数据，请先设置步骤再保存！");
            return;
        }
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(FormId);

        var info = BizContext.OA_CustomWorkFlowInstance.FirstOrDefault(x => x.FormID == FormId);
        if (info != null)
        {
            var firstDept=itemInfos.Where(x=>!string.IsNullOrEmpty(x.PartUsers)).OrderBy(x => x.OrderId).FirstOrDefault();
            info.CurrentStepId = firstDept == null ? -1 : firstDept.StepID;

        }
        BizContext.SubmitChanges();
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
    }
    protected void lbtnAddStep_Click(object sender, EventArgs e)
    {
        LoadWorkItemsData();
    }
    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "EDIT":
                LoadWorkItemsData();
                break;
            case "Delete":
                DeleteWorkItemData(long.Parse(e.CommandArgument.ToString()));
                break;
            default:
                break;
        }
    }
    protected void lbtnSelectTemplation_Click(object sender, EventArgs e)
    {
        string sessionKey = string.Format("Steps_{0}", FormId);
        if (HttpContext.Current.Session[sessionKey + "Name"] != null)
        {
            lbFormTitle.Text = HttpContext.Current.Session[sessionKey + "Name"].ToString();
        }
        if (HttpContext.Current.Session[sessionKey + "Des"]!=null)
        {
            string des = HttpContext.Current.Session[sessionKey + "Des"].ToString();
            if (!string.IsNullOrWhiteSpace(des))
            {
                Div_des.Visible = true;
                tbContent.Text = des;
            }
            else
            {
                Div_des.Visible = false;
            }
        }
        LoadWorkItemsData();
    }

}
