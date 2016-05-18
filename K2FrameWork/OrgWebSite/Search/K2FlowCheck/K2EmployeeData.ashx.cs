using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using BLL;
using Model;
using System.Configuration;

namespace OrgWebSite.Search.K2FlowCheck
{
    /// <summary>
    /// Summary description for K2EmployeeData
    /// </summary>
    public class K2EmployeeData : IHttpHandler
    {
        private int count = Convert.ToInt32(ConfigurationManager.AppSettings["K2EmployeeCheckCountry"]);
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            StringBuilder strxml = new StringBuilder();

            strxml.Append(DataInfo(context));

            context.Response.ContentType = "text/plain";
            context.Response.Write(strxml);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns>>xml拼接字符串</returns>
        public string DataInfo(HttpContext context)
        {
            int i = 0;
            string filter = string.Empty;
            string Param = string.Empty;
            string CheckStyle = "false";
            if (!string.IsNullOrEmpty(context.Request["filter"]))
            {
                filter = context.Request["filter"].ToString();
            }
            if (!string.IsNullOrEmpty(context.Request["Param"]))
            {
                Param = context.Request["Param"].ToString();
            }

            if (!string.IsNullOrEmpty(context.Request["CheckStyle"]))
            {
                CheckStyle = context.Request["CheckStyle"].ToString();
                if (Convert.ToBoolean(CheckStyle))
                {
                    count = Convert.ToInt32(ConfigurationManager.AppSettings["K2EmployeeCheckCountryOne"]);
                }
            }

            StringBuilder xml = new StringBuilder();
            UserProfileBLL bll = new UserProfileBLL();
            IList<UserProfileInfo> list = bll.GetAllUserProfile(filter);
            string deptname = "";
            string deptcode = Guid.Empty.ToString();
            xml.Append("<tr bgcolor='#fef5c7'><th class='th2' style='width:130px;'>员工姓名</th><th class='th1' style='width:130px;'>员工编号 </th><th class='th1' style='width:218px;'>员工邮箱</th> <th class='th1' style='border-right:0px; width:219px;'>员工部门</th> <th class='th1' style='display:none;'>大部门编号</th> <th class='th1' style='display:none;'>中部门编号</th></tr>");
            if (list != null && list.Count > 0)
            {
                foreach (UserProfileInfo employeeInfo in list)
                {
                    xml.Append("<tr>");
                    xml.Append("<td class='mentd1' style='width:130px;'>" + (string.IsNullOrEmpty(employeeInfo.CHName) ? "" : employeeInfo.CHName) + "</td>");
                    xml.Append("<td class='mentd1' style='width:130px;'>" + (string.IsNullOrEmpty(employeeInfo.ADAccount) ? "" : employeeInfo.ADAccount) + "</td>");
                    xml.Append("<td class='mentd1'>" + (string.IsNullOrEmpty(employeeInfo.Email) ? "" : employeeInfo.Email) + "</td>");
                    deptname = "测试部门";
                    List<UserDepartmentInfo> deptList = bll.GetDepartmentByUserID(employeeInfo.ID.ToString());
                    if (deptList != null)
                    {
                        UserDepartmentInfo info = deptList.Find(delegate(UserDepartmentInfo tmp)
                        {
                            if (tmp.IsMain)
                                return true;
                            else
                                return false;
                        });
                        if (info == null)
                        {
                            deptname = "未分配部门";
                        }
                        else
                        {
                            deptname = info.DeptName;
                            deptcode = info.DeptCode.ToString();
                        }
                    }
                    else
                    {
                        deptname = "未分配部门";
                    }
                    xml.Append("<td class='mentd1' style='border-right:0px;'>" + deptname + "</td>");
                    xml.Append("<td class='mentd1' style='display:none;'>" + deptcode + "</td>");
                    xml.Append("<td class='mentd1' style='display:none;'>" + (string.IsNullOrEmpty(employeeInfo.WorkPlace) ? "" : employeeInfo.WorkPlace) + "</td>");
                    xml.Append("<td class='mentd1' style='display:none;'>" + (string.IsNullOrEmpty(employeeInfo.OfficePhone) ? "" : employeeInfo.OfficePhone) + "</td>");
                    xml.Append("<td class='mentd1' style='display:none;'>" + employeeInfo.ID + "</td>");

                    xml.Append("</tr>");
                    if (++i >= count)
                    {
                        break;
                    }
                }
            }
            return xml.ToString();

        }
    }
}