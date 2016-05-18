using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using Model;
using BLL;

namespace OrgWebSite.Admin.Popup
{
    public partial class OrgEdit : BasePage
    {
        protected string orgCode
        {
            get
            {
                try
                {
                    return Request.QueryString["orgCode"].Trim();
                }
                catch
                {
                    return null;
                }
            }
        }

        private string action
        {
            get
            {
                return Request.QueryString["action"].Trim();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            if (action == "edit")
            {
                //获取某个组织的信息
                OrganizationBLL bll = new OrganizationBLL();
                OrganizationInfo info = bll.GetOrgByID(orgCode);

                if (info != null)
                {
                    txtOrderNo.Text = info.OrderNo.ToString();
                    txtOrgCode.Text = info.OrgCode;
                    txtOrgDesc.Text = info.OrgDescription;
                    txtOrgName.Text = info.OrgName;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化数据
                InitData();
            }
        }

        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool ret = false;
            string result = "";
            if (action == "new")
            {
                OrganizationBLL bll = new OrganizationBLL();
                ret = bll.CreateOrgInfo(txtOrgName.Text, txtOrgCode.Text, txtOrgDesc.Text, txtOrderNo.Text);
                result = "添加";
            }
            else if(action == "edit")
            {
                OrganizationBLL bll = new OrganizationBLL();
                Guid id = new Guid(orgCode);
                ret = bll.UpdateOrgInfo(id, txtOrgName.Text, txtOrgCode.Text, txtOrgDesc.Text, txtOrderNo.Text);
                result = "更新";
            }

            if (ret)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.ymPrompt.doHandler('ok', false);</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>top.ymPrompt.doHandler('failed', false);</script>");
            }
        }
    }
}