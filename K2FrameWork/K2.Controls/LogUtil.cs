using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace K2.Controls
{
    public class LogUtil
    {
        private static readonly log4net.ILog appLog = (log4net.ILog)LogManager.GetLogger("app");
        //private static readonly log4net.ILog opLog = (log4net.ILog)LogManager.GetLogger("operationLog");

        static LogUtil()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static log4net.ILog Log
        {
            get
            {
                return appLog;
            }
        }
    }
}
