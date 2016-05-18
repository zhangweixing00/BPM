using System;
using System.Collections.Generic;
using Pkurg.PWorldBPM.Common.Info;

namespace Pkurg.PWorldBPM.Common.IServices
{
    public interface IOrgService
    {
        UserInfo GetUserInfoById(Guid id);

        DepartmentInfo GetDepartmentInfoById(Guid id);

        //根据部门ID获取会签部门列表
        List<DepartmentInfo> GetCounterSignDepsByDepId(Guid depId);

        //获取部门下某角色用户
        UserInfo GetUserByRole(string roleName,Guid  depId);


        UserInfo GetCurrentUser();

        PWorld.Entities.Employee GetCurrentPWordUser();

        UserInfo GetUserInfo(string loginId);
    }
}
