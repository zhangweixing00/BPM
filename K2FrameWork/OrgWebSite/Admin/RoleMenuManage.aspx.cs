using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2Utility;
using BLL;
using System.Data;
namespace Sohu.OA.Web.Manage.RoleManage
{
    /// <summary>
    /// 创建菜单和角色的关系
    /// 删除角色和菜单的关系
    /// 王红福
    /// </summary>
    public partial class RoleMenuManage : BasePage
    {
        #region   protected void Page_Load(object sender, EventArgs e)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        #endregion

        #region private void BindData()
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {

            this.hfRoleCode.Value = Request["rolecode"];
            BindTreeView();
            BindPerssion();
        }
        #endregion

        #region  private void BindTreeView()
        /// <summary>
        /// 绑定TreeView
        /// </summary>
        private void BindTreeView()
        {
            MenuBLL bll = new MenuBLL();
            DataSet MenuInfo = bll.GetMenuInfo("", "", "");

            DataView dv = MenuInfo.Tables[0].DefaultView;
            dv.RowFilter = "ParentMenuGuid=''";
            DataTable dt = dv.ToTable();
            trMenu.Nodes.Clear();
            if (dt != null)
            {
                //过滤跟节点
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode(dr["MenuName"].ToString(), dr["MenuGuid"].ToString());
                    trMenu.Nodes.Add(tn);
                    //递归添加字节点
                    BindChildNodes(MenuInfo.Tables[0].DefaultView, tn);
                }

            }
        }
        #endregion

        #region  private void BindChildNodes(List<T_Application> menuList, TreeNode currentNode)
        /// <summary>
        /// 递归绑定子节点
        /// </summary>
        private void BindChildNodes(DataView dv, TreeNode currentNode)
        {
            dv.RowFilter = "ParentMenuGuid='" + currentNode.Value + "'";
            DataTable dt = dv.ToTable();
            if (dt != null)
            {
                //过滤跟节点
                foreach (DataRow dr in dt.Rows)
                {
                    TreeNode tn = new TreeNode(dr["MenuName"].ToString(), dr["MenuGuid"].ToString());
                    currentNode.ChildNodes.Add(tn);
                    BindChildNodes(dv, tn);       //递归绑定子节点
                }
            }
        }
        #endregion

        #region     private void BindPerssion()
        private void BindPerssion()
        {
            ClearTreeViewChecked(trMenu.Nodes);
            if (!string.IsNullOrEmpty(hfRoleCode.Value))
            {
                MenuBLL bll = new MenuBLL();
                DataSet MenuInfo = bll.GetMenuPermisionByRoleID(hfRoleCode.Value);
                if (MenuInfo != null && MenuInfo.Tables.Count > 0)
                {
                    BindTreeViewPermission(trMenu.Nodes, MenuInfo);  //绑定TreeView权限
                }
            }
        }

        #endregion

        #region private void BindTreeViewPermission(TreeNodeCollection tnc, List<T_AppRole> appRoleList)
        private void BindTreeViewPermission(TreeNodeCollection tnc, DataSet MenuInfo)
        {
            foreach (TreeNode tn in tnc)
            {
                foreach (DataRow dr in MenuInfo.Tables[0].Rows)
                {
                    if (tn.Value.Equals(dr["MenuGuid"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        tn.Checked = true;
                        break;
                    }
                }
                BindTreeViewPermission(tn.ChildNodes, MenuInfo);
            }
        }
        #endregion

        #region  private void ClearTreeViewChecked(TreeNodeCollection tnc)
        private void ClearTreeViewChecked(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                tn.Checked = false;
                ClearTreeViewChecked(tn.ChildNodes);
            }
        }
        #endregion

        #region  protected void bnSave_Click(object sender, ImageClickEventArgs e)
        /// <summary>
        /// 保存菜单角色关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bnSave_Click(object sender, ImageClickEventArgs e)
        {
            //获取所有菜单的名称
            string strGuid = string.Empty;
            strGuid = GetSelectedValue(trMenu);
            try
            {
                MenuBLL bll = new MenuBLL();
                bll.UpdateMenuPermision(strGuid, hfRoleCode.Value);

                ExecAlertScritp("保存成功！");

                //  BindData();
            }
            catch (Exception ex)
            {
                ExecAlertScritp("保存失败！");
            }
        }
        #endregion
        #region private string GetSelectedValue(TreeView tv)
        /// <summary>
        /// 取得所有选中的GUID
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        private string GetSelectedValue(TreeView tv)
        {
            string strGuid = string.Empty;
            if (tv != null)
            {
                if (tv.CheckedNodes != null)
                {
                    foreach (TreeNode tn in tv.CheckedNodes)
                    {
                        if (string.IsNullOrEmpty(strGuid))
                        {
                            strGuid = tn.Value;
                        }
                        else
                        {
                            strGuid += "," + tn.Value;
                        }
                    }
                }
            }
            return strGuid;
        }
        #endregion

    }
}