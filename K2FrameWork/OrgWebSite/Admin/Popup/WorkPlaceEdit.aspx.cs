using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace OrgWebSite.Admin.Popup
{
    public partial class WorkPlaceEdit : K2Utility.BasePage
    {
        public string ID
        {
            get
            {
                return hfID.Value;
            }
            set
            {
                hfID.Value = value;
            }
        }

        public string Status
        {
            get
            {
                return hfStatus.Value;
            }
            set
            {
                hfStatus.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["state"]))
                {
                    Status = Request.QueryString["state"];
                }
                
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    ID = Request.QueryString["ID"];
                    Bind();
                }
            }
        }

        private void Bind()
        {
            WorkPlaceBLL bll = new WorkPlaceBLL();
            WorkPlaceInfo info = bll.GetWorkPlaceByID(ID);
            if (info != null)
            {
                txtPlaceName.Text = info.PlaceName;
                txtPlaceCode.Text = info.PlaceCode;
            }

            if (Status == "view")
            {
                txtPlaceCode.Enabled = false;
                txtPlaceName.Enabled = false;
                ibtAdd.Enabled = false;
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            WorkPlaceBLL bll = new WorkPlaceBLL();
            if (Status == "new")
            {
                bool exists = bll.IsExists(txtPlaceCode.Text.Trim());   //判断是否存在
                if (exists)
                {
                    ExecAlertScritp("该编码已存在");
                    return;
                }
            }

            string Id = ID;
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();

            bool ret = bll.AddEditWorkPlace(Id, txtPlaceName.Text.Trim(), txtPlaceCode.Text.Trim());
            if (ret)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加或编辑成功');</script>");
            }
            else
            {
                ExecAlertScritp("添加失败，请联系管理员");
            }
        }
    }
}