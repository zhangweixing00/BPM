using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace Sohu.OA.Web.Manage.RoleManage.UC
{
    public partial class UC_RoleInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetRoleName();
        }

        /// <summary>
        /// 显示角色名称 
        /// </summary>
        void SetRoleName()
        {
            string roleCode = Server.UrlDecode(Request["rolecode"]);
            
            if (!String.IsNullOrEmpty(roleCode))
            {
                this.RoleCode = roleCode;
                RoleBLL bll = new RoleBLL();
                RoleInfo info = bll.GetRoleByRoleCode(RoleCode);
                OrganizationBLL orgbll = new OrganizationBLL();
                OrganizationInfo orginfo = orgbll.GetOrgByID(info.OrgID.ToString());

                hfRoleName.Text = info.RoleName;
                Description.Text = info.Desciption;
                if (orginfo != null)
                    lblOrg.Text = orginfo.OrgName;
                lblProc.Text = info.ProcessType;
            }            
        }

        public string RoleCode
        {
            get;
            set;
        }
    }
}