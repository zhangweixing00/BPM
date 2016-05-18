using System;
using System.Collections.Generic;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_InstitutionSearch : UPageBase
{
    Rule bll = new Rule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRuleList();
        }
    }

    private void BindRuleList()
    {
        string key = Request.QueryString["key"];
        if (!string.IsNullOrEmpty(key))
        {
            lblTitle.Text = " 关键字“" + key + "”";
        }

        int count = bll.GetRuleCount(-1, key);
        this.AspNetPager1.RecordCount = count;
        //lblCount.Text = count.ToString();
        List<V_WR_Rule> list = bll.GetRuleList(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, -1, key);

        rptRule.DataSource = list;
        rptRule.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindRuleList();
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