using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using BLL;

namespace OrgWebSite.Admin
{
    public partial class RoleUserManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        DataTable dtUser = null;
        DataTable dtRole = null;
        private void BindData()
        {
            //DataSet ds = DBManager.GetRoleUserByCode("");
            RoleBLL bll = new RoleBLL();
            DataSet ds = bll.GetRoleUserByCode("");
            
            if (ds != null && ds.Tables.Count > 0)
            {
                dtRole = ds.Tables[0];
                dtUser = ds.Tables[1];
                BindTreeView();
            }
        }

        /// <summary>
        /// 绑定角色树
        /// </summary>
        private void BindTreeView()
        {
            TreeNode rootNode = tvRole.Nodes[0];            //取得跟节点
            if (dtRole != null && dtRole.Rows.Count > 0)
            {
                for (int i = 0; i < dtRole.Rows.Count; i++)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = dtRole.Rows[i]["RoleName"].ToString();
                    tn.Value = dtRole.Rows[i]["ID"].ToString();
                    rootNode.ChildNodes.Add(tn);
                }
            }
        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            if (tvRole.SelectedNode != null)
            {
                //DataSet ds = DBManager.GetRoleUserByCode(tvRole.SelectedNode.Value);
                RoleBLL bll = new RoleBLL();
                DataSet ds = bll.GetRoleUserByCode(tvRole.SelectedNode.Value);

                if (ds != null && ds.Tables.Count > 1)
                {
                    gvUser.DataSource = ds.Tables[1];
                    gvUser.DataBind();
                }

                //取得角色信息
                //DataTable dtRole = DBManager.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                dtRole = bll.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                if (dtRole != null)
                {
                    txtRoleName.Text = dtRole.Rows[0]["RoleName"].ToString();
                    txtProcessType.Text = dtRole.Rows[0]["processtype"].ToString();
                }
            }
            else
            {
                BindData();
            }
        }

        protected void tvDept_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (tvRole.SelectedNode != null)
            {
                hfSelectNode.Value = tvRole.SelectedNode.Value; //保存选中节点值
                //DataSet ds = DBManager.GetRoleUserByCode(tvRole.SelectedNode.Value);
                RoleBLL bll = new RoleBLL();
                DataSet ds = bll.GetRoleUserByCode(tvRole.SelectedNode.Value);

                if (ds != null && ds.Tables.Count > 1)
                {
                    gvUser.DataSource = ds.Tables[1];
                    gvUser.DataBind();
                }

                //取得角色信息
                //DataTable dtRole = DBManager.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                dtRole = bll.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                if (dtRole != null)
                {
                    txtRoleName.Text = dtRole.Rows[0]["RoleName"].ToString();
                    txtProcessType.Text = dtRole.Rows[0]["processtype"].ToString();
                }
            }
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Attributes["style"] = "display:none;";
                //e.Row.Cells[10].Attributes["style"] = "display:none;";
            }
        }

        /// <summary>
        /// 向角色中添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAddUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectUserAD.Value) && tvRole.SelectedNode != null)
            {
                RoleBLL bll = new RoleBLL();
                bool ret = bll.AddUserToRoleUser(tvRole.SelectedValue, hfSelectUserAD.Value);
                if (ret)
                {
                    hfSelectUserAD.Value = string.Empty;
                    if (tvRole.SelectedNode != null)
                    {
                        //DataSet ds = DBManager.GetRoleUserByCode(tvRole.SelectedNode.Value);
                        DataSet ds = bll.GetRoleUserByCode(tvRole.SelectedNode.Value);
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            gvUser.DataSource = ds.Tables[1];
                            gvUser.DataBind();
                        }

                        //取得角色信息
                        //DataTable dtRole = DBManager.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                        dtRole = bll.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);

                        if (dtRole != null)
                        {
                            txtRoleName.Text = dtRole.Rows[0]["RoleName"].ToString();
                            txtProcessType.Text = dtRole.Rows[0]["processtype"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string Ids = string.Empty;
            RoleBLL bll = new RoleBLL();

            //取得所有选中的条目
            for (int i = 0; i < gvUser.Rows.Count; i++)
            {
                CheckBox cbU = gvUser.Rows[i].FindControl("chkUser") as CheckBox;
                HiddenField hfID = gvUser.Rows[i].FindControl("hfID") as HiddenField;
                if (cbU != null && hfID != null && cbU.Checked)
                {
                    Ids += hfID.Value + ",";
                }
            }

            if (!string.IsNullOrEmpty(Ids))
            {
                if (Ids.EndsWith(","))
                    Ids = Ids.Substring(0, Ids.Length - 1);

                //bool ret = DBManager.DeleteUserFromRoleUser(Ids);       //执行删除
                bool ret = bll.DeleteUserFromRoleUser(Ids);     //执行删除
                
                if (ret)
                {
                    //重新加载
                    if (tvRole.SelectedNode != null)
                    {
                        //DataSet ds = DBManager.GetRoleUserByCode(tvRole.SelectedNode.Value);
                        DataSet ds = bll.GetRoleUserByCode(tvRole.SelectedNode.Value);

                        if (ds != null && ds.Tables.Count > 1)
                        {
                            gvUser.DataSource = ds.Tables[1];
                            gvUser.DataBind();
                        }

                        //取得角色信息
                        //DataTable dtRole = DBManager.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                        dtRole = bll.GetRoleInfoByRoleCode(tvRole.SelectedNode.Value);
                        if (dtRole != null)
                        {
                            txtRoleName.Text = dtRole.Rows[0]["RoleName"].ToString();
                            txtProcessType.Text = dtRole.Rows[0]["processtype"].ToString();
                        }
                    }
                }
            }
        }

        protected void btnDelDept_Click(object sender, EventArgs e)
        {
            if (tvRole.SelectedNode != null)
            {
                //DBManager.DeleteRoles(tvRole.SelectedNode.Value);
                RoleBLL bll = new RoleBLL();
                bll.DeleteRoles(tvRole.SelectedNode.Value);
                if (tvRole.Nodes.Count > 0)
                {
                    tvRole.Nodes[0].ChildNodes.Clear();
                    tvRole.Nodes[0].Expand();
                    txtRoleName.Text = string.Empty;
                    txtProcessType.Text = string.Empty;
                    gvUser.DataSource = null;
                    gvUser.DataBind();
                    BindData();
                }
            }
        }
    }
}