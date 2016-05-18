using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;

namespace IDAL
{
    public interface IWorkList
    {
        DataSet GetWorkList(string pagenum, string pagesize, string procFullName, string actionerName, string folio, string StartDate, string endDate, string procstate, string originator, string paraEmpression);
        void Redirect(string sn, string targetUser);
        void Delegate(string sn, string targetUser);
        void Release(string sn);
        void Sleep(string sn, int second);
        void Approve(string sn, string action, NameValueCollection dataFields);
        DataSet GetMyWorklist(string user, string pagenum, string pagesize, string procName, string folio, string group, string Submitor, string StartTime, string EndTime, string FlowStatus, string Employee);
    }
}
