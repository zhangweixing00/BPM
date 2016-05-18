using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IMyJoined
    {
        DataSet GetMyJoined(string actionerName, string folio, string startTime, string endTime, string pagenum, string pagesize, string procFullName, string submitor, string paraEmpression);
    }
}
