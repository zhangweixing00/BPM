using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorldBPM.Business.Sys;

public partial class Modules_BaseData_DeptOperation : UPageBase
{
    #region 声明

    BFPmsPermission pms = new BFPmsPermission();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh("");
        }
    }
    /// <summary>
    /// 刷新页面
    /// </summary>
    /// <param name="DepartName"></param>
    private void Refresh(string Remark)
    {
        IList<V_Pworld_Department> ds = SysContext.V_Pworld_Department.ToList();

        if (!string.IsNullOrEmpty(Remark))
        {
            ds = ds.Where(x => x.DepartName.Contains(Remark)).ToList();
        }
        //根据公司编号筛选出资源投资以及资源集团的所有部门
        //ds = ds.Where(x => x.DepartCode.Contains("S363-S973") || x.DepartCode.Contains("S363-S969")).ToList();
        if (!string.IsNullOrEmpty(Request["dept"]))
        {
            string[] depts = Request["dept"].Split(',');
            ds = ds.Where(x =>
            {
                foreach (string item in depts)
                {
                    if (x.DepartCode.Contains(item))
                        return true;
                }
                return false;
            }).ToList();
        }
        gvData.DataSource = ds;
        gvData.DataBind();
    }

    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        string result = "";
        try
        {
            string Remark = e.CommandArgument.ToString();
            var uInfo = SysContext.V_Pworld_Department.FirstOrDefault(x => x.Remark == Remark);
            result = JsonHelper.JsonSerializer<V_Pworld_Department>(uInfo);
        }
        catch (Exception)
        {

        }
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", result));
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Refresh(tbDepartName.Text);
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        Refresh(tbDepartName.Text);
    }
}