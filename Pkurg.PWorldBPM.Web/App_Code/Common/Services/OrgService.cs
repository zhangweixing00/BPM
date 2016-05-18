using System;
using System.Collections.Generic;
using System.Web;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Common.Info;
using Pkurg.PWorldBPM.Common.IServices;

namespace Pkurg.PWorldBPM.Common.Services
{
    public class OrgService : IOrgService
    {

        public UserInfo GetUserInfoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public DepartmentInfo GetDepartmentInfoById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentInfo> GetCounterSignDepsByDepId(Guid depId)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserByRole(string roleName, Guid depId)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetCurrentUser()
        {
            string loginName = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(loginName))
            {
                //error
                ExceptionHander.GoToErrorPage();
            }
            BFEmployee employee = new BFEmployee();

            loginName = loginName.ToLower().Replace(@"founder\", "");
            EmployeeAdditional userInfo = employee.GetEmployeeAdditionalByLoginName(loginName);//"xupc"

            Employee em = employee.GetEmployeeByEmployeeCode(userInfo.EmployeeCode);//get user info


            return new UserInfo()
            {
                Id = em.EmployeeCode,
                LoginId = loginName,
                Name = em.EmployeeName,
                MainDeptId = em.DepartCode,
                DepIds = new List<string>() { em.DepartCode },
                PWordUser = em
            };
        }

        public PWorld.Entities.Employee GetCurrentPWordUser()
        {
            string loginName = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(loginName))
            {
                //error
            }
            BFEmployee employee = new BFEmployee();
            EmployeeAdditional userInfo = employee.GetEmployeeAdditionalByLoginName(loginName.ToLower().Replace(@"founder\", ""));

            Employee em = employee.GetEmployeeByEmployeeCode(userInfo.EmployeeCode);//get user info
            return em;
        }


        public UserInfo GetUserInfo(string loginName)
        {
            if (string.IsNullOrEmpty(loginName))
            {
                //error
                ExceptionHander.GoToErrorPage("域用户名为空", null);
            }
            BFEmployee employee = new BFEmployee();

            loginName = loginName.ToLower().Replace(@"k2:founder\", "");
            loginName = loginName.ToLower().Replace(@"founder\", "");

            EmployeeAdditional userInfo = employee.GetEmployeeAdditionalByLoginName(loginName);
            Employee em = employee.GetEmployeeByEmployeeCode(userInfo.EmployeeCode);//get user info
            if (em != null)
            {
                return new UserInfo()
                {
                    Id = em.EmployeeCode,
                    LoginId = loginName,
                    FounderLoginId = "founder\\" + loginName,
                    Name = em.EmployeeName,
                    MainDeptId = em.DepartCode,
                    DepIds = new List<string>() { em.DepartCode },
                    PWordUser = em
                };
            }
            else
            {
                return null;
            }
        }
    }
}
