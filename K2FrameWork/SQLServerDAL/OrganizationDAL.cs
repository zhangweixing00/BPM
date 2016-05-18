using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using DBUtility;
using Utility;

namespace SQLServerDAL
{
    public class OrganizationDAL : IOrganizationDAL
    {
        /// <summary>
        /// 取得所有组织信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrgList()
        {
            try
            {
                string sql = "SProc_Admin_GetAllOrgInfo";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.GetOrgList", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得某个组织信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetOrgByID(Guid id)
        {
            try
            {
                string sql = "SProc_Admin_GetOrgInfoByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",id)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.GetOrgByID", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 创建组织信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool CreateOrgInfo(OrganizationInfo info)
        {
            try
            {
                string sql = "SProc_Admin_AddOrgInfo";
                SqlParameter[] parms ={
                                          new SqlParameter("@OrgName",info.OrgName),
                                          new SqlParameter("@OrgCode",info.OrgCode),
                                          new SqlParameter("@OrgDesc",info.OrgDescription),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD()),
                                          new SqlParameter("@OrderNo",info.OrderNo)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.CreateOrgInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新组织信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateOrgInfo(OrganizationInfo info)
        {
            try
            {
                string sql = "SProc_Admin_UpdateOrgInfo";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",info.ID),
                                          new SqlParameter("@OrgName",info.OrgName),
                                          new SqlParameter("@OrgCode",info.OrgCode),
                                          new SqlParameter("@OrgDesc",info.OrgDescription),
                                          new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD()),
                                          new SqlParameter("@OrderNo",info.OrderNo)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.UpdateOrgInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOrgInfo(Guid id)
        {
            try
            {
                string sql = "SProc_Admin_DeleteOrgInfo";
                SqlParameter[] parms ={
                                          new SqlParameter("@ID",id)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.DeleteOrgInfo", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOrgInfoByID(Guid Id)
        {
            try
            {
                string sql = "SProc_Admin_DeleteOrgInfoByID";
                SqlParameter[] parms ={
                                          new SqlParameter("@OrgID",Id)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.DeleteOrgInfoByID", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool DeleteDepartmentByCode(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_DeleteDepartmentByCode";
                SqlParameter[] parms ={
                                          new SqlParameter("@DeptCode",deptCode)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.DeleteDepartmentByCode", DBManager.GetCurrentUserAD());
                return false;
            }
        }


        /// <summary>
        /// 取得部门数据类型的组织信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgOfDeptType()
        {
            try
            {
                string sql = "P_K2_GetOrgOfDeptType";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "OrganizationDAL.GetOrgOfDeptType", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
