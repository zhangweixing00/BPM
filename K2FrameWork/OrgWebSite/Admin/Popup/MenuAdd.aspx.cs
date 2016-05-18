using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using K2Utility;
using System.Data;
using BLL;

namespace Sohu.OA.Web.Manage.RoleManage
{
    /// <summary>
    /// create:康亚兵
    /// date：2011-09-07
    /// description：添加菜单或者修改菜单信息
    /// </summary>
    public partial class MenuAdd :BasePage
    {
        protected DataSet MenuInfo
        {
            get
            {
                if (ViewState["MenuInfo"] == null)
                {
                    ViewState["MenuInfo"] = new DataSet();
                }
                return ViewState["MenuInfo"] as DataSet;
            }
            set { ViewState["MenuInfo"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //加载窗体信息
                LoadMessage();
                //加载dropdownlist菜单信息
                StringBuilder sb = new StringBuilder("");
                LoadParentMenuGuid("",sb);
                //加载编辑菜单信息
                LoadEditMes();
            }
        }
        /// <summary>
        /// 加载窗体title信息
        /// </summary>
        private void LoadMessage()
        {
            string operflag = Request.QueryString["oper"].ToString();

            MenuBLL bll = new MenuBLL();
            MenuInfo = bll.GetMenuInfo("", "", "");

            if (operflag == "add")
            {
                lblOperName.Text = "添加菜单";
            }
            else if (operflag == "edit")
            {
                lblOperName.Text = "修改菜单";
            }
        }
        /// <summary>
        /// 加载编辑信息方法
        /// </summary>
        private void LoadEditMes()
        {
            if (Request.QueryString["oper"].ToString() == "edit")
            {
                hfMenuGUID.Value = Request.QueryString["menuguid"].ToString();
                if (!String.IsNullOrEmpty(hfMenuGUID.Value))
                {
                    //T_Application t_application = new T_Application();
                    //t_application.MenuGuid = menuguid;
                    //T_Application t_result = Biz_T_Application.biz_T_Application.Query_Application(t_application).FirstOrDefault();
                    //Biz_BindOperator<T_Application>.BindEntityToForm(this.form1, t_result);

                    DataView dv = MenuInfo.Tables[0].DefaultView;
                    dv.RowFilter = "MenuGuid='" + Request.QueryString["menuguid"].ToString() + "'";

                    this.MenuName.Text = dv[0]["MenuName"].ToString();
                    this.MenuType.SelectedValue = dv[0]["MenuType"].ToString();
                    this.MenuURL.Text = dv[0]["MenuURL"].ToString();
                    this.ParentMenuGuid.SelectedValue = dv[0]["ParentMenuGuid"].ToString();
                }
            }
        }
        /// <summary>
        /// 加载上级菜单选项
        /// </summary>
        private void LoadParentMenuGuid(string ParentMenuGuid, StringBuilder sb)
        {
            if (MenuInfo != null && MenuInfo.Tables.Count > 0 && MenuInfo.Tables[0].Rows.Count > 0)
            {
                DataView dv = MenuInfo.Tables[0].DefaultView;
                dv.RowFilter = "ParentMenuGuid='" + ParentMenuGuid + "'";

                foreach (DataRow dr in dv.ToTable().Rows)
                {

                    ListItem menuitem = new ListItem();
                    menuitem.Text = sb.ToString() + dr["MenuName"].ToString();
                    menuitem.Value = dr["MenuGuid"].ToString();
                    this.ParentMenuGuid.Items.Add(menuitem);
                    sb.Append("--");
                    LoadParentMenuGuid(dr["MenuGuid"].ToString(), sb);
                    sb.Remove(0, 2);
                }
            }           
        }
        /// <summary>
        /// 保存功能（添加菜单保存或者更新菜单保存）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (MenuName.Text.ToString().Trim() == "")
            {
                ExecAlertScritp("菜单名称不能为空！");
                return;
            }
            //if (DisplayOrder.Text.ToString().Trim() == "")
            //{
            //    ExecAlertScritp("排序编号不能为空！");
            //    return;
            //}
            if (Request.QueryString["oper"].ToString() == "add")
            {
                MenuBLL bll = new MenuBLL();
                if (bll.CreateMenuInfo(System.Guid.NewGuid().ToString(),ParentMenuGuid.SelectedValue,MenuName.Text,MenuURL.Text,MenuType.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                }
                else
                {
                    ExecAlertScritp("菜单创建失败！");
                }
            }
            else if (Request.QueryString["oper"].ToString() == "edit")
            {
                MenuBLL bll = new MenuBLL();
                if (bll.UpdateMenuInfo(hfMenuGUID.Value, ParentMenuGuid.SelectedValue, MenuName.Text, MenuURL.Text, MenuType.SelectedValue))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
                }
                else
                {
                    ExecAlertScritp("菜单修改失败！");
                }
            }
        }
    }
}