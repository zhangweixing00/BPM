using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Pkurg.PWorldTemp.WorkflowCommon
{
    /// <summary>
    /// 程序信息的信息位置枚举
    /// </summary>
    public enum EnumLogPosition
    {
        /// <summary>
        /// IN
        /// </summary>
        [Description("IN")]
        IN,
        /// <summary>
        /// OUT
        /// </summary>
        [Description("OUT")]
        OUT
    }
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum EnumLogLevel
    {
        /// <summary>
        /// 关闭,不记录日志
        /// </summary>
        [Description("关闭")]
        Off,
        /// <summary>
        /// 致命错误
        /// </summary>
        [Description("致命错误")]
        Fatal,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warn,
        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Info,
        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug,
        /// <summary>
        /// 所有信息
        /// </summary>
        [Description("所有信息")]
        All
    }
    /// <summary>
    /// 名称：Logger
    /// 描述：记录系统的运行日志
    /// 功能：
    ///     记录系统的运行日志
    /// </summary>
    /// <history>
    /// Create by zhaohwi 2010-10-20 ver 0.1
    /// </history>
    public static class Logger
    {
        static Logger() { }

        /// <summary>
        /// 取得日志所在的方法和位置
        /// </summary>
        /// <returns>位置的字符串</returns>
        public static string LogPosition()
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(3);

            return string.Format("[method: {0}]", frame.GetMethod().Name);
        }
        /// <summary>
        /// 记录系统运行日志
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志信息</param>
        public static void Write(Type type, EnumLogLevel level, string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(type);
            message = string.Format("{0}, Message:{1}", LogPosition(), message); 

            switch (level)
            {
                case EnumLogLevel.Fatal:
                    if (log.IsFatalEnabled) log.Fatal(message); 
                    break;
                case EnumLogLevel.Error:
                    if (log.IsErrorEnabled) log.Error(message);
                    break;
                case EnumLogLevel.Warn:
                    if (log.IsWarnEnabled) log.Warn(message);
                    break;
                case EnumLogLevel.Debug:
                    if (log.IsDebugEnabled) log.Debug(message);
                    break;
                case EnumLogLevel.Info:
                    if (log.IsInfoEnabled) log.Info(message);
                    break;
            }

            log = null;
        }
        /// <summary>
        /// 记录系统运行日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        public static void Write(Type type, EnumLogLevel level, Exception exception)
        {
            Write(type, level, exception.Message);
        }
    }
    /// <summary>
    /// 名称：Trace
    /// 描述：记录系统的运行日志
    /// 功能：
    ///     记录系统的运行日志
    /// </summary>
    /// <history>
    /// Create by zhaohwi 2010-10-20 ver 0.1
    /// </history>
    public static class LogTrace
    {
        /// <summary>
        /// 记录系统运行日志
        /// </summary>
        public static void IN(Type type)
        {
            Logger.Write(type, EnumLogLevel.Info, EnumLogPosition.IN.ToString());
        }
        /// <summary>
        /// 记录系统运行日志
        /// </summary>
        public static void OUT(Type type)
        {
            Logger.Write(type, EnumLogLevel.Info, EnumLogPosition.OUT.ToString());
        }
    }
}
