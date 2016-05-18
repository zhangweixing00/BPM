using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using IDAL;
using System.Reflection;
using System.Data;

namespace BLL
{
    public class CustomFlowBLL
    {
        //创建dal连接
        private static readonly ICustomFlowDAL dal = DALFactory.DataAccess.CreateCustomFlowDAL();

        /// <summary>
        /// 添加自定义流程
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public bool AddCustomFlow(CustomFlow cf)
        {
            if (string.IsNullOrEmpty(cf.Applicant))
                cf.Applicant = string.Empty;
            if (string.IsNullOrEmpty(cf.AppReason))
                cf.AppReason = string.Empty;
            if (string.IsNullOrEmpty(cf.AttachIds))
                cf.AttachIds = string.Empty;
            if (string.IsNullOrEmpty(cf.BBCategoryCode))
                cf.BBCategoryCode = string.Empty;
            if (string.IsNullOrEmpty(cf.BSCategoryCode))
                cf.BSCategoryCode = string.Empty;
            if (string.IsNullOrEmpty(cf.CreatedBy))
                cf.CreatedBy = string.Empty;
            if (string.IsNullOrEmpty(cf.FormId))
                cf.FormId = string.Empty;
            if (string.IsNullOrEmpty(cf.jqFlowChart))
                cf.jqFlowChart = string.Empty;
            if (string.IsNullOrEmpty(cf.Operator))
                cf.Operator = string.Empty;
            if (string.IsNullOrEmpty(cf.Priority))
                cf.Priority = string.Empty;
            if (string.IsNullOrEmpty(cf.ProcessId))
                cf.Priority = string.Empty;
            if (string.IsNullOrEmpty(cf.ProcessState))
                cf.ProcessState = string.Empty;
            if (string.IsNullOrEmpty(cf.SubmitId))
                cf.SubmitId = string.Empty;
            if (string.IsNullOrEmpty(cf.Urgent))
                cf.Urgent = string.Empty;
            if (string.IsNullOrEmpty(cf.AppExplain))
                cf.AppExplain = string.Empty;
            return dal.AddCustomFlow(cf);
        }


        /// <summary>
        /// 取得业务大类
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusinessClass()
        {
            return dal.GetBusinessClass();
        }

        /// <summary>
        /// 取得业务小类
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataTable GetBusinessSubClass(string classId)
        {
            return dal.GetBusinessSubClass(classId);
        }

        /// <summary>
        /// 以业务小类获取文档
        /// </summary>
        /// <param name="subClassId"></param>
        /// <returns></returns>
        public DataTable GetDocBySubClassId(string subClassCode)
        {
            return dal.GetDocBySubClassId(subClassCode);
        }

