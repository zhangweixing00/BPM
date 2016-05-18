using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using K2Utility;
using DevExpress.Web.ASPxEditors;

namespace OrgWebSite.Admin.Popup
{
    public partial class ProcessNodeEdit : BasePage
    {
        /// <summary>
        /// new和edit状态
        /// </summary>
        public string State
        {
            get
            {
                return Request.QueryString["state"];
            }
        }

        /// <summary>
        /// 节点所属流程ID
        /// </summary>
        public string ProcessID
        {
            get
            {
                return Request.QueryString["ProcessID"];
            }
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeID
        {
            get
            {
                return Request.QueryString["NodeID"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ProcessID))
                {
                    BindNodes();
                    BindDeptList();
                    if (State.Equals("edit", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(NodeID))
                    {
                        BindData();
                    }
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        ///  绑定流程下所有的审批节点
        /// </summary>
        private void BindNodes()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            List<ProcessNodeInfo> pnList = bll.GetProcessNodesByProcessID(ProcessID);
            ddlWayBack.DataSource = pnList;
            ddlWayBack.DataBind();
            ddlWayBack.Items.Insert(0, new ListItem());


            //绑定所属部门
            //List<PWorldDeptInfo> lPdi = bll.GetPWorldDepartment();
            //foreach (PWorldDeptInfo pdi in lPdi)
            //{
            //    ddlDept.Items.Add(new ListItem(pdi.DepartName, pdi.DepartCode));
            //}
            //ddlDept.Items.Insert(0, new ListItem());
        }

        private void BindDeptList()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
           
            //绑定所属部门
            List<PWorldDeptInfo> lPdi = bll.GetPWorldDepartment();
            this.gvDeptList.DataSource = lPdi;
            gvDeptList.DataBind();
           
        }
      
        /// <summary>
        /// 当为编辑状态(edit)时绑定数据
        /// </summary>
        private void BindData()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            ProcessNodeInfo pn = bll.GetProcessNodeByNodeID(NodeID);
            if (pn != null)
            {
                txtNodeName.Text = pn.NodeName;
                //cbMeet.Checked = pn.IsAllowMeet;
                //cbEndorsement.Checked = pn.IsAllowEndorsement;
                //cbIsAllowSpecialApproval.Checked = pn.IsAllowSpecialApproval;
                //txtApproveRule.Text = pn.ApproveRule.ToString();
                //txtDeclineRule.Text = pn.DeclineRule.ToString();
                txtURL.Text = pn.URL;
                txtNotification.Text = pn.Notification;

                txtOrderNo.Text = pn.OrderNo.ToString();

                if (pn.WayBackNodeID != Guid.Empty)
                {
                    foreach (ListItem li in ddlWayBack.Items)
                    {
                        if (li.Value.Equals(pn.WayBackNodeID.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }

                //绑定加权方式
                foreach (ListItem li in ddlWeighted.Items)
                {
                    if (li.Value.Equals(pn.WeightedType, StringComparison.OrdinalIgnoreCase))
                    {
                        li.Selected = true;
                        break;
                    }
                }

                txtSamplingRate.Text = pn.SamplingRate;
                if (pn.DepartCode !="")
                {
                    ListItem listItem = new ListItem(pn.DepartName, pn.DepartCode);
                    ddlDept.Items.Add(listItem); 
                }
                
                //所属部门
                //foreach (ListItem li in ddlDept.Items)
                //{
                //    if (li.Value.Equals(pn.DepartCode, StringComparison.OrdinalIgnoreCase))
                //    {
                //        li.Selected = true;
                //        break;
                //    }
                //}
            }
        }


        /// <summary>
        /// 创建页面对象
        /// </summary>
        /// <returns></returns>
        private ProcessNodeInfo CreateObject()
        {
            ProcessNodeInfo info = new ProcessNodeInfo();
            info.ProcessID = ProcessID;
            info.State = true;
            //if (string.IsNullOrEmpty(txtApproveRule.Text))
            info.ApproveRule = 1;
            //else
            //    info.ApproveRule = Int32.Parse(txtApproveRule.Text);

            //if (string.IsNullOrEmpty(txtDeclineRule.Text))
            info.DeclineRule = 1;
            //else
            //    info.DeclineRule = Int32.Parse(txtDeclineRule.Text);

            info.NodeName = txtNodeName.Text;
            //info.IsAllowMeet = cbMeet.Checked;
            //info.IsAllowEndorsement = cbEndorsement.Checked;
            info.IsAllowMeet = true;
            info.IsAllowEndorsement = true;
            info.Notification = txtNotification.Text;
            info.WayBack = 1;   //退回方式去除
            //info.IsAllowSpecialApproval = cbIsAllowSpecialApproval.Checked;
            info.URL = txtURL.Text;
            if (string.IsNullOrEmpty(ddlWayBack.SelectedValue))
                info.WayBackNodeID = Guid.Empty;
            else
                info.WayBackNodeID = new Guid(ddlWayBack.SelectedValue);
            info.WeightedType = ddlWeighted.SelectedValue;  //加权
            if (string.IsNullOrEmpty(txtSamplingRate.Text.Trim()))
                info.SamplingRate = string.Empty;
            else
                info.SamplingRate = txtSamplingRate.Text.Trim();
            info.CreatedBy = Page.User.Identity.Name;
            try
            {
                info.OrderNo = Convert.ToInt32(txtOrderNo.Text.Trim());
            }
            catch
            {
                info.OrderNo = 1;
            }
            if (ddlDept.SelectedItem != null)
            {
                info.DepartName = ddlDept.SelectedItem.Text;
                info.DepartCode = ddlDept.SelectedItem.Value;
            }
            else
            {
                info.DepartName = "";
                info.DepartCode = "";
            }
           
            return info;
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private bool AddProcessNode(ProcessNodeInfo pn)
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            return bll.InsertProcessNode(pn);
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private bool UpdateProcessNode(ProcessNodeInfo pn)
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            return bll.UpdateProcessNode(pn);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool ret = false;
            if (State.Equals("new", StringComparison.OrdinalIgnoreCase))
            {
                ret = AddProcessNode(CreateObject());
                if (ret)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                    RefreshClose(this.Page, "/Admin/ProcessNodeManage.aspx");
                }
                else
                {
                    ExecAlertScritp("添加失败");
                }
            }
            else if (State.Equals("edit", StringComparison.OrdinalIgnoreCase))
            {
                ProcessNodeInfo info = CreateObject();
                info.NodeID = new Guid(NodeID);
                ret = UpdateProcessNode(info);
                if (ret)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
                    RefreshClose(this.Page, "/Admin/ProcessNodeManage.aspx");
                }
                else
                {
                    ExecAlertScritp("更新失败");
                }
            }
        }

        public static void RefreshClose(Page page, string url)
        {

            string strScript = @"
    <script type='text/javascript'>
    function InsertCloseThis()
    {
       window.opener.location.href ='" + url + @"';
        window.opener = null;
        window.open('','_self'); 
        window.close();
        return false;
    }
    InsertCloseThis();
    </script>";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "CloseThis", strScript, false);

        }
        protected void gvDeptList_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            this.BindDeptList();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string hfdeptCode = ((HiddenField)(sender as ASPxButton).Parent.FindControl("hfdeptCode")).Value;
            string hfdeptName = ((HiddenField)(sender as ASPxButton).Parent.FindControl("hfdeptName")).Value;

            ddlDept.Items.Clear();
            ListItem listItem = new ListItem(hfdeptName,hfdeptCode);
            ddlDept.Items.Add(listItem);
        }
    }
}