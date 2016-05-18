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
    public partial class MyStarted1 : WorkSpaceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkFlowTypeBind();
                BindData("1");
                txtPageNum.Text = "1";
                txtCurrPage.Text = "1";
            }
        }

        protected void BindData(string PageNum)
        {
            MyApplicationBLL bll = new MyApplicationBLL();
            ds = bll.GetMyApplication(CurrentUserAdaccoutWithK2Lable, txtFolio.Text.Trim(), txtStartDate.Text, txtEndDate.Text, PageNum, PageSize, "K2WorkflowProject1\\Process1", "");

            if (ds != null && ds.Tables.Count > 2)
            {
                txtPageCount.Text = ds.Tables[2].Rows[0]["PageCount"].ToString();
                txtTotalNum.Text = ds.Tables[2].Rows[0]["TotalNum"].ToString();
                gvMyStarted.DataSource = ds.Tables[0];
                gvMyStarted.DataBind();
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
            //ddlProcess.DataSource = K2.BDAdmin.Settings.GetAllProcessSettings();
            //ddlProcess.DataTextField = "Description";
            //ddlProcess.DataValueField = "ProcessName";
            //ddlProcess.DataBind();

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

        protected void gvMyStarted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "CallBack")
            //{
            //    Control cmdControl = e.CommandSource as Control; // 表示触发事件的 IButtonControl，保持统一性并便于后续操作，我们这里直接转化为控件基类 Control
            //    GridViewRow row = cmdControl.NamingContainer as GridViewRow;
            //    int procinstid = Convert.ToInt32(gvMyStarted.DataKeys[row.RowIndex].Values[0]);
            //    if ((gvMyStarted.DataKeys[row.RowIndex].Values[1].ToString().Trim().ToLower() == "0" || gvMyStarted.DataKeys[row.RowIndex].Values[1].ToString().Trim().ToLower() == "1") && gvMyStarted.DataKeys[row.RowIndex].Values[2].ToString().Trim().ToLower() == "1")
            //    {
            //        WorkflowManagementServer oK2Conn = new WorkflowManagementServer(ConfigurationManager.AppSettings["K2ServerName"], 5555, ConfigurationManager.AppSettings["DomainName"] + "\\" + ConfigurationManager.AppSettings["K2ServiceUser"], ConfigurationManager.AppSettings["K2ServicePwd"], "K2", true);
            //        oK2Conn.Open();

            //        //if (oK2Conn.GotoActivity(procinstid, gvMyStarted.DataKeys[row.RowIndex].Values[3].ToString()))
            //        //{
            //        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "ymPrompt.succeedInfo({message:'任务已经被成功召回！',title:'召回成功'})", true);
            //        //}
            //        //else
            //        //{
            //        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "ymPrompt.errorInfo({message:'任务召回失败！',title:'召回错误'})", true);
            //        //}

            //        if (oK2Conn.GotoActivity(procinstid, "申请人重新提交"))
            //        {
            //            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "ymPrompt.succeedInfo({message:'任务已经被成功召回！',title:'召回成功'})", true);
            //            MessageBox.ShowAndPop(this.Page, "召回成功，您可以在‘我的任务’中重新处理该任务！", "/WorkSpace/MyWorklist.aspx");
            //        }
            //        else
            //        {
            //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "ymPrompt.errorInfo({message:'任务召回失败！',title:'召回错误'})", true);
            //        }
            //    }
            //    else
            //    {
            //        //审批人已经查看了该任务，拒绝被召回  ApproveCount FirstActivity
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "ymPrompt.errorInfo({message:'审批人已经处理了该任务，拒绝被召回！',title:'召回错误'})", true);
            //    }
            //}
        }
    }
}