using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2.Controls.Exception
{
    public class SystemSupportException : ApplicationException
    {
        /// <summary>
        /// SystemSupportException的缺省构造函数
        /// </summary>
        /// <remarks>SystemSupportException的缺省构造函数.
        /// </remarks>
        public SystemSupportException()
        {
        }

        /// <summary>
        /// SystemSupportException的带错误消息参数的构造函数
        /// </summary>
        /// <param name="strMessage">错误消息串</param>
        /// <remarks>SystemSupportException的带错误消息参数的构造函数,该错误消息将在消息抛出异常时显示出来。
        /// <seealso cref="MCS.Library.Expression.ExpTreeExecutor"/>
        /// </remarks>
        public SystemSupportException(string strMessage)
            : base(strMessage)
        {
        }

        /// <summary>
        /// SystemSupportException的构造函数。
        /// </summary>
        /// <param name="strMessage">错误消息串</param>
        /// <param name="ex">导致该异常的异常</param>
        /// <remarks>该构造函数把导致该异常的异常记录了下来。
        /// </remarks>
        public SystemSupportException(string strMessage, System.Exception ex)
            : base(strMessage, ex)
        {
            
        }
    }
}
