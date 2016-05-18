using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using Model;
using BLL;

namespace OrgWebSite.Admin.Popup
{
    public partial class UserEdit : K2Utility.BasePage
    {
        protected string deptCode
        {
            get
            {
                if (Request.QueryString["deptCode"] != null)
                    return Request.QueryString["deptCode"];

                return "";
            }
        }

        protected string action
        {
            get
            {
                if (Request.QueryString["action"] != null)
                    return Request.QueryString["action"];

                return "";
            }
        }

        protected string UserCode
        {
            get
            {
                if (Request.QueryString["userCode"] != null)
                    return Request.QueryString["userCode"];

                return "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPosition();
                BindExtPro();
                if (UserCode != "" && action == "edit")
                {
                    Page.Title = "人员编辑";
                    tdMain.Attributes["style"] = "";
                    ddlMainDept.Attributes["style"] = "";
                    InitUserInfo();
                    BindMainDept();
                }
                else
                {
                    tdMain.Attributes["style"] = "display:none;";
                    ddlMainDept.Attributes["style"] = "display:none;";
                }
            }
        }

        /// <summary>
        /// 绑定部门
        /// </summary>
        private void BindMainDept()
        {
            ddlMainDept.Items.Clear();
            DepartmentBLL bll = new DepartmentBLL();
            IList<DepartmentInfo> deptList = bll.GetDepartmentByUserCode(UserCode);
            if (deptList != null)
            {
                foreach(DepartmentInfo info in deptList)
                {
                    ListItem li = new ListItem();
                    li.Text = info.DeptName;
                    li.Value = info.DeptCode;
                    if (info.IsMainDept)
                        li.Selected = true;
                    ddlMainDept.Items.Add(li);
                }
            }
        }

        private void BindPosition()
        {
            //DataTable dt = DBManager.GetPosition();
            PositionBLL bll = new PositionBLL();
            IList<PositionInfo> pList = bll.GetPosition();
            if (pList != null)
            {
                ddlPosition.DataSource = pList;
                ddlPosition.DataTextField = "PositionName";
                ddlPosition.DataValueField = "ID";
                ddlPosition.DataBind();
                ddlPosition.Items.Insert(0, new ListItem("Choose", "-1"));
            }
        }

        /// <summary>
        /// 绑定扩展信息
        /// </summary>
        private void BindExtPro()
        {
            //DataTable dt = DBManager.GetUserExtProperty(UserCode);
            UserProfileBLL bll = new UserProfileBLL();
            IList<UserProfileExtPropertyInfo> upepList = bll.GetUserExtProperty(UserCode);
            if (upepList != null)
            {
                dlExtendInfo.DataSource = upepList;
                dlExtendInfo.DataBind();
            }
        }

        private void InitUserInfo()
        {
            UserProfileBLL bll = new UserProfileBLL();
            UserProfileInfo up = bll.GetUserProfile(UserCode);

            //UserProfile up = DBManager.GetUserProfile(UserCode);
            txtADAccount.Text = up.ADAccount;
            txtBirthDate.Text = up.Birthdate;
            txtCellPhone.Text = up.CellPhone;
            txtCHNName.Text = up.CHName;
            txtCostCenter.Text = up.CostCenter;
            txtEmail.Text = up.Email;
            txtENName.Text = up.ENName;
            txtHireDate.Text = up.HireDate;
            txtManagerAccount.Text = up.ManagerAccount;
            txtOfficePhone.Text = up.OfficePhone;
            txtFax.Text = up.FAX;
            txtBlackBerry.Text = up.BlackBerry;
            txtGraduateFrom.Text = up.GraduateFrom;
            txtOAC.Text = up.OAC;
            txtPA.Text = up.PoliticalAffiliation;
            txtPositionDesc.Text = up.PositionName;
            txtEductionBackground.Text = up.EducationalBackground;
            txtWorkExperienceBefore.Text = up.WorkExperienceBefore;
            txtWorkExperienceNow.Text = up.WorkExperienceNow;
            txtOrderNO.Text = up.OrderNo.ToString();
            txtPhotoUrl.Text = up.PhotoUrl;
            imgPhono.ImageUrl = string.IsNullOrEmpty(up.PhotoUrl) ? imgPhono.ImageUrl : up.PhotoUrl;
            ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(up.Gender));
            ddlPosition.SelectedIndex = ddlPosition.Items.IndexOf(ddlPosition.Items.FindByValue(up.PositionGuid.ToString()));
            ddlWorkPlace.SelectedIndex = ddlWorkPlace.Items.IndexOf(ddlWorkPlace.Items.FindByValue(up.WorkPlace));
        }

