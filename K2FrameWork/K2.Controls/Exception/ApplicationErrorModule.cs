using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Configuration;
using System.Configuration;
using System.Web;
using System.Reflection;

[assembly: WebResource("K2FrameWork.K2.Controls.Exception.ApplicationErrorModuleTemplate_stopLogo.gif", "image/gif")]
namespace K2.Controls.Exception
{
    public class ApplicationErrorModule : IHttpModule
    {
        public static readonly object ExceptionItemKey = new object();

        void IHttpModule.Init(HttpApplication context)
        {
            context.Error += new EventHandler(context_Error);
        }

        void IHttpModule.Dispose()
        {
        }

        private void context_Error(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.CurrentHandler is Page ||
                HttpContext.Current.Request.FilePath.EndsWith("aspx", StringComparison.OrdinalIgnoreCase)))
                return;

            Page page = (Page)HttpContext.Current.CurrentHandler;

            if (page == null || page.IsCallback == false)
            {
                string errorPageUrl = string.Empty;

                HttpContext context = HttpContext.Current;

                string stackTrace = GetAllStackTrace(context.Error);
                System.Exception realEx = ExceptionHelper.GetRealException(context.Error);

                context.ClearError();
                if (realEx != null)
                {
                    //TryWriteLog(realEx, stackTrace);
                    LogUtil.Log.Error(realEx);

                    ResponseErrorMessage(realEx.Message, stackTrace);
                }
            }
        }


        private void TryWriteLog(System.Exception ex, string stackTrace)
        {
            try
            {
                LogUtil.Log.Error(ex);
                //Logger logger = LoggerFactory.Create("webApplicationError");
                //LogEntity logEntity = new LogEntity(ex);
                //HttpContext context = HttpContext.Current;

                //logEntity.LogEventType = ApplicationErrorLogSection.GetSection().GetExceptionLogEventType(ex);

                //string[] paths = context.Request.ApplicationPath.Split('/');
                //logEntity.StackTrace = stackTrace;
                //logEntity.Source = paths[paths.Length - 1];
                //logEntity.Title = string.Format("{0}应用页面错误", context.Request.ApplicationPath);
                //logEntity.ExtendedProperties.Add("RequestUrl", context.Request.Url.AbsoluteUri);
                //try
                //{
                //    if (DeluxeIdentity.CurrentUser != null)
                //    {
                //        logEntity.ExtendedProperties.Add("UserLogOnName", DeluxeIdentity.CurrentUser.LogOnName);
                //        logEntity.ExtendedProperties.Add("UserFullPath", DeluxeIdentity.CurrentUser.FullPath);
                //        logEntity.ExtendedProperties.Add("UserDisplayName", DeluxeIdentity.CurrentUser.DisplayName);
                //    }
                //}
                //catch
                //{
                //}
                //logger.Write(logEntity);
            }
            catch
            {
            }
        }

        private string GetAllStackTrace(System.Exception ex)
        {
            StringBuilder strB = new StringBuilder();

            for (System.Exception innerEx = ex; innerEx != null; innerEx = innerEx.InnerException)
            {
                if (strB.Length > 0)
                    strB.Append("\n");

                strB.AppendFormat("-------------{0}：{1}-----------\n", innerEx.GetType().Name, innerEx.Message);
                strB.Append(innerEx.StackTrace);
            }

            return strB.ToString();
        }

        private void ResponseErrorMessage(string strErrorMsg, string strStackTrace)
        {
            string errorFormat = ResourceHelper.LoadStringFromResource(Assembly.GetExecutingAssembly(),
                             "Sohu.OA.Controls.Exception.ApplicationErrorModuleTemplate.htm");

            Page page = HttpContext.Current.CurrentHandler is Page ? (Page)HttpContext.Current.CurrentHandler : new Page();
            string imageUrl = page.ClientScript.GetWebResourceUrl(this.GetType(), "Sohu.OA.Controls.Exception.ApplicationErrorModuleTemplate_stopLogo.gif");

            string goBackBtnDisplay = HttpContext.Current.Request.UrlReferrer != null ? "inline" : "none";
            string closePromptValue = HttpContext.Current.Request.UrlReferrer != null ? "true" : "false";

            string[] strArray = errorFormat.Split('$');
            for (int i = 0; i < strArray.Length; i++)
            {
                switch (strArray[i])
                {
                    case "imageUrl":
                        strArray[i] = imageUrl;
                        break;
                    case "goBackBtnDisplay":
                        strArray[i] = goBackBtnDisplay;
                        break;
                    case "closePromptValue":
                        strArray[i] = closePromptValue;
                        break;
                    case "errorMessage":
                        strArray[i] = HttpUtility.HtmlEncode(strErrorMsg);
                        break;
                    case "errorStackTrace":
                        strArray[i] = AllowResponseExceptionStackTrace() ? HttpUtility.HtmlEncode(strStackTrace).Replace("\r\n", "<br/>") : string.Empty;
                        break;
                }
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(string.Join("", strArray));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 是否允许向客户端输出异常详细信息
        /// </summary>
        public static bool AllowResponseExceptionStackTrace()
        {
            return GetWebApplicationCompilationDebug();
        }

        internal static bool GetWebApplicationCompilationDebug()
        {
            bool debug = false;
            CompilationSection compilation = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");

            if (compilation != null)
            {
                debug = compilation.Debug;
            }

            return debug;
        }
    }
}
