using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using Utility;
namespace Sohu.OA.Web.Manage.RoleManage.UC
{
    /**
   * 
   * 
   * 角色员工管理
   * 王红福
   * 2011-9-2
   * 
   * ***/
    public partial class UC_RoleUser : System.Web.UI.UserControl
    {
        string roleCode = "kkkkkk";
        public int totalRecordCount = 0;  //总共多少条记录

        int intPageIndex = 0; //当前页索引
        // int intPageNum =15; //一页多少条数据
        // int intPageSelectNum = 10;//从数据库查询多少条数据

        #region     protected void Page_Load(object sender, EventArgs e)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoleUserMes();
            }
        }
        #endregion

        #region  public void ExecAlertScritp(string msg)
        /// <summary>
        /// 执行弹出消息
        /// </summary>
        /// <param name="msg"></param>
        public void ExecAlertScritp(string msg)
        {
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "Msg", "<script language='javascript'>alert('" + msg + "')</script>"
                );
        }
        #endregion

        #region   private void LoadRoleUserMes()
        /// <summary>
        /// 邦定角色人员
        /// </summary>
        private void LoadRoleUserMes()
        {
            roleCode = Request["rolecode"];
            if (!String.IsNullOrEmpty(roleCode))
            {
                this.hfRoleCode.Text = roleCode;
                RoleBLL bll = new RoleBLL();
                DataSet ds = bll.GetRoleUserByCode(roleCode);
                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    totalRecordCount = dt.Rows.Count;
                    Common.BindBoundControl(dt, this.GridView1, intPageIndex, this.GridView1.PageSize, totalRecordCount);
                }
            }
            else
            {

                this.GridView1.DataBind();
            }
        }
        #endregion

        #region 自定义分页方法
        /// <summary>
        /// 自定义分页方法
        /// </summary>
        /// <param name="PageIndex">页码索引</param>
        protected void SelecctInfoByPageIndex(int PageIndex)
        {
            intPageIndex = PageIndex;
            LoadRoleUserMes();

            //绑定跳转文本框
            TextBox tbPage = (TextBox)this.GridView1.BottomPagerRow.FindControl("txt_PageIndex");
            tbPage.Text = (PageIndex + 1).ToString();
        }
        #endregion

        #region 分页按钮
        /// <summary>
        /// 分页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelecctInfoByPageIndex(e.NewPageIndex);
        }
        #endregion
               
        #region protected void bnSave_Click(object sender, ImageClickEventArgs e)
        /// <summary>
        /// 保存多个员工到角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectUserAD.Value))
            {
                RoleBLL bll = new RoleBLL();
                bool ret = bll.AddUserToRoleUser(this.hfRoleCode.Text, hfSelectUserAD.Value);
                LoadRoleUserMes();
                if (ret)
                {
                    ExecAlertScritp("添加成功！");
                }
                else
                {
                    ExecAlertScritp("添加失败！");
                }
            }
        }
        #endregion

        #region  protected void bnDelete_Click(object sender, ImageClickEventArgs e)
        /// <summary>
        /// 删除多个角色用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bnDelete_Click(object sender, ImageClickEventArgs e)
        {           
            if (this.GridView1.Rows.Count > 0)
            {
                RoleBLL bll = new RoleBLL();
                string ids = "";
                foreach (GridViewRow item in GridView1.Rows)
                {
                    CheckBox ckSelect = (CheckBox)item.FindControl("ckSelect");
                    if (ckSelect.Checked)
                    {
                        if (string.IsNullOrEmpty(ids))
                        {
                            ids = GridView1.DataKeys[item.RowIndex].Values[0].ToString();
                        }
                        else
                        {
                            ids += "," + GridView1.DataKeys[item.RowIndex].Values[0].ToString();
                        }
                    }
                }
                bool ret = bll.DeleteUserFromRoleUser(ids);
                if (ret)
                {
                    LoadRoleUserMes();
                    ExecAlertScritp("删除成功！");
                }
                else
                {
                    ExecAlertScritp("删除失败！");
                }
            }
        }
        #endregion
    }
}