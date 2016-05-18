using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using K2Utility;

namespace OrgWebSite.Admin.Popup
{
    public partial class ProcessRuleEdit : BasePage
    {
        public string State
        {
            get
            {
                return Request.QueryString["state"];
            }
        }

        public string NodeID
        {
            get
            {
                return Request.QueryString["NodeID"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPworldRole();
                if (!string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(NodeID) && State.Equals("edit", StringComparison.OrdinalIgnoreCase))
                {
                    BindData();
                }
            }
        }

        private void BindPworldRole()
        {
            PWorldRoleBLL bll = new PWorldRoleBLL();
            IList<PWorldRoleInfo> roleInfoList = bll.GetPworldRole();
            foreach (var item in roleInfoList)
            {
                ListItem listItem = new ListItem(item.RoleName, item.RoleId);
                dplist.Items.Add(listItem);
            }
        }

        private void BindData()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            ApproveNodeRuleInfo info = bll.GetApproveNodeRuleByNodeID(NodeID);
            if (info != null)
            {
                txtConditionExpression.Text = info.ConditionExpression;

                string[] express = info.ConditionExpression.Split('=');
                dplist.SelectedValue = express[1].Trim();

                //txtDisplayName.Text = info.KeyName;
                //txtSPName.Text = info.SPName;
                dplistSPName.SelectedValue = info.SPName.Trim();
                //txtTableName.Text = info.TableName;
                hfID.Value = info.ID.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            bool ret = false;
            string conditionExpression = "RoleId = " + dplist.SelectedValue.Trim();
            if (string.IsNullOrEmpty(hfID.Value))
            {
                ret = bll.InsertApproveNodeRule(NodeID, txtDisplayName.Text, txtTableName.Text, conditionExpression, dplistSPName.SelectedValue.Trim());
                if (ret)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                }
                else
                {
                    ExecAlertScritp("添加失败");
                }
            }
            else
            {
                ret = bll.UpdateApproveNodeRule(hfID.Value, NodeID, txtDisplayName.Text, txtTableName.Text, conditionExpression,dplistSPName.SelectedValue.Trim());
                if (ret)
                {
                    //ExecAlertScritp("添加成功");
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
                }
                else
                {
                    ExecAlertScritp("更新失败");
                }
            }
        }
    }
}