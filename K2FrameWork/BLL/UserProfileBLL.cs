using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IDAL;
using Model;
using System.DirectoryServices;
using System.Configuration;

namespace BLL
{
    public class UserProfileBLL
    {
        //创建dal连接
        private static readonly IUserProfile dal = DALFactory.DataAccess.CreateUserProfileDAL();


        /// <summary>
        /// 通过部门ID取得该部门下所有用户信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataSet GetUserByDeptCode(string deptCode)
        {
            return dal.GetUserByDeptCode(deptCode);
        }

        public IList<UserProfileInfo> GetUserProfileOutDept(string deptCode, string filter)
        {
            DataSet ds = dal.GetUserProfileOutDept(deptCode, filter);
            List<UserProfileInfo> upList = new List<UserProfileInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfileInfo info = new UserProfileInfo();
                    info.ID = dr["ID"].ToString();
                    info.CHName = dr["CHName"].ToString();
                    info.ENName = dr["ENName"].ToString();
                    info.ADAccount = dr["ADAccount"].ToString();
                    info.Email = dr["Email"].ToString();
                    info.EmailOrig = dr["EmailOrig"].ToString();
                    info.OfficePhone = dr["OfficePhone"].ToString();
                    info.CellPhone = dr["CellPhone"].ToString();
                    info.WorkPlace = dr["WorkPlace"].ToString();
                    info.HireDate = dr["HireDate"].ToString();
                    info.Birthdate = dr["Birthdate"].ToString();
                    try
                    {
                        if (dr["PositionGuid"] != null && !string.IsNullOrEmpty(dr["PositionGuid"].ToString()))
                            info.PositionGuid = new Guid(dr["PositionGuid"].ToString());
                        else
                            info.PositionGuid = Guid.Empty;
                    }
                    catch
                    {
                        info.PositionGuid = Guid.Empty;
                    }
                    info.PositionName = dr["PositionName"].ToString();
                    info.ManagerAccount = dr["ManagerAccount"].ToString();
                    info.EmployeeAccount = dr["EmployeeAccount"].ToString();
                    info.CostCenter = dr["CostCenter"].ToString();

                    int _state = -1;
                    int.TryParse(dr["State"].ToString(), out _state);
                    info.State = _state;

                    info.FAX = dr["FAX"].ToString();
                    info.BlackBerry = dr["BlackBerry"].ToString();
                    info.GraduateFrom = dr["GraduateFrom"].ToString();
                    info.OAC = dr["OAC"].ToString();
                    info.PoliticalAffiliation = dr["PoliticalAffiliation"].ToString();
                    info.Gender = dr["Gender"].ToString();
                    info.EducationalBackground = dr["EducationalBackground"].ToString();
                    info.WorkExperienceBefore = dr["WorkExperienceBefore"].ToString();
                    info.WorkExperienceNow = dr["WorkExperienceNow"].ToString();
                    info.PhotoUrl = dr["PhotoUrl"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());

                    int _orderNo = 0;
                    int.TryParse(dr["OrderNo"].ToString(), out _orderNo);
                    info.OrderNo = _orderNo;

                    upList.Add(info);
                }
            }
            return upList;
        }

        /// <summary>
        /// 根据条件取得用户信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<UserProfileInfo> GetAllUserProfile(string filter)
        {
            DataSet ds = dal.GetAllUserProfile(filter);
            List<UserProfileInfo> upList = new List<UserProfileInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfileInfo info = new UserProfileInfo();
                    info.ID = dr["ID"].ToString();
                    info.CHName = dr["CHName"].ToString();
                    info.ENName = dr["ENName"].ToString();
                    info.ADAccount = dr["ADAccount"].ToString();
                    info.Email = dr["Email"].ToString();
                    info.EmailOrig = dr["EmailOrig"].ToString();
                    info.OfficePhone = dr["OfficePhone"].ToString();
                    info.CellPhone = dr["CellPhone"].ToString();
                    info.WorkPlace = dr["WorkPlace"].ToString();
                    info.HireDate = dr["HireDate"].ToString();
                    info.Birthdate = dr["Birthdate"].ToString();
                    try
                    {
                        if (dr["PositionGuid"] != null && !string.IsNullOrEmpty(dr["PositionGuid"].ToString()))
                            info.PositionGuid = new Guid(dr["PositionGuid"].ToString());
                        else
                            info.PositionGuid = Guid.Empty;
                    }
                    catch
                    {
                        info.PositionGuid = Guid.Empty;
                    }
                    info.PositionName = dr["PositionName"].ToString();
                    info.ManagerAccount = dr["ManagerAccount"].ToString();
                    info.EmployeeAccount = dr["EmployeeAccount"].ToString();
                    info.CostCenter = dr["CostCenter"].ToString();

                    int _state = -1;
                    int.TryParse(dr["State"].ToString(), out _state);
                    info.State = _state;

                    info.FAX = dr["FAX"].ToString();
                    info.BlackBerry = dr["BlackBerry"].ToString();
                    info.GraduateFrom = dr["GraduateFrom"].ToString();
                    info.OAC = dr["OAC"].ToString();
                    info.PoliticalAffiliation = dr["PoliticalAffiliation"].ToString();
                    info.Gender = dr["Gender"].ToString();
                    info.EducationalBackground = dr["EducationalBackground"].ToString();
                    info.WorkExperienceBefore = dr["WorkExperienceBefore"].ToString();
                    info.WorkExperienceNow = dr["WorkExperienceNow"].ToString();
                    info.PhotoUrl = dr["PhotoUrl"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());

                    int _orderNo = 0;
                    int.TryParse(dr["OrderNo"].ToString(), out _orderNo);
                    info.OrderNo = _orderNo;

                    upList.Add(info);
                }
            }
            return upList;
        }

        /// <summary>
        /// 取得某个用户信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserProfileInfo GetUserProfile(string userCode)
        {
            DataSet ds = dal.GetUserProfile(userCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfileInfo info = new UserProfileInfo();
                    info.ID = dr["ID"].ToString();
                    info.CHName = dr["CHName"].ToString();
                    info.ENName = dr["ENName"].ToString();
                    info.ADAccount = dr["ADAccount"].ToString();
                    info.Email = dr["Email"].ToString();
                    info.EmailOrig = dr["EmailOrig"].ToString();
                    info.OfficePhone = dr["OfficePhone"].ToString();
                    info.CellPhone = dr["CellPhone"].ToString();
                    info.WorkPlace = dr["WorkPlace"].ToString();
                    info.HireDate = dr["HireDate"].ToString();
                    info.Birthdate = dr["Birthdate"].ToString();
                    try
                    {
                        if (dr["PositionGuid"] != null && !string.IsNullOrEmpty(dr["PositionGuid"].ToString()))
                            info.PositionGuid = new Guid(dr["PositionGuid"].ToString());
                        else
                            info.PositionGuid = Guid.Empty;
                    }
                    catch
                    {
                        info.PositionGuid = Guid.Empty;
                    }
                    info.PositionName = dr["PositionName"].ToString();
                    info.ManagerAccount = dr["ManagerAccount"].ToString();
                    info.EmployeeAccount = dr["EmployeeAccount"].ToString();
                    info.CostCenter = dr["CostCenter"].ToString();

                    int _state = -1;
                    int.TryParse(dr["State"].ToString(), out _state);
                    info.State = _state;

                    info.FAX = dr["FAX"].ToString();
                    info.BlackBerry = dr["BlackBerry"].ToString();
                    info.GraduateFrom = dr["GraduateFrom"].ToString();
                    info.OAC = dr["OAC"].ToString();
                    info.PoliticalAffiliation = dr["PoliticalAffiliation"].ToString();
                    info.Gender = dr["Gender"].ToString();
                    info.EducationalBackground = dr["EducationalBackground"].ToString();
                    info.WorkExperienceBefore = dr["WorkExperienceBefore"].ToString();
                    info.WorkExperienceNow = dr["WorkExperienceNow"].ToString();
                    info.PhotoUrl = dr["PhotoUrl"].ToString();
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());

                    int _orderNo = 0;
                    int.TryParse(dr["OrderNo"].ToString(), out _orderNo);
                    info.OrderNo = _orderNo;

                    return info;
                }
            }
            return null;
        }
         

        public bool AddDeptUser(string deptCode, string userCodes)
        {
            return dal.AddDeptUser(deptCode, userCodes);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool CreateUserProfile(UserProfileInfo up)
        {
            return dal.CreateUserProfile(up);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool UpdateUserProfile(UserProfileInfo up)
        {
            return dal.UpdateUserProfile(up);
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="up"></param>
        /// <returns></returns>
        public bool IsExist(UserProfileInfo up)
        {
            return dal.IsExist(up);
        }

        /// <summary>
        /// 更新主部门
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="maindeptcode"></param>
        /// <returns></returns>
        public bool UpdateMainDepartment(string usercode, string maindeptcode)
        {
            return dal.UpdateMainDepartment(usercode, maindeptcode);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIDs"></param>
        /// <returns></returns>
        public bool DeleteUserProfile(string userIDs)
        {
            return dal.DeleteUserProfile(userIDs);
        }

        /// <summary>
        /// 从导入AD导入
        /// </summary>
        /// <param name="adPath"></param>
        /// <param name="adUser"></param>
        /// <param name="adPass"></param>
        /// <param name="adUserInfos"></param>
        private void ImportData(string adPath, string adUser, string adPass, ref List<Model.ADUserInfo> adUserInfos)
        {
            DirectoryEntry entryPC = new DirectoryEntry(adPath, adUser, adPass);
            string account = "";
            object email = null;
            object displayName = null;
            foreach (DirectoryEntry child in entryPC.Children)
            {
                if (child.SchemaClassName.ToUpper() == "USER")
                {
                    ADUserInfo adUserInfo = new ADUserInfo();
                    account = ConfigurationManager.AppSettings["Domain"] + @"\" + child.Properties["sAMAccountName"].Value.ToString();
                    adUserInfo.Account = account;
                    email = child.Properties["mail"].Value;
                    adUserInfo.Email = (email == null) ? "" : email.ToString();
                    displayName = child.Properties["displayName"].Value;
                    adUserInfo.DisplayName = (displayName == null) ? account : displayName.ToString();
                    adUserInfos.Add(adUserInfo);
                }

                ImportData(child.Path, adUser, adPass, ref adUserInfos);
            }
        }

        public DataSet GetUserByType(string deptCode, string selectType, string filter)
        {
            return dal.GetUserByType(deptCode, selectType, filter);
        }


        /// <summary>
        /// 删除扩展属性
        /// </summary>
        /// <param name="Id"></param>
        public bool DeleteExtProp(string Ids)
        {
            return dal.DeleteExtProp(Ids);
        }

        /// <summary>
        /// 添加扩展属性
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="desc"></param>
        /// <param name="company"></param>
        public bool AddExtProp(string prop, string desc, string company)
        {
            return dal.AddExtProp(prop, desc, company);
        }

        /// <summary>
        /// 更新扩展属性表
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="prop"></param>
        /// <param name="des"></param>
        /// <param name="company"></param>
        public bool UpdateExtPropById(string Id, string prop, string des, string company)
        {
            return dal.UpdateExtPropById(Id, prop, des, company);
        }

        /// <summary>
        /// 取得所有扩展属性
        /// </summary>
        /// <returns></returns>
        public IList<UserProfileExtPropertyInfo> GetAllExtProp()
        {
            DataSet ds = dal.GetAllExtProp();
            List<UserProfileExtPropertyInfo> upepList = new List<UserProfileExtPropertyInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfileExtPropertyInfo info = new UserProfileExtPropertyInfo();
                    info.UserExtPropID = new Guid(dr["UserExtPropID"].ToString());
                    info.UserExtProperty = dr["UserExtProperty"].ToString();
                    info.Description = dr["Description"].ToString();
                    info.Company = dr["Company"].ToString();
                    info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());
                    info.CreatedBy = dr["CreatedBy"].ToString();
                    upepList.Add(info);
                }
            }
            return upepList;
        }

        /// <summary>
        /// 取得人员信息的扩展属性
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IList<UserProfileExtPropertyInfo> GetUserExtProperty(string userCode)
        {
            Guid UserCode = Guid.Empty;
            try
            {
                UserCode = new Guid(userCode);
            }
            catch
            {
                return null;
            }
            DataSet ds = dal.GetUserExtProperty(UserCode);
            List<UserProfileExtPropertyInfo> upepList = new List<UserProfileExtPropertyInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserProfileExtPropertyInfo info = new UserProfileExtPropertyInfo();
                    info.UserExtPropID = new Guid(dr["UserExtPropID"].ToString());
                    info.UserExtProperty = dr["UserExtProperty"].ToString();
                    info.Description = dr["Description"].ToString();
                    info.Company = dr["Company"].ToString();
                    info.UserCode = new Guid(dr["UserCode"].ToString());
                    info.Value = dr["Value"].ToString();
                    upepList.Add(info);
                }
            }
            return upepList;
        }

        /// <summary>
        /// 添加扩展属性值
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="value"></param>
        public bool AddExtValue(string Id, string value, string usercode)
        {
            Guid ID = Guid.Empty;
            Guid UserCode = Guid.Empty;
            try
            {
                ID = new Guid(Id);
                UserCode = new Guid(usercode);
            }
            catch
            {
                return false;
            }

            return dal.AddExtValue(ID, value, UserCode);
        }

        /// <summary>
        /// 取得某个用户的所有所在部门
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserDepartmentInfo> GetDepartmentByUserID(string userID)
        {
            Guid UserID = Guid.Empty;
            try
            {
                UserID = new Guid(userID);
            }
            catch
            {
                return null;
            }
            DataSet ds = dal.GetDepartmentByUserID(UserID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                List<UserDepartmentInfo> udiList = new List<UserDepartmentInfo>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UserDepartmentInfo udi = new UserDepartmentInfo();
                    udi.DeptCode = new Guid(dr["DeptCode"].ToString());
                    udi.OrgID = new Guid(dr["OrgID"].ToString());
                    udi.OrgName = dr["OrgName"].ToString();
                    udi.DeptName = dr["Department"].ToString();
                    udi.IsMain = Convert.ToBoolean(dr["IsMainDept"]);
                    udiList.Add(udi);
                }
                return udiList;
            }
            return null;
        }

        public UserProfileInfo GetUserProfileByADAccount(string ad)
        {
            DataSet ds = dal.GetUserProfileByADAccount(ad);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                UserProfileInfo info = new UserProfileInfo();
                info.ID = dr["ID"].ToString();
                info.CHName = dr["CHName"].ToString();
                info.ENName = dr["ENName"].ToString();
                info.ADAccount = dr["ADAccount"].ToString();
                info.Email = dr["Email"].ToString();
                info.EmailOrig = dr["EmailOrig"].ToString();
                info.OfficePhone = dr["OfficePhone"].ToString();
                info.CellPhone = dr["CellPhone"].ToString();
                info.WorkPlace = dr["WorkPlace"].ToString();
                info.HireDate = dr["HireDate"].ToString();
                info.Birthdate = dr["Birthdate"].ToString();
                try
                {
                    if (dr["PositionGuid"] != null && !string.IsNullOrEmpty(dr["PositionGuid"].ToString()))
                        info.PositionGuid = new Guid(dr["PositionGuid"].ToString());
                    else
                        info.PositionGuid = Guid.Empty;
                }
                catch
                {
                    info.PositionGuid = Guid.Empty;
                }
                info.PositionName = dr["PositionName"].ToString();
                info.ManagerAccount = dr["ManagerAccount"].ToString();
                info.EmployeeAccount = dr["EmployeeAccount"].ToString();
                info.CostCenter = dr["CostCenter"].ToString();

                int _state = -1;
                int.TryParse(dr["State"].ToString(), out _state);
                info.State = _state;

                info.FAX = dr["FAX"].ToString();
                info.BlackBerry = dr["BlackBerry"].ToString();
                info.GraduateFrom = dr["GraduateFrom"].ToString();
                info.OAC = dr["OAC"].ToString();
                info.PoliticalAffiliation = dr["PoliticalAffiliation"].ToString();
                info.Gender = dr["Gender"].ToString();
                info.EducationalBackground = dr["EducationalBackground"].ToString();
                info.WorkExperienceBefore = dr["WorkExperienceBefore"].ToString();
                info.WorkExperienceNow = dr["WorkExperienceNow"].ToString();
                info.PhotoUrl = dr["PhotoUrl"].ToString();
                info.CreatedBy = dr["CreatedBy"].ToString();
                info.CreatedOn = DateTime.Parse(dr["CreatedOn"].ToString());

                int _orderNo = 0;
                int.TryParse(dr["OrderNo"].ToString(), out _orderNo);
                info.OrderNo = _orderNo;
                return info;
            }
            return null;
        }


        /// <summary>
        /// 根据条件取得用户信息
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="orgID"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable GetUsersByFilter(string rows, string orgID, string parms,bool isMain)
        {
            return dal.GetUsersByFilter(rows, orgID, parms, isMain);
        }
    }
}
