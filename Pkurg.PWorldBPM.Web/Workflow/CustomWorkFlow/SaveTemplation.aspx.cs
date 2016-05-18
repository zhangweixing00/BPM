using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public partial class Workflow_CustomWorkFlow_SaveTemplation : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string FormId = Request["FormId"];
            if (string.IsNullOrWhiteSpace(FormId))
            {
                DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", 0));
                return;
            }
            SettingOptions();
        }
    }

    private void SettingOptions()
    {
        var infos = SysContext.Custom_GetWfDeptsByUserCode(_BPMContext.CurrentPWordUser.EmployeeCode).ToList();
        if (infos.Count == 0)
        {
            tr_cbIsOpen.Visible = false;
        }
        else
        {
            tr_cbIsOpen.Visible = true;
        }
    }

    /// <summary>
    /// 当前步骤ID
    /// </summary>
    public long StepID
    {
        get
        {
            object stepIdInfo = ViewState["StepId"];
            if (stepIdInfo != null && !string.IsNullOrWhiteSpace(stepIdInfo.ToString()))
            {
                return long.Parse(stepIdInfo.ToString());
            }
            return -1;
        }
        set { ViewState["StepId"] = value; }
    }

    private void SaveWorkItemDataToTemplation()//Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems itemInfo)
    {
        int result = 0;

        string FormId = Request["FormId"];
        try
        {
            var itemList = CustomWorkflowDataProcess.GetWorkItemsData(FormId);
            if (itemList == null)
            {
                DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));
                return;
            }
            string syncString = string.Empty;
            lock (syncString)
            {
                int maxId = 1;
                if (SysContext.WF_Custom_Templation.Count() > 0)
                {
                    maxId = SysContext.WF_Custom_Templation.Max(x => x.Id) + 1;
                    if (maxId <= 0)
                    {
                        maxId = 1;
                    }
                }

                SysContext.WF_Custom_Templation.InsertOnSubmit(new Pkurg.PWorldBPM.Business.Sys.WF_Custom_Templation()
                {
                    Id = maxId,
                    CreateTime = DateTime.Now,
                    Name = tbStepName.Text,
                    CreateUserID = _BPMContext.CurrentPWordUser.EmployeeCode,
                    CreateUserDeptCode = _BPMContext.CurrentPWordUser.DepartCode,
                    CreateUserName = _BPMContext.CurrentPWordUser.EmployeeName,
                    LastUpdateTime = DateTime.Now,
                    IsOpen = cbIsOpen.Checked ? 1 : 0,
                    RelationDeptCode = GetCheckDept(),
                    Des = tbDes.Text
                });


                long stepId = 1;
                if (SysContext.WF_Custom_TemplationItems.Count() > 0)
                {
                    stepId = SysContext.WF_Custom_TemplationItems.Max(x => x.StepID) + 1;
                }
                foreach (var item in itemList)
                {
                    SysContext.WF_Custom_TemplationItems.InsertOnSubmit(new Pkurg.PWorldBPM.Business.Sys.WF_Custom_TemplationItems()
                    {
                        TemplD = maxId,
                        CreateTime = DateTime.Now,
                        PartUsers = item.PartUsers,
                        OrderId = item.OrderId,
                        StepID = stepId++,
                        StepName = item.StepName,
                        Condition = item.Condition
                    });
                }
                SysContext.SubmitChanges();
            }
            result = 1;
        }
        catch (Exception)
        {
            //
        }
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));

    }

    private string GetCheckDept()
    {
        StringBuilder depts = new StringBuilder();
        if (cbIsOpen.Checked)
        {
            foreach (ListItem item in cblOpenList.Items)
            {
                depts.AppendFormat("{0},", item.Value);
            }
        }
        return depts.ToString().TrimEnd(',');
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        SaveWorkItemDataToTemplation();
    }
    protected void cbIsOpen_CheckedChanged(object sender, EventArgs e)
    {
        if (cbIsOpen.Checked)
        {
            cblOpenList.Visible = true;
            var infos = SysContext.Custom_GetWfDeptsByUserCode(_BPMContext.CurrentPWordUser.EmployeeCode).ToList();
            cblOpenList.DataSource = infos;
            cblOpenList.DataTextField = "DepartName";
            cblOpenList.DataValueField = "DepartCode";
            cblOpenList.DataBind();
        }
        else
        {
            cblOpenList.Visible = false;
        }
    }
}