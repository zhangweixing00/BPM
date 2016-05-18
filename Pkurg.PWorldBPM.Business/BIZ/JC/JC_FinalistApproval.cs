using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Pkurg.PWorld.Business.Common;
using Pkurg.BPM.Entities;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_FinalistApproval
    {
        /// <summary>
        /// 通过FormID得到一个对象实体 
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public static JC_FinalistApprovalInfo GetJC_FinalistApprovalInfoByFormID(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = FormID;
            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfoInfoByFormID_Get", parameters);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                return DataRowToModel(datatable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
        /// <summary>
        /// 得到一个实体对象【封装】
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static JC_FinalistApprovalInfo DataRowToModel(DataRow dataRow)
        {
            JC_FinalistApprovalInfo info = new JC_FinalistApprovalInfo();

            if (dataRow != null)
            {
                if (dataRow["FormID"] != null)
                {
                    info.FormID = dataRow["FormID"].ToString();
                }
                if (dataRow["ProjectName"] != null)
                {
                    info.ProjectName = dataRow["ProjectName"].ToString();
                }
                if (dataRow["ReportDept"] != null)
                {
                    info.ReportDept = dataRow["ReportDept"].ToString();
                }
                if (dataRow["ReportDate"] != null)
                {
                    info.ReportDate = dataRow["ReportDate"].ToString();
                }
                if (dataRow["IsAccreditByGroup"] != null)
                {
                    info.IsAccreditByGroup = dataRow["IsAccreditByGroup"].ToString();
                }
                if (dataRow["CheckStatus"] != null)
                {
                    info.CheckStatus = dataRow["CheckStatus"].ToString();
                }
                if (dataRow["GroupPurchaseDept"] != null)
                {
                    info.GroupPurchaseDept = dataRow["GroupPurchaseDept"].ToString();
                }
                if (dataRow["GroupLegalDept"] != null)
                {
                    info.GroupLegalDept = dataRow["GroupLegalDept"].ToString();
                }
                if (dataRow["StartDeptId"] != null)
                {
                    info.StartDeptId = dataRow["StartDeptId"].ToString();
                }
                if (dataRow["IsApproval"] != null)
                {
                    info.IsApproval = dataRow["IsApproval"].ToString();
                }
            }
            return info;
        }
        /// <summary>
        /// 插入新的表单数据
        /// </summary>
        /// <param name="info"></param>
        public static void InsertJC_FinalistApprovalInfo(JC_FinalistApprovalInfo info)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@FormID",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ProjectName",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ReportDept",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ReportDate",SqlDbType.NVarChar,100),
                                          new SqlParameter("@IsAccreditByGroup",SqlDbType.NVarChar,100),
                                          new SqlParameter("@CheckStatus",SqlDbType.NVarChar,2000),
                                          //new SqlParameter("@ArchiveDate",SqlDbType.NVarChar,100),
                                          //new SqlParameter("@ArchiveName",SqlDbType.NVarChar,100),
                                          new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
                                          new SqlParameter("@GroupPurchaseDept",SqlDbType.NVarChar,100),
                                          new SqlParameter("@GroupLegalDept",SqlDbType.NVarChar,100),
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.ProjectName;
            parameters[2].Value =info.ReportDept;
            parameters[3].Value=info.ReportDate;
            parameters[4].Value=info.IsAccreditByGroup;
            parameters[5].Value=info.CheckStatus;
            parameters[6].Value=info.StartDeptId;
            parameters[7].Value = info.GroupPurchaseDept;
            parameters[8].Value = info.GroupLegalDept;

            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfo_Insert", parameters);
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateJC_FinalistApprovalInfo(JC_FinalistApprovalInfo info)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("@FormID",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ProjectName",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ReportDept",SqlDbType.NVarChar,100),
                                          new SqlParameter("@ReportDate",SqlDbType.NVarChar,100),
                                          new SqlParameter("@IsAccreditByGroup",SqlDbType.NVarChar,100),
                                          new SqlParameter("@CheckStatus",SqlDbType.NVarChar,2000),
                                          //new SqlParameter("@ArchiveDate",SqlDbType.NVarChar,100),
                                          //new SqlParameter("@ArchiveName",SqlDbType.NVarChar,100),
                                          new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
                                          new SqlParameter("@GroupPurchaseDept",SqlDbType.NVarChar,100),
                                          new SqlParameter("@GroupLegalDept",SqlDbType.NVarChar,100),
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.ProjectName;
            parameters[2].Value = info.ReportDept;
            parameters[3].Value = info.ReportDate;
            parameters[4].Value = info.IsAccreditByGroup;
            parameters[5].Value = info.CheckStatus;
            //parameters[6].Value = info.ArchiveDate;
            //parameters[7].Value = info.ArchiveName;
            parameters[6].Value = info.StartDeptId;
            parameters[7].Value = info.GroupPurchaseDept;
            parameters[8].Value = info.GroupLegalDept;

            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfo_Update", parameters);
        }
        /// <summary>
        /// 根据流程ID获取FormId
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static JC_FinalistApprovalInfo GetJC_FinalistApprovalInfoByWfId(string id)
        {
            SqlParameter[] parameters ={
                                          new SqlParameter("ID",SqlDbType.NVarChar,100)
                                      };
            parameters[0].Value = id;

            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfoByWfId_Get", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过FormID更新表单IsApproval字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static JC_FinalistApprovalInfo UpdateJC_FinalistApprovalInfoInfoByModel(JC_FinalistApprovalInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsApproval",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.IsApproval;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfoByID_Update", parameters);

            return model;
        }
        /// <summary>
        /// 给表WF_Approval_Record插入数据
        /// </summary>
        /// <param name="currentActiveName"></param>
        /// <param name="LoginName"></param>
        /// <param name="WFInstanceId"></param>
        public static void InsertWF_Approval_RecordByInstanceID(string currentActiveName, string LoginName, string WFInstanceId)
        {
            ApprovalRecord appRecord = new ApprovalRecord();
            SqlParameter[] parameters ={
                                          new SqlParameter("@WfInstanceID",SqlDbType.NVarChar,100),
                                          new SqlParameter("@CurrentActiveName",SqlDbType.NVarChar,200),
                                          new SqlParameter("@LoginName",SqlDbType.NVarChar,50)
                                      };

            parameters[0].Value = WFInstanceId;
            parameters[1].Value = currentActiveName;
            parameters[2].Value = LoginName;

            DataTable datatable = DBHelper.ExecutedProcedure("Biz.WF_Approval_Record_Insert", parameters);
        }

        public static JC_FinalistApprovalInfo GetJC_FinalistApprovalInfoByFormId(string ProId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = ProId;
            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfoInfoByFormID_Get", parameters);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                return DataRowToModel(datatable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
        //appflow_2003
        public JC_FinalistApprovalInfo GetJC_FinalistApprovalInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
               new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = formId;
            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            DataTable datatable = DBHelper.ExecutedProcedure("Biz.JC_FinalistApprovalInfoInfoByFormID_Get", parameters);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                return DataRowToModel(datatable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
    }
}
