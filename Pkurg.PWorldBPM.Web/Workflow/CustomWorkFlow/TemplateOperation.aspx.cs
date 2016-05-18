using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.Sys;

public partial class Workflow_CustomWorkFlow_TemplateOperation : UPageBase
{
    /// <summary>
    /// 共有流程1，私有0
    /// </summary>
    public string CurrentSearchType
    {
        get
        {
            var currentSearchType = ViewState["CurrentSearchType"];
            if (currentSearchType == null)
            {
                return "1";
            }
            return currentSearchType.ToString();
        }
        set { ViewState["CurrentSearchType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //var templates = new List<WF_Custom_Templation>();
        LoadData();
    }

    private void LoadData()
    {
        IQueryable<WF_Custom_Templation> templates = null;
        if (CurrentSearchType == "1")
        {
            templates = SysContext.WF_Custom_Templation.Where(x => x.IsOpen == 1 && x.RelationDeptCode.Contains(_BPMContext.CurrentPWordUser.CompanyCode));
        }
        else
        {
            templates = SysContext.WF_Custom_Templation.Where(x => x.IsOpen == 0 && x.CreateUserID == _BPMContext.CurrentPWordUser.EmployeeCode);
        }
        gvData.DataSource = FilterTemplates(templates);
        gvData.DataBind();
    }

    private List<WF_Custom_Templation> FilterTemplates(IQueryable<WF_Custom_Templation> templates)
    {
        string tName = tbSearchName.Text;
        if (!string.IsNullOrWhiteSpace(tName))
        {
            templates = templates.Where(x => x.Name.Contains(tName));
        }
        string dateText = tbDate.Text;
        if (!string.IsNullOrWhiteSpace(dateText))
        {
            
            DateTime createDate=DateTime.Now;
            if (DateTime.TryParse(dateText, out createDate))
            {
                templates = templates.Where(x => x.CreateTime.Value.Date==createDate.Date);
            }
        }

        return templates.ToList();
    }

    public void ChangeColor(Button button)
    {
        if (button.ID=="lbtnCommon")
        {
            lbtnCommon.CssClass = "tabActive";
            lbtnSelf.CssClass = "tabUnActive";
        }
        else
        {
            lbtnCommon.CssClass = "tabUnActive";
            lbtnSelf.CssClass = "tabActive";
        }
    }

    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        string FormId = Request["FormId"];
        if (string.IsNullOrWhiteSpace(FormId))
        {
            DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", 0));
            return;
        }
        CustomWorkflowDataProcess.SaveSessionDataToTemplation(FormId, e.CommandArgument.ToString());
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", 1));

    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void lbtnCommon_Click(object sender, EventArgs e)
    {
        CurrentSearchType = "1";
        LoadData();
        ChangeColor(sender as Button);
    }
    protected void lbtnSelf_Click(object sender, EventArgs e)
    {
        CurrentSearchType = "0";
        LoadData();
        ChangeColor(sender as Button);
    }
    protected void lbDel_Command(object sender, CommandEventArgs e)
    {
        SysContext.WF_Custom_TemplationItems.DeleteAllOnSubmit(
            SysContext.WF_Custom_TemplationItems.Where(x=>x.TemplD.ToString()==e.CommandArgument.ToString()).ToList());
        SysContext.WF_Custom_Templation.DeleteOnSubmit(
            SysContext.WF_Custom_Templation.Where(x=>x.Id.ToString()==e.CommandArgument.ToString()).FirstOrDefault());

        SysContext.SubmitChanges();
        LoadData();
        
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lbDel = (LinkButton)e.Row.FindControl("lbDel");

        var infos = SysContext.Custom_GetWfDeptsByUserCode(_BPMContext.CurrentPWordUser.EmployeeCode).ToList();
        if (infos.Count == 0)
        {
            lbDel.Visible = false;
        }
        else
        {
            lbDel.Visible = true;
        }
    }

    protected bool LinkButtonVisible
    {
        get
        {
            return SysContext.Custom_GetWfDeptsByUserCode(_BPMContext.CurrentPWordUser.EmployeeCode).ToList().Count > 0;
        }
    }
}