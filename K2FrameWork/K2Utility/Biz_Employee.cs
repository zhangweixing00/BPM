using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace K2Utility
{
    public sealed class Biz_Employee
    {
        /// <summary>
        /// 通过AD取用户
        /// </summary>
        /// <param name="adaccount"></param>
        /// <returns></returns>
        public static UserProfileInfo GetEmployee(string adaccount)
        {
            try
            {
                string sql = "SProc_Admin_GetAllUserProfile";

                SqlParameter[] paras = { 
                                        new SqlParameter("@Filter", "")
                                   };

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, sql, paras);

                List<UserProfileInfo> upList = new List<UserProfileInfo>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserProfileInfo info = new UserProfileInfo();
                        info.ID = dr["ID"].ToString();
                        info.CHName = dr["CHName"].ToString();
                        info.ENName = dr["ENName"].ToString();
                        info.ADAccount = dr["ADAccount"].ToString();
                        info.Email = dr["Email"].ToString();
                        info.EmailOrig = dr["EmailOrig"].ToString();
                        info.OfficePhone = dr["OfficePhone"].ToString();
                        info.CellPhone = dr["CellPhone"].ToString();
                        info.WorkPlace = dr["WorkPlace"].ToString();
                        info.HireDate = dr["HireDate"].ToString();
                        info.Birthdate = dr["Birthdate"].ToString();
                        try
                        {
                            if (dr["PositionGuid"] != null && !string.IsNullOrEmpty(dr["PositionGuid"].ToString()))
                                info.PositionGuid = new Guid(dr["PositionGuid"].ToString());
                            else
                                info.PositionGuid = Guid.Empty;
                        }
                        catch
                        {
                            info.PositionGuid = Guid.Empty;
                        }
                        info.PositionName = dr["PositionName"].ToString();
                        info.ManagerAccount = dr["ManagerAccount"].ToString();
                        info.EmployeeAccount = dr["EmployeeAccount"].ToString();
                        info.CostCenter = dr["CostCenter"].ToString();

                        int _state = -1;
                        int.TryParse(dr["State"].ToString(), out _state);
                        info.State = _state;

                        info.FAX = dr["FAX"].ToString();
                        info.BlackBerry = dr["BlackBerry"].ToString();
                        info.GraduateFrom = dr["GraduateFrom"].ToString();
                        info.OAC = dr["OAC"].ToString();
                        info.PoliticalAffiliation = dr["PoliticalAffiliation"].ToString();
                        info.Gender = dr["Gender"].ToString();
                        info.EducationalBackground = dr["EducationalBackground"].ToString();
                        info.WorkExperienceBefore = dr["WorkExperienceBefore"].ToString();
                        info.WorkExperienceNow = dr["WorkExperienceNow"].ToString();
                        info.PhotoUrl = dr["PhotoUrl"].ToString();
                        info.CreatedBy = dr["CreatedBy"].ToString();
                        info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());

                        int _orderNo = 0;
                        int.TryParse(dr["OrderNo"].ToString(), out _orderNo);
                        info.OrderNo = _orderNo;

                        upList.Add(info);
                    }
                }
                if (upList != null)
                {
                    return upList.Find(delegate(UserProfileInfo info)
                    {
                        if (info.ADAccount.Equals(adaccount, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                        else
                            return false;
                    });
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
