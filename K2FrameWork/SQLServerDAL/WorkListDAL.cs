using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using Utility;
using DBUtility;
using System.Data.SqlClient;
using K2Utility;
using System.Collections.Specialized;

namespace SQLServerDAL
{
    public class WorkListDAL : IWorkList
    {
        /// <summary>
        /// Get WorkList
        /// </summary>
        /// <returns></returns>
        public DataSet GetWorkList(string pagenum, string pagesize, string procFullName, string actionerName, string folio, string StartDate ,string endDate,string procstate,string originator,string paraEmpression)
        {
            try
            {
                string sql = "SProc_GetMyWorklist_Test";

                SqlParameter[] paras = { 
                                        new SqlParameter("@pagenum", pagenum),
                                        new SqlParameter("@pagesize", pagesize),
                                        new SqlParameter("@ProcFullName", procFullName),
                                        new SqlParameter("@ActionerName", actionerName),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@StartDate", StartDate),
                                        new SqlParameter("@endDate", endDate),
                                        new SqlParameter("@procstate", procstate),
                                        new SqlParameter("@originator", originator),
                                        new SqlParameter("@ParaEmpression", paraEmpression)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.WorkSpaceConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                DataTable dtGroup = ds.Tables[1];
                dtGroup.Columns.Add("AliasName");
                foreach (DataRow r in dtGroup.Rows)
                {
                    string name = r["ProcName"].ToString();
                    r["AliasName"] = Settings.GetConfigurationValue(name);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                    if (entity != null)
                    {
                        if (!string.IsNullOrEmpty(entity.Description))
                            r["AliasName"] = entity.Description;
                    }
                    else
                    {
                        //r.Delete();
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");

                foreach (DataRow r in dt.Rows)
                {
                    string urlLink = "<a href=\"../{0}\" id=\"HL_{3}\">{1}</a>";
                    string actName = r["ActivityName"].ToString();
                    r["ActivityName"] = Common.FormatBrackets(actName);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    if (procSetId > 0 && procId > 0)
                    {

                        r["FormURL"] = string.Format(urlLink, r["data"].ToString(), r["Folio"].ToString(), "MyApp" + r["ProcInstID"], procId.ToString());
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.GetWorkList", DBManager.GetCurrentUserAD());
                return null;
            }
        }
        public void Redirect(string sn, string targetUser)
        {
            try
            {
                K2Helper.Redirect(sn, targetUser);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.Redirect", DBManager.GetCurrentUserAD());
            }
        }

        public void Delegate(string sn, string targetUser)
        {
            try
            {
                K2Helper.Delegate(sn, targetUser);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.Delegate", DBManager.GetCurrentUserAD());
            }
        }


        public void Release(string sn)
        {
            try
            {
                K2Helper.Release(sn);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.Release", DBManager.GetCurrentUserAD());
            }
        }

        public void Sleep(string sn, int second)
        {
            try
            {
                K2Helper.Sleep(sn, second);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.Sleep", DBManager.GetCurrentUserAD());
            }
        }

        public void Approve(string sn, string action, NameValueCollection dataFields)
        {
            try
            {
                K2Helper.Approve(sn, action, dataFields);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.Approve", DBManager.GetCurrentUserAD());
            }
        }

        public DataSet GetMyWorklist(string user, string pagenum, string pagesize, string procName, string folio, string group, string Submitor, string StartTime, string EndTime, string FlowStatus, string Employee)
        {
            try
            {
                string sql = "SProc_GetMyWorklist";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ActionerName", user),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcSetName", procName),
                                        new SqlParameter("@StartTime", StartTime),
                                        new SqlParameter("@EndTime", EndTime),
                                        new SqlParameter("@PageNum",pagenum),
                                        new SqlParameter("@PageSize",pagesize),
                                        new SqlParameter("@Group",group),
                                        new SqlParameter("@Submitor",Submitor),
                                        new SqlParameter("@FlowStatus",FlowStatus),
                                        new SqlParameter("@Employee",Employee)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                if (ds == null || ds.Tables.Count == 0)
                    return null;

                DataTable dtGroup = ds.Tables[1];
                dtGroup.Columns.Add("AliasName");
                foreach (DataRow r in dtGroup.Rows)
                {
                    string name = r["ProcName"].ToString();
                    r["AliasName"] = Settings.GetConfigurationValue(name);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                    if (entity != null)
                    {
                        if (!string.IsNullOrEmpty(entity.Description))
                            r["AliasName"] = entity.Description;
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");

                foreach (DataRow r in dt.Rows)
                {
                    string urlLink = "<a href=\"../{0}\" id=\"HL_{3}\">{1}</a>";
                    string actName = r["ActivityName"].ToString();
                    r["ActivityName"] = Common.FormatBrackets(actName);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    if (procSetId > 0 && procId > 0)
                    {

                        r["FormURL"] = string.Format(urlLink, r["data"].ToString(), r["Folio"].ToString(), "MyApp" + r["ProcInstID"], procId.ToString());
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "WorkListDAL.GetMyWorklist", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
