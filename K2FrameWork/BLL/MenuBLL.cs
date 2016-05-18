using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using System.Data;

namespace BLL
{
    public class MenuBLL
    {
        //创建dal连接
        private static readonly IMenu dal = DALFactory.DataAccess.CreateMenuDAL();
        public DataSet GetMenuPermision(string user)
        {
            return dal.GetMenuPermision(user);
        }
        public DataSet GetMenuInfo(string MenuName, string ParentMenuGuid, string MenuType)
        {
            return dal.GetMenuInfo(MenuName, ParentMenuGuid, MenuType);
        }
        public bool UpdateMenuInfo(string MenuGuids)
        {
            return dal.DeleteMenuInfo(MenuGuids);
        }
        public bool CreateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType)
        {
            return dal.CreateMenuInfo(MenuGuid, ParentMenuGuid, MenuName, MenuURL, MenuType);
        }
        public bool UpdateMenuInfo(string MenuGuid, string ParentMenuGuid, string MenuName, string MenuURL, string MenuType)
        {
            return dal.UpdateMenuInfo(MenuGuid, ParentMenuGuid, MenuName, MenuURL, MenuType);
        }
        public DataSet GetMenuPermisionByRoleID(string RoleID)
        {
            return dal.GetMenuPermisionByRoleID(RoleID);
        }
        public bool UpdateMenuPermision(string MenuGuids, string RoleGuid)
        {
            return dal.UpdateMenuPermision(MenuGuids, RoleGuid);
        }
    }
}
