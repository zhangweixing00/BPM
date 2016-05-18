using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Utility;

namespace OrgWebSite.Admin.FormDesign.ChildPage
{
    public partial class AddFormTemplateLibrary : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["action"]))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    string Id = Request.QueryString["ID"];
                    string action = Request.QueryString["action"];
                    if (action == "edit")
                    {
                        LoadData(Id);
                    }
                }
            }
        }

        /// <summary>
        /// 编辑时，加载数据
        /// </summary>
        /// <param name="Id"></param>
        private void LoadData(string Id)
        {
            FormDesignBLL bll = new FormDesignBLL();
            FormTemplateInfo info = bll.GetFormTemplateById(Id);
            if (info != null)
            {
                txtName.Text = info.Name;
                txtVersion.Text = info.Version;
                txtDescription.Text = info.Description;
            }
        }

        /// <summary>
        /// 取得表单数据
        /// </summary>
        /// <returns></returns>
        private FormTemplateInfo CreateObject()
        {
            FormTemplateInfo ftli = new FormTemplateInfo();
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                ftli.ID = new Guid(Request.QueryString["ID"]);
            ftli.Name = txtName.Text;
            ftli.Version = txtVersion.Text;
            ftli.Html = "";
            ftli.Description = txtDescription.Text;
            return ftli;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            FormDesignBLL bll = new FormDesignBLL();

            string action = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(action) && action == "add")
            {
                bool ret = bll.CreateFormTemplate(CreateObject());
                if (ret)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '添加成功!','',function(){window.location.href='FormTemplateManage.aspx'});", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '添加失败!');", true);
                }
            }
            else if (!string.IsNullOrEmpty(action) && action == "edit")
            {
                bool ret = bll.UpdateFormTemplate(CreateObject());
                if (ret)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '更新成功!','',function(){window.location.href='FormTemplateManage.aspx'});", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '更新失败!');", true);
                }
            }
        }
    }
}