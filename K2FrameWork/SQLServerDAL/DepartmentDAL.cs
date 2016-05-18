using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data.SqlClient;
using DBUtility;
using System.Data;
using Utility;

namespace SQLServerDAL
{
    public class DepartmentDAL:IDepartment
    {
        /// <summary>
        /// 取得指定部门
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public IList<DepartmentInfo> GetSubDepartments(string orgCode, string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_GetSubDepartments";
                SqlParameter[] parms ={
                                      new SqlParameter("@ParentCode",deptCode),
                                      new SqlParameter("@OrgID",orgCode)
                                 };
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, System.Data.CommandType.StoredProcedure, sql, parms);

                List<DepartmentInfo> retDeptList = new List<DepartmentInfo>();

                while (dr.Read())
                {
                    DepartmentInfo deptInfo = new DepartmentInfo();
                    deptInfo.ID = dr.GetInt32(0);
                    deptInfo.OrgID = dr.GetGuid(1);

                    if (!dr.IsDBNull(2))
                        deptInfo.Code = dr.GetString(2);

                    if (!dr.IsDBNull(3))
                        deptInfo.DeptCode = dr.GetString(3);

                    if (!dr.IsDBNull(4))
                        deptInfo.ParentCode = dr.GetString(4);

                    if (!dr.IsDBNull(5))
                        deptInfo.ParentID = dr.GetInt32(5);

                    if (!dr.IsDBNull(6))
                        deptInfo.DeptName = dr.GetString(6);

                    if (!dr.IsDBNull(7))
                        deptInfo.Abbreviation = dr.GetString(7);

                    if (!dr.IsDBNull(8))
                        deptInfo.Levels = dr.GetInt32(8);

                    if (!dr.IsDBNull(9))
                        deptInfo.Description = dr.GetString(9);

                    if (!dr.IsDBNull(10))
                        deptInfo.DeptTypeCode = dr.GetString(10);

                    if (!dr.IsDBNull(11))
                        deptInfo.State = dr.GetInt32(11);

                    if (!dr.IsDBNull(12))
                        deptInfo.OrderNo = dr.GetInt32(12);

                    if (!dr.IsDBNull(13))
                        deptInfo.CreatedOn = dr.GetDateTime(13);

                    if (!dr.IsDBNull(14))
                        deptInfo.CreatedBy = dr.GetString(14);

                    retDeptList.Add(deptInfo);
                }

                return retDeptList;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.GetDepartmentInfo", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得部门类别
        /// </summary>
        /// <returns></returns>
        public DataSet GetDeptType()
        {
            try
            {
                string sql = "SProc_Admin_GetDeptType";

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.GetDeptType", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得部门
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataSet GetSortDepartment(string orgID)
        {
            try
            {
                string sql = "SProc_Admin_GetTreeData";

                SqlParameter[] parms ={
                                          new SqlParameter("@OrgID",orgID)
                                     };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.GetSortDepartment", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得单一部门信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataSet GetDepartmentInfo(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_GetDepartmentInfo";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode)
                                   };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.GetDepartmentInfo", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="shouldOrderNO"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool CreateDepartment(DepartmentInfo dept, string shouldOrderNO, string action)
        {
            try
            {
                string sql = "SProc_Admin_CreateDepartment";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Code", dept.Code),
                                        new SqlParameter("@Department",dept.DeptName),
                                        new SqlParameter("@AbbreViation",dept.Abbreviation),
                                        new SqlParameter("@ParentCode",dept.ParentCode),
                                        new SqlParameter("@DeptTypeCode",dept.DeptTypeCode),
                                        new SqlParameter("@OrderNO",dept.OrderNo),
                                        new SqlParameter("@OrgID",dept.OrgID),
                                        new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                   };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.CreateDepartment", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="shouldOrderNO"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentInfo dept, string shouldOrderNO, string action)
        {
            try
            {
                string sql = "SProc_Admin_UpdateDepartment";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode",dept.DeptCode),
                                        new SqlParameter("@Code",dept.Code),
                                        new SqlParameter("@Department",dept.DeptName),
                                        new SqlParameter("@AbbreViation",dept.Abbreviation),
                                        new SqlParameter("@State",dept.State),
                                        new SqlParameter("@ParentCode",dept.ParentCode),
                                        new SqlParameter("@DeptTypeCode",dept.DeptTypeCode),
                                        new SqlParameter("@OrderNO",dept.OrderNo),
                                        new SqlParameter("@OrgID",dept.OrgID)
                                   };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.UpdateDepartment", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得用户所在部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public DataSet GetDepartmentByUserCode(string usercode)
        {
            try
            {
                string sql = "SProc_Admin_GetDeptByUserCode";
                SqlParameter[] parms ={
                                         new SqlParameter("@UserCode",usercode)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.UpdateDepartment", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 删除部门用户
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="userCodes"></param>
        /// <returns></returns>
        public bool DeleteDeptUser(string deptCode, string userCodes)
        {
            try
            {
                string sql = "SProc_Admin_DeleteDeptUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@UserCodes",userCodes)
                                   };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.DeleteDeptUser", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得部门信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepartmentInfo()
        {
            try
            {
                string sql = "SProc_Admin_GetDeptInfo";
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, null);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "DepartmentDAL.GetDepartmentInfo", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
