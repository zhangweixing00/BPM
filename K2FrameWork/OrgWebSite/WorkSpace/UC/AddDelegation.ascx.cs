using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using K2Utility;
using BLL;
using Model;

namespace Sohu.OA.Web.WorkSpace.UC
{
    public partial class AddDelegation : WorkSpaceControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.txtFromUser.Text = this.CurrentUser;
            this.hfFromUser.Value = this.CurrentUser;
            this.txtToUser.Attributes.Add("readonly", "readonly");
            //this.txtBDeligation.Attributes.Add("onclick", "SelectSubmitor(this);");
            this.txtToUser.Attributes.Add("readonly", "readonly");
            this.txtToUser.Attributes.Add("onclick", "SelectSubmitor(this);");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WorkFlowTypeBind();
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            DelegationBLL bll = new DelegationBLL();
            DelegationInfo info = new DelegationInfo();
            info.ProcName = ddlProcess.SelectedValue;
            info.ActivityName = "";
            info.Conditions = rbtnNew.Checked;
            info.FromUser = txtFromUser.Text;
            info.ToUser = txtToUser.Text;
            info.StartDate = Convert.ToDateTime(txtStartDate.Text);
            info.EndDate =  Convert.ToDateTime(txtEndDate.Text + " 23:59:59.998");
            info.CreateDate = System.DateTime.Now;
            info.CreatedByUser = CurrentUser;
            info.Remark = txtRemark.Text;
            info.State = "1";

            bool result = bll.CreateDelegation(info);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "ss", "alert('添加代理成功成功');", true);
            if (result)
            {
                //MessageBox.Show(this.Page, "添加代理成功！");
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "js", "window.parent.ymPromptclose('ok');", true);
            }
            //else if (result == "false")
            //{
            //    MessageBox.Show(this.Page, "添加代理失败！");
            //}
            //else if (result == "falsecontain")
            //{
            //    MessageBox.Show(this.Page, "添加代理失败！<br/><br/>在此时间段存在您已经选择过" + GetEmployeeNameByAd(tdele.Deligationor) + "作为代理人的流程，不能重复代理！");
            //}
            //else if (result == "falserecursive" || result == "falseall")
            //{
            //    MessageBox.Show(this.Page, "添加代理失败！<br/><br/>在此时间段存在您已经被选为代理人的流程，不能再选" + GetEmployeeNameByAd(tdele.Deligationor) + "作为代理人了！");
            //}
        }

        protected void WorkFlowTypeBind()
        {
            ddlProcess.DataSource = Utility.Settings.GetAllProcessSettings();
            ddlProcess.DataTextField = "Description";
            ddlProcess.DataValueField = "ProcessFullName";
            ddlProcess.DataBind();

            ddlProcess.Items.Insert(0, new ListItem("所有流程", "All"));
            ddlProcess.SelectedIndex = 0;
        }

    }
}
