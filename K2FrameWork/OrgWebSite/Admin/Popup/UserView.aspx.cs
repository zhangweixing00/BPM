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
    public partial class UserView : BasePage
    {
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
                InitUserInfo();
                if (Request.QueryString["from"] != null)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "hidden", "HiddenSitemap();", true);
                }
            }
        }

        private void BindPosition()
        {
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

        private void InitUserInfo()
        {
            //UserProfile up = DBManager.GetUserProfile(UserCode);
            UserProfileBLL bll = new UserProfileBLL();
            UserProfileInfo up = bll.GetUserProfile(UserCode);
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
            txtEductionBackground.Text = up.EducationalBackground;
            txtWorkExperienceBefore.Text = up.WorkExperienceBefore;
            txtWorkExperienceNow.Text = up.WorkExperienceNow;
            txtOrderNO.Text = up.OrderNo.ToString();
            txtPhotoUrl.Text = up.PhotoUrl;
            imgPhono.ImageUrl = string.IsNullOrEmpty(up.PhotoUrl) ? imgPhono.ImageUrl : up.PhotoUrl;
            ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(up.Gender));
            ddlPosition.SelectedIndex = ddlPosition.Items.IndexOf(ddlPosition.Items.FindByValue(up.PositionGuid.ToString()));
            ddlWorkPlace.SelectedIndex = ddlWorkPlace.Items.IndexOf(ddlWorkPlace.Items.FindByValue(up.WorkPlace));
            //DataTable dt = DBManager.GetUserMainDepartmentByCode(UserCode);
            //if (dt != null && dt.Rows.Count > 0)
            //    txtMainDeptName.Text = dt.Rows[0]["Department"].ToString();

            DataTable dt = DBManager.GetUserMainDepartmentByCode(UserCode);
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (Boolean.Parse(dr["IsMainDept"].ToString()))
                        {
                            txtMainDeptName.Text = dr["DeptName"].ToString();
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
    }
}