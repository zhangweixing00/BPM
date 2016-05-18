using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;

namespace BLL
{
    public class MyJoinedBLL
    {
        //创建dal连接
        private static readonly IMyJoined dal = DALFactory.DataAccess.CreateMyJoinedDAL();
        public DataSet GetMyJoined(string actionerName, string folio, string startTime, string endTime, string pagenum, string pagesize, string procFullName, string submitor, string paraEmpression)
        {
            return dal.GetMyJoined(actionerName, folio, startTime, endTime, pagenum, pagesize, procFullName, submitor, paraEmpression);
        }
    }
}
