using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using K2Utility;
using BLL;

namespace Sohu.OA.Web.WorkSpace.UC
{
    public partial class MyWorkList1 : WorkSpaceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkFlowTypeBind();
                BindData("1");
                txtPageNum.Text = "1";
                txtCurrPage.Text = "1";

                #region 为了避免找不到表2,在此处判断下数据--create by guochao
                if (ds != null && ds.Tables.Count > 2)
                {
                    txtPageCount.Text = ds.Tables[2].Rows[0]["PageCount"].ToString();
                    txtTotalNum.Text = ds.Tables[2].Rows[0]["TotalNum"].ToString();
                }
                else
                {
                    txtPageCount.Text = "0";
                    txtTotalNum.Text = "0";
                    txtCurrPage.Text = "1";
                }
                #endregion
            }
        }

        protected void BindData(string PageNum)
        {
            WorlListBLL workListBll = new WorlListBLL();
            ds = workListBll.GetWorkList(PageNum, PageSize, ddlProcess.SelectedValue, CurrentUserAdaccoutWithK2Lable, txtFolio.Text, txtStartDate.Text, txtEndDate.Text, ddlFlowStatus.SelectedValue, hfSubmittor.Value, "");
            if (ds != null && ds.Tables.Count > 2)
            {
                gvMyWorkList.DataSource = ds.Tables[0];
                gvMyWorkList.DataBind();
                divfenye.Visible = ds.Tables[0].Rows.Count > 0 ? true : false;

                txtPageCount.Text = ds.Tables[2].Rows[0]["PageCount"].ToString();
                txtTotalNum.Text = ds.Tables[2].Rows[0]["TotalNum"].ToString();

                //判断显示
                if (!string.IsNullOrEmpty(txtPageCount.Text.Trim()))
                {
                    int pageCount = int.Parse(txtPageCount.Text.Trim());
                    if (pageCount == 1)
                    {
                        lbFirst.Attributes["style"] = "display:none;";
                        lbPrev.Attributes["style"] = "display:none;";
                        lbNext.Attributes["style"] = "display:none;";
                        lbLast.Attributes["style"] = "display:none;";
                    }
                    else if (pageCount > 0 && PageNum == "1")
                    {
                        lbFirst.Attributes["style"] = "display:none;";
                        lbPrev.Attributes["style"] = "display:none;";
                        lbNext.Attributes["style"] = "display:inline;";
                        lbLast.Attributes["style"] = "display:inline;";
                    }
                    else if (pageCount > 0 && PageNum == pageCount.ToString())
                    {
                        lbNext.Attributes["style"] = "display:none;";
                        lbLast.Attributes["style"] = "display:none;";
                        lbFirst.Attributes["style"] = "display:inline;";
                        lbPrev.Attributes["style"] = "display:inline;";
                    }
                    else
                    {
                        lbFirst.Attributes["style"] = "display:inline;";
                        lbPrev.Attributes["style"] = "display:inline;";
                        lbNext.Attributes["style"] = "display:inline;";
                        lbLast.Attributes["style"] = "display:inline;";
                    }
                }
            }
        }
        protected void WorkFlowTypeBind()
        {
            ddlProcess.DataSource = Utility.Settings.GetAllProcessSettings();
            ddlProcess.DataTextField = "Description";
            ddlProcess.DataValueField = "ProcessFullName";
            ddlProcess.DataBind();

            ddlProcess.Items.Insert(0, new ListItem("全部", ""));
            ddlProcess.SelectedIndex = 0;
        }

        protected void gvMyWorkList_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

            BindData("1");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData("1");
            txtPageNum.Text = "1";
            txtCurrPage.Text = "1";
            txtPageCount.Text = ds.Tables[2].Rows[0]["PageCount"].ToString();
            txtTotalNum.Text = ds.Tables[2].Rows[0]["TotalNum"].ToString();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPage_Command(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            hfSelectedSN.Value = "";

            int curpage;
            switch (commandName)
            {
                case "First":
                    BindData("1");
                    txtPageNum.Text = "1";
                    txtCurrPage.Text = "1";
                    break;
                case "Prev":
                    if (txtCurrPage.Text == "1")
                    {
                        txtCurrPage.Text = "1";
                        txtPageNum.Text = "1";
                    }
                    else
                    {
                        curpage = Convert.ToInt32(txtPageNum.Text);
                        int prevPage = curpage - 1;
                        txtCurrPage.Text = prevPage.ToString();
                        txtPageNum.Text = prevPage.ToString();
                    }

                    BindData(txtPageNum.Text);

                    break;

                case "Next":
                    curpage = Convert.ToInt32(txtCurrPage.Text);
                    int nextPage = curpage + 1;
                    if (nextPage > Convert.ToInt32(txtPageCount.Text))
                        nextPage = Convert.ToInt32(txtPageCount.Text);
                    if (nextPage == 0)
                    {
                        nextPage = 1;
                    }
                    txtCurrPage.Text = nextPage.ToString();
                    txtPageNum.Text = nextPage.ToString();
                    BindData(txtCurrPage.Text);
                    break;

                case "Last":
                    txtCurrPage.Text = txtPageCount.Text;
                    txtPageNum.Text = txtPageCount.Text;
                    BindData(txtCurrPage.Text);
                    break;

                default:
                    BindData("1");
                    break;
            }
        }

        /// <summary>
        /// 转到某页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoPage_Click(object sender, EventArgs e)
        {
            int pagenum = Convert.ToInt32(txtPageNum.Text);

            if (pagenum > Convert.ToInt32(txtPageCount.Text))
            {
                pagenum = Convert.ToInt32(txtPageCount.Text);
            }

            if (pagenum < 1)
            {
                pagenum = 1;
            }

            txtPageNum.Text = pagenum.ToString();
            txtCurrPage.Text = pagenum.ToString();
            hfSelectedSN.Value = "";
            BindData(pagenum.ToString());
        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            string toUserAD = hfAdAcount.Value;
            string[] sns = hfSelectedSN.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            WorlListBLL workList = new WorlListBLL();
            foreach (string sn in sns)
            {
                workList.Redirect(sn, toUserAD);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "alert('转发成功');", true);
            btnGoPage_Click(sender, e);
        }

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            string[] sns = hfSelectedSN.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sn in sns)
            {
                WorlListBLL workList = new WorlListBLL();
                workList.Release(sn);
            }

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "alert('任务释放成功');", true);
            btnGoPage_Click(sender, e);
        }

        protected void btnSleep_Click(object sender, EventArgs e)
        {
            string[] sns = hfSelectedSN.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sn in sns)
            {
                WorlListBLL workList = new WorlListBLL();
                workList.Sleep(sn, Convert.ToInt32(hfSleep.Value));
            }

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "alert('休眠成功');", true);
            btnGoPage_Click(sender, e);
        }

        protected void btnDelegate_Click(object sender, EventArgs e)
        {
            string[] sns = hfSelectedSN.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            string toUserAD = hfAdAcount.Value;
            foreach (string sn in sns)
            {
                WorlListBLL workList = new WorlListBLL();
                workList.Delegate(sn, toUserAD);
            }

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "alert('代理成功');", true);
            btnGoPage_Click(sender, e);
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string[] sns = hfSelectedSN.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string sn in sns)
            {
                WorlListBLL workList = new WorlListBLL();
                workList.Approve(sn, "Task Completed", null);
            }

            btnGoPage_Click(sender, e);
            chkBatch.Checked = false;
        }

    }
}