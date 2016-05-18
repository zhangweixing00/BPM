using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;
using System.Data;

namespace BLL
{
    public class DelegationBLL
    {
        //创建dal连接
        private static readonly IDelegation dal = DALFactory.DataAccess.CreateDelegationDAL();

        public bool CreateDelegation(DelegationInfo info)
        {
            return dal.CreateDelegation(info);
        }

        public DataSet GetDelegation(string ProcName, string FromUser,string ToUser, string StartDate, string EndDate, string State)
        {
            return dal.GetDelegation(ProcName, FromUser,ToUser, StartDate, EndDate, State);
        }
    }
}
