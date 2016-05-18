using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Logger 的摘要说明
/// </summary>
public class Logger
{
    public static log4net.ILog logger = log4net.LogManager.GetLogger("logger");
}