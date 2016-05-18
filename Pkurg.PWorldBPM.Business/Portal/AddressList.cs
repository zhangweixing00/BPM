using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Portal
{
    /// <summary>
    /// 通讯录
    /// </summary>
    public class AddressList
    {
        /// <summary>
        /// 获取一级公司列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanyList()
        {
            string sql = "SELECT * FROM T_Department WHERE IsDel=0 AND DeptLevel=1 order by OrderNo ";
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ESSPConnectionString"].ConnectionString;
            DataTable dt = dataProvider.ExecuteDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 根据部门ID获取子部门列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDeptList(string code)
        {
            int level = code.Split('-').Count() + 1;
            code = code + "-";
            string sql = string.Format("SELECT * FROM T_Department WHERE IsDel=0 AND DepartCode LIKE '{0}%' AND DeptLevel={1} order by OrderNo", code, level);
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ESSPConnectionString"].ConnectionString;
            DataTable dt = dataProvider.ExecuteDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 获取通讯录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="companyCode"></param>
        /// <param name="departCode"></param>
        /// <param name="employeeName"></param>
        /// <param name="email"></param>
        /// <param name="telephone"></param>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public DataTable GetAddressList(int pageIndex, int pageSize, out int count, string companyCode, string departCode, string employeeName,
            string email, string telephone, string mobilephone)
        {
            count = 0;
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ESSPConnectionString"].ConnectionString;

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@PageIndex",System.Data.SqlDbType.Int),
                new SqlParameter("@pageSize",System.Data.SqlDbType.Int),
                new SqlParameter("@Count",System.Data.SqlDbType.Int),
                new SqlParameter("@CompanyCode",System.Data.SqlDbType.NVarChar,1000),
                new SqlParameter("@DepartCode",System.Data.SqlDbType.NVarChar,1000),
                new SqlParameter("@EmployeeName",System.Data.SqlDbType.NVarChar,1000),
                new SqlParameter("@Email",System.Data.SqlDbType.NVarChar,1000),
                new SqlParameter("@Telephone",System.Data.SqlDbType.NVarChar,1000),
                new SqlParameter("@Mobilephone",System.Data.SqlDbType.NVarChar,1000)
                };
            parameters[0].Value = pageIndex;
            parameters[1].Value = pageSize;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[2].Value = count;
            parameters[3].Value = companyCode;
            parameters[4].Value = departCode;
            parameters[5].Value = employeeName;
            parameters[6].Value = email;
            parameters[7].Value = telephone;
            parameters[8].Value = mobilephone;

            DataTable dt = dataProvider.ExecutedProcedure("proc_GetAddressListTree", parameters);

            count = Convert.ToInt32(parameters[2].Value);

            return dt;
        }

    }
}
