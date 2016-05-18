using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Common.Log;
using SourceCode.Workflow.Client;
using SourceCode.Workflow.Management;

public partial class Sys_EditDataField : UPageBase
{
    readonly string K2ServerName = ConfigurationManager.AppSettings["K2ServerName"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["procInstID"]))
            {
                int procInstID = 0;
                bool flag = int.TryParse(Request.QueryString["procInstID"], out procInstID);
                if (flag)
                {
                    lblId.Text = procInstID.ToString();
                    lblTitle.Text = " - " + Request.QueryString["FormID"];
                    lblTitle2.Text = " - " + Request.QueryString["FormID"];
                    LoadDataFields(procInstID);
                    LoadSteps(procInstID);
                }
            }
        }
    }

    #region 更新DataFields

    /// <summary>
    /// 绑定默认DataFields
    /// </summary>
    /// <param name="procInstID"></param>
    private void LoadDataFields(int procInstID)
    {
        List<BPMListItem> items = new List<BPMListItem>();
        Connection k2Conn = new Connection();
        try
        {
            k2Conn.Open(K2ServerName, WorkflowHelper.GetConnString());
            SourceCode.Workflow.Client.ProcessInstance inst = k2Conn.OpenProcessInstance(procInstID);
            if (inst != null)
            {
                foreach (DataField df in inst.DataFields)
                {
                    items.Add(new BPMListItem { Name = df.Name, Value = df.Value.ToString() });
                }
                rptList.DataSource = items;
                rptList.DataBind();

                k2Conn.Close();
            }
            else
            {
                k2Conn.Close();
            }
        }
        catch (Exception ex)
        {
            k2Conn.Close();
            lblException.Text = "异常信息:" + ex.Message;
            lblException.Visible = true;
        }
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dicts = new Dictionary<string, string>();
        foreach (RepeaterItem item in rptList.Items)
        {
            Label lblName = (Label)item.FindControl("lblName");
            Label lblOldValue = (Label)item.FindControl("lblOldValue");
            TextBox txtNewValue = (TextBox)item.FindControl("txtNewValue");
            dicts.Add(lblName.Text, txtNewValue.Text.Trim());
        }

        UpdateDataFields(Convert.ToInt32(lblId.Text), dicts);
        Logger.Write(this.GetType(), EnumLogLevel.Info, string.Format("***更新DataField成功,Current User=" + Employee_Name));
        BPMHelp.InsertInstanceLog("修改审批人", lblTitle.Text.Replace("-", "").Trim(), "", Employee_Name + " - " + CurrentEmployee.EmployeeCode, HttpContext.Current.User.Identity.Name.Replace("founder\\", ""));
        lblMsg.Text = "保存成功";
        //Response.Redirect("ProcessesManage_List.aspx", false);
    }

    public void UpdateDataFields(int procInstID, Dictionary<string, string> dict)
    {
        Connection k2Conn = new Connection();

        try
        {
            k2Conn.Open(K2ServerName, WorkflowHelper.GetConnString());

            SourceCode.Workflow.Client.ProcessInstance inst = k2Conn.OpenProcessInstance(procInstID);
            if (inst != null)
            {
                foreach (string s in dict.Keys)
                {
                    inst.DataFields[s].Value = dict[s];
                }
            }
            else
            {
                Response.Redirect("ProcessesManage_List.aspx", false);
            }
            inst.Update();

            k2Conn.Close();
        }
        catch
        {
            k2Conn.Close();
        }
    }

    #endregion

    #region 流程跳转

    /// <summary>
    /// 绑定流程步骤
    /// </summary>
    /// <param name="procInstID"></param>
    private void LoadSteps(int procInstID)
    {
        List<StepListItem> items = new List<StepListItem>();

        WorkflowManagementServer svr = new WorkflowManagementServer();
        svr.CreateConnection();
        svr.Connection.Open(WorkflowHelper.GetConnString4Management());
        Activities activities = svr.GetProcInstActivities(procInstID);

        foreach (Activity activity in activities)
        {
            items.Add(new StepListItem { Name = activity.Name, ID = activity.ID });
        }

        rbtnListSteps.DataSource = items;
        rbtnListSteps.DataValueField = "ID";
        rbtnListSteps.DataTextField = "Name";
        rbtnListSteps.DataBind();

        svr.Connection.Close();
    }

    protected void lbtnSave2_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(rbtnListSteps.SelectedValue))
        {
            GoToActitvy(Convert.ToInt32(lblId.Text), rbtnListSteps.SelectedItem.Text);
            new Pkurg.PWorldBPM.Business.Controls.Management().GoToActitvy(Convert.ToInt32(lblId.Text), rbtnListSteps.SelectedValue, rbtnListSteps.SelectedItem.Text);

            BPMHelp.InsertInstanceLog("流程跳转 ", lblTitle2.Text.Replace("-", "").Trim(), string.Format("activityId={0},activityName={1}", rbtnListSteps.SelectedValue, rbtnListSteps.SelectedItem.Text),
                Employee_Name + " - " + CurrentEmployee.EmployeeCode, HttpContext.Current.User.Identity.Name.Replace("founder\\", ""));


            lblMsg2.Text = "保存成功";
        }
    }

    /// <summary>
    /// 跳转
    /// </summary>
    /// <param name="procInstID"></param>
    /// <param name="activityName"></param>
    /// <returns></returns>
    bool GoToActitvy(int procInstID, string activityName)
    {
        WorkflowManagementServer svr = new WorkflowManagementServer();
        svr.CreateConnection();
        svr.Connection.Open(WorkflowHelper.GetConnString4Management());
        bool flag = svr.GotoActivity(procInstID, activityName);
        svr.Connection.Close();
        return flag;
    }

    #endregion

    #region 强制终止

    protected void lbtnStop_Click(object sender, EventArgs e)
    {
        Stop();

        lblMsg3.Text = "终止成功";
    }

    void Stop()
    {
        int procInstID = Convert.ToInt32(lblId.Text);

        bool isExist = ExistprocInst(procInstID);
        //1-停止K2流程
        WorkflowManagementServer svr = new WorkflowManagementServer();
        svr.CreateConnection();
        svr.Connection.Open(WorkflowHelper.GetConnString4Management());
        if (isExist)
        {
            try
            {
                bool flag = svr.StopProcessInstances(procInstID);
            }
            catch (Exception ex)
            {
                Logger.Write(this.GetType(), EnumLogLevel.Info, "*****StopProcessInstances:" + ex.Message);
            }
        }
        svr.Connection.Close();

        bool isExistDataField = ExistDataField(procInstID);
        //2-更新K2的ispass=2;
        if (isExistDataField)
        {
            try
            {
                NameValueCollection dataFields = new NameValueCollection();
                dataFields.Add("IsPass", "2");
                WorkflowHelper.UpdateDataFields(procInstID.ToString(), dataFields, "founder\\zybpmadmin");
            }
            catch (Exception ex)
            {
                Logger.Write(this.GetType(), EnumLogLevel.Info, "*****UpdateDataFields:" + ex.Message);
            }
        }
        //3-更新实例状态为5
        new Pkurg.PWorldBPM.Business.Controls.Management().StopActitvy(procInstID);

        //4-调用业务系统更新状态
        int k2Sn = procInstID;
        string instanceID = "";
        string formId = "";
        string appId = "";
        DataTable dt = new Pkurg.PWorldBPM.Business.Controls.Management().GetFlowInstance(k2Sn);
        if (dt != null && dt.Rows.Count > 0)
        {
            instanceID = dt.Rows[0]["InstanceID"].ToString();
            formId = dt.Rows[0]["FormID"].ToString();
            appId = dt.Rows[0]["AppID"].ToString();
        }

        try
        {
            new Invoke().StopWorkFlow(k2Sn, instanceID, formId, appId);
        }
        catch (Exception ex)
        {
            Logger.Write(this.GetType(), EnumLogLevel.Info, "*****StopActitvy:" + ex.Message);
        }

        Logger.Write(this.GetType(), EnumLogLevel.Info, "*****终止流程操作结束");

        BPMHelp.InsertInstanceLog("终止流程", formId.Replace("-","").Trim(), "", Employee_Name + " - " + CurrentEmployee.EmployeeCode, HttpContext.Current.User.Identity.Name.Replace("founder\\", ""));
    }

    /// <summary>
    /// 是否存在实例
    /// </summary>
    /// <param name="procInstID"></param>
    /// <returns></returns>
    bool ExistprocInst(int procInstID)
    {
        Connection k2Conn = new Connection();
        try
        {
            k2Conn.Open(K2ServerName, WorkflowHelper.GetConnString());
            SourceCode.Workflow.Client.ProcessInstance inst = k2Conn.OpenProcessInstance(procInstID);
            k2Conn.Close();
            return inst != null;
        }
        catch
        {
            k2Conn.Close();
            return false;
        }
    }
    /// <summary>
    /// 是否DataField
    /// </summary>
    /// <param name="procInstID"></param>
    /// <returns></returns>
    bool ExistDataField(int procInstID)
    {
        bool flag = false;
        Connection k2Conn = new Connection();
        try
        {
            k2Conn.Open(K2ServerName, WorkflowHelper.GetConnString());
            SourceCode.Workflow.Client.ProcessInstance inst = k2Conn.OpenProcessInstance(procInstID);
            if (inst != null)
            {
                foreach (DataField item in inst.DataFields)
                {
                    if (item.Name == "IsPass")
                    {
                        flag = true;
                        break;
                    }
                }
            }
            k2Conn.Close();
        }
        catch
        {
            k2Conn.Close();
        }
        return flag;
    }

    #endregion

    string Employee_Name
    {
        get
        {
            if (CurrentEmployee != null)
            {
                return CurrentEmployee.EmployeeName;
            }
            return "";
        }
    }
}

/// <summary>
/// 自定义类
/// </summary>
public class BPMListItem
{
    public string Name { get; set; }
    public string Value { get; set; }
}

/// <summary>
/// 自定义类
/// </summary>
public class StepListItem
{
    public string Name { get; set; }
    public int ID { get; set; }
}