using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Utility;
using DBUtility;
using IDAL;
using Model;

namespace SQLServerDAL
{
    public class UserProfileDAL : IUserProfile
    {
        /// <summary>
        /// 通过部门ID取得该部门下所有用户信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataSet GetUserByDeptCode(string deptCode)
        {
            try
            {
                string sql = "SProc_Admin_GetUserByDeptCode";
                SqlParameter[] parms ={
                                          new SqlParameter("@DeptCode",deptCode)
                                     };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserByDeptCode", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public DataSet GetUserProfileOutDept(string deptCode, string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfileOutDept";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@Filter",filter)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserProfileOutDept", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public bool AddDeptUser(string deptCode, string userCodes)
        {
            try
            {
                string sql = "SProc_Admin_AddDeptUser";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@UserCodes",userCodes),
                                        new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                   };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.AddDeptUser", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得某个用户信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataSet GetUserProfile(string userCode)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserCode",userCode)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserProfile", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool CreateUserProfile(UserProfileInfo up)
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
                                        new SqlParameter("@HireDate", up.HireDate==""?DBNull.Value.ToString():up.HireDate),
                                        new SqlParameter("@Birthdate",up.Birthdate==""?DBNull.Value.ToString():up.Birthdate),
                                        new SqlParameter("@PositionGuid",up.PositionGuid),
                                        new SqlParameter("@ManagerAccount",up.ManagerAccount),
                                        new SqlParameter("@FAX", up.FAX),
                                        new SqlParameter("@BlackBerry",up.BlackBerry),
                                        new SqlParameter("@GraduateFrom",up.GraduateFrom),
                                        new SqlParameter("@OAC",up.OAC),
                                        new SqlParameter("@PoliticalAffiliation", up.PoliticalAffiliation),
                                        new SqlParameter("@Gender",up.Gender),
                                        new SqlParameter("@EducationalBackground",up.EducationalBackground),
                                        new SqlParameter("@WorkExperienceBefore", up.WorkExperienceBefore),
                                        new SqlParameter("@WorkExperienceNow",up.WorkExperienceNow),
                                        new SqlParameter("@OrderNo", up.OrderNo),
                                        new SqlParameter("@PhotoUrl",up.PhotoUrl),
                                        new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())

            };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.CreateUserProfile", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="up"></param>
        public bool UpdateUserProfile(UserProfileInfo up)
        {
            try
            {
                string sql = "SProc_Admin_UpdateUserProfile";

                SqlParameter[] paras = {   
                                        new SqlParameter("@ID",up.ID),
                                        new SqlParameter("@CHName", up.CHName),
                                        new SqlParameter("@ENName",up.ENName),
                                        new SqlParameter("@ADAccount",up.ADAccount),
                                        new SqlParameter("@Email",up.Email),
                                        new SqlParameter("@OfficePhone", up.OfficePhone),
                                        new SqlParameter("@CellPhone",up.CellPhone),
                                        new SqlParameter("@WorkPlace",up.WorkPlace),
                                        new SqlParameter("@HireDate", up.HireDate==""?DBNull.Value.ToString():up.HireDate),
                                        new SqlParameter("@Birthdate",up.Birthdate==""?DBNull.Value.ToString():up.Birthdate),
                                        new SqlParameter("@PositionGuid",up.PositionGuid),
                                        new SqlParameter("@ManagerAccount",up.ManagerAccount),
                                        new SqlParameter("@FAX", up.FAX),
                                        new SqlParameter("@BlackBerry",up.BlackBerry),
                                        new SqlParameter("@GraduateFrom",up.GraduateFrom),
                                        new SqlParameter("@OAC",up.OAC),
                                        new SqlParameter("@PoliticalAffiliation", up.PoliticalAffiliation),
                                        new SqlParameter("@Gender",up.Gender),
                                        new SqlParameter("@EducationalBackground",up.EducationalBackground),
                                        new SqlParameter("@WorkExperienceBefore", up.WorkExperienceBefore),
                                        new SqlParameter("@WorkExperienceNow",up.WorkExperienceNow),
                                        new SqlParameter("@OrderNo", up.OrderNo),
                                        new SqlParameter("@PhotoUrl",up.PhotoUrl)
                                       };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.UpdateUserProfile", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool IsExist(UserProfileInfo up)
        {
            try
            {
                if (string.IsNullOrEmpty(up.Email))
                    return false;   //表示不存在

                string sql = "SProc_Admin_UserIsExist";
                int retCount = 0;
                SqlParameter[] parms ={
                                      new SqlParameter("@email",up.Email)
                                 };
                SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);

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
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.IsExist", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新主部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="maindeptcode"></param>
        /// <returns></returns>
        public bool UpdateMainDepartment(string usercode, string maindeptcode)
        {
            try
            {
                string sql = "SProc_Admin_UpdateMainDepartment";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserCode",usercode),
                                          new SqlParameter("@MainDeptCode",maindeptcode)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.UpdateMainDepartment", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 根据条件取得用户信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataSet GetAllUserProfile(string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetAllUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Filter", filter)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetAllUserProfile", DBManager.GetCurrentUserAD());
                return null;
            }

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool DeleteUserProfile(string userIDs)
        {
            try
            {
                string sql = "SProc_Admin_DeleteUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserIDs", userIDs)
                                   };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.DeleteUserProfile", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        public DataSet GetUserByType(string deptCode, string selectType, string filter)
        {
            try
            {
                string sql = "SProc_Admin_GetUserByType";

                SqlParameter[] paras = { 
                                        new SqlParameter("@DeptCode", deptCode),
                                        new SqlParameter("@SelectType",selectType),
                                        new SqlParameter("@Filter",filter)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserByType", DBManager.GetCurrentUserAD());
                return null;
            }
        }


        /// <summary>
        /// 删除扩展属性
        /// </summary>
        /// <param name="Id"></param>
        public bool DeleteExtProp(string Ids)
        {
            try
            {
                string sql = "SProc_Admin_DeleteExtProperty";
                SqlParameter[] parms ={
                                          new SqlParameter("@Ids",Ids)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.DeleteExtProp", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 添加扩展属性
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="desc"></param>
        /// <param name="company"></param>
        public bool AddExtProp(string prop, string desc, string company)
        {
            try
            {
                string sql = "SProc_Admin_AddExtProp";
                SqlParameter[] parms = { 
                                       new SqlParameter("@UserExtProperty",prop),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@Company",company),
                                       new SqlParameter("@CreatedBy",DBManager.GetCurrentUserAD())
                                       };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.AddExtProp", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 更新扩展属性表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="prop"></param>
        /// <param name="des"></param>
        /// <param name="company"></param>
        public bool UpdateExtPropById(string Id, string prop, string des, string company)
        {
            try
            {
                string sql = "SProc_Admin_UpdateExtPropByID";
                SqlParameter[] parms = {
                    new SqlParameter("@ExtPropID",int.Parse(Id)),
                    new SqlParameter("@UserExtProperty",prop),
                    new SqlParameter("@Description",des),
                    new SqlParameter("@Company",company)
                };

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.UpdateExtPropById", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得所有扩展属性
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllExtProp()
        {
            try
            {
                string sql = "SProc_Admin_GetAllExtProp";

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetAllExtProp", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 取得人员信息的扩展属性
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataSet GetUserExtProperty(Guid userCode)
        {
            try
            {
                string sql = "SProc_Admin_GetExtProperty";

                SqlParameter[] paras = { 
                                        new SqlParameter("@UserCode",userCode)
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserExtProperty", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 添加扩展属性值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="value"></param>
        public bool AddExtValue(Guid Id, string value, Guid usercode)
        {
            try
            {
                string sql = "SProc_Admin_AddExtValue";
                SqlParameter[] parms ={
                                          new SqlParameter("@Id",Id),
                                          new SqlParameter("@txtValue",value),
                                          new SqlParameter("@userCode",usercode)
                                     };
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return true;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.AddExtValue", DBManager.GetCurrentUserAD());
                return false;
            }
        }

        /// <summary>
        /// 取得某个用户的所有所在部门
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetDepartmentByUserID(Guid UserID)
        {
            try
            {
                string sql = "SProc_GetDepartmentByUserID";
                SqlParameter[] parms ={
                                          new SqlParameter("@UserID",UserID)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetDepartmentByUserID", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        public DataSet GetUserProfileByADAccount(string ad)
        {
            try
            {
                string sql = "SProc_Admin_GetUserProfileByADAccount";
                SqlParameter[] parms ={
                                          new SqlParameter("@ADAccount",ad)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                return ds;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserProfileByADAccount", DBManager.GetCurrentUserAD());
                return null;
            }
        }

        /// <summary>
        /// 根据条件取得用户信息
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="orgID"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetUsersByFilter(string rows, string orgID, string param,bool isMain)
        {
            try
            {
                string sql = "P_K2_GetUsersByFilter";
                SqlParameter[] parms ={
                                          new SqlParameter("@Rows",rows),
                                          new SqlParameter("@OrgID",orgID),
                                          new SqlParameter("@Params",param),
                                          new SqlParameter("@IsMainDept",isMain)
                                     };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, parms);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                DBManager.RecoreErrorProfile(ex, "UserProfileDAL.GetUserProfileByADAccount", DBManager.GetCurrentUserAD());
                return null;
            }
        }
    }
}
