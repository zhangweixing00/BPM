using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IRoleDAL
    {
        DataSet GetRoles(string processCode);

        DataSet GetRoleByRoleCode(string roleCode);

        bool UpdateRole(string roleCode, string roleName, string processCode, Guid orgID, string description);

        bool AddNewRole(string roleName, string processCode, Guid orgID, string description);

        DataSet GetRoleUserByCode(string roleCode);

        DataTable GetRoleInfoByRoleCode(string roleCode);

        bool DeleteUserFromRoleUser(string ids);

        bool DeleteRoles(string roleCodes);

        bool AddUserToRoleUser(string roleCode, string ad);

        DataSet GetRoleUser(Guid roleCode, Guid userCode);

        bool UpdateRoleUserByID(Guid Id, string userCode, string deptName, string deptCode, string dutyRegion, string mainRoleName, string mainRoleCode, string expand1, string expand2, string expand3, string expand4);
        
        DataSet GetRoleByMenuID(string MenuGUID);

        DataTable GetRolesByOrgParams(Guid orgID, string parm, string rows);
    }
}
