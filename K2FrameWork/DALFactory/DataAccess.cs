using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IDAL;
using System.Reflection;

namespace DALFactory
{
    /// <summary>
    /// 产生数据访问层类的类工厂，即利用反射创建实例
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly String path = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSO"]) ? ConfigurationManager.AppSettings["WebSODAL"] : ConfigurationManager.AppSettings["WebDAL"];

        DataAccess() { }

        /// <summary>
        /// 创建Message数据访问层类
        /// </summary>
        /// <returns></returns>
        public static IDepartment CreateDepartmentDAL()
        {
            String className = path + ".DepartmentDAL";
            return (IDepartment)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建UserProfile数据访问层
        /// </summary>
        /// <returns></returns>
        public static IUserProfile CreateUserProfileDAL()
        {
            String className = path + ".UserProfileDAL";
            return (IUserProfile)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建组织数据访问层
        /// </summary>
        /// <returns></returns>
        public static IOrganizationDAL CreateOrganizationDAL()
        {
            String className = path + ".OrganizationDAL";
            return (IOrganizationDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建Position数据访问层
        /// </summary>
        /// <returns></returns>
        public static IPositionDAL CreatePositionDAL()
        {
            String className = path + ".PositionDAL";
            return (IPositionDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建Role数据访问层
        /// </summary>
        /// <returns></returns>
        public static IRoleDAL CreateRoleDAL()
        {
            String className = path + ".RoleDAL";
            return (IRoleDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建ProcessType数据访问层
        /// </summary>
        /// <returns></returns>
        public static IProcessTypeDAL CreateProcessTypeDAL()
        {
            String className = path + ".ProcessTypeDAL";
            return (IProcessTypeDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建ProcessRule数据访问层
        /// </summary>
        /// <returns></returns>
        public static IProcessRuleDAL CreateProcessRuleDAL()
        {
            String className = path + ".ProcessRuleDAL";
            return (IProcessRuleDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建WorkList数据访问层
        /// </summary>
        /// <returns></returns>
        public static IWorkList CreateWorkListDAL()
        {
            String className = path + ".WorkListDAL";
            return (IWorkList)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建业务表数据访问层
        /// </summary>
        /// <returns></returns>
        public static ICustomFlowDAL CreateCustomFlowDAL()
        {
            String className = path + ".CustomFlowDAL";
            return (ICustomFlowDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建业务表数据访问层
        /// </summary>
        /// <returns></returns>
        public static IWorkPlaceDAL CreateWorkPlaceDAL()
        {
            String className = path + ".WorkPlaceDAL";
            return (IWorkPlaceDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建ProcessLog数据访问层
        /// </summary>
        /// <returns></returns>
        public static IProcessLogDAL CreateProcessLogDAL()
        {
            String className = path + ".ProcessLogDAL";
            return (IProcessLogDAL)Assembly.Load(path).CreateInstance(className);
        }

        public static IMyApplication CreateMyApplicationDAL()
        {
            String className = path + ".MyApplicationDAL";
            return (IMyApplication)Assembly.Load(path).CreateInstance(className);
        }

        public static IMyJoined CreateMyJoinedDAL()
        {
            String className = path + ".MyJoinedDAL";
            return (IMyJoined)Assembly.Load(path).CreateInstance(className);
        }

        public static IDelegation CreateDelegationDAL()
        {
            String className = path + ".DelegationDAL";
            return (IDelegation)Assembly.Load(path).CreateInstance(className);
        }

        public static IMenu CreateMenuDAL()
        {
            String className = path + ".MenuDAL";
            return (IMenu)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建FormDesignDAL数据访问层
        /// </summary>
        /// <returns></returns>
        public static IFormDesignDAL CreateFormDesignDAL()
        {
            String className = path + ".FormDesignDAL";
            return (IFormDesignDAL)Assembly.Load(path).CreateInstance(className);
        }

        public static IFormTemplateControlDAL CreateFormTemplateControlDAL()
        {
            String className = path + ".FormTemplateControlDAL";
            return (IFormTemplateControlDAL)Assembly.Load(path).CreateInstance(className);
        }

        public static IFormTemplateControlPropertyDAL CreateFormTemplateControlPropertyDAL()
        {
            String className = path + ".FormTemplateControlPropertyDAL";
            return (IFormTemplateControlPropertyDAL)Assembly.Load(path).CreateInstance(className);
        }

        /// <summary>
        /// 创建PworldRole数据访问层类
        /// </summary>
        /// <returns></returns>
        public static IPWorldRoleDAL CreatePWorldRoleDAL()
        {
            String className = path + ".PWorldRoleDAL";
            return (IPWorldRoleDAL)Assembly.Load(path).CreateInstance(className);
        }
    }
}
