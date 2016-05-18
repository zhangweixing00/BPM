using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace K2Utility
{
    public abstract class ProcessControl : WorkflowControl
    {
        #region control properties

        #region control can or not be modified
        private bool _enabledModify = false;
        /// <summary>
        /// control can or not be modified
        /// </summary>
        public bool EnabledModify
        {
            get
            {
                return _enabledModify;
            }
            set
            {
                _enabledModify = value;
            }
        }
        private bool _isEnableEditPage = false;
        /// <summary>
        /// is edit page add by lee
        /// </summary>
        public bool IsEnableEditPage
        {
            get
            {
                return _isEnableEditPage;
            }
            set
            {
                _isEnableEditPage = value;
            }
        }

        private bool _enabledEdit = false;
        /// <summary>
        /// control can or not be edit
        /// </summary>
        public bool EnabledEdit
        {
            get
            {
                return _enabledEdit;
            }
            set
            {
                _enabledEdit = value;
            }
        }
        #endregion

        #endregion

        #region base methods

        #region onload
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                LoadData();
                if (!(Page is ViewPage) && !string.IsNullOrEmpty(Request.QueryString["ProcessID"]))
                {
                    //SqlParameter[] paras = new SqlParameter[3];
                    //paras[0] = new SqlParameter("@inbProcessID", TaskPage.ProcessID);
                    //paras[1] = new SqlParameter("@nvchrFlowName", TaskPage.FlowName);
                    //paras[2] = new SqlParameter("@nvchrFieldName", "Status");
                    //using (SqlDataReader sdr = SqlHelper.ExecuteReader(Database.MerckHRWorkFlow, "GetProcessInfoByProcessID", paras))
                    //{
                    //    if (sdr.Read())
                    //    {
                    //        if (sdr[0].ToString().ToLower() != ProcessStatus.Draft.ToString().ToLower())
                    //        {
                    //            if (!EnabledEdit)
                    //            {
                    //                throw new Exception("00003 You have no permission to open this process.");
                    //            }
                    //        }
                    //    }
                    //}

                    //权限判断，需要补充代码
                }
            }
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", "var processClient='" + this.ClientID + "_'", true);
        }
        #endregion

        #region on prerender
        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!EnabledModify)
                    DisabledControl(this);
            }

            base.OnPreRender(e);
        }
        #endregion

        #endregion

        #region control methods

        #region abstruct method
        /// <summary>
        /// load process data
        /// </summary>
        public abstract void LoadData();
        /// <summary>
        /// save process data
        /// </summary>
        /// <param name="action">process action</param>
        /// <returns>successfully or not</returns>
        public abstract bool SaveData(ProcessAction action);
        /// <summary>
        /// get process data fields
        /// </summary>
        public abstract void GetDataFields();

        /// <summary>
        /// get process xml fields
        /// </summary>
        public abstract void GetXmlFields();

        /// <summary>
        /// update process data fields
        /// </summary>
        public abstract void UpdateDataFields();

        /// <summary>
        /// get approve xml
        /// </summary>
        /// <returns>start process of xml</returns>
        public abstract string GetApproveXml();
        #endregion

        #region disabled control
        /// <summary>
        /// 去掉边框
        /// </summary>
        /// <param name="ctl"></param>
        public void DisabledControl(Control ctl)
        {
            if (ctl.HasControls())
            {
                foreach (Control col in ctl.Controls)
                {
                    /*添加日期2011-7-15  添加目的：附件控件不能置为不可用*/
                    if (!(TaskPage.WorkflowID == 3 && ctl.ClientID == "CDF1_Upload1"))
                    {
                        DisabledControl(col);
                    }
                }
            }
            else
            {
                if (ctl is TextBox)
                {
                    TextBox txt = (TextBox)ctl;
                    txt.Attributes.Remove("onfocus");
                    txt.Attributes.Remove("onblur");
                    txt.ReadOnly = true;
                }
                else if (ctl is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctl;
                    DisabledRadioButtonList(rbl);
                }
                else if (ctl is CheckBox && TaskPage.WorkflowID != 3)       // && TaskPage.WorkflowID != 3姚骁然添加（排除自定义流程，因为要上传附件）
                    ((CheckBox)ctl).Attributes.Add("onclick", "this.checked=!this.checked");

                else if (ctl is LinkButton || ctl is DropDownList || ctl is ImageButton || ctl is RadioButton) //added ImageButton by pccai
                    ((WebControl)ctl).Enabled = false;
            }
        }
        public void DisabledRadioButtonList(RadioButtonList rbl)
        {
            string radioId = rbl.ClientID + "_" + rbl.SelectedIndex.ToString();

            for (int i = 0; i < rbl.Items.Count; i++)
            {
                rbl.Items[i].Attributes.Add("onclick", "document.getElementById('" + radioId + "').checked='checked';return;");
            }
        }
        #endregion

        #region enabled control
        public void EnabledControl(Control ctl)
        {
            if (ctl.HasControls())
            {
                foreach (Control col in ctl.Controls)
                {
                    EnabledControl(col);
                }
            }
            else
            {
                if (ctl is TextBox)
                    ((TextBox)ctl).ReadOnly = false;
                else if (ctl is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctl;
                    for (int i = 0; i < rbl.Items.Count; i++)
                    {
                        rbl.Items[i].Attributes.Remove("onclick");
                    }
                }
                else if (ctl is CheckBox)
                    ((CheckBox)ctl).Attributes.Remove("onclick");

                else if (ctl is Button || ctl is FileUpload || ctl is LinkButton || ctl is DropDownList)
                    ((WebControl)ctl).Enabled = true;
            }
        }
        #endregion



        #endregion
    }
}