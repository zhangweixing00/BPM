using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

namespace Pkurg.PWorldBPM.Business.BIZ.OA
{
   public class FixedAssetAllocation
    {
       public static void UpdateFixedAssetAllocation(FixedAssetAllocationInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@CreateTime",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DepartName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ApplyDate",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@Applicant",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Phone",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@FixedName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Quantity",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Price",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Account",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Sum",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeployReason",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.CreateTime;
            parameters[7].Value = info.LeadersSelected;
            parameters[8].Value = info.ApproveResult;
            parameters[9].Value = info.DepartName;
            parameters[10].Value = info.ApplyDate;
            parameters[11].Value = info.Applicant;
            parameters[12].Value = info.Phone;
            parameters[13].Value = info.FixedName;
            parameters[14].Value = info.Quantity;
            parameters[15].Value = info.Price;
            parameters[16].Value = info.Account;
            parameters[17].Value = info.Sum;
            parameters[18].Value = info.DeployReason;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_FixedAssetAllocation_Update", parameters);

        }
        public static void InsertFixedAssetAllocationInfo(FixedAssetAllocationInfo info)
        {
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@SecurityLevel",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UrgenLevel",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@StartDeptId",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeptName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@UserName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@CreateTime",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DepartName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@ApplyDate",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@Applicant",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Phone",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@FixedName",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Quantity",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Price",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Account",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Sum",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DeployReason",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.CreateTime;
            parameters[7].Value = info.LeadersSelected;
            parameters[8].Value = info.ApproveResult;
            parameters[9].Value = info.DepartName;
            parameters[10].Value = info.ApplyDate;
            parameters[11].Value = info.Applicant;
            parameters[12].Value = info.Phone;
            parameters[13].Value = info.FixedName;
            parameters[14].Value = info.Quantity;
            parameters[15].Value = info.Price;
            parameters[16].Value = info.Account;
            parameters[17].Value = info.Sum;
            parameters[18].Value = info.DeployReason;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_FixedAssetAllocation_Insert", parameters);
        }

