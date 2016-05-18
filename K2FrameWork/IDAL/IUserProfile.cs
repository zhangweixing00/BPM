using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IDAL
{
    public interface IUserProfile
    {
        /// <summary>
        /// 通过部门ID取得该部门下所有用户信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        DataSet GetUserByDeptCode(string deptCode);

        DataSet GetUserProfileOutDept(string deptCode, string filter);

        bool AddDeptUser(string deptCode, string userCodes);

        DataSet GetUserProfile(string userCode);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        bool CreateUserProfile(UserProfileInfo up);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        bool UpdateUserProfile(UserProfileInfo up);

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        bool IsExist(UserProfileInfo up);

        /// <summary>
        /// 更新主部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="maindeptcode"></param>
        /// <returns></returns>
        bool UpdateMainDepartment(string usercode, string maindeptcode);

        /// <summary>
        /// 更具条件获取用户信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        DataSet GetAllUserProfile(string filter);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        bool DeleteUserProfile(string userIDs);

        DataSet GetUserByType(string deptCode, string selectType, string filter);

        bool DeleteExtProp(string Ids);

        bool AddExtProp(string prop, string desc, string company);

        bool UpdateExtPropById(string Id, string prop, string des, string company);

        DataSet GetAllExtProp();

        DataSet GetUserExtProperty(Guid userCode);

        bool AddExtValue(Guid Id, string value, Guid usercode);

        DataSet GetDepartmentByUserID(Guid UserID);

        DataSet GetUserProfileByADAccount(string ad);

        DataTable GetUsersByFilter(string rows, string orgID, string parms, bool isMain);
    }
}
