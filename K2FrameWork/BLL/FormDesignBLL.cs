using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;
using Utility;

namespace BLL
{
    public class FormDesignBLL
    {
        //创建dal连接
        private static readonly IFormDesignDAL dal = DALFactory.DataAccess.CreateFormDesignDAL();
        

        /// <summary>
        /// 添加Control
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateControl(ControlInfo info)
        {
            return dal.CreateControl(info);
        }

        /// <summary>
        /// 取得所有控件
        /// </summary>
        /// <returns></returns>
        public string GetAllCountrol()
        {
            DataTable dt = dal.GetAllCountrol();
            if (dt != null)
            {
                return EasyUIJsonConvert.CreateJsonParameters(dt, true, dt.Rows.Count);
            }
            return string.Empty;
        }

        /// <summary>
        /// 删除控件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelControlById(string Id)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(Id);
            }
            catch
            {
                return false;
            }
            return dal.DelControlById(ID);
        }

        /// <summary>
        /// 取得某个控件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ControlInfo GetControlById(string Id)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(Id);
            }
            catch
            {
                return null;
            }
            DataTable dt = dal.GetControlById(ID);
            if (dt != null)
            {
                ControlInfo info = new ControlInfo();
                info.ID = new Guid(dt.Rows[0]["ID"].ToString());
                info.Class = dt.Rows[0]["Class"].ToString();
                info.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                info.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                info.Description = dt.Rows[0]["Description"].ToString();
                info.Json = dt.Rows[0]["Json"].ToString();
                info.Name = dt.Rows[0]["Name"].ToString();
                info.Type = dt.Rows[0]["Type"].ToString();
                return info;
            }

            return null;
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateControl(ControlInfo info)
        {
            return dal.UpdateControl(info);
        }

        /// <summary>
        /// 取得所有表单模板库
        /// </summary>
        /// <returns></returns>
        public string GetALLFormTemplate()
        {
            DataTable dt = dal.GetALLFormTemplate();
            if (dt != null)
            {
                return EasyUIJsonConvert.CreateJsonParameters(dt, true, dt.Rows.Count);
            }
            return string.Empty;
        }

        /// <summary>
        /// 更新表单模板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateFormTemplate(FormTemplateInfo info)
        {
            return dal.UpdateFormTemplate(info);
        }
        
        public bool UpdateFormTemplateHtml(FormTemplateInfo info)
        {
            return dal.UpdateFormTemplateHtml(info);
        }

        /// <summary>
        /// 创建表单模板库
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateFormTemplate(FormTemplateInfo info)
        {
            return dal.CreateFormTemplate(info);
        }

        /// <summary>
        /// 取得某个表单模板库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public FormTemplateInfo GetFormTemplateById(string Id)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(Id);
            }
            catch
            {
                return null;
            }
            DataTable dt = dal.GetFormTemplateById(ID);
            if (dt != null)
            {
                FormTemplateInfo info = new FormTemplateInfo();
                info.ID = new Guid(dt.Rows[0]["ID"].ToString());
                info.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
                info.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                info.Description = dt.Rows[0]["Description"].ToString();
                info.Version = dt.Rows[0]["Version"].ToString();
                info.Html = dt.Rows[0]["Html"].ToString();
                info.Name = dt.Rows[0]["Name"].ToString();
                return info;
            }

            return null;
        }

        /// <summary>
        /// 删除表单模板库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DelFormTemplateById(string Id)
        {
            Guid ID = Guid.Empty;
            try
            {
                ID = new Guid(Id);
            }
            catch
            {
                return false;
            }
            return dal.DelFormTemplateById(ID);
        }
    }
}
