using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;

public partial class Workflow_CustomWorkFlow_StepOperation : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string FormId = Request["FormId"];
            if (string.IsNullOrWhiteSpace(FormId) && string.IsNullOrWhiteSpace(_BPMContext.ProcID))
            {
                DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", 0));
                return;
            }
            else
            {
                long newId = -1;
                string newIdString = Request["stepId"];
                if (!string.IsNullOrWhiteSpace(newIdString))
                {
                    long.TryParse(newIdString, out newId);
                }
                StepID = newId;
                if (StepID > 0)
                {
                    List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(FormId);
                    if (itemInfos != null)
                    {
                        var currentInfo = itemInfos.FirstOrDefault(x => x.StepID == StepID);
                        if (currentInfo == null)
                        {
                            DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", 0));
                            return;
                        }
                        tbStepName.Text = currentInfo.StepName;
                        ShowUserNames1.UserList = currentInfo.PartUsers;
                    }
                }

                Refresh("", "");
            }

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

    private void SaveWorkItemData()//Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems itemInfo)
    {
        //if (string.IsNullOrEmpty(ShowUserNames1.UserList))
        //{
        //    DisplayMessage.ExecuteJs("alert('没有设置审批人员！')");
        //    return;
        //}
        
        string FormId = Request["FormId"];
        List<Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems> itemInfos = CustomWorkflowDataProcess.GetWorkItemsData(FormId);
        if (itemInfos != null && itemInfos.Exists(x => x.StepName.ToLower() == tbStepName.Text))
        {
            DisplayMessage.ExecuteJs("alert('该步骤名称与已有步骤重名，请更换步骤名称！')");
            return;
        }

        int result = 0;

        try
        {
            CustomWorkflowDataProcess.SaveWorkItemData(new Pkurg.PWorldBPM.Business.Sys.WF_Custom_InstanceItems()
            {
                StepID = StepID,
                StepName = tbStepName.Text,
                PartUsers = ShowUserNames1.UserList
            },
            FormId);
            result = 1;
        }
        catch (Exception)
        {
            //
        }
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));

    }


    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        int result = 0;
        try
        {
            string userDomainName = e.CommandArgument.ToString();
            ShowUserNames1.AddUser(userDomainName);

        }
        catch (Exception)
        {

        }
        //DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Refresh(tbUserName.Text, tbLoginName.Text);
    }

    private void Refresh(string userName, string loginName)
    {
        BFPmsUserRoleDepartment ur = new BFPmsUserRoleDepartment();
        IList<VEmployeeAndAdditionalInfo> ds = ur.GetNotSelectRoleUser("0");
        string DeptID = _BPMContext.CurrentUser.MainDeptId;
        string[] array = DeptID.Split('-');
        if (!(array[0] == "S363" || array[0] == "S366"))
        {
            ds = ds.Where(x => x.DepartCode.Contains(array[0])).ToList();
        }

        string deptIdString = Request["deptid"];
        if (string.IsNullOrEmpty(deptIdString) || hd_SelectType.Value == "1")
        {
            //加签
        }
        else
        {
            //部门内处理
            BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
            DataTable dtUsers = bfurd.GetSelectRoleUser(deptIdString, "部门成员");


            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                ds = new List<VEmployeeAndAdditionalInfo>();
                foreach (DataRow item in dtUsers.Rows)
                {
                    ds.Add(new VEmployeeAndAdditionalInfo()
                    {
                        LoginName = item["LoginName"].ToString(),
                        DepartName = item["DepartName"].ToString(),
                        EmployeeName = item["EmployeeName"].ToString()
                    });
                }
                if (dtUsers.Rows.Count <= 0)
                {
                    ds = ds.Where(x => x.DepartCode == deptIdString).ToList();
                }
            }
            else
            {
                ds = ds.Where(x => x.DepartCode == deptIdString).ToList();
            }

            //gvData.DataSource = dtUsers;
            //gvData.DataBind();
            //return;
        }

        if (!string.IsNullOrEmpty(userName))
        {
            ds = ds.Where(x => x.EmployeeName.Contains(userName)).ToList();
        }
        if (!string.IsNullOrEmpty(loginName))
        {
            ds = ds.Where(x => x.LoginName.Contains(loginName)).ToList();
        }
        //ds = ds.Where(x => x.OrderNo.HasValue && x.OrderNo.Value > 5).ToList();
        gvData.DataSource = ds.OrderBy(x => x.OrderNo.HasValue && x.OrderNo > 0 ? x.OrderNo.Value : int.MaxValue).ToList();
        gvData.DataBind();
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        Refresh(tbUserName.Text, tbLoginName.Text);
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        SaveWorkItemData();
    }
}