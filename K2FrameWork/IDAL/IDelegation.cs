using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace IDAL
{
    public interface IDelegation
    {
        bool CreateDelegation(DelegationInfo info);
        DataSet GetDelegation(string ProcName, string FromUser,string ToUser, string StartDate, string EndDate, string State);
    }
}
