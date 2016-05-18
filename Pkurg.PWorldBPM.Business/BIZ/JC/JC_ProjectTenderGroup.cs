using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Pkurg.BPM.Entities;

namespace Pkurg.PWorldBPM.Business.BIZ.JC
{
    public class JC_ProjectTenderGroup
    {
        /// <summary>
        /// 插入新的表单数据
        /// </summary>
        /// <param name="info"></param>
        public static void InsertJC_ProjectTenderGroupInfo(JC_ProjectTenderGroupInfo info)
        {
            SqlParameter[] parameters ={
            new SqlParameter("@FormID",SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",SqlDbType.NVarChar,100),
            new SqlParameter("@Tel",SqlDbType.NVarChar,100),
            new SqlParameter("@DateTime",SqlDbType.NVarChar,100),
            new SqlParameter("@Title",SqlDbType.NVarChar,200),
            new SqlParameter("@Substance",SqlDbType.NVarChar,2000),
            new SqlParameter("@GroupRealateDept",SqlDbType.NVarChar,500),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500)
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Tel;
            parameters[7].Value = info.DateTime;
            parameters[8].Value = info.Title;
            parameters[9].Value = info.Substance;
            parameters[10].Value = info.GroupRealateDept;
            parameters[11].Value = info.Remark;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfo_Insert", parameters);
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="info"></param>
        public static void UpdateJC_ProjectTenderGroupInfo(JC_ProjectTenderGroupInfo info)
        {
            SqlParameter[] parameters ={
            new SqlParameter("@FormID",SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",SqlDbType.NVarChar,100),
            new SqlParameter("@Tel",SqlDbType.NVarChar,100),
            new SqlParameter("@DateTime",SqlDbType.NVarChar,100),
            new SqlParameter("@Title",SqlDbType.NVarChar,200),
            new SqlParameter("@Substance",SqlDbType.NVarChar,2000),
            new SqlParameter("@GroupRealateDept",SqlDbType.NVarChar,500),
            new SqlParameter("@Remark",SqlDbType.NVarChar,500)
                                      };
            parameters[0].Value = info.FormID;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Tel;
            parameters[7].Value = info.DateTime;
            parameters[8].Value = info.Title;
            parameters[9].Value = info.Substance;
            parameters[10].Value = info.GroupRealateDept;
            parameters[11].Value = info.Remark;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfo_Update", parameters);
        }
        /// <summary>
        /// 通过FormID得到一个对象实体
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        public static JC_ProjectTenderGroupInfo GetJC_ProjectTenderGroupInfoByFormID(string FormID)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = FormID;

            JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
        /// <summary>
        /// 得到一个实体对象[封装]
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private static JC_ProjectTenderGroupInfo DataRowToModel(DataRow dataRow)
        {
            JC_ProjectTenderGroupInfo info = new JC_ProjectTenderGroupInfo();

            if (dataRow != null)
            {
                if (dataRow["FormID"] != null)
                {
                    info.FormID = dataRow["FormID"].ToString();
                }
                if (dataRow["SecurityLevel"] != null)
                {
                    info.SecurityLevel = dataRow["SecurityLevel"].ToString();
                }
                if (dataRow["UrgenLevel"] != null)
                {
                    info.UrgenLevel = dataRow["UrgenLevel"].ToString();
                }
                if (dataRow["StartDeptId"] != null)
                {
                    info.StartDeptId = dataRow["StartDeptId"].ToString();
                }
                if (dataRow["DeptName"] != null)
                {
                    info.DeptName = dataRow["DeptName"].ToString();
                }
                if (dataRow["UserName"] != null)
                {
                    info.UserName = dataRow["UserName"].ToString();
                }
                if (dataRow["Tel"] != null)
                {
                    info.Tel = dataRow["Tel"].ToString();
                }
                if (dataRow["DateTime"] != null)
                {
                    info.DateTime = dataRow["DateTime"].ToString();
                }
                if (dataRow["Title"] != null)
                {
                    info.Title = dataRow["Title"].ToString();
                }
                if (dataRow["Substance"] != null)
                {
                    info.Substance = dataRow["Substance"].ToString();
                }
                if (dataRow["GroupRealateDept"] != null)
                {
                    info.GroupRealateDept = dataRow["GroupRealateDept"].ToString();
                }
                if (dataRow["IsApproval"] != null)
                {
                    info.IsApproval = dataRow["IsApproval"].ToString();
                }
                if (dataRow["Remark"] != null)
                {
                    info.Remark = dataRow["Remark"].ToString();
                }
            }
            return info;
        }
        /// <summary>
        /// 根据流程ID获取FormId
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static JC_ProjectTenderGroupInfo GetJC_ProjectTenderGroupInfoByWfId(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,100)			};
            parameters[0].Value = id;

            JC_ProjectTenderGroupInfo model = new JC_ProjectTenderGroupInfo();
            DataTable dt = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfoByWfId_Get", parameters);
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
        /// <summary>
        /// 通过FormID更新表单IsApproval字段
        /// </summary>
        /// <param name="model"></param>
        public static JC_ProjectTenderGroupInfo UpdateJC_ProjectTenderGroupInfoByModel(JC_ProjectTenderGroupInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsApproval",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = model.FormID;
            parameters[1].Value = model.IsApproval;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfoByID_Update", parameters);
            return model;
        }

        public static JC_ProjectTenderGroupInfo GetJC_ProjectTenderGroupInfoByFormId(string ProId)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = ProId;

            JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
        //appflow_2005
        public JC_ProjectTenderGroupInfo GetJC_ProjectTenderGroupInfo(string formId)
        {
            SqlParameter[] parameters = new SqlParameter[] { 
           new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
           };
            parameters[0].Value = formId;

            JC_ProjectTenderCityCompanyInfo model = new JC_ProjectTenderCityCompanyInfo();
            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.JC_ProjectTenderGroupInfoByFormID_Get", parameters);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return DataRowToModel(dataTable.Rows[0]);
            }
            else
            {
                return null;
            } 
        }
    }
}
