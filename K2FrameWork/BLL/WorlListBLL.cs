using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;
using System.Collections.Specialized;

namespace BLL
{
    public class WorlListBLL
    {
        //创建dal连接
        private static readonly IWorkList dal = DALFactory.DataAccess.CreateWorkListDAL();

        /// <summary>
        /// 通过部门ID取得该部门下所有用户信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataSet GetWorkList(string pagenum, string pagesize, string procFullName, string actionerName, string folio, string StartDate, string endDate, string procstate, string originator, string paraEmpression)
        {
            return dal.GetWorkList(pagenum, pagesize, procFullName, actionerName, folio, StartDate ,endDate,procstate,originator,paraEmpression);
        }
        public void Redirect(string sn, string targetUser)
        {
            dal.Redirect(sn, targetUser);
        }
        public void Delegate(string sn, string targetUser)
        {
            dal.Delegate(sn, targetUser);
        }
        public void Release(string sn)
        {
            dal.Release(sn);
        }

        public void Sleep(string sn, int second)
        {
            dal.Sleep(sn, second);
        }

        public void Approve(string sn, string action, NameValueCollection dataFields)
        {
            dal.Approve(sn, action, dataFields);
        }

        public DataSet GetMyWorklist(string user, string pagenum, string pagesize, string procName, string folio, string group, string Submitor, string StartTime, string EndTime, string FlowStatus, string Employee)
        {
            return dal.GetMyWorklist(user, pagenum, pagesize, procName, folio, group, Submitor, StartTime, EndTime, FlowStatus, Employee);
        }
    }
}
