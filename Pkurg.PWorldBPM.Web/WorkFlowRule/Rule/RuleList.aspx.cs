using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_Rule_RuleList : UPageBase
{
    Rule bll = new Rule();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "制度列表";
        if (!IsPostBack)
        {
            BindCategory();
            BindDataList();
        }
    }

    private void BindCategory()
    {
        List<WR_Category> items = bll.GetCategoryList(GetEmployee().LoginId, IsRuleAdmin);
        ddlCategory.DataSource = items;
        ddlCategory.DataTextField = "Category_Name";
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataBind();
    }

    private void BindDataList()
    {
        string key = txtKey.Text.Trim();
        int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);
        string userCode = GetEmployee().LoginId;
        int count = bll.GetViewListCount(userCode, IsRuleAdmin, categoryId, key);
        this.AspNetPager1.RecordCount = count;
        lblCount.Text = count.ToString();
        List<V_WR_Rule> list = bll.GetViewList(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, userCode, IsRuleAdmin, categoryId, key);

        //AspNetPager修复删除最后一页最后一条记录的问题
        if (this.AspNetPager1.CurrentPageIndex > 1 && list.Count == 0)
        {
            this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex - 1;
            BindDataList();
        }
        else
        {
            this.rptList.DataSource = list;
            this.rptList.DataBind();
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDataList();
    }

    protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                WR_Rule model = new WR_Rule();
                model.ID = Convert.ToInt16(e.CommandArgument);
                model.Record_Status = 2;
                model.Modified_By = CurrentEmployee.EmployeeCode;
                model.Modified_By_Name = CurrentEmployee.EmployeeName;
                model.Modified_On = DateTime.Now;
                bll.UpdateRecordStatus(model);

                Label lblCategoryName = (Label)e.Item.FindControl("lblCategoryName");
                //删除附件
                List<WR_Attachment> items = new Attachement().GetListByRuleId(model.ID);
                foreach (var item in items)
                {
                    new Attachement().Delete(item.ID);
                    //删除服务器文件
                    string path = "/WorkFlowRule/Upload/" + lblCategoryName.Text + "/" + item.FileName;
                    if (File.Exists(Server.MapPath("~" + path)))
                    {
                        File.Delete(Server.MapPath("~" + path));
                    }
                }

                BindDataList();
            }
            if (e.CommandName == "Edit")
            {
                Response.Redirect("RuleEdit.aspx?ID=" + e.CommandArgument, false);
            }
        }
        catch (Exception ex)
        {
            JsHelper.AlertOperationFailure(Page, ex);
        }
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("RuleEdit.aspx");
    }

    protected string FormatDate(object dt)
    {
        if (dt != null)
        {
            return Convert.ToDateTime(dt).ToShortDateString();
        }
        return "";
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        BindDataList();
    }
}