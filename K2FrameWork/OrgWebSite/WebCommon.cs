using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pkurg.PWorld.Business.Permission;
using BLL;
using Model;
using Pkurg.PWorld.Business.Manage;
using Pkurg.PWorld.Entities;

namespace OrgWebSite
{
    public class WebCommon
    {
        BFFunctionPermissions functionPermissions = new BFFunctionPermissions();

        public static IList<ProcessTypeInfo> GetDeptListByEmployeeCode(string loginName)
        {
            List<ProcessTypeInfo> ptiList = new List<ProcessTypeInfo>();

            BFEmployee bfemployee = new BFEmployee();
            EmployeeAdditional userInfo = bfemployee.GetEmployeeAdditionalByLoginName(loginName);
            
            ProcessTypeBLL bll = new ProcessTypeBLL();
            IList<ProcessTypeInfo> processTypeInfoList= bll.GetProcessType();
            foreach (var item in processTypeInfoList)
            {
                if (BFFunctionPermissions.IsDepartmentRightByEmployeeCode(userInfo.EmployeeCode, item.ID.Trim(), "公司流程筛选", "BPM", "无代码平台"))
                {
                    ptiList.Add(item);
                }
            }
            return ptiList;
        }

    }
}