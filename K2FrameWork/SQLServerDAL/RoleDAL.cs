using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Utility;
using IDAL;

namespace SQLServerDAL
{
    public class RoleDAL : IRoleDAL
    {
        public DataSet GetRoles(string processCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoles";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ProcessCode",processCode)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoles", DBManager.GetCurrentUserAD());
                return null;
            }
        }
        public DataSet GetRoleByMenuID(string MenuGUID)
        {
            try
            {
                string sql = "SProc_GetRoleByMenuID";

                SqlParameter[] paras = { 
                                        new SqlParameter("@MenuGUID",MenuGUID)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleByMenuID", DBManager.GetCurrentUserAD());
                return null;
            }
        }
        public DataSet GetRoleByRoleCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleByRoleCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleByRoleCode", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public bool UpdateRole(string roleCode, string roleName, string processCode, Guid orgID, string description)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRole";

                SqlParameter[] paras = { 
                                        new SqlParameter("@ID",roleCode),
                                        new SqlParameter("@RoleName",roleName),
                                        new SqlParameter("@ProcessCode",processCode),
                                        new SqlParameter("@OrgID",orgID),
                                        new SqlParameter("@Desciption",description)
                                   };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleByRoleCode", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public bool AddNewRole(string roleName, string processCode, Guid orgID, string description)
        {
            try
            {
                string sql = "SProc_Admin_AddNewRole";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleName",roleName),
                                        new SqlParameter("@ProcessCode",processCode),
                                        new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD()),
                                        new SqlParameter("@OrgID",orgID),
                                        new SqlParameter("@Desciption",description)
                                   };

                SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleByRoleCode", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得后台角色管理信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataSet GetRoleUserByCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleUserManageByRoleCode";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCodeParm",roleCode),
                                        new SqlParameter("@CreatedByParm",DBManager.GetCurrentUserAD())
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleUserByCode", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得角色信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetRoleInfoByRoleCode(string roleCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleInfoByRoleCode";
                SqlParameter[] parms ={
                                      new SqlParameter("@RoleCode",roleCode)
                                 };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleInfoByRoleCode", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 删除角色用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteUserFromRoleUser(string ids)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRoleUserByIDs";
                SqlParameter[] parms ={
                                          new SqlParameter("@IDs",ids)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.DeleteUserFromRoleUser", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleCodes"></param>
        /// <returns></returns>
        public bool DeleteRoles(string roleCodes)
        {
            try
            {
                string sql = "SProc_Admin_DeleteRoles";

                SqlParameter[] paras = { 
                                        new SqlParameter("@RoleCodes",roleCodes)
                                   };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.DeleteRoles", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 添加角色用户
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="ad"></param>
        /// <returns></returns>
        public bool AddUserToRoleUser(string roleCode, string ad)
        {
            try
            {
                string sql = "SProc_Admin_AddUserToRoleUser";
                SqlParameter[] parms ={
                                          new SqlParameter("@RoleCode",roleCode),
                                          new SqlParameter("@AdAccount",ad),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.AddUserToRoleUser", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得角色用户信息
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataSet GetRoleUser(Guid roleCode, Guid userCode)
        {
            try
            {
                string sql = "SProc_Admin_GetRoleUserByCode";

                SqlParameter[] paras = { 
                                           new SqlParameter("UserCode",userCode),
                                        new SqlParameter("@RoleCode",roleCode)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRoleUser", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 更新用户角色表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userCode"></param>
        /// <param name="deptName"></param>
        /// <param name="deptCode"></param>
        /// <param name="dutyRegion"></param>
        /// <param name="mainRoleName"></param>
        /// <param name="mainRoleCode"></param>
        /// <param name="expand1"></param>
        /// <param name="expand2"></param>
        /// <param name="expand3"></param>
        /// <param name="expand4"></param>
        /// <returns></returns>
        public bool UpdateRoleUserByID(Guid Id, string userCode, string deptName, string deptCode,string dutyRegion, string mainRoleName, string mainRoleCode, string expand1, string expand2, string expand3, string expand4)
        {
            try
            {
                string sql = "SProc_Admin_UpdateRoleUserByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@RoleUserID",Id),
                                          new SqlParameter("@ADAccount",userCode),
                                          new SqlParameter("@DutyDeptName",deptName),
                                          new SqlParameter("@DutyDeptCode",deptCode),
                                          new SqlParameter("@DutyRegion",dutyRegion),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD()),
                                          new SqlParameter("@MainRoleName",mainRoleCode),
                                          new SqlParameter("@MainRoleCode",mainRoleCode),
                                          new SqlParameter("@ExpandField1",expand1),
                                          new SqlParameter("@ExpandField2",expand2),
                                          new SqlParameter("@ExpandField3",expand3),
                                          new SqlParameter("@ExpandField4",expand4)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.UpdateRoleUserByID", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 通过组织ID获取角色信息
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="parm"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable GetRolesByOrgParams(Guid orgID, string parm, string rows)
        {
            try
            {
                string sql = "P_K2_GetRolesByOrgParams";
                SqlParameter[] parms ={
                                          new SqlParameter("@OrgID",orgID.ToString()),
                                          new SqlParameter("@Params",parm),
                                          new SqlParameter("@Rows",rows)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "RoleDAL.GetRolesByOrganization", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}