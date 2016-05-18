using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using K2.BDAdmin;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using K2Utility;
using Model;
using BLL;

namespace Sohu.OA.Web.WorkSpace.UC
{
    public partial class MyDelegation : WorkSpaceControl
    {
        public string PageCount
        {
            get
            {
                return ViewState["pageCount"].ToString();
            }
            set
            {
                ViewState["pageCount"] = value;
            }
        }


        protected DataSet dtDelegation
        {
            get
            {
                if (ViewState["dtDelegation"] == null)
                    ViewState["dtDelegation"] = new DataSet();
                return ViewState["dtDelegation"] as DataSet;
            }
            set
            {
                ViewState["dtDelegation"] = value;
            }
        }
        //private Biz_MyDelegation deletaion = new Biz_MyDelegation();
        //private T_MyDeligation tdele = new T_MyDeligation();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkFlowTypeBind();
                DeligationBind();
            }
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DeligationBind();
        }
        protected void WorkFlowTypeBind()
        {
            ddlProcess.DataSource = Utility.Settings.GetAllProcessSettings();
            ddlProcess.DataTextField = "Description";
            ddlProcess.DataValueField = "ProcessFullName";
            ddlProcess.DataBind();

            ddlProcess.Items.Insert(0, new ListItem("全部", "All"));
            ddlProcess.Items.Insert(1, new ListItem("所有流程", "AllProc"));
            ddlProcess.SelectedIndex = 0;
        }

        protected void DeligationBind()
        {
            DelegationBLL bll = new DelegationBLL();
            dtDelegation = bll.GetDelegation(ddlProcess.SelectedValue,CurrentUser, txtSubmittor.Text, txtStartDate.Text, txtEndDate.Text, ddlFlowStatus.SelectedValue);
              
            GvDataBind();

        }
        protected string GetFlowName(string name)
        {
            return name == "All" ? "所有流程" : "";// K2.BDAdmin.Settings.GetProcessDescription(name);
        }
        private void GvDataBind()
        {
            DataTable dt = dtDelegation.Tables[0];
            if (dt.Rows.Count > 0)
            {
                gvDeligation.PageSize = Convert.ToInt32(this.PageSize);
                
                //dtDelegation.Sort(delegate(T_MyDeligation p1, T_MyDeligation p2) { return Comparer<int?>.Default.Compare(p2.State, p1.State); }); 
                PageCount = dt.Rows.Count.ToString();
                gvDeligation.DataSource = dt;
                gvDeligation.DataBind();
            }
            else
            {
                gvDeligation.DataSource = null;
                gvDeligation.DataBind();
            }
        }

        protected void gvDeligation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int id = Convert.ToInt32(gvDeligation.DataKeys[e.RowIndex].Values[0].ToString());
            //T_MyDeligation del = dtDelegation.Find(delegate(T_MyDeligation d) { return d.ID == id; });
            //dtDelegation.Remove(del);
            //del.State = -1;
            //del.ModifiedByCode = Employee.EmployeeCode;
            //del.ModifiedOn = DateTime.Now;
            //del.DeleteDate = DateTime.Now;
            //deletaion.UpdateMydelegation(del);
            //dtDelegation.Insert(dtDelegation.Count, del);
            //this.GvDataBind();
        }
        protected void gvDeligation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (-2 == e.NewPageIndex)
            {
                TextBox txtNewPageIndex = null;
                GridViewRow pagerRow = theGrid.BottomPagerRow;
                if (null != pagerRow)
                {
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (null != txtNewPageIndex)
                {
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
                }
            }
            else
            {
                newPageIndex = e.NewPageIndex;
            }
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;
            theGrid.PageIndex = newPageIndex;

            GvDataBind();
        }

        protected void gvDeligation_PreRender(object sender, EventArgs e)
        {
            if (gvDeligation.BottomPagerRow != null)
                gvDeligation.BottomPagerRow.Visible = true;
        }
               
        private DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {

            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();

                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }

                }

                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        protected void gvDeligation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //绑定时判断
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //取得备注字段
            //    Label lbRemark = e.Row.FindControl("Label1") as Label;
            //    HiddenField lbBD = e.Row.FindControl("lbBD") as HiddenField;
            //    LinkButton LinkButton0 = e.Row.FindControl("LinkButton0") as LinkButton;

            //    if (lbRemark != null)
            //    {
            //        lbRemark.Attributes["title"] = lbRemark.Text;
            //        lbRemark.Text = SubStringStyle.GetSubString(lbRemark.Text, 18, true);
            //    }

            //    //取得状态字段
            //    Label lbStatus = e.Row.FindControl("lbStatus") as Label;
            //    if (lbStatus != null)
            //    {
            //        if (lbStatus.Text == "1")
            //            lbStatus.Text = "有效";
            //        else if (lbStatus.Text == "0")
            //            lbStatus.Text = "过期";
            //        else if (lbStatus.Text == "-1")
            //            lbStatus.Text = "取消";
            //        else
            //            lbStatus.Text = "异常状态";
            //    }

            //    if (LinkButton0 != null && !lbBD.Value.Equals(ConfigurationManager.AppSettings["DomainName"] + "\\" + Page.User.Identity.Name.Split('@')[0]))
            //    {
            //        LinkButton0.Attributes["style"] = "display:none";
            //    }
            //}
        }
    }
}