using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BLL;
using Model;
using Utility;

namespace OrgWebSite.Admin.FormDesign
{
    /// <summary>
    /// Summary description for FormDesignAjaxHandler
    /// </summary>
    public class FormDesignAjaxHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Form["action"]))
            {
                String retString = string.Empty;
                switch (context.Request.Form["action"])
                {
                    case "getCLDataSource":
                        retString = GetCLDataSource();
                        break;
                    case "delCLByID":
                        retString = DelCLByID();
                        break;
                    case "getControlByID":
                        retString = GetControlByID();
                        break;
                    case "getFLDataSource":
                        retString = GetFLDataSource();
                        break;
                    case "delFLByID":
                        retString = DelFLByID();
                        break;
                    case "EditFormTemplateControl":
                        retString = SaveTemplateControlHtml();
                        //SaveTemplateControl();
                        break;
                    case "getFormTemplateControl":
                        retString = GetFormTemplateControl();
                        break;
                }
                HttpContext.Current.Response.Write(retString);
            }
        }

        private string GetFormTemplateControl()
        {
            string formTemplateID = HttpContext.Current.Request.Form["formTemplateID"];
            FormDesignBLL bll = new FormDesignBLL();
            FormTemplateInfo info = bll.GetFormTemplateById(formTemplateID);
            return info.Html;
        }
        public string SaveTemplateControlHtml()
        {
            //string controlID = HttpContext.Current.Request.Form["controlID"];
            string formTemplateID = HttpContext.Current.Request.Form["formTemplateID"];
            FormDesignBLL bll = new FormDesignBLL();
            FormTemplateInfo info = new FormTemplateInfo();
            info.ID = new Guid(formTemplateID);
            info.Html = HttpContext.Current.Request.Form["html"].Replace("\r\n","");
            bool ret = bll.UpdateFormTemplateHtml(info);
            if (ret)
                return "Success";
            else
                return "Fail";
        }

        public string SaveTemplateControl()
        {
            string controlID = HttpContext.Current.Request.Form["controlID"];
            string formTemplateID = HttpContext.Current.Request.Form["formTemplateID"];
            FormTemplateControlBLL bll = new FormTemplateControlBLL();
            FormTemplateControlInfo info = new FormTemplateControlInfo();
            info.ControlID = new Guid(controlID);
            info.FormTemplateID = new Guid(formTemplateID);
            bool ret = bll.CreateFormTemplateControl(info);
            if (ret)
                return "Success";
            else
                return "Fail";
        }
        /// <summary>
        /// 取得控件库所有控件JSON字符串
        /// </summary>
        /// <returns></returns>
        private string GetCLDataSource()
        {
            FormDesignBLL bll = new FormDesignBLL();
            return bll.GetAllCountrol();
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <returns></returns>
        private string DelCLByID()
        {
            string Id = HttpContext.Current.Request.Form["ID"];
            if (string.IsNullOrEmpty(Id))
                return "Fail";
            else
            {
                FormDesignBLL bll = new FormDesignBLL();
                bool ret = bll.DelControlById(Id);
                if (ret)
                    return "Success";
                else
                    return "Fail";
            }
        }

        /// <summary>
        /// 取得某个控件
        /// </summary>
        /// <returns></returns>
        private string GetControlByID()
        {
            string Id = HttpContext.Current.Request.Form["ID"];
            if (string.IsNullOrEmpty(Id))
                return "Fail";
            else
            {
                FormDesignBLL bll = new FormDesignBLL();
                ControlInfo info = bll.GetControlById(Id);
                if (info != null)
                    return EasyUIJsonConvert.Serialize<ControlInfo>(info);
                else
                    return "Fail";
            }
        }

        /// <summary>
        /// 取得表单控件库模板JSON字符串
        /// </summary>
        /// <returns></returns>
        private string GetFLDataSource()
        {
            FormDesignBLL bll = new FormDesignBLL();
            return bll.GetALLFormTemplate();
        }

        /// <summary>
        /// 删除表单模板库
        /// </summary>
        /// <returns></returns>
        private string DelFLByID()
        {
            string Id = HttpContext.Current.Request.Form["ID"];
            if (string.IsNullOrEmpty(Id))
                return "Fail";
            else
            {
                FormDesignBLL bll = new FormDesignBLL();
                bool ret = bll.DelFormTemplateById(Id);
                if (ret)
                    return "Success";
                else
                    return "Fail";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}