        /// <summary>
        /// 取得业务数据
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public CustomFlow GetCustomFlowByFormId(string FormId)
        {
            DataTable dt = dal.GetCustomFlowByFormId(FormId);
            CustomFlow cf = new CustomFlow();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cf.SubmitDate = Convert.ToDateTime(dr["SubmitDate"]);
                    //申请信息
                    cf.ApplicantName = dr["CHName"].ToString();
                    cf.EmployeeId = dr["SubmitId"].ToString();
                    cf.EmployeeName = dr["CHName"].ToString();
                    cf.DeptCode = dr["DeptCode"].ToString();
                    cf.Tel = dr["BlackBerry"].ToString();
                    cf.Email = dr["Email"].ToString();
                    cf.State = Convert.ToInt32(dr["State"]);
                    cf.FormId = dr["FormId"].ToString();
                    cf.CreatedBy = dr["CreatedBy"].ToString();
                    cf.ProcessId = dr["ProcessId"].ToString();
                    cf.SubmitId = dr["SubmitId"].ToString();
                    cf.AppReason = dr["AppReason"].ToString();
                    cf.ApproveXML = dr["ApproveXML"].ToString();
                    cf.ProcessId = dr["ProcessId"].ToString();
                }
            }
            return cf;
        }

        /// <summary>
        /// 取得业务数据
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public CustomFlow GetCustomFlowEnityByFormId(string FormId)
        {
            return FillModel(dal.GetCustomFlowByFormId(FormId));
        }

        private CustomFlow FillModel(DataTable dt)
        {
            List<CustomFlow> l = new List<CustomFlow>();
            CustomFlow model = default(CustomFlow);

            foreach (DataRow dr in dt.Rows)
            {
                model = Activator.CreateInstance<CustomFlow>();
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    PropertyInfo pi = model.GetType().GetProperty(dc.ColumnName);
                    if (dr[dc.ColumnName] != DBNull.Value)
                    {
                        object obj = dr[dc.ColumnName];
                        if (dr[dc.ColumnName].GetType().FullName == "System.Guid")
                        {
                            obj = obj.ToString();
                        }
                        pi.SetValue(model, obj, null);
                    }
                    else
                    {
                        pi.SetValue(model, null, null);
                    }
                }
                l.Add(model);
            }
            return l.Count > 0 ? l[0] : null;
        }

        /// <summary>
        /// 通过ProcessId取得表单信息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataTable GetCustomFlowByProcessId(string processId)
        {
            return dal.GetCustomFlowByProcessId(processId);
        }

        /// <summary>
        /// 通过InstanceID取得审批XML
        /// </summary>
        /// <param name="procInsId"></param>
        /// <returns></returns>
        public string GetApproveXMLByProcInsID(string procInsId)
        {
            return dal.GetApproveXMLByProcInsID(procInsId);
        }

        /// <summary>
        /// 更新自定义流程业务表
        /// </summary>
        /// <param name="cf"></param>
        /// <returns></returns>
        public bool UpdateCustomFlowByFormId(CustomFlow cf)
        {
            if (string.IsNullOrEmpty(cf.Applicant))
                cf.Applicant = string.Empty;
            if (string.IsNullOrEmpty(cf.AppReason))
                cf.AppReason = string.Empty;
            if (string.IsNullOrEmpty(cf.AttachIds))
                cf.AttachIds = string.Empty;
            if (string.IsNullOrEmpty(cf.BBCategoryCode))
                cf.BBCategoryCode = string.Empty;
            if (string.IsNullOrEmpty(cf.BSCategoryCode))
                cf.BSCategoryCode = string.Empty;
            if (string.IsNullOrEmpty(cf.CreatedBy))
                cf.CreatedBy = string.Empty;
            if (string.IsNullOrEmpty(cf.FormId))
                cf.FormId = string.Empty;
            if (string.IsNullOrEmpty(cf.jqFlowChart))
                cf.jqFlowChart = string.Empty;
            if (string.IsNullOrEmpty(cf.Operator))
                cf.Operator = string.Empty;
            if (string.IsNullOrEmpty(cf.Priority))
                cf.Priority = string.Empty;
            if (string.IsNullOrEmpty(cf.ProcessId))
                cf.Priority = string.Empty;
            if (string.IsNullOrEmpty(cf.ProcessState))
                cf.ProcessState = string.Empty;
            if (string.IsNullOrEmpty(cf.SubmitId))
                cf.SubmitId = string.Empty;
            if (string.IsNullOrEmpty(cf.Urgent))
                cf.Urgent = string.Empty;
            if (string.IsNullOrEmpty(cf.AppExplain))
                cf.AppExplain = string.Empty;
            return dal.UpdateCustomFlowByFormId(cf);
        }

        /// <summary>
        /// 审批时更新附件
        /// </summary>
        /// <param name="attachIds"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public bool UpdateCustomFlowStatusByAttachIds(string attachIds, string formId)
        {
            return dal.UpdateCustomFlowStatusByAttachIds(attachIds, formId);
        }

        /// <summary>
        /// 修改附件表的相应附件的状态
        /// </summary>
        /// <param name="attachIds"></param>
        /// <returns></returns>
        public bool UpdateAttachStatusByAttachAttachCodes(string attachcodes)
        {
            return dal.UpdateAttachStatusByAttachAttachCodes(attachcodes);
        }

        /// <summary>
        /// 更新流程状态
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="processStatus"></param>
        /// <returns></returns>
        public bool UpdateCostomFlowStatusByFormId(string formId, string processStatus)
        {
            return dal.UpdateCostomFlowStatusByFormId(formId, processStatus);
        }
    }
}
