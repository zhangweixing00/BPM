using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pkurg.PWorldBPM.WorkFlowRule
{
    public static class BPMConnectionString
    {
        //LINQ的连接字符串
        //Pkurg.PWorldBPM.WorkFlowRule.BPMConnectionString.ConnectionString
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            }
        }

    }
}