        private void BindDept()
        {
            DataTable dt = DBManager.GetSortDepartment();

            if (dt != null)
            {
                ddlDepts.DataSource = dt;
                ddlDepts.DataTextField = "sortname";
                ddlDepts.DataValueField = "DeptCode";
                ddlDepts.DataBind();

                ddlDepts.Items.Insert(0, new ListItem("Root", "R0"));
                ddlDepts.SelectedIndex = ddlDepts.Items.IndexOf(ddlDepts.Items.FindByValue(deptCode));
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (action == "new")
            {
                UserProfileBLL bll = new UserProfileBLL();
                UserProfileInfo up = GetUserInfo();

                if (!bll.IsExist(up))
                {
                    if (bll.CreateUserProfile(up))
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                    else
                        ExecAlertScritp("添加失败！");
                }
                else
                    ExecAlertScritp("用户已存在");
            }
            else if (action == "edit")
            {
                UserProfileBLL bll = new UserProfileBLL();
                UserProfileInfo up = bll.GetUserProfile(UserCode);
                UserProfileInfo upi = GetUserInfo();
                upi.ID = up.ID;

                bll.UpdateUserProfile(up);


                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    //添加扩展属性值
                    for (int i = 0; i < dlExtendInfo.Items.Count; i++)
                    {
                        HiddenField hfId = dlExtendInfo.Items[i].FindControl("hfPropertyID") as HiddenField;
                        TextBox txtValue = dlExtendInfo.Items[i].FindControl("txtPropertyValue") as TextBox;
                        if (hfId != null && txtValue != null)
                        {
                            //DBManager.AddExtValue(hfId.Value, txtValue.Text, UserCode);
                            bll.AddExtValue(hfId.Value, txtValue.Text, UserCode);
                        }
                    }
                    ts.Complete();  //提交事务
                }


                if (ddlMainDept.SelectedItem != null)
                {
                    if(bll.UpdateMainDepartment(UserCode, ddlMainDept.SelectedItem.Value))
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('操作成功');</script>");
                    else
                        ExecAlertScritp("操作失败！");
                }
            }
        }

        protected void btnShowPhoto_Click(object sender, EventArgs e)
        {
            imgPhono.ImageUrl = txtPhotoUrl.Text;
        }

        private UserProfileInfo GetUserInfo()
        {
            UserProfileInfo up = new UserProfileInfo();
            up.ADAccount = txtADAccount.Text;
            up.Birthdate = txtBirthDate.Text;
            up.CellPhone = txtCellPhone.Text;
            up.CHName = txtCHNName.Text;
            up.Email = txtEmail.Text;
            up.ENName = txtENName.Text;
            up.HireDate = txtHireDate.Text;
            up.ManagerAccount = txtManagerAccount.Text;
            up.OfficePhone = txtOfficePhone.Text;
            if (ddlPosition.SelectedIndex == 0)
                up.PositionGuid = Guid.Empty;
            else
                up.PositionGuid = new Guid(ddlPosition.SelectedValue);
            up.WorkPlace = ddlWorkPlace.SelectedValue;
            up.FAX = txtFax.Text;
            up.BlackBerry = txtBlackBerry.Text;
            up.GraduateFrom = txtGraduateFrom.Text;
            up.OAC = txtOAC.Text;
            up.PoliticalAffiliation = txtPA.Text;
            up.Gender = ddlGender.SelectedValue;
            up.EducationalBackground = txtEductionBackground.Text;
            up.WorkExperienceBefore = txtWorkExperienceBefore.Text;
            up.WorkExperienceNow = txtWorkExperienceNow.Text;

            try
            {
                up.OrderNo = int.Parse(txtOrderNO.Text);
            }
            catch
            {
                up.OrderNo = 0;
            }

            up.PhotoUrl = txtPhotoUrl.Text;

            return up;
        }
    }
}