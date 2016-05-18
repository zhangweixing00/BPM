using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K2Utility
{
    public class WorkflowControl : System.Web.UI.UserControl
    {
        #region task page
        /// <summary>
        /// task page
        /// </summary>
        public ProcessPage TaskPage
        {
            get
            {
                return Page is ProcessPage ? (ProcessPage)Page : null;
            }
        }
        #endregion
    }
}