using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace Utility
{
    public class DBManager
    {
        private static string dbConnStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
             
        public static void RecoreErrorProfile(Exception ex, string method, string createdby)
        {
            string sql = "SProc_AddErrorProfile";

            SqlParameter[] paras = { 
                                        new SqlParameter("@ErrorMsg",ex.Message),
                                        new SqlParameter("@ErrorSource",ex.Source), 
                                        new SqlParameter("@ErrorStackTrace",ex.StackTrace), 
                                        new SqlParameter("@ErrorMethod",method),
                                        new SqlParameter("@CreatedBy",createdby)
                                   };

            SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);

        }

        public static string GetCurrentUserAD()
        {
            return System.Web.HttpContext.Current.User.Identity.Name;
        }

        public static DataTable GetSubDepartments(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_GetSubDepartments";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ParentCode", deptCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetSubDepartments", "HC.Admin");
                return null;
            }
        }

        public static Department GetDepartmentInfo(string deptCode)
        {
            
            Department dept = new Department(deptCode);
            try
            {
                string sql = "SProc_Admin_GetDepartmentInfo";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    dept.Abbreviation = dt.Rows[0]["Abbreviation"].ToString();
                    dept.Code = dt.Rows[0]["Code"].ToString();
                    dept.DepartmentName = dt.Rows[0]["Department"].ToString();
                    dept.DeptCode = dt.Rows[0]["DeptCode"].ToString();
                    dept.Description = dt.Rows[0]["Description"].ToString();
                    dept.State = Convert.ToInt32(dt.Rows[0]["State"].ToString());
                    dept.Levels = Convert.ToInt32(dt.Rows[0]["Levels"].ToString());
                    dept.ParentCode = dt.Rows[0]["ParentCode"].ToString();
                    dept.ParentDepartment = dt.Rows[0]["ParentDepartment"].ToString();
                    dept.DeptTypeCode = dt.Rows[0]["DeptTypeCode"].ToString();
                    dept.DeptType = dt.Rows[0]["DeptType"].ToString();
                    dept.OrderNO = dt.Rows[0]["OrderNO"].ToString();
                }
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDepartmentInfo", "HC.Admin");
            }

            return dept ;
        }

        public static void CreateDepartment(Department dept,string shouldOrderNO,string action)
        {
            try
            {
                string sql = "SProc_Admin_CreateDepartment";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Code", dept.Code),
                                        new SqlParameter("@Department",dept.DepartmentName),
                                        new SqlParameter("@AbbreViation",dept.Abbreviation),
                                        new SqlParameter("@ParentCode",dept.ParentCode),
                                        new SqlParameter("@DeptTypeCode",dept.DeptTypeCode),
                                        new SqlParameter("@OrderNO",dept.OrderNO)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.CreateDepartment", "HC.Admin");
            }

        }

        public static void UpdateDepartment(Department dept,string shouldOrderNO,string action)
        {
            try
            {
                string sql = "SProc_Admin_UpdateDepartment";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode",dept.DeptCode),
                                        new SqlParameter("@Code",dept.Code),
                                        new SqlParameter("@Department",dept.DepartmentName),
                                        new SqlParameter("@AbbreViation",dept.Abbreviation),
                                        new SqlParameter("@State",dept.State),
                                        new SqlParameter("@ParentCode",dept.ParentCode),
                                        new SqlParameter("@DeptTypeCode",dept.DeptTypeCode),
                                        new SqlParameter("@OrderNO",dept.OrderNO)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateDepartment", "HC.Admin");
            }
        }

        public static DataTable GetSortDepartment()
        {
            try
            {
                string sql = "SProc_Admin_GetTreeData";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetSortDepartment", "HC.Admin");
                return null;
            }
        }

        public static DataTable GetDeptType()
        {
            try
            {
                string sql = "SProc_Admin_GetDeptType";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDeptType", "HC.Admin");
                return null;
            }
        }


        public static DataTable GetProcessType()
        {
            try
            {
                string sql = "SProc_Admin_GetProcessType";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetProcessType", "HC.Admin");
                return null;
            }
        }

        public static void DeleteDepartment(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_DeleteDepartment";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteDepartment", "HC.Admin");
            }

        }

        public static DataSet GetUserByDeptCode(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_GetUserByDeptCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null)
                    return ds;

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserByDeptCode", "HC.Admin");
                return null;
            }
        }

        public static DataSet GetUserByType(string deptCode,string selectType,string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetUserByType";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@SelectType",selectType),
                                        new SqlParameter("@Filter",filter)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null)
                    return ds;

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserByType", "HC.Admin");
                return null;
            }
        }

        public static UserProfile GetUserProfile(string userCode)
        {
            UserProfile up = new UserProfile();
            try
            {
                string sql = "SProc_Admin_GetUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserCode", userCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    up.ID = dt.Rows[0]["ID"].ToString();
                    up.ADAccount = dt.Rows[0]["ADAccount"].ToString();
                    up.Birthdate = dt.Rows[0]["Birthdate"].ToString();
                    up.CellPhone = dt.Rows[0]["CellPhone"].ToString();
                    up.CHName = dt.Rows[0]["CHName"].ToString();
                    up.CostCenter = dt.Rows[0]["CostCenter"].ToString();
                    up.DeptCode = dt.Rows[0]["DeptCode"].ToString();
                    up.Email = dt.Rows[0]["Email"].ToString();
                    up.EmployeeID = dt.Rows[0]["EmployeeID"].ToString();
                    up.ENName = dt.Rows[0]["ENName"].ToString();
                    up.HireDate = dt.Rows[0]["HireDate"].ToString();
                    up.ManagerAccount = dt.Rows[0]["ManagerAccount"].ToString();
                    up.OfficePhone = dt.Rows[0]["OfficePhone"].ToString();
                    up.PositionGuid = dt.Rows[0]["PositionGuid"].ToString();
                    up.WorkPlace = dt.Rows[0]["WorkPlace"].ToString();

                    up.FAX = dt.Rows[0]["FAX"].ToString();
                    up.BlackBerry = dt.Rows[0]["BlackBerry"].ToString();
                    up.GraduateFrom = dt.Rows[0]["GraduateFrom"].ToString();
                    up.OAC = dt.Rows[0]["OAC"].ToString();
                    up.PoliticalAffiliation = dt.Rows[0]["PoliticalAffiliation"].ToString();
                    string gender = dt.Rows[0]["Gender"].ToString();
                    up.Gender = string.IsNullOrEmpty(gender) ? "N" : gender;
                    up.PositionDesc = dt.Rows[0]["PositionDesc"].ToString();
                    up.EducationalBackground = dt.Rows[0]["EducationalBackground"].ToString();
                    up.WorkExperienceBefore = dt.Rows[0]["WorkExperienceBefore"].ToString();
                    up.WorkExperienceNow = dt.Rows[0]["WorkExperienceNow"].ToString();
                    up.OrderNo = dt.Rows[0]["OrderNo"].ToString();
                    up.PhotoUrl = dt.Rows[0]["PhotoUrl"].ToString();
                }
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserProfile", "HC.Admin");
            }

            return up;
        }

        public static DataTable GetPosition()
        {
            try
            {
                string sql = "SProc_Admin_GetPosition";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetPosition", "HC.Admin");
                return null;
            }
        }

        public static void CreateUserProfile(UserProfile up)
        {
            try
            {
                string sql = "SProc_Admin_CreateUserProfile";

                SqlParameter[] paras = {                
                                        new SqlParameter("@CHName", up.CHName),
                                        new SqlParameter("@ENName",up.ENName),
                                        new SqlParameter("@ADAccount",up.ADAccount),
                                        new SqlParameter("@Email",up.Email),
                                        new SqlParameter("@OfficePhone", up.OfficePhone),
                                        new SqlParameter("@CellPhone",up.CellPhone),
                                        new SqlParameter("@WorkPlace",up.WorkPlace),
                                        new SqlParameter("@DeptCode",up.DeptCode),
                                        new SqlParameter("@HireDate", up.HireDate==""?DBNull.Value.ToString():up.HireDate),
                                        new SqlParameter("@Birthdate",up.Birthdate==""?DBNull.Value.ToString():up.Birthdate),
                                        new SqlParameter("@PositionGuid",up.PositionGuid),
                                        new SqlParameter("@ManagerAccount",up.ManagerAccount),
                                        new SqlParameter("@EmployeeID", up.EmployeeID),
                                        //new SqlParameter("@CostCenter",up.CostCenter),

                                        new SqlParameter("@FAX", up.FAX),
                                        new SqlParameter("@BlackBerry",up.BlackBerry),
                                        new SqlParameter("@GraduateFrom",up.GraduateFrom),
                                        new SqlParameter("@OAC",up.OAC),
                                        new SqlParameter("@PoliticalAffiliation", up.PoliticalAffiliation),
                                        new SqlParameter("@Gender",up.Gender),
                                        new SqlParameter("@PositionDesc",up.PositionDesc),
                                        new SqlParameter("@EducationalBackground",up.EducationalBackground),
                                        new SqlParameter("@WorkExperienceBefore", up.WorkExperienceBefore),
                                        new SqlParameter("@WorkExperienceNow",up.WorkExperienceNow),
                                        new SqlParameter("@OrderNo", up.OrderNo),
                                        new SqlParameter("@PhotoUrl",up.PhotoUrl)

            };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.CreateUserProfile", "HC.Admin");
            }
        }

        public static void UpdateUserProfile(UserProfile up)
        {
            try
            {
                string sql = "SProc_Admin_UpdateUserProfile";

                SqlParameter[] paras = {   
                                        new SqlParameter("@UserID",up.ID),
                                        new SqlParameter("@CHName", up.CHName),
                                        new SqlParameter("@ENName",up.ENName),
                                        new SqlParameter("@ADAccount",up.ADAccount),
                                        new SqlParameter("@Email",up.Email),
                                        new SqlParameter("@OfficePhone", up.OfficePhone),
                                        new SqlParameter("@CellPhone",up.CellPhone),
                                        new SqlParameter("@WorkPlace",up.WorkPlace),
                                        new SqlParameter("@DeptCode",up.DeptCode),
                                        new SqlParameter("@HireDate", up.HireDate==""?DBNull.Value.ToString():up.HireDate),
                                        new SqlParameter("@Birthdate",up.Birthdate==""?DBNull.Value.ToString():up.Birthdate),
                                        new SqlParameter("@PositionGuid",up.PositionGuid),
                                        new SqlParameter("@ManagerAccount",up.ManagerAccount),
                                        new SqlParameter("@EmployeeID", up.EmployeeID),
                                        //new SqlParameter("@CostCenter",up.CostCenter),

                                        new SqlParameter("@FAX", up.FAX),
                                        new SqlParameter("@BlackBerry",up.BlackBerry),
                                        new SqlParameter("@GraduateFrom",up.GraduateFrom),
                                        new SqlParameter("@OAC",up.OAC),
                                        new SqlParameter("@PoliticalAffiliation", up.PoliticalAffiliation),
                                        new SqlParameter("@Gender",up.Gender),
                                        new SqlParameter("@PositionDesc",up.PositionDesc),
                                        new SqlParameter("@EducationalBackground",up.EducationalBackground),
                                        new SqlParameter("@WorkExperienceBefore", up.WorkExperienceBefore),
                                        new SqlParameter("@WorkExperienceNow",up.WorkExperienceNow),
                                        new SqlParameter("@OrderNo", up.OrderNo),
                                        new SqlParameter("@PhotoUrl",up.PhotoUrl)
            };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateUserProfile", "HC.Admin");
            }
        }

        public static bool IsExist(UserProfile up)
        {
            if (string.IsNullOrEmpty(up.Email))
                return false;   //表示不存在

            string sql = "SProc_Admin_UserIsExist";
            int retCount = 0;
            SqlParameter[] parms ={
                                      new SqlParameter("email",up.Email)
                                 };
            SqlDataReader dr = SQLHelper.ExecuteReader(dbConnStr, CommandType.StoredProcedure, sql, parms);

            if (dr != null)
            {
                if (dr.Read())
                {
                    retCount = dr.GetInt32(0);
                    if (retCount == 0)
                        return false;
                }
            }
            return true;        //异常情况不能输入
        }

        public static void EnabelOrDisableUser(string userIDs,string state)
        {
            try
            {
                string sql = "SProc_Admin_EnabelorDisableUserProfile";

                SqlParameter[] paras = {   
                                        new SqlParameter("@UserIDs",userIDs),
                                        new SqlParameter("@State",state)
            };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.EnabelOrDisableUser", "HC.Admin");
            }
        }

        public static DataSet GetMyDoc(string user, string pagenum, string pagesize,string procSetID,string startDate,string endDate,string folio)
        {
            try
            {
                string sql = "SProc_GetMyDoc";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserName", user),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcSetID", procSetID),
                                        new SqlParameter("@StartTime", startDate),
                                        new SqlParameter("@EndTime", endDate),
                                        new SqlParameter("@PageNum",pagenum),
                                        new SqlParameter("@PageSize",pagesize)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

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
                    else
                    {
                        //r.Delete();
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");
                //dt.Columns.Add("AliasName");
                dt.Columns.Add("StatusChnName");
                //dt.Columns.Add("ViewURL");

                foreach (DataRow r in dt.Rows)
                {
                    //string name = r["ProcName"].ToString();
                    //r["AliasName"] = Settings.GetConfigurationValue(name);

                    string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"window.open('{0}', '{2}', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=950,height=500,left=100,top=100')\">{1}</a>";
                    string status = r["Status"].ToString().Trim();
                    //status = string.Format(urlLink, string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()), Settings.GetConfigurationValue(status), "MyDocStatus" + r["ProcInstID"]);
                    r["StatusChnName"] = Settings.GetConfigurationValue(status);
                    string originator = r["Originator"].ToString();
                    r["Originator"] = UserInfo.GetUserName(originator.Replace("K2:", ""));
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procInstId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    //r["FormURL"] = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">流程历史</a>";
                    if (procSetId > 0 && procInstId > 0)
                    {
                        ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                        if (entity != null)
                        {
                            string viewFormUrl = string.IsNullOrEmpty(entity.ViewPage) ? string.Empty : (entity.ViewPage + "?ProcInstId=" + procInstId.ToString());

                            r["FormURL"] = string.Format(urlLink, viewFormUrl, r["Folio"].ToString(), "MyDoc" + procInstId, procInstId.ToString());
                            //r["ViewURL"] = viewFormUrl;
                            //if (!string.IsNullOrEmpty(entity.Description))
                            //    r["AliasName"] = entity.Description;
                        }
                        else
                        {
                            //r.Delete();
                        }
                    }
                }
                //dt.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetMyDoc", "HC.Admin");
                return null;
            }
        }

        public static DataSet GetMyStartedProcess(string user, string pagenum, string pagesize, string procSetID, string startDate, string endDate, string folio)
        {
            try
            {
                string sql = "SProc_GetMyApplication";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserName", user),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcSetID", procSetID),
                                        new SqlParameter("@StartTime", startDate),
                                        new SqlParameter("@EndTime", endDate),
                                        new SqlParameter("@PageNum",pagenum),
                                        new SqlParameter("@PageSize",pagesize)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

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
                    else
                    {
                        //r.Delete();
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");
                //dt.Columns.Add("AliasName");
                dt.Columns.Add("StatusChnName");
                //dt.Columns.Add("ViewURL");

                foreach (DataRow r in dt.Rows)
                {
                    //string name = r["Name"].ToString();
                    //r["AliasName"] = Settings.GetConfigurationValue(name);

                    string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"window.open('{0}', '{2}', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=950,height=500,left=100,top=100')\">{1}</a>";
                    string status = r["Status"].ToString().Trim();
                    //status = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">" + Settings.GetConfigurationValue(status) + "</a>";
                    //status = string.Format(urlLink, string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()), Settings.GetConfigurationValue(status), "Status" + r["ProcInstID"]);
                    r["StatusChnName"] = Settings.GetConfigurationValue(status);
                    //r["FormURL"] = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">流程历史</a>";

                    string actName = r["CurrentActName"].ToString();
                    r["CurrentActName"] = Common.FormatBrackets(actName);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    if (procSetId > 0 && procId > 0)
                    {
                        ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                        if (entity != null)
                        {
                            string viewFormUrl = string.IsNullOrEmpty(entity.ViewPage) ? string.Empty : (entity.ViewPage + "?ProcInstId=" + procId.ToString());

                            //r["ViewURL"] = viewFormUrl;
                            r["FormURL"] = string.Format(urlLink, viewFormUrl, r["Folio"].ToString(), "MyApp" + r["ProcInstID"],procId.ToString());
                            //if (!string.IsNullOrEmpty(entity.Description))
                            //    r["AliasName"] = entity.Description;
                        }
                        else
                        {
                            //r.Delete();
                        }
                    }
                }
                //dt.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetMyStartedProcess", "HC.Admin");
                return null;
            }
        }

        public static DataSet GetMyWorklist(string user, string pagenum, string pagesize, string procSetID, string folio,string group)
        {
            try
            {
                string sql = "SProc_GetMyWorklist";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ActionerName", user),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcSetID", procSetID),
                                        //new SqlParameter("@StartTime", startDate),
                                        //new SqlParameter("@EndTime", endDate),
                                        new SqlParameter("@PageNum",pagenum),
                                        new SqlParameter("@PageSize",pagesize),
                                        new SqlParameter("@Group",group)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

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
                    else
                    {
                        //r.Delete();
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");
                //dt.Columns.Add("AliasName");
                //dt.Columns.Add("StatusChnName");
                //dt.Columns.Add("ViewURL");

                string originator = "";
                foreach (DataRow r in dt.Rows)
                {
                    //string name = r["Name"].ToString();
                    //r["AliasName"] = Settings.GetConfigurationValue(name);

                    //string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"window.open('{0}', '{2}', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=950,height=500,left=100,top=100')\">{1}</a>";
                    string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"OpenWorkList('{0}', '{2}')\">{1}</a>";
                    originator = r["Originator"].ToString();
                    r["Originator"] = UserInfo.GetUserName(originator.Replace("K2:", ""));
                    //string status = r["Status"].ToString().Trim();
                    //status = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">" + Settings.GetConfigurationValue(status) + "</a>";
                    //status = string.Format(urlLink, string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()), Settings.GetConfigurationValue(status), "Status" + r["ProcInstID"]);
                    //r["StatusChnName"] = Settings.GetConfigurationValue(status);
                    //r["FormURL"] = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">流程历史</a>";

                    string actName = r["ActivityName"].ToString();
                    r["ActivityName"] = Common.FormatBrackets(actName);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    if (procSetId > 0 && procId > 0)
                    {
                        ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                        if (entity != null)
                        {
                            //string viewFormUrl = string.IsNullOrEmpty(entity.ViewPage) ? string.Empty : (entity.ViewPage + "?ProcInstId=" + procId.ToString());

                            //r["ViewURL"] = viewFormUrl;
                            r["FormURL"] = string.Format(urlLink, r["data"].ToString(), r["Folio"].ToString(), "MyApp" + r["ProcInstID"], procId.ToString());
                            //if (!string.IsNullOrEmpty(entity.Description))
                            //    r["AliasName"] = entity.Description;
                        }
                        else
                        {
                            //r.Delete();
                        }
                    }
                }
                //dt.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetMyWorklist", "HC.Admin");
                return null;
            }
        }

        public static DataSet GetActiveApplication(string user, string pagenum, string pagesize, string procSetID, string startDate, string endDate, string folio,string operate)
        {
            try
            {
                string sql = "SProc_GetActiveApplication";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserName", user),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcSetID", procSetID),
                                        new SqlParameter("@StartTime", startDate),
                                        new SqlParameter("@EndTime", endDate),
                                        new SqlParameter("@PageNum",pagenum),
                                        new SqlParameter("@PageSize",pagesize),
                                        new SqlParameter("@CurrentActioners",operate)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

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
                    else
                    {
                        //r.Delete();
                    }
                }

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormURL");
                //dt.Columns.Add("AliasName");
                dt.Columns.Add("StatusChnName");
                //dt.Columns.Add("ViewURL");

                string originator = "";
                foreach (DataRow r in dt.Rows)
                {
                    //string name = r["Name"].ToString();
                    //r["AliasName"] = Settings.GetConfigurationValue(name);

                    string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"window.open('{0}', '{2}', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=950,height=500,left=100,top=100')\">{1}</a>";
                    string status = r["Status"].ToString().Trim();
                    //status = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">" + Settings.GetConfigurationValue(status) + "</a>";
                    //status = string.Format(urlLink, string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()), Settings.GetConfigurationValue(status), "Status" + r["ProcInstID"]);
                    r["StatusChnName"] = Settings.GetConfigurationValue(status);
                    //r["FormURL"] = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">流程历史</a>";

                    originator = r["Originator"].ToString();
                    r["Originator"] = UserInfo.GetUserName(originator.Replace("K2:", ""));

                    string actName = r["CurrentActName"].ToString();
                    r["CurrentActName"] = Common.FormatBrackets(actName);
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    int procId = Common.SafeInt(r["ProcInstID"].ToString(), 0);
                    if (procSetId > 0 && procId > 0)
                    {
                        ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                        if (entity != null)
                        {
                            string viewFormUrl = string.IsNullOrEmpty(entity.ViewPage) ? string.Empty : (entity.ViewPage + "?ProcInstId=" + procId.ToString());

                            //r["ViewURL"] = viewFormUrl;
                            r["FormURL"] = string.Format(urlLink, viewFormUrl, r["Folio"].ToString(), "MyApp" + r["ProcInstID"], procId.ToString());
                            //if (!string.IsNullOrEmpty(entity.Description))
                            //    r["AliasName"] = entity.Description;
                        }
                        else
                        {
                            //r.Delete();
                        }
                    }
                }
                //dt.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetMyStartedProcess", "HC.Admin");
                return null;
            }
        }


        public static DataTable GetAllProcSet()
        {
            try
            {
                string sql = "SProc_GetAllProcSet";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);

                if (ds == null || ds.Tables.Count == 0)
                    return null;

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("Description");
                dt.Columns.Add("StartPage");
                dt.Columns.Add("ViewPage");
                dt.Columns.Add("EditPage");

                foreach (DataRow r in dt.Rows)
                {
                    int procSetId = Common.SafeInt(r["ProcSetId"].ToString(), 0);
                    ProcessSettingsEntity entity = Settings.GetProcessSettings(procSetId);
                    r["Description"] = entity.Description;
                    r["StartPage"] = entity.StartPage;
                    r["ViewPage"] = entity.ViewPage;
                    r["EditPage"] = entity.EditPage;
                }

                return dt;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetAllProcSet", "HC.Admin");
                return null;
            }
        }

        public static DataSet GetDraftByUser(string account,string pageNum,string pagesize)
        {
            try
            {
                string sql = "SProc_GetDraftByUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@SaveUser", account),
                                        new SqlParameter("@PageNum",pageNum),
                                        new SqlParameter("@PageSize",pagesize)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds == null || ds.Tables.Count == 0)
                    return null;

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("FormUrl");
                //dt.Columns.Add("AliasName");
                //dt.Columns.Add("StatusChnName");
                //dt.Columns.Add("ViewURL");

                foreach (DataRow r in dt.Rows)
                {
                    //string name = r["Name"].ToString();
                    //r["AliasName"] = Settings.GetConfigurationValue(name);

                    string urlLink = "<a href=\"#\" id=\"HL_{3}\" onclick=\"window.open('{0}', '{2}', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,copyhistory=yes,width=950,height=500,left=100,top=100')\">{1}</a>";
                    //string originator = r["Originator"].ToString();
                    //r["Originator"] = UserInfo.GetUserName(originator.Replace("K2:", ""));
                    //string status = r["Status"].ToString().Trim();
                    //status = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">" + Settings.GetConfigurationValue(status) + "</a>";
                    //status = string.Format(urlLink, string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()), Settings.GetConfigurationValue(status), "Status" + r["ProcInstID"]);
                    //r["StatusChnName"] = Settings.GetConfigurationValue(status);
                    //r["FormURL"] = "<a href=\"" + string.Format(ViewFlowPageUrl, r["ProcInstID"].ToString()) + "\" target=\"_blank\">流程历史</a>";

                    //string actName = r["ActivityName"].ToString();
                    //r["ActivityName"] = Common.FormatBrackets(actName);
                    r["FormURL"] = string.Format(urlLink, r["data"].ToString(), "打开草稿", "MyDraft" + r["ID"],r["ID"].ToString());

                }
                //dt.AcceptChanges();

                return ds;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDraftByUser", "HC.Admin");
                return null;
            }
        }

        public static void DelDraft(string id)
        {
            try
            {
                string sql = "SProc_DelDraft";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID", id)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DelDraft", "HC.Admin");
            }

        }

        public static DataTable GetUserProfileOutDept(string deptCode,string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfileOutDept";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@Filter",filter)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserProfileOutDept", "HC.Admin");
                return null;
            }

        }

        public static void AddDeptUser(string deptCode, string userCodes)
        {
            try
            {
                string sql = "SProc_Admin_AddDeptUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@UserCodes",userCodes)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddDeptUser", "HC.Admin");
            }

        }

        public static void DeleteDeptUser(string deptCode, string userCodes)
        {
            try
            {
                string sql = "SProc_Admin_DeleteDeptUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@UserCodes",userCodes)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteDeptUser", "HC.Admin");
            }

        }

        public static DataTable GetAllUserProfile(string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetAllUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Filter", filter)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetAllUserProfile", "HC.Admin");
                return null;
            }

        }

        public static void DeleteUserProfile(string userIDs,string state)
        {
            try
            {
                string sql = "SProc_Admin_DeleteUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserIDs", userIDs),
                                        new SqlParameter("@State",state)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteUserProfile", "HC.Admin");
            }

        }

        public static DataSet GetDCP(string deptCode, string processCode)
        {
            try
            {
                string sql = "SProc_Admin_GetDCP";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@ProcessCode",processCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds;

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDCP", "HC.Admin");
                return null;
            }

        }

        public static void DeleteDCP(string IDs)
        {
            try
            {
                string sql = "SProc_Admin_DeleteDCP";

                SqlParameter[] paras = { 
                                        new SqlParameter("@IDs", IDs)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteDCP", "HC.Admin");
            }

        }

        public static DCP GetDCPByID(string id)
        {
            DCP dcp = new DCP();
            try
            {
                string sql = "SProc_Admin_GetDCPByID";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID", id)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    dcp.ChargePersonAccount = dt.Rows[0]["ChargePersonAccount"].ToString();
                    dcp.DeptCode = dt.Rows[0]["DeptCode"].ToString();
                    dcp.ID = dt.Rows[0]["ID"].ToString();
                    dcp.MDAccount = dt.Rows[0]["MDAccount"].ToString();
                    dcp.ProcessCode = dt.Rows[0]["ProcessCode"].ToString();
                    dcp.WorkPlace = dt.Rows[0]["WorkPlace"].ToString();
                    dcp.ChargePersonName = dt.Rows[0]["ChargePersonName"].ToString();
                    dcp.MDName = dt.Rows[0]["MDName"].ToString();
                }
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDCPByID", "HC.Admin");
            }

            return dcp;

        }

        public static void CreateDCP(DCP dcp)
        {
            try
            {
                string sql = "SProc_Admin_CreateDCP";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", dcp.DeptCode),
                                        new SqlParameter("@ProcessCode", dcp.ProcessCode),
                                        new SqlParameter("@ChargePersonAccount", dcp.ChargePersonAccount),
                                        new SqlParameter("@ChargePersonName", dcp.ChargePersonName),
                                        new SqlParameter("@MDAccount", dcp.MDAccount),
                                        new SqlParameter("@MDName", dcp.MDName),
                                        new SqlParameter("@WorkPlace", dcp.WorkPlace)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.CreateDCP", "HC.Admin");
            }

        }

        public static void UpdateDCP(DCP dcp)
        {
            try
            {
                string sql = "SProc_Admin_UpdateDCP";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",dcp.ID),
                                        new SqlParameter("@DeptCode", dcp.DeptCode),
                                        new SqlParameter("@ProcessCode", dcp.ProcessCode),
                                        new SqlParameter("@ChargePersonAccount", dcp.ChargePersonAccount),
                                        new SqlParameter("@ChargePersonName", dcp.ChargePersonName),
                                        new SqlParameter("@MDAccount", dcp.MDAccount),
                                        new SqlParameter("@MDName", dcp.MDName),
                                        new SqlParameter("@WorkPlace", dcp.WorkPlace)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateDCP", "HC.Admin");
            }

        }

        public static DataTable GetRoles(string processCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoles";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ProcessCode",processCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoles", "HC.Admin");
                return null;
            }

        }

        public static void AddNewRole(string roleName, string processCode)
        {
            try
            {
                string sql = "SProc_Admin_AddNewRole";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleName",roleName),
                                        new SqlParameter("@ProcessCode",processCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddNewRole", "HC.Admin");
            }

        }

        public static void UpdateRole(string roleCode, string roleName, string processCode)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRole";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@RoleName",roleName),
                                        new SqlParameter("@ProcessCode",processCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateRole", "HC.Admin");
            }

        }

        public static void DeleteRoles(string roleCodes)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRoles";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCodes",roleCodes)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteRoles", "HC.Admin");
            }

        }

        public static DataTable GetRoleByRoleCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleByRoleCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoleByRoleCode", "HC.Admin");
                return null;
            }

        }

        public static DataSet GetRoleUser(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds;

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoleUser", "HC.Admin");
                return null;
            }

        }

        /// <summary>
        /// 取得角色用户信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static DataSet GetRoleUser(string roleCode, string userCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleUserByCode";

                SqlParameter[] paras = { 
                                           new SqlParameter("UserCode",userCode),
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds;

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoleUser", "HC.Admin");
                return null;
            }
        }

        /// <summary>
        /// 取得后台角色管理信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static DataSet GetRoleUserByCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleUserManageByRoleCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds;

                return null;
            }
            catch (Exception ex){
                RecoreErrorProfile(ex, "HC.Admin.GetRoleUserByCode", "HC.Admin");
                return null;
            }
        }

        public static DataTable GetUserByRoleCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetUserByRoleCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserByRoleCode", "HC.Admin");
                return null;
            }

        }

        public static void UpdateRoleUser(string roleCode, string userCodes)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRoleUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@userCodes",userCodes)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateRoleUser", "HC.Admin");
            }

        }

        public static bool ContainFormPermission(string account, string formName, string permission)
        {
            try
            {
                string sql = "SProc_Admin_ValidateFormPermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserAccount",account),
                                        new SqlParameter("@FormName",formName),
                                        new SqlParameter("@Permission",permission)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                {
                    string retValue = ds.Tables[0].Rows[0][0].ToString();

                    if (retValue == "1")
                        return true;

                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.ContainFormPermission", "HC.Admin");
                return false;
            }
        }

        public static DataTable GetFormByFormCode(string formCode)
        {
            try
            {
                string sql = "SProc_Admin_GetFormByFormCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FormCode",formCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetFormByFormCode", "HC.Admin");
                return null;
            }

        }

        public static void UpdateForm(string formCode, string formName, string formType)
        {
            try
            {
                string sql = "SProc_Admin_UpdateForm";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FormCode",formCode),
                                        new SqlParameter("@formName",formName),
                                        new SqlParameter("@formType",formType)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateForm", "HC.Admin");
            }

        }

        public static void DeleteForms(string formCodes)
        {
            try
            {
                string sql = "SProc_Admin_DeleteForms";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FormCodes",formCodes)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteForms", "HC.Admin");
            }

        }

        public static void AddNewForm(string formName, string formType)
        {
            try
            {
                string sql = "SProc_Admin_AddNewForm";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FormName",formName),
                                        new SqlParameter("@FormType",formType)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddNewForm", "HC.Admin");
            }

        }

        public static DataTable GetForms(string formCode)
        {
            try
            {
                string sql = "SProc_Admin_GetForms";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FormCode",formCode)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetForms", "HC.Admin");
                return null;
            }

        }

        public static DataTable GetRoleFormPermission(string id)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleFormPermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",id)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoleFormPermission", "HC.Admin");
                return null;
            }

        }

        public static void UpdateRoleFormPermission(string id,string roleCode,string formCode,string permCode)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRoleFormPermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",id),
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@FormCode",formCode),
                                        new SqlParameter("@PermCode",permCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateRoleFormPermission", "HC.Admin");
            }

        }

        public static void AddNewRoleFormPermission(string roleCode,string formCode,string permCode)
        {
            try
            {
                string sql = "SProc_Admin_AddNewRoleFormPermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@FormCode",formCode),
                                        new SqlParameter("@PermCode",permCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddNewRoleFormPermission", "HC.Admin");
            }

        }

        public static void DeleteRoleFormPermission(string ids)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRoleFormPermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@IDS",ids)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteRoleFormPermission", "HC.Admin");
            }

        }

        public static DataTable GetPermission()
        {
            try
            {
                string sql = "SProc_Admin_GetPermission";

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetPermission", "HC.Admin");
                return null;
            }
        }

        #region 酒店维护

        /// <summary>
        /// 取得全部酒店信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllHotel()
        {
            string sql = "SProc_GetHotelInfo";

            DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, null);

            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];

            return null;
        }

        /// <summary>
        /// 搜索酒店名称
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public static DataTable GetHotelByName(string searchName)
        {
            string sql = "SProc_GetHotalByName";

            SqlParameter[] paras ={
                new SqlParameter("@name",searchName)
            };

            DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 取得酒店信息ById
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static HotelInfo GetHotalById(string Id)
        {
            HotelInfo info = new HotelInfo();
            string sql = "SProc_GetHotalById";

            SqlParameter[] paras ={
                new SqlParameter("@Id",Id)
            };

            DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                info.Id = int.Parse(Id);
                info.HotelLevel = dt.Rows[0]["HotelLevel"].ToString();
                info.HotelName = dt.Rows[0]["HotelName"].ToString();
                info.Region = dt.Rows[0]["Region"].ToString();
                info.Address = dt.Rows[0]["Address"].ToString();
                info.Phone = dt.Rows[0]["Phone"].ToString();
                info.Chamber = dt.Rows[0]["Chamber"].ToString();
                info.NegotiatedPrice = dt.Rows[0]["NegotiatedPrice"].ToString();
                info.Breakfast = dt.Rows[0]["Breakfast"].ToString();
                info.Broadband = dt.Rows[0]["Broadband"].ToString();
                info.Preordain = dt.Rows[0]["Preordain"].ToString();
                info.Evaluation = dt.Rows[0]["Evaluation"].ToString();
                info.Remark = dt.Rows[0]["Remark"].ToString();
                info.HotelGroup = dt.Rows[0]["HotelGroup"].ToString();
                info.Sale = dt.Rows[0]["Sale"].ToString();
                info.CreatedBy = dt.Rows[0]["CreatedBy"].ToString();
            }
            return info;
        }

        /// <summary>
        /// 更新酒店表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool UpdateHotelById(HotelInfo hInfo)
        {
            string sql = "SProc_UpdateHotel";
            SqlParameter[] paras ={
                new SqlParameter("@HotelLevel",hInfo.HotelLevel),
                new SqlParameter("@HotelName",hInfo.HotelName),
                new SqlParameter("@Region",hInfo.Region),
                new SqlParameter("@Address",hInfo.Address),
                new SqlParameter("@Phone",hInfo.Phone),
                new SqlParameter("@Chamber",hInfo.Chamber),
                new SqlParameter("@NegotiatedPrice",hInfo.NegotiatedPrice),
                new SqlParameter("@Breakfast",hInfo.Breakfast),
                new SqlParameter("@Broadband",hInfo.Broadband),
                new SqlParameter("@Preordain",hInfo.Preordain),
                new SqlParameter("@Evaluation",hInfo.Evaluation),
                new SqlParameter("@Remark",hInfo.Remark),
                new SqlParameter("@HotelGroup",hInfo.HotelGroup),
                new SqlParameter("@Sale",hInfo.Sale),
                new SqlParameter("@CreatedBy",hInfo.CreatedBy),
                new SqlParameter("@Id",hInfo.Id)
            };

            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "UpdateHotelById", string.Empty);
                return false;
            }
            return true;
        }

        public static bool InsertHotelInfo(HotelInfo hInfo)
        {
            string sql = "SProc_AddHotel";
            SqlParameter[] paras ={
                new SqlParameter("@HotelLevel",hInfo.HotelLevel),
                new SqlParameter("@HotelName",hInfo.HotelName),
                new SqlParameter("@Region",hInfo.Region),
                new SqlParameter("@Address",hInfo.Address),
                new SqlParameter("@Phone",hInfo.Phone),
                new SqlParameter("@Chamber",hInfo.Chamber),
                new SqlParameter("@NegotiatedPrice",hInfo.NegotiatedPrice),
                new SqlParameter("@Breakfast",hInfo.Breakfast),
                new SqlParameter("@Broadband",hInfo.Broadband),
                new SqlParameter("@Preordain",hInfo.Preordain),
                new SqlParameter("@Evaluation",hInfo.Evaluation),
                new SqlParameter("@Remark",hInfo.Remark),
                new SqlParameter("@HotelGroup",hInfo.HotelGroup),
                new SqlParameter("@Sale",hInfo.Sale),
                new SqlParameter("@CreatedBy",hInfo.CreatedBy)
            };

            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "InsertHotelInfo", string.Empty);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除酒店信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool DeleteHotelById(int Id)
        {
            string sql = "SProc_DeleteHotelById";
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@Id",Id)
            };
            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "DeleteHotelById", string.Empty);
                return false;
            }
            return true;
        }

        #endregion

        #region 印章公司维护

        /// <summary>
        /// 通过公司名称检索公司信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable GetSealProfileByName(string name)
        {
            string sql = "SProc_GetSealProfileByName";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@Name",name)
            };

            try
            {
                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "GetSealProfileByName", string.Empty);
                return null; ;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool UpdateSealProfile(SealProfileInfo info)
        {
            string sql = "SProc_UpdateSealProfile";

            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@Id",info.Id),
                new SqlParameter("@SealName",info.SealName),
                new SqlParameter("@CreatedBy",info.CreatedBy)
            };

            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UpdateSealProfile", string.Empty);
                return false;
            }
            return true;
        }

        /// <summary>
        /// SealProfile表新增一条记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool AddSealProfile(SealProfileInfo info)
        {
            string sql = "SProc_AddSealProfile";
            SqlParameter[] paras = new SqlParameter[]{                
                new SqlParameter("@SealName",info.SealName),
                new SqlParameter("@CreatedBy",info.CreatedBy)
            };

            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "AddSealProfile", string.Empty);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除SealProfile一条记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool DeleteSealProfileById(int Id)
        {
            string sql = "SProc_DeleteSealProfileById";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@Id",Id)
            };
            try
            {
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DeleteSealProfileById", string.Empty);
                return false;
            }
            return true;
        }

        #endregion

        public static DataTable GetRolePermission(string id)
        {
            try
            {
                string sql = "SProc_Admin_GetRolePermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",id)
                                   };

                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, paras);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];

                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRolePermission", "HC.Admin");
                return null;
            }

        }

        public static void UpdateRolePermission(string id, string roleCode, string permCode)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRolePermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",id),
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@PermCode",permCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateRolePermission", "HC.Admin");
            }

        }

        public static void AddNewRolePermission(string roleCode, string permCode)
        {
            try
            {
                string sql = "SProc_Admin_AddNewRolePermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode),
                                        new SqlParameter("@PermCode",permCode)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddNewRolePermission", "HC.Admin");
            }

        }

        public static void DeleteRolePermission(string ids)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRolePermission";

                SqlParameter[] paras = { 
                                        new SqlParameter("@IDS",ids)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteRolePermission", "HC.Admin");
            }

        }

        public static void AddWorkListLog(string fromUser, string toUser, string sn, int procInstID,int actInstDestID,string logType,string createdBy)
        {
            try
            {
                string sql = "SProc_AddWorkListLog";

                SqlParameter[] paras = { 
                                        new SqlParameter("@FromUser",fromUser),
                                        new SqlParameter("@ToUser",toUser),
                                        new SqlParameter("@SN",sn),
                                        new SqlParameter("@ProcInstID",procInstID),
                                        new SqlParameter("@ActInstDestID",actInstDestID),
                                        new SqlParameter("@LogType",logType),
                                        new SqlParameter("@CreatedBy",createdBy)
                                   };

                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, paras);
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddWorkListLog", "HC.Admin");
            }
        }

        /// <summary>
        /// 取得部门信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDepartmentInfo()
        {
            try
            {
                string sql = "SProc_Admin_GetDeptInfo";
                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, null);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDepartmentInfo", "HC.Admin");
                return null;
            }
        }

        /// <summary>
        /// 更新角色用户表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="deptName"></param>
        /// <param name="deptCode"></param>
        /// <param name="mainRoleName"></param>
        /// <param name="mainRoleCode"></param>
        /// <param name="expand1"></param>
        /// <param name="expand2"></param>
        /// <param name="expand3"></param>
        /// <param name="expand4"></param>
        /// <returns></returns>
        public static bool UpdateRoleUserByID(int Id, string userCode,string deptName, string deptCode, string mainRoleName, string mainRoleCode, string expand1, string expand2, string expand3, string expand4)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRoleUserByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",Id),
                                          new SqlParameter("@UserCode",userCode),
                                          new SqlParameter("@DeptName",deptName),
                                          new SqlParameter("@DeptCode",deptCode),
                                          new SqlParameter("@MainRoleName",mainRoleName),
                                          new SqlParameter("@MainRoleCode",mainRoleCode),
                                          new SqlParameter("@ExpandField1",expand1),
                                          new SqlParameter("@ExpandField2",expand2),
                                          new SqlParameter("@ExpandField3",expand3),
                                          new SqlParameter("@ExpandField4",expand4)
                                     };
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateRoleUserByID", "HC.Admin");
                return false;
            }
        }

        /// <summary>
        /// 添加角色用户
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="ad"></param>
        /// <returns></returns>
        public static bool AddUserToRoleUser(string roleCode, string ad)
        {
            try
            {
                string sql = "SProc_Admin_AddUserToRoleUser";
                SqlParameter[] parms ={
                                          new SqlParameter("@RoleCode",roleCode),
                                          new SqlParameter("@AdAccount",ad)
                                     };
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.AddUserToRoleUser", "HC.Admin");
                return false;
            }
        }

        /// <summary>
        /// 删除角色用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteUserFromRoleUser(string ids)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRoleUserByIDs";
                SqlParameter[] parms ={
                                          new SqlParameter("@IDs",ids)
                                     };
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.DeleteUserFromRoleUser", "HC.Admin");
                return false;
            }
        }

        /// <summary>
        /// 取得角色信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public static DataTable GetRoleInfoByRoleCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleInfoByRoleCode";
                SqlParameter[] parms ={
                                      new SqlParameter("@RoleCode",roleCode)
                                 };
                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetRoleInfoByRoleCode", "HC.Admin");
                return null;
            }
        }

        /// <summary>
        /// 导入UserProfile
        /// </summary>
        /// <param name="ad"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ImportUserProfile(string ad, string name, string email)
        {
            try
            {
                string sql = "SProc_Admin_ImportUserProfile";
                SqlParameter[] parms ={
                                          new SqlParameter("@ADAccount",ad),
                                          new SqlParameter("@Email",email),
                                          new SqlParameter("@DisplayName",name)
                                     };
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.ImportUserProfile", "HC.Admin");
                return false;
            }
        }

        /// <summary>
        /// 取得用户所在部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public static DataTable GetDepartmentByUserCode(string usercode)
        {
            try
            {
                string sql = "SProc_Admin_GetDeptByUserCode";
                SqlParameter[] parms={
                                         new SqlParameter("@UserCode",usercode)
                                     };
                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetDepartmentByUserCode", "HC.Admin");
                return null;
            }
        }

        /// <summary>
        /// 更新主部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="maindeptcode"></param>
        /// <returns></returns>
        public static bool UpdateMainDepartment(string usercode, string maindeptcode)
        {
            try
            {
                string sql = "SProc_Admin_UpdateMainDepartment";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserCode",usercode),
                                          new SqlParameter("@MainDeptCode",maindeptcode)
                                     };
                SQLHelper.ExecuteNonQuery(dbConnStr, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.UpdateMainDepartment", "HC.Admin");
                return false;
            }
        }

        /// <summary>
        /// 取得用户主部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public static DataTable GetUserMainDepartmentByCode(string usercode)
        {
            try
            {
                string sql = "SProc_Admin_GetDeptByUserCode";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserCode",usercode)
                                     };
                DataSet ds = SQLHelper.ExecuteDataset(dbConnStr, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                RecoreErrorProfile(ex, "HC.Admin.GetUserMainDepartmentByCode", "HC.Admin");
                return null;
            }
        }
    }
}
