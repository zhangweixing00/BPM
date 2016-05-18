using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

/// <summary>
///RequestProcess 的摘要说明
/// </summary>
public class RequestProcess
{
    public RequestProcess()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static List<WorkFlowItem> GetTodoList(string loginId, int pageIndex)
    {
        List<WorkFlowItem> infos = DTToList<WorkFlowItem>(Pkurg.PWorldBPM.Business.Controls.WF_Instance.GetInterfaceList(loginId, "GetTodoList"));
        return PagedList(infos, pageIndex, -1);
    }
    public static List<WorkFlowItem> GetDoneList(string loginId, int pageIndex)
    {
        List<WorkFlowItem> infos = DTToList<WorkFlowItem>(Pkurg.PWorldBPM.Business.Controls.WF_Instance.GetInterfaceList(loginId, "GetDoneList"));
        return PagedList(infos, pageIndex, -1);
    }
    public static List<WorkFlowItem> GetDoneListPaged(string loginId, int pageIndex)
    {
        int defaultPageSize = GetDefaultPageCount();
        int totalCount = 0;
        List<WorkFlowItem> infos = DTToList<WorkFlowItem>(Pkurg.PWorldBPM.Business.Controls.WF_Instance.GetInterfaceList_DoneList(loginId, pageIndex, defaultPageSize, out totalCount));
        return infos;
    }
    public static List<WorkFlowItem> GetArchiveList(string loginId, int pageIndex)
    {
        int defaultPageSize = GetDefaultPageCount();
        int totalCount = 0;
        List<WorkFlowItem> infos = DTToList<WorkFlowItem>(Pkurg.PWorldBPM.Business.Controls.WF_Instance.GetInterfaceList_Archive(loginId, pageIndex, defaultPageSize, out totalCount));
        return infos;
    }

    public static List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> GetUserList(string key, int pageIndex, int pageSize)
    {
        if (!string.IsNullOrWhiteSpace(key))
        {
            key = key.ToLower();
        }

        var context = DBContext.GetSysContext();
        int defaultPageSize = pageSize <= 0 ? GetDefaultPageCount() : pageSize;
        pageIndex = pageIndex <= 0 ? 1 : pageIndex;
        int totalCount = context.V_Pworld_UserInfo.Count();
        if (totalCount <= 0)
        {
            return new List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo>();
        }

        List<Pkurg.PWorldBPM.Business.Sys.V_Pworld_UserInfo> infos = context.V_Pworld_UserInfo.Where(x => string.IsNullOrWhiteSpace(key) ||
            x.LoginName.ToLower().Contains(key) || x.EmployeeName.ToLower().Contains(key) ||
            x.FullPY.ToLower().Contains(key)).OrderBy(x => x.OrderNo).Skip((pageIndex - 1) * defaultPageSize).Take(defaultPageSize).ToList();
        return infos;
    }

    private static List<T> DTToList<T>(DataTable dt)
    {
        if (dt == null)
        {
            return new List<T>();
        }
        List<T> list = new List<T>();
        Type t = typeof(T);
        var propInfos = t.GetProperties();
        List<PropertyInfo> exsitPropInfos = new List<PropertyInfo>();
        foreach (var item in propInfos)
        {
            foreach (DataColumn colItem in dt.Columns)
            {
                if (colItem.ColumnName.ToLower() == item.Name.ToLower())
                {
                    exsitPropInfos.Add(item);
                }
            }
        }
        foreach (DataRow item in dt.Rows)
        {
            T info = Activator.CreateInstance<T>();
            foreach (var propItem in exsitPropInfos)
            {
                object value = item[propItem.Name];
                t.GetProperty(propItem.Name).SetValue(info, value == null ? "" : value.ToString(), null);
            }
            list.Add(info);
        }
        return list;
    }

    private static List<T> PagedList<T>(List<T> infos, int pageIndex, int pageSize = 2)
    {
        pageIndex++;

        int defaultPageCount = GetDefaultPageCount();
        if (infos == null)
        {
            return new List<T>();
        }
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        if (pageSize < 0)
        {
            pageSize = defaultPageCount;
        }
        if ((pageIndex - 1) * pageSize > infos.Count)
        {
            return new List<T>();
            //pageIndex = infos.Count % pageSize == 0 ? infos.Count / pageSize : infos.Count / pageSize + 1;
        }

        List<T> pagedInfos = infos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(); ;
        return pagedInfos;
    }

    private static int GetDefaultPageCount()
    {
        int defaultPageCount = 2;
        string defaultPageCountString = System.Configuration.ConfigurationManager.AppSettings["App_DefaultPageSize"];
        if (string.IsNullOrWhiteSpace(defaultPageCountString) || !int.TryParse(defaultPageCountString, out defaultPageCount) || defaultPageCount < 0)
        {
            return 2;
        }

        return defaultPageCount;
    }

    internal static MobileUserInfo GetMobileUserInfoByAccount(string account)
    {
        var info = DBContext.GetSysContext().V_Pworld_UserInfo.FirstOrDefault(x => x.LoginName.ToLower() == account.ToLower());
        if (info != null)
        {
            return new MobileUserInfo()
            {
                CompanyName = info.CompanyName,
                DeptName = info.DepartName,
                EmployeeId = info.EmployeeCode,
                UserCode = account,
                UserName = info.EmployeeName

            };
        }
        return new MobileUserInfo();
    }
}