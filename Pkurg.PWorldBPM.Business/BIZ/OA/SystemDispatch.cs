using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Entites.BIZ.OA;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;
using System.Data;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;

namespace Pkurg.PWorldBPM.Business.BIZ.OA
{
    public class SystemDispatch
    {
        public static void UpdateSystemDispatch(SystemDispatchInfo info)
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
            new SqlParameter("@Mobile",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DateTime",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@RedHeadDocument",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsPublish",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Title",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@Content",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Mobile;
            parameters[7].Value = info.DateTime;
            parameters[8].Value = info.RedHeadDocument;
            parameters[9].Value = info.IsPublish;
            parameters[10].Value = info.Title;
            parameters[11].Value = info.Content;
            parameters[12].Value = info.LeadersSelected;
            parameters[13].Value = info.ApproveResult;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_SystemDispatch_Update", parameters);

        }
        public static void InsertSystemDispatchInfo(SystemDispatchInfo info)
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
            new SqlParameter("@Mobile",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@DateTime",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@RedHeadDocument",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@IsPublish",System.Data.SqlDbType.NVarChar,100),
            new SqlParameter("@Title",System.Data.SqlDbType.NVarChar,200),
            new SqlParameter("@Content",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@LeadersSelected",System.Data.SqlDbType.NVarChar,2000),
            new SqlParameter("@ApproveResult",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = info.FormId;
            parameters[1].Value = info.SecurityLevel;
            parameters[2].Value = info.UrgenLevel;
            parameters[3].Value = info.StartDeptId;
            parameters[4].Value = info.DeptName;
            parameters[5].Value = info.UserName;
            parameters[6].Value = info.Mobile;
            parameters[7].Value = info.DateTime;
            parameters[8].Value = info.RedHeadDocument;
            parameters[9].Value = info.IsPublish;
            parameters[10].Value = info.Title;
            parameters[11].Value = info.Content;
            parameters[12].Value = info.LeadersSelected;
            parameters[13].Value = info.ApproveResult;

            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_SystemDispatch_Insert", parameters);
        }

        public static SystemDispatchInfo GetSystemDispatchInfoByInstanceId(string id)
        {
            WorkFlowInstance instance = new WF_WorkFlowInstance().GetWorkFlowInstanceById(id);
            if (instance != null)
            {
                string formId = instance.FormId;
                return GetSystemDispatchInfo(formId);
            }
            return null;
        }
        public static SystemDispatchInfo GetSystemDispatchInfo(string FormID)
        {
            SystemDispatchInfo info = null;
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@FormID",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = FormID;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_SystemDispatch_Get", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new SystemDispatchInfo();
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
                if (dataTable.Rows[0]["Mobile"] != null && dataTable.Rows[0]["Mobile"].ToString() != "")
                {
                    info.Mobile = dataTable.Rows[0]["Mobile"].ToString();
                }
                if (dataTable.Rows[0]["DateTime"] != null && dataTable.Rows[0]["DateTime"].ToString() != "")
                {
                    info.DateTime = dataTable.Rows[0]["DateTime"].ToString();
                }
                if (dataTable.Rows[0]["RedHeadDocument"] != null && dataTable.Rows[0]["RedHeadDocument"].ToString() != "")
                {
                    info.RedHeadDocument = dataTable.Rows[0]["RedHeadDocument"].ToString();
                }
                if (dataTable.Rows[0]["IsPublish"] != null && dataTable.Rows[0]["IsPublish"].ToString() != "")
                {
                    info.IsPublish = dataTable.Rows[0]["IsPublish"].ToString();
                }
                if (dataTable.Rows[0]["Title"] != null && dataTable.Rows[0]["Title"].ToString() != "")
                {
                    info.Title = dataTable.Rows[0]["Title"].ToString();
                }
                if (dataTable.Rows[0]["Content"] != null && dataTable.Rows[0]["Content"].ToString() != "")
                {
                    info.Content = dataTable.Rows[0]["Content"].ToString();
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
            }
            return info;
        }

        public static SystemDispatchInfo GetSystemDispatchInfoByWfId(string wfId)
        {
            SystemDispatchInfo info = null;
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@WFInstanceId",System.Data.SqlDbType.NVarChar,100),
            };
            parameters[0].Value = wfId;
            DataTable dataTable = dataProvider.ExecutedProcedure("Biz.OA_SystemDispatch_GetByWfId", parameters);
            ///给实体赋值
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                info = new SystemDispatchInfo();
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
                if (dataTable.Rows[0]["Mobile"] != null && dataTable.Rows[0]["Mobile"].ToString() != "")
                {
                    info.Mobile = dataTable.Rows[0]["Mobile"].ToString();
                }
                if (dataTable.Rows[0]["DateTime"] != null && dataTable.Rows[0]["DateTime"].ToString() != "")
                {
                    info.DateTime = dataTable.Rows[0]["DateTime"].ToString();
                }
                if (dataTable.Rows[0]["RedHeadDocument"] != null && dataTable.Rows[0]["RedHeadDocument"].ToString() != "")
                {
                    info.RedHeadDocument = dataTable.Rows[0]["RedHeadDocument"].ToString();
                }
                if (dataTable.Rows[0]["IsPublish"] != null && dataTable.Rows[0]["IsPublish"].ToString() != "")
                {
                    info.IsPublish = dataTable.Rows[0]["IsPublish"].ToString();
                }
                if (dataTable.Rows[0]["Title"] != null && dataTable.Rows[0]["Title"].ToString() != "")
                {
                    info.Title = dataTable.Rows[0]["Title"].ToString();
                }
                if (dataTable.Rows[0]["Content"] != null && dataTable.Rows[0]["Content"].ToString() != "")
                {
                    info.Content = dataTable.Rows[0]["Content"].ToString();
                }
                if (dataTable.Rows[0]["LeadersSelected"] != null && dataTable.Rows[0]["LeadersSelected"].ToString() != "")
                {
                    info.LeadersSelected = dataTable.Rows[0]["LeadersSelected"].ToString();
                }
                if (dataTable.Rows[0]["ApproveResult"] != null && dataTable.Rows[0]["ApproveResult"].ToString() != "")
                {
                    info.ApproveResult = dataTable.Rows[0]["ApproveResult"].ToString();
                }
            }
            return info;
        }
    }
}
