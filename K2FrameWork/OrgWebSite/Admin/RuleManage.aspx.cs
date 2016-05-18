using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using BLL;
using Model;
using K2.Common;
using System.Data;
using System.Text;

namespace OrgWebSite.Admin
{
    public partial class RuleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProcessType();
                BindGroup();
                BindRuleTable();
                BindFormula();
            }
        }

        /// <summary>
        /// 绑定公式
        /// </summary>
        private void BindFormula()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            IList<FormulaInfo> flList = bll.GetFormulaByGroupName(ddlProcessType.SelectedValue, ddlGroup.SelectedValue);
            if (flList != null && flList.Count > 0)
            {
                foreach (FormulaInfo info in flList)
                {
                    txtExpression.Text = info.Formula.Replace("*", "AND").Replace("+", "OR").Replace("-", "NOT");
                    break;
                }
            }
        }

        /// <summary>
        /// 绑定规则表
        /// </summary>
        private void BindRuleTable()
        {
            ddlSelectID.Items.Clear();
            ProcessRuleBLL bll = new ProcessRuleBLL();
            if (ddlProcessType.Items.Count > 0 && ddlGroup.Items.Count > 0)
            {
                IList<ApproveRuleGroupInfo> argiList = bll.GetApproveTableByProcessID(ddlProcessType.SelectedValue, ddlGroup.SelectedValue);
                gvRuleList.DataSource = argiList;
                gvRuleList.DataBind();

                //绑定下拉框
                if (argiList != null)
                {
                    foreach (ApproveRuleGroupInfo info in argiList)
                    {
                        ListItem li = new ListItem();
                        li.Text = info.OrderNo.ToString();
                        li.Value = info.OrderNo.ToString();
                        ddlSelectID.Items.Add(li);
                    }
                }
            }
        }

        /// <summary>
        /// 绑定流程类别
        /// </summary>
        private void BindProcessType()
        {
            ddlProcessType.Items.Clear();
            string loginName = Page.User.Identity.Name.Split('\\')[1];
            ddlProcessType.DataSource = WebCommon.GetDeptListByEmployeeCode(loginName);
            ddlProcessType.DataBind();
        }

        private void BindGroup()
        {
            ddlGroup.Items.Clear();
            ProcessRuleBLL bll = new ProcessRuleBLL();
            if (ddlProcessType.Items.Count > 0)
            {
                IList<ApproveRuleGroupInfo> argi = bll.GetApproveRuleGroupNameByProcessType(ddlProcessType.SelectedValue);
                if (argi != null && argi.Count > 0)
                {
                    foreach (ApproveRuleGroupInfo tmp in argi)
                    {
                        ListItem li = new ListItem();
                        li.Text = tmp.GroupName;
                        li.Value = tmp.GroupName;
                        ddlGroup.Items.Add(li);
                    }
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            bool ret = bll.InsertFormula(ddlProcessType.SelectedValue, ddlGroup.SelectedValue, txtExpression.Text.Replace(" ", "").Replace("AND", "*").Replace("OR", "+").Replace("NOT", "-"));
            if (ret)
            {
                MessageBox.Show(this.Page, "操作成功");
            }
            else
            {
                MessageBox.Show(this.Page, "操作失败");
            }
        }

        protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGroup();
            BindFormula();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtExpression.Text = string.Empty;
            BindRuleTable();
            BindFormula();
        }
    }
}