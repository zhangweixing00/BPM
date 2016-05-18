using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Business.Sys;
using Pkurg.PWorldBPM.Business.BIZ;

namespace Pkurg.PWorldBPM.Business.Context
{
    public class DBContext
    {
        public static SysDBDataContext GetSysContext()
        {
            return new SysDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
        }

        public static BizFormDBDataContext GetBizContext()
        {
            return new BizFormDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
        }

    }
}