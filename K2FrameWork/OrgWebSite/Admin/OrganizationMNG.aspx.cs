using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using Model;
using BLL;

namespace OrgWebSite.Admin
{
    public partial class OrganizationMNG : BasePage
    {
        public string DeptCode
        {
            get
            {
                try
                {
                    if (Request.QueryString["deptCode"] != null)
                        return Request.QueryString["deptCode"];
                    else
                        return tvDept.Nodes[0].Value;
                }
                catch
                {
                    return "R0";
                }
            }
        }

        public string OrgCode
        {
            get
            {
                try
                {
                    if (Request.QueryString["orgCode"] != null)
                        return Request.QueryString["orgCode"];
                    else
                        return ddlOrg.SelectedItem.Value;
                }
                catch
                {
                    return "";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitOrg();
                string a= ddlOrg.SelectedValue;
                InitTreeView();
                InitDeptInfo();
                InitUser();
            }

        }

        private void InitOrg()
        {
            OrganizationBLL bll = new OrganizationBLL();
            IList<OrganizationInfo> orgList = bll.GetOrgList();
            ddlOrg.Items.Clear();
            foreach (OrganizationInfo info in orgList)
            {
                ListItem li = new ListItem();
                li.Text = info.OrgName;
                li.Value = info.ID.ToString();
                if (li.Value.Equals(OrgCode, StringComparison.OrdinalIgnoreCase))
                    li.Selected = true;
                ddlOrg.Items.Add(li);
            }

            //获取某个组织的信息
            OrganizationInfo orgInfo = bll.GetOrgByID(ddlOrg.SelectedValue);

            if (orgInfo != null)
            {
                txtOrderNo.Text = orgInfo.OrderNo.ToString();
                txtOrgCode.Text = orgInfo.OrgCode;
                txtOrgName.Text = orgInfo.OrgName;
            }
        }

        private void InitUser()
        {
            if (!string.IsNullOrEmpty(DeptCode))
            {
                UserProfileBLL bll = new UserProfileBLL();
                DataSet ds = bll.GetUserByDeptCode(DeptCode);

                if (ds != null && ds.Tables.Count > 1)
                {
                    gvUser.DataSource = ds.Tables[0];
                    gvUser.DataBind();

                    txtUserCount.Text = ds.Tables[1].Rows[0][0].ToString();
                }
            }
        }

        private void InitDeptInfo()
        {
            if (!string.IsNullOrEmpty(DeptCode))
            {
                DepartmentBLL deptBLL = new DepartmentBLL();
                DepartmentInfo dept = deptBLL.GetDepartmentInfo(DeptCode);

                if (dept != null)
                {
                    txtCode.Text = dept.Code;
                    txtDepartment.Text = dept.DeptName;
                    //txtParentDepartment.Text = dept.ParentCode;
                    txtAbbreviation.Text = dept.Abbreviation;
                    txtDeptType.Text = dept.DeptTypeCode.ToString();
                    txtOrderNo.Text = dept.OrderNo.ToString();
                    rblState.SelectedIndex = rblState.Items.IndexOf(rblState.Items.FindByValue(dept.State.ToString()));
                }
            }
        }

        /// <summary>
        /// 初始化组织树
        /// </summary>
        private void InitTreeView()
        {
            tvDept.Nodes.Clear();

            //首先判断是否选择了组织
            if (ddlOrg.Items.Count > 0 && ddlOrg.SelectedItem != null)
            {
                DepartmentBLL deptBLL = new DepartmentBLL();
                IList<DepartmentInfo> rootDepts = deptBLL.GetSubDepartments(OrgCode, "R0");

                if ((rootDepts != null) && (rootDepts.Count > 0))
                {
                    foreach (DepartmentInfo row in rootDepts)
                    {
                        TreeNode rootNode = new TreeNode(row.DeptName, row.DeptCode);
                        rootNode.NavigateUrl = Request.FilePath + "?deptCode=" + row.DeptCode + "&orgCode=" + row.OrgID.ToString();
                        this.tvDept.Nodes.Add(rootNode);

                        AddNodes(rootNode, rootNode.Value);
                    }
                }

                tvDept.DataBind();
            }
        }

        private void AddNodes(TreeNode node, string deptCode)
        {
            DepartmentBLL deptBLL = new DepartmentBLL();
            IList<DepartmentInfo> departments = deptBLL.GetSubDepartments(OrgCode, deptCode);

            if (departments != null && departments.Count > 0)
            {
                foreach (DepartmentInfo row in departments)
                {
                    TreeNode newNode = new TreeNode(row.DeptName, row.DeptCode);
                    newNode.NavigateUrl = Request.FilePath + "?deptCode=" + row.DeptCode + "&orgCode=" + row.OrgID.ToString();
                    node.ChildNodes.Add(newNode);
                    AddNodes(newNode, newNode.Value);
                }
            }
        }

        private void AddNodes(TreeNodeCollection nodes, TreeNode sourceNode)
        {
            foreach (TreeNode n in nodes)
            {
                TreeNode node = new TreeNode(n.Text, n.Value);
                node.NavigateUrl = n.NavigateUrl;
                sourceNode.ChildNodes.Add(node);

                AddNodes(n.ChildNodes, node);
            }
        }

        protected void btnDelDept_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DeptCode))
            {
                OrganizationBLL bll = new OrganizationBLL();
                bll.DeleteDepartmentByCode(DeptCode);
                lbReload_Click(sender, e);
            }
        }

        protected void btnDisableUser_Click(object sender, EventArgs e)
        {
            ActionUser("0");
        }

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            ActionUser("1");
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            ActionUser("-1");
        }

        private void ActionUser(string state)
        {
            string userCodes = "";
            for (int i = 0; i < gvUser.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)(gvUser.Rows[i].FindControl("chkUser"));
                if (chk.Checked)
                    userCodes += gvUser.DataKeys[i].Value.ToString() + ";";
            }

            if (userCodes != "")
            {
                //DBManager.DeleteDeptUser(DeptCode, userCodes);
                DepartmentBLL bll = new DepartmentBLL();
                bll.DeleteDeptUser(DeptCode, userCodes);
            }

            InitUser();
        }

        public string GetState(string state)
        {
            if (state == "1")
                return "启用";

            return "禁用";
        }

        public string GetWorkPlace(string wp)
        {
            if (wp.ToUpper() == "BJ")
                return "北京";

            if (wp.ToUpper() == "HK")
                return "香港";

            if (wp.ToUpper() == "ALL")
                return "所有";

            return wp;

        }

        protected void lbReload_Click(object sender, EventArgs e)
        {
            InitOrg();
            InitTreeView();
            InitDeptInfo();
            InitUser();
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Request.FilePath + "?orgCode=" + ddlOrg.SelectedItem.Value);
        }

        protected void btnDelOrg_Click(object sender, EventArgs e)
        {
            if (ddlOrg.SelectedItem != null)
            {
                OrganizationBLL bll = new OrganizationBLL();
                bll.DeleteOrgInfoByID(ddlOrg.SelectedItem.Value);
                lbReload_Click(sender, e);
            }
        }

        protected void btnAddUserHF_Click(object sender, EventArgs e)
        {
            UserProfileBLL bll = new UserProfileBLL();
            bll.AddDeptUser(DeptCode, hfSelectUserAD.Value);
            lbReload_Click(sender, e);
        }
    }
}