using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using Utility;
using System.Xml;
using K2Utility;

namespace OrgWebSite.Admin
{
    public partial class ApproveRuleManage : Utility.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProcessType();
                BindGroup();
                BindSearchRuleTable();
            }
            else
            {
                if (Request.Form["__EVENTTARGET"] == "lbtnEditTable")
                {
                    lbtnEditTable_Click(null, null);
                }
                else if (Request.Form["__EVENTTARGET"] == "lbReload1")
                {
                    lbReload_Click(null, null);
                }
            }
        }

        private void BindProcessType()
        {
            ddlProcessType.Items.Clear();
            string loginName = Page.User.Identity.Name.Split('\\')[1];
            loginName = "hechunhai";
            ddlProcessType.DataSource = WebCommon.GetDeptListByEmployeeCode(loginName);
            ddlProcessType.DataBind();
        }

        private void BindGroup()
        {
            ddlGroup.Items.Clear();
            ProcessRuleBLL bll = new ProcessRuleBLL();
            IList<ApproveRuleGroupInfo> argi = bll.GetApproveRuleGroupNameByProcessType(ddlProcessType.SelectedValue);
            if (argi != null && argi.Count > 0)
            {
                foreach (ApproveRuleGroupInfo tmp in argi)
                {
                    ListItem li = new ListItem();
                    li.Text = tmp.GroupName;
                    li.Value = tmp.GroupName;
                    ddlGroup.Items.Add(li);
                }
            }
        }

        private void ReviewSearch(IList<ApproveRuleGroupInfo> argList)
        {
            if (argList != null)
            {
                ViewState["SearchTable"] = argList;
                appedTable.Controls.Clear();
                foreach (ApproveRuleGroupInfo info in argList)
                {
                    //创建“编辑”和“删除”按钮
                    ImageButton btnEdit = new ImageButton();
                    btnEdit.ID = "btnEdit" + Guid.NewGuid().ToString();
                    btnEdit.ImageUrl = "../pic/btnImg/btnEdt_nor.png";
                    btnEdit.Attributes["ApproveID"] = info.ID.ToString();
                    btnEdit.OnClientClick = "return EditTable('" + info.ID.ToString() + "')";

                    ImageButton btnDel = new ImageButton();
                    btnDel.ID = "btnDel" + Guid.NewGuid().ToString();
                    btnDel.ImageUrl = "../pic/btnImg/btnDelete_nor.png";
                    btnDel.Attributes["ApproveID"] = info.ID.ToString();
                    btnDel.OnClientClick = "return DelTable('" + info.ID.ToString() + "')";

                    appedTable.Controls.Add(btnEdit);
                    appedTable.Controls.Add(btnDel);
                    ProcessRuleBLL bll = new ProcessRuleBLL();
                    DataSet ds = bll.GetApproveTableExtendByTableName(info.RuleTableName, info.ProcessID.ToString());     //审批表
                    GridView gvTable = new GridView();
                    gvTable.RowDataBound += new GridViewRowEventHandler(gvTable_RowDataBound);
                    gvTable.Width = new Unit(540);
                    gvTable.CssClass = "datalist2";
                    gvTable.DataSource = ds.Tables[0];
                    gvTable.DataBind();
                    appedTable.Controls.Add(gvTable);
                }
            }
        }

        /// <summary>
        /// 绑定查询后的审批表
        /// </summary>
        private void BindSearchRuleTable()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            if (ddlGroup.Items.Count > 0 && ddlProcessType.Items.Count > 0)
            {
                IList<ApproveRuleGroupInfo> argList = bll.GetApproveRuleGroupByGroupNameAndProcessID(ddlGroup.SelectedItem.Text, ddlProcessType.SelectedValue);
                if (argList != null)
                {
                    ViewState["SearchTable"] = argList;
                    appedTable.Controls.Clear();
                    foreach (ApproveRuleGroupInfo info in argList)
                    {
                        //创建“编辑”和“删除”按钮
                        ImageButton btnEdit = new ImageButton();
                        btnEdit.ID = "btnEdit" + info.ID.ToString();
                        btnEdit.ImageUrl = "../pic/btnImg/btnEdt_nor.png";
                        btnEdit.Attributes["ApproveID"] = info.ID.ToString();
                        btnEdit.OnClientClick = "return EditTable('" + info.ID.ToString() + "')";

                        ImageButton btnDel = new ImageButton();
                        btnDel.ID = "btnDel" + info.ID.ToString();
                        btnDel.ImageUrl = "../pic/btnImg/btnDelete_nor.png";
                        btnDel.Attributes["ApproveID"] = info.ID.ToString();
                        btnDel.OnClientClick = "return DelTable('" + info.ID.ToString() + "')";

                        appedTable.Controls.Add(btnEdit);
                        appedTable.Controls.Add(btnDel);

                        DataSet ds = bll.GetApproveTableExtendByTableName(info.RuleTableName, info.ProcessID.ToString());     //审批表
                        GridView gvTable = new GridView();
                        gvTable.RowDataBound += new GridViewRowEventHandler(gvTable_RowDataBound);
                        gvTable.Width = new Unit(540);
                        gvTable.CssClass = "datalist2";
                        DataTable dt = null;
                        if (ds !=null)
                        {
                            dt = ds.Tables[0];
                        }
                        gvTable.DataSource = ds;
                        gvTable.DataBind();
                        appedTable.Controls.Add(gvTable);
                    }
                }
            }
        }

        protected void gvTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Attributes["style"] = "display:none;";
        }

       

        protected void lbReload_Click(object sender, EventArgs e)
        {
            //重新绑定
            BindProcessType();
            BindGroup();
            //BindRuleTable();
        }

       

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            hfSelectedApproveNode.Value = string.Empty;
            hfSelectedRequestNode.Value = string.Empty;
            hfSelectedCheckBox.Value = string.Empty;
            BindSearchRuleTable();
            //BindRuleTable();
        }

        protected void lbtnEditTable_Click(object sender, EventArgs e)
        {
            string tableId = Request.Form["__EVENTARGUMENT"];
            hfSelectedApproveNode.Value = string.Empty;
            hfSelectedRequestNode.Value = string.Empty;
            hfSelectedCheckBox.Value = string.Empty;
            string url = "/Admin/ApproveRuleEdit.aspx?ProcessID=" + ddlProcessType.SelectedValue + "&TableId=" + tableId;
            ShowUrl(this.Page, url);
           
        }

        public void ShowUrl(Page page, string url)
        {
            string strScript = @"
    <script type='text/javascript'>
    function OpenUrl()
    {
       window.open('" + url + @"', '_blank');
       
    }
    OpenUrl();
    </script>";

            ScriptManager.RegisterStartupScript(page, typeof(System.Web.UI.Page), "CloseThis1", strScript, false);
        }
        protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGroup();
        }

       

        
    }
}