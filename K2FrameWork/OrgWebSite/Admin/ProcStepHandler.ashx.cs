using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrgWebSite.Admin
{
    /// <summary>
    /// Summary description for ProcStepHandler
    /// </summary>
    public class ProcStepHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}