using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IDAL
{
    public interface IProcessLogDAL
    {
        DataSet GetProcessLogList(string formId);

        bool InsertProcessLog(ProcessLogInfo info);
    }
}
