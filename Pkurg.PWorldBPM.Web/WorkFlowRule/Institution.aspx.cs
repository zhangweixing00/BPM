using System;
using System.Collections.Generic;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_Institution : UPageBase
{
    Rule bll = new Rule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();
            BindTopRule();
            BindRuleList();
        }
    }

    private void BindCategory()
    {
        List<WR_Category> items = bll.GetCategoryList(CurrentEmployee.EmployeeCode, true);

        items.Add(new WR_Category { ID = -1, Category_Name = "全部" });

        rptCategory.DataSource = items;
        rptCategory.DataBind();
        if (items.Count > 0)
        {
            lblCategoryId.Text = items[0].ID.ToString();
            lblCategoryName.Text = "当前分类：" + items[0].Category_Name;
        }
    }

    /// <summary>
    /// 最新3个月发布的制度
    /// </summary>
    private void BindTopRule()
    {
        List<V_WR_Rule> list = new Rule().GetTopRuleList();
        rptTopRule.DataSource = list;
        rptTopRule.DataBind();
        if (list.Count == 0)
        {
            lblNoRecord.Visible = true;
        }
    }

    void BindRuleList()
    {
        int categoryId = -1;
        if (!string.IsNullOrEmpty(Request.QueryString["categoryId"]))
        {
            bool flag = int.TryParse(Request.QueryString["categoryId"], out categoryId);
            lblCategoryId.Text = categoryId.ToString();
            lblCategoryName.Text = "当前分类：" + Request.QueryString["categoryName"];
        }

        int count = bll.GetRuleCount(Convert.ToInt16(lblCategoryId.Text), "");
        this.AspNetPager1.RecordCount = count;
        //lblCount.Text = count.ToString();
        List<V_WR_Rule> list = bll.GetRuleList(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, Convert.ToInt16(lblCategoryId.Text), "");

        rptRule.DataSource = list;
        rptRule.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindRuleList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("InstitutionSearch.aspx?key=" + txtKey.Text.Trim());
    }

    protected string FormatPublishDate(object dt)
    {
        if (dt != null)
        {
            return Convert.ToDateTime(dt).ToString("yyyy-MM-dd") + " - ";
        }
        return "";
    }
}