using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_Favorites : UPageBase
{
    Focus bll = new Focus();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "制度列表";
        if (!IsPostBack)
        {
            BindDataList();
        }
    }

    private void BindDataList()
    {
        string userCode = CurrentEmployee.EmployeeCode;
        int count = bll.GetRuleFocusCount(userCode);
        this.AspNetPager1.RecordCount = count;
        lblCount.Text = count.ToString();
        List<V_WR_RuleFocus> list = bll.GetRuleFocusList(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, userCode);

        //AspNetPager修复删除最后一页最后一条记录的问题
        if (this.AspNetPager1.CurrentPageIndex > 1 && list.Count == 0)
        {
            this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex - 1;
            BindDataList();
        }
        else
        {
            this.rptRule.DataSource = list;
            this.rptRule.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDataList();
    }

    protected void rptRule_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                bll.Delete("rule", Convert.ToInt16(e.CommandArgument), CurrentEmployee.EmployeeCode);

                BindDataList();
            }
        }
        catch (Exception ex)
        {
            JsHelper.AlertOperationFailure(Page, ex);
        }
    }
}