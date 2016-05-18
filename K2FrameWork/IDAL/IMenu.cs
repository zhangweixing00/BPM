using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IDAL
{
    public interface IMenu
    {
        DataSet GetMenuPermision(string user);
        DataSet GetMenuInfo(string MenuName, string ParentMenuGuid, string MenuType);
        bool DeleteMenuInfo(string MenuGuids);
        bool CreateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType);
        bool UpdateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType);
        DataSet GetMenuPermisionByRoleID(string RoleID);
        bool UpdateMenuPermision(string MenuGuids, string RoleGuid);
    }
}
