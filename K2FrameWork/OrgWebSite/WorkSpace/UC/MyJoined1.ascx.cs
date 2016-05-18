using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using K2.BDAdmin;
using K2Utility;
using BLL;

namespace Sohu.OA.Web.WorkSpace.UC
{
    public partial class MyJoined1 : WorkSpaceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkFlowTypeBind();
                BindData("1");
                txtPageNum.Text = "1";
                txtCurrPage.Text = "1";
                //txtPageCount.Text = ds.Tables[2].Rows[0]["PageCount"].ToString();
                //txtTotalNum.Text = ds.Tables[2].Rows[0]["TotalNum"].ToString();
            }
        }

        protected void BindData(string PageNum)
        {
            MyJoinedBLL bll = new MyJoinedBLL();
            ds = bll.GetMyJoined(CurrentUserAdaccoutWithK2Lable, txtFolio.Text.Trim(), txtStartDate.Text, txtEndDate.Text, PageNum, PageSize,ddlProcess.SelectedValue,hfSubmittor.Value,"");

            if (ds != null && ds.Tables.Count > 1)
            {
                gvMyJoined.DataSource = ds.Tables[0];
                gvMyJoined.DataBind();
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
            BindData(pagenum.ToString());
        }

        protected void gvMyJoined_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatus = e.Row.FindControl("hfFlowStatus") as HiddenField;
                Label lbActName = e.Row.FindControl("lbActName") as Label;
                if (hfStatus != null && hfStatus.Value.Equals("Cancelled", StringComparison.OrdinalIgnoreCase) && lbActName != null)
                {
                    lbActName.Text = "申请人重提交";
                }
                else if (lbActName.Text.Equals("申请人重新提交"))
                {
                    lbActName.Text = "申请人重提交";
                }
            }
        }
    }
}