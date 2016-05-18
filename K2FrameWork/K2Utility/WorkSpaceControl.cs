using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace K2Utility
{
    public class WorkSpaceControl : System.Web.UI.UserControl
    {
        #region Properties
        #region 获取的列表
        private DataSet _ds;
        protected DataSet ds
        {
            get
            {
                if (ViewState["_ds"] != null)
                {
                    return ViewState["_ds"] as DataSet;
                }
                else
                {
                    _ds = new DataSet();
                    ViewState["_ds"] = _ds;
                    return ViewState["_ds"] as DataSet;
                }
            }
            set
            {
                ViewState["_ds"] = value;
            }
        }

        //protected T_Employee Employee
        //{
        //    get
        //    {
        //        if (ViewState["_emlpoyee"] != null)
        //        {
        //            return ViewState["_emlpoyee"] as T_Employee;
        //        }
        //        else
        //        {
        //            GetCurrentEmployee();
        //            return ViewState["_emlpoyee"] as T_Employee;
        //        }
        //    }
        //    set
        //    {
        //        ViewState["_emlpoyee"] = value;
        //    }
        //}
        #endregion

        protected string PageSize
        {
            get
            {
                string pagesize = ConfigurationManager.AppSettings["PageSize"];
                if (pagesize != null)
                    return pagesize;
                else
                    return "10";
            }
        }
        protected string CurrentUser
        {
            get { return Page.User.Identity.Name; }
        }
        private void GetCurrentEmployee()
        {
            //T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(CurrentUserEmail.ToLower());
            //if (employee == null)
            //{
            //    employee = new T_Employee();
            //}
            //ViewState["_emlpoyee"] = employee;
        }
        protected string CurrentUserAdaccoutWithK2Lable
        {
            get { return "K2:" + CurrentUser; }
        }
        protected string CurentUserAdaccountWithOutK2Labble
        {
            get { return CurrentUser; }
        }
        protected string GetAdAccoutByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return "";
            return ConfigurationManager.AppSettings["DomainName"] + "\\" + email.Split('@')[0];
        }
        protected string GetEmployeeNameOneByAd(string adArray)
        {
            //string returnValue = "";
            //if (string.IsNullOrEmpty(adArray))
            //    return "";
            //string[] ads = adArray.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //string Email = "";
            //foreach (string ad in ads)
            //{
            //    if (!string.IsNullOrEmpty(ads[0].Split('\\')[1]))
            //    {
            //        Email = ads[0].Split('\\')[1] + "@" + ConfigurationManager.AppSettings["DomainName"] + ".com";
            //        break;
            //    }
            //}

            //T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(Email.ToLower());
            //returnValue = employee == null ? "" : employee.EmployeeName;
            //return returnValue;
            return string.Empty;
        }
        protected string GetEmployeeNameByAd(string adArray)
        {
            //string returnValue = "";
            //if (string.IsNullOrEmpty(adArray))
            //    return "";
            //string[] ads = adArray.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string ad in ads)
            //{
            //    string Email = ad.Split('\\')[1] + "@" + ConfigurationManager.AppSettings["DomainName"] + ".com";
            //    T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(Email.ToLower());
            //    if (string.IsNullOrEmpty(returnValue))
            //    {
            //        returnValue = employee == null ? "" : employee.EmployeeName;
            //    }
            //    else
            //    {
            //        returnValue += (employee == null ? "" : "<br/>" + employee.EmployeeName);
            //    }
            //}

            //return returnValue;
            return string.Empty;
        }
        //protected T_Employee GetEmployeeByAd(string ad)
        //{
        //    if (string.IsNullOrEmpty(ad))
        //        return null;
        //    string Email = ad.Split('\\')[1] + "@" + ConfigurationManager.AppSettings["DomainName"] + ".com";
        //    T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(Email.ToLower());

        //    return employee;
        //}
        //protected string GetEmployeeNameByEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        return "";
        //    T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(email.ToLower());

        //    return employee == null ? "" : employee.EmployeeName;
        //}
        //protected T_Employee GetEmployeeByEmail(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        return null;
        //    T_Employee employee = Biz_Employee.biz_Employee.GetEmployee(email.ToLower());

        //    return employee;
        //}
        protected string Group
        {
            get
            {
                return "''";
            }
        }
        protected string ViewFlowUrl(string ProcInstID, string ProcessState, string FlowName)
        {
            //return "<a href='" + string.Format(ConfigurationManager.AppSettings["ViewFlowPage"], ProcInstID) + "' target='_blank'>查看</a>";
            if (FlowName.Equals("CustomWF", StringComparison.OrdinalIgnoreCase))
            {
                return "<a href='#' onclick=\"ymPrompt.win('" + string.Format("../Process/CDF/ReView.aspx?ProcInstID={0}", ProcInstID) + "', 830, 470, '流程状态', null, null, null, true);\">" + ProcessPage.GetChProcessStatus(ProcessState) + "</a>";
            }
            else
            {
                // return "<a href='#' onclick=\"ymPrompt.win('" + string.Format("http://test-k2:81/ViewFlow/ViewFlow.aspx?ViewTypeName=ProcessView&K2Server=test-k2:5252&HostServerName=test-k2&HostServerPort=5555&ProcessID={0}", ProcInstID) + "', 930, 670, '流程状态', null, null, null, true);\">" + ProcessPage.GetChProcessStatus(ProcessState) + "</a>";
                return "<a href='#' onclick=\"ymPrompt.win('" + string.Format("WorkSpace/ViewFlow.aspx?ProcInstID={0}", ProcInstID) + "', 830, 470, '流程状态', null, null, null, true);\">" + ProcessPage.GetChProcessStatus(ProcessState) + "</a>";
            }
        }
        #endregion
    }
}