        public static FixedAssetAllocationInfo GetFixedAssetAllocationInfoByInstanceId(string id)
        {
            WorkFlowInstance instance = new WF_WorkFlowInstance().GetWorkFlowInstanceById(id);
            if (instance != null)
            {
                string formId = instance.FormId;
                return GetFixedAssetAllocationInfo(formId);
            }
            return null;
        }
        public static FixedAssetAllocationInfo GetFixedAssetAllocationInfo(string FormID)
        {
            FixedAssetAllocationInfo info = null;
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = FormID;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_FixedAssetAllocation_Get", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new FixedAssetAllocationInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["SecurityLevel"] != null && dataTable.Rows[0]["SecurityLevel"].ToString() != "")
                {
                    info.SecurityLevel = dataTable.Rows[0]["SecurityLevel"].ToString();
                }
                if (dataTable.Rows[0]["UrgenLevel"] != null && dataTable.Rows[0]["UrgenLevel"].ToString() != "")
                {
                    info.UrgenLevel = dataTable.Rows[0]["UrgenLevel"].ToString();
                }
                if (dataTable.Rows[0]["StartDeptId"] != null && dataTable.Rows[0]["StartDeptId"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["StartDeptId"].ToString();
                }
                if (dataTable.Rows[0]["DeptName"] != null && dataTable.Rows[0]["DeptName"].ToString() != "")
                {
                    info.DeptName = dataTable.Rows[0]["DeptName"].ToString();
                }
                if (dataTable.Rows[0]["UserName"] != null && dataTable.Rows[0]["UserName"].ToString() != "")
                {
                    info.UserName = dataTable.Rows[0]["UserName"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["Phone"].ToString() != "")
                {
                    info.CreateTime = dataTable.Rows[0]["CreateTime"].ToString();
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["RedHeadDocument"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["DepartName"] != null && dataTable.Rows[0]["IsPublish"].ToString() != "")
                {
                    info.DepartName = dataTable.Rows[0]["DepartName"].ToString();
                }
                if (dataTable.Rows[0]["ApplyDate"] != null && dataTable.Rows[0]["Title"].ToString() != "")
                {
                    info.ApplyDate = dataTable.Rows[0]["ApplyDate"].ToString();
                }
                if (dataTable.Rows[0]["Applicant"] != null && dataTable.Rows[0]["Content"].ToString() != "")
                {
                    info.Applicant = dataTable.Rows[0]["Applicant"].ToString();
                }
                if (dataTable.Rows[0]["Phone"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.Phone = dataTable.Rows[0]["Phone"].ToString();
                }
                if (dataTable.Rows[0]["FixedName"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.FixedName = dataTable.Rows[0]["FixedName"].ToString();
                }
                if (dataTable.Rows[0]["Quantity"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Quantity = dataTable.Rows[0]["Quantity"].ToString();
                }
                if (dataTable.Rows[0]["Price"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Price = dataTable.Rows[0]["Price"].ToString();
                }
                if (dataTable.Rows[0]["Account"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Account = dataTable.Rows[0]["Account"].ToString();
                }
                if (dataTable.Rows[0]["Sum"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Sum = dataTable.Rows[0]["Sum"].ToString();
                }
                if (dataTable.Rows[0]["DeployReason"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.DeployReason = dataTable.Rows[0]["DeployReason"].ToString();
                }
            }
            return info;
        }

        public static FixedAssetAllocationInfo GetFixedAssetAllocationInfoByWfId(string wfId)
        {
            FixedAssetAllocationInfo info = null;
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@WFInstanceId",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = wfId;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_FixedAssetAllocation_GetByWfId", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new FixedAssetAllocationInfo();
                if (dataTable.Rows[0]["FormId"] != null && dataTable.Rows[0]["FormId"].ToString() != "")
                {
                    info.FormId = dataTable.Rows[0]["FormId"].ToString();
                }
                if (dataTable.Rows[0]["SecurityLevel"] != null && dataTable.Rows[0]["SecurityLevel"].ToString() != "")
                {
                    info.SecurityLevel = dataTable.Rows[0]["SecurityLevel"].ToString();
                }
                if (dataTable.Rows[0]["UrgenLevel"] != null && dataTable.Rows[0]["UrgenLevel"].ToString() != "")
                {
                    info.UrgenLevel = dataTable.Rows[0]["UrgenLevel"].ToString();
                }
                if (dataTable.Rows[0]["StartDeptId"] != null && dataTable.Rows[0]["StartDeptId"].ToString() != "")
                {
                    info.StartDeptId = dataTable.Rows[0]["StartDeptId"].ToString();
                }
                if (dataTable.Rows[0]["DeptName"] != null && dataTable.Rows[0]["DeptName"].ToString() != "")
                {
                    info.DeptName = dataTable.Rows[0]["DeptName"].ToString();
                }
                if (dataTable.Rows[0]["UserName"] != null && dataTable.Rows[0]["UserName"].ToString() != "")
                {
                    info.UserName = dataTable.Rows[0]["UserName"].ToString();
                }
                if (dataTable.Rows[0]["CreateTime"] != null && dataTable.Rows[0]["Phone"].ToString() != "")
                {
                    info.CreateTime = dataTable.Rows[0]["CreateTime"].ToString();
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["CreateTime"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["RedHeadDocument"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
                if (dataTable.Rows[0]["DepartName"] != null && dataTable.Rows[0]["IsPublish"].ToString() != "")
                {
                    info.DepartName = dataTable.Rows[0]["DepartName"].ToString();
                }
                if (dataTable.Rows[0]["ApplyDate"] != null && dataTable.Rows[0]["Title"].ToString() != "")
                {
                    info.ApplyDate = dataTable.Rows[0]["ApplyDate"].ToString();
                }
                if (dataTable.Rows[0]["Applicant"] != null && dataTable.Rows[0]["Content"].ToString() != "")
                {
                    info.Applicant = dataTable.Rows[0]["Applicant"].ToString();
                }
                if (dataTable.Rows[0]["Phone"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.Phone = dataTable.Rows[0]["Phone"].ToString();
                }
                if (dataTable.Rows[0]["FixedName"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.FixedName = dataTable.Rows[0]["FixedName"].ToString();
                }
                if (dataTable.Rows[0]["Quantity"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Quantity = dataTable.Rows[0]["Quantity"].ToString();
                }
                if (dataTable.Rows[0]["Price"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Price = dataTable.Rows[0]["Price"].ToString();
                }
                if (dataTable.Rows[0]["Account"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Account = dataTable.Rows[0]["Account"].ToString();
                }
                if (dataTable.Rows[0]["Sum"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.Sum = dataTable.Rows[0]["Sum"].ToString();
                }
                if (dataTable.Rows[0]["DeployReason"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.DeployReason = dataTable.Rows[0]["DeployReason"].ToString();
                }
            }
            return info;
        }
    }
}
