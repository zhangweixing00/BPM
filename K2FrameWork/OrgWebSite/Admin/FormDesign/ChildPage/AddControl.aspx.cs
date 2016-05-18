using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Utility;
using BLL;

namespace OrgWebSite.Admin.FormDesign.ChildPage
{
    public partial class AddControl : BasePage
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
            ControlInfo info = bll.GetControlById(Id);
            if (info != null)
            {
                txtControlType.Text = info.Class;
                txtDesc.Text = info.Description;
                txtJson.Text = info.Json;
                txtName.Text = info.Name;
                txtType.Text = info.Type;
            }
        }

        /// <summary>
        /// 取得表单数据
        /// </summary>
        /// <returns></returns>
        private ControlInfo CreateObject()
        {
            ControlInfo ci = new ControlInfo();
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                ci.ID = new Guid(Request.QueryString["ID"]);
            ci.Name = txtName.Text;
            ci.Type = txtType.Text;
            ci.Class = txtControlType.Text;
            ci.Json = txtJson.Text;
            ci.Html = "";
            ci.Description = txtDesc.Text;
            return ci;
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            FormDesignBLL bll = new FormDesignBLL();

            string action = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(action) && action == "add")
            {
                bool ret = bll.CreateControl(CreateObject());
                if (ret)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '添加成功!','',function(){window.location.href='ControlManage.aspx'});", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '添加失败!');", true);
                }
            }
            else if (!string.IsNullOrEmpty(action) && action == "edit")
            {
                bool ret = bll.UpdateControl(CreateObject());
                if (ret)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '更新成功!','',function(){window.location.href='ControlManage.aspx'});", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "$.messager.alert('提示', '更新失败!');", true);
                }
            }
        }
    }
}