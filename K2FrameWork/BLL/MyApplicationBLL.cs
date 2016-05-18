using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;

namespace BLL
{
    public class MyApplicationBLL
    {

        //创建dal连接
        private static readonly IMyApplication dal = DALFactory.DataAccess.CreateMyApplicationDAL();
        public DataSet GetMyApplication(string actionerName, string folio, string startTime, string endTime, string pagenum, string pagesize, string procFullName, string paraEmpression)
        {
            return dal.GetMyApplication(actionerName, folio, startTime, endTime, pagenum, pagesize, procFullName, paraEmpression);
        }
    }
}
