using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IProcessTypeDAL
    {
        DataSet GetProcessType();
        DataSet GetProcessTypeByID(string ID);
        DataSet GetProcessNameByID(string  ID);
        DataSet GetPWorldCompanyData();
    }
}
