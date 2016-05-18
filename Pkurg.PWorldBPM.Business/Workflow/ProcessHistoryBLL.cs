using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;

namespace Pkurg.PWorldBPM.Business.Workflow
{
    public class ProcessHistoryBLL
    {
        /// <summary>
        /// 获取流程流转历史记录
        /// </summary>
        /// <param name="domainID"></param>
        /// <returns></returns>
        public DataTable GetProcessHistoryList(string caseID)
        {
            string sql = "SELECT * FROM V_ProcessHistory WHERE InstanceID='" + caseID + "' order by FinishedTime ";
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            DataTable dt = dataProvider.ExecuteDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 通过域名获取对应人的信息
        /// </summary>
        /// <param name="domainID"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string domainID)
        {
            string sql = "select employee.EmployeeName,employee.DepartName from [dbo].[T_EmployeeAdditional] employeeAdditional left join [dbo].[T_Employee] employee on employeeAdditional.EmployeeCode=employee.EmployeeCode WHERE employeeAdditional.LoginName='" + domainID + "' ";
            DataProvider dataProvider = new DataProvider();
            dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ESSPConnectionString"].ConnectionString;
            DataTable dt = dataProvider.ExecuteDataTable(sql, CommandType.Text);
            return dt;
        }
    }
}
