using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.WorkFlowRule;

public partial class WorkFlowRule_Rule_RuleEdit : UPageBase
{
    Rule bll = new Rule();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "制度";

        if (!IsPostBack)
        {
            InitControl();

            txtPublishDate.MaxLength = 10;
            txtPublishDate.Attributes.Add("onBlur", "checkDate(this.id);");
            txtPublishDate.Attributes.Add("autocomplete", "off");

            ViewState["Attachments"] = new List<AttachmentItem>();
            ViewState["AttachmentsDelete"] = new List<AttachmentItem>();

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                int id = 0;
                bool flag = int.TryParse(Request.QueryString["ID"], out id);
                if (flag)
                {
                    BindForm(id);
                }
                else
                {
                    Response.Redirect("RuleList.aspx", false);
                }
            }
        }
    }

    void InitControl()
    {
        List<WR_Category> items = bll.GetCategoryList(GetEmployee().LoginId, IsRuleAdmin);
        ddlCategory.DataSource = items;
        ddlCategory.DataTextField = "Category_Name";
        ddlCategory.DataValueField = "ID";
        ddlCategory.DataBind();
    }

    void BindForm(int id)
    {
        WR_Rule model = bll.GetModel(id);
        if (model == null)
        {
            Response.Redirect("RuleList.aspx", false);
        }

        lblID.Text = model.ID.ToString();
        txtPublishDate.Value = model.Publish_Date.HasValue ? model.Publish_Date.Value.ToString("yyyy-MM-dd") : "";
        ddlCategory.SelectedValue = model.Category_ID.ToString();
        txtTitle.Text = model.Title;
        txtSummary.Text = model.Summary;
        //获取附件
        List<WR_Attachment> items = new Attachement().GetListByRuleId(model.ID);

        List<AttachmentItem> attachmentItems = (from p in items
                                                select new AttachmentItem
                                                {
                                                    Attachment_ID = p.Attachment_GUID,
                                                    Created_On = p.Created_On,
                                                    FileName = p.FileName,
                                                    FileSize = p.FileSize,
                                                    FilePath = p.FilePath
                                                }).ToList();

        ViewState["Attachments"] = attachmentItems;

        rptAttachment.DataSource = attachmentItems;
        rptAttachment.DataBind();
    }

    void BindAttachment()
    {
        List<AttachmentItem> items = (List<AttachmentItem>)ViewState["Attachments"];
        rptAttachment.DataSource = items;
        rptAttachment.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lblID.Text))
        {
            try
            {
                WR_Rule model = GetModel();
                model.Rule_GUID = Guid.NewGuid();
                model.Created_By = CurrentEmployee.EmployeeCode;
                model.Created_By_Name = CurrentEmployee.EmployeeName;
                model.Created_On = DateTime.Now;
                model.Record_Status = 0;
                bll.Insert(model);

                List<WR_Attachment> attachements = GetAttachementModel(model.ID);
                new Attachement().Insert(attachements);

                JsHelper.AlertOperationSuccessAndRedirect(Page, "RuleList.aspx");
            }
            catch (Exception ex)
            {
                JsHelper.AlertOperationFailure(Page, ex);
            }
        }
        else
        {
            try
            {
                WR_Rule model = GetModel();
                model.ID = Convert.ToInt16(lblID.Text);
                bll.Update(model);

                new Attachement().DeleteByRuleId(model.ID);

                List<WR_Attachment> attachements = GetAttachementModel(model.ID);
                new Attachement().Insert(attachements);

                JsHelper.AlertOperationSuccessAndRedirect(Page, "RuleList.aspx");
            }
            catch (Exception ex)
            {
                JsHelper.AlertOperationFailure(Page, ex);
            }
        }
    }


    WR_Rule GetModel()
    {
        WR_Rule model = new WR_Rule();

        if (!string.IsNullOrEmpty(txtPublishDate.Value.Trim()))
        {
            model.Publish_Date = Convert.ToDateTime(txtPublishDate.Value.Trim()).Date;
        }
        model.Category_ID = Convert.ToInt32(ddlCategory.SelectedValue);
        model.Title = txtTitle.Text.Trim();
        model.Summary = txtSummary.Text.Trim();

        model.Record_Status = 0;
        model.Modified_By = CurrentEmployee.EmployeeCode;
        model.Modified_By_Name = CurrentEmployee.EmployeeName;
        model.Modified_On = DateTime.Now;

        return model;
    }

    //附件
    List<WR_Attachment> GetAttachementModel(int ruleId)
    {
        List<WR_Attachment> attachments = new List<WR_Attachment>();
        List<AttachmentItem> items = (List<AttachmentItem>)ViewState["Attachments"];
        foreach (var item in items)
        {
            attachments.Add(new WR_Attachment
            {
                Attachment_GUID = Guid.NewGuid(),
                Created_By = CurrentEmployee.EmployeeCode,
                Created_By_Name = CurrentEmployee.EmployeeName,
                Created_On = DateTime.Now,
                FileName = item.FileName,
                FileSize = item.FileSize,
                FilePath = item.FilePath,
                Record_Status = 0,
                Rule_ID = ruleId
            });
        }
        return attachments;
    }


    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("RuleList.aspx");
    }

    //附件
    protected void rptAttachment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string Attachment_ID = (string)e.CommandArgument;
            List<AttachmentItem> items = (List<AttachmentItem>)ViewState["Attachments"];

            int index = items.FindIndex(p => p.Attachment_ID == new Guid(Attachment_ID));
            if (index > -1)
            {
                string fileName = items[index].FileName;
                new Attachement().Delete(items[index].Attachment_ID);
                items.RemoveAt(index);
                ViewState["Attachments"] = items;
                //删除服务器文件
                string path = "/WorkFlowRule/Upload/" + ddlCategory.SelectedItem.Text + "/" + fileName;
                if (File.Exists(Server.MapPath("~" + path)))
                {
                    File.Delete(Server.MapPath("~" + path));
                }


                BindAttachment();
            }
        }
    }

    //上传
    protected void btnAddAttachment_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            JsHelper.Alert(Page, string.Format("请先选择“分类”再上传！"));
            ddlCategory.Focus();
            return;
        }

        if (FileUpload1.HasFile)
        {
            if (FileUpload1.FileName.Contains("#"))
            {
                JsHelper.Alert(Page, string.Format("文件名不能包含 # 特殊符号，请修改！"));
                return;
            }
            int fileUploadSize = 20;
            //fileUploadSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FileUploadSize"]);
            if (FileUpload1.FileContent.Length / 1024.0 / 1024.0 > fileUploadSize)
            {
                JsHelper.Alert(Page, string.Format("附件大小不能超过{0}M", fileUploadSize));
                return;
            }
            string dir = ddlCategory.SelectedItem.Text;
            if (!Directory.Exists(Server.MapPath("~/WorkFlowRule/Upload/" + dir + "/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/WorkFlowRule/Upload/" + dir + "/"));
            }
            string fileName = FileUpload1.FileName;
            string path = "/WorkFlowRule/Upload/" + dir + "/" + fileName;

            List<AttachmentItem> items = (List<AttachmentItem>)ViewState["Attachments"];

            if (File.Exists(Server.MapPath("~" + path)))
            {
                AttachmentItem item = items.Where(p => p.FilePath + p.FileName == path).FirstOrDefault();
                int index = items.IndexOf(item);
                if (index > -1)
                {
                    items.RemoveAt(index);
                }
            }

            string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            FileUpload1.PostedFile.SaveAs(Server.MapPath(path));

            AttachmentItem model = new AttachmentItem();
            model.FileSize = FileUpload1.PostedFile.ContentLength;
            model.FileName = fileName;
            model.FilePath = "/WorkFlowRule/Upload/" + dir + "/";
            model.Attachment_ID = Guid.NewGuid();
            model.Created_On = DateTime.Now;

            items.Add(model);

            ViewState["Attachments"] = items;
            BindAttachment();
        }
        else
        {
            JsHelper.Alert(Page, "请选择文件！");
        }
    }
}