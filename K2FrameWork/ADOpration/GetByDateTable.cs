using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace ADOpration
{
    public class GetByDateTable
    {
        public static GetByDateTable getByAd = new GetByDateTable();

        //public DataTable getAdUsers()
        //{

        //    if (HttpRuntime.Cache[enumDictCache.AdUsers.ToString()] == null)
        //    {
        //        //HttpRuntime.Cache.Add(enumDictCache.ADusers.ToString(),ADHelper.GetUserList( ADHelper.ExecuteAD(Enums.EntryType.User)), null, DateTime.Now.AddHours(12), TimeSpan.Zero, CacheItemPriority.High, null);
        //        HttpRuntime.Cache.Add(enumDictCache.AdUsers.ToString(), ADHelper.GetUserList(ADHelper.GetEmailsAndGroups()), null, DateTime.Now.AddHours(12), TimeSpan.Zero, CacheItemPriority.High, null);
        //    }

        //    return (DataTable)HttpRuntime.Cache[enumDictCache.AdUsers.ToString()];

        //    //return ADHelper.GetUserList( ADHelper.ExecuteAD(Enums.EntryType.User));
        //}

        public DataTable getAdGroups()
        {

            if (HttpRuntime.Cache[enumDictCache.AdGroups.ToString()] == null)
            {
                HttpRuntime.Cache.Add(enumDictCache.AdGroups.ToString(), ADHelper.GetUserList(ADHelper.GetEmailGroups()), null, DateTime.Now.AddHours(12), TimeSpan.Zero, CacheItemPriority.High, null);
            }

            return (DataTable)HttpRuntime.Cache[enumDictCache.AdGroups.ToString()];

            //return ADHelper.GetUserList( ADHelper.ExecuteAD(Enums.EntryType.User));
        }
    }
}
