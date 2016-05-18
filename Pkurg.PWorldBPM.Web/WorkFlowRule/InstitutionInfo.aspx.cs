using System;
using System.Collections.Generic;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_InstitutionInfo : UPageBase
{
    Rule bll = new Rule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                int id = 0;
                bool flag = int.TryParse(Request.QueryString["ID"], out id);
                if (flag)
                {
                    BindInfo(id);
                }
                else
                {
                    Response.Redirect("Institution.aspx", false);
                }
            }
        }
    }

    private void BindInfo(int id)
    {
        WR_Rule model = bll.GetModel(id);
        if (model != null)
        {
            if (model.Rule_GUID.ToString() != Request.QueryString["GUID"])
            {
                Response.Redirect("Institution.aspx", false);
                return;
            }

            if (model.Publish_Date.HasValue)
            {
                lblPublishDate.Visible = true;
                lblPublishDate.Text = "自 " + model.Publish_Date.Value.ToString("yyyy-MM-dd") + " 施行";
            }

            lblTitle.Text = model.Title;
            lblSummary.Text = model.Summary.Replace("\n","<br/>");
            lblCreatedByName.Text = model.Created_By_Name;
            lblCreatedOn.Text = model.Created_On.ToString();

            //Hidden

            hdId.Value = model.ID.ToString();
            hdcreatedBy.Value = CurrentEmployee.EmployeeCode;
            hdcreatedByName.Value = CurrentEmployee.EmployeeName;

            //附件
            List<WR_Attachment> items = new Attachement().GetListByRuleId(model.ID);
            rptAttachements.DataSource = items;
            rptAttachements.DataBind();

            if (items.Count == 0)
            {
                lblAttachements.Visible = true;
            }
        }
    }

}