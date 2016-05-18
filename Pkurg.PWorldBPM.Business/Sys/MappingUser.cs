using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Business.Sys;

namespace Pkurg.PWorldBPM.Business.Sys
{
    /// <summary>
    /// 流程门户设置
    /// </summary>
    public class MappingUser
    {
        public string GetToUserCode(string user, int state)
        {
            using (SysDBDataContext db = new SysDBDataContext(BPMConnectionString.ConnectionString))
            {
                SYS_MappingUser obj = db.SYS_MappingUser.Where(p => p.FormUserCode == user && p.State == state).FirstOrDefault();
                return obj != null ? obj.ToUserCode : "";
            }
        }

        public SYS_MappingUser GetState(string user)
        {
            user = user.ToLower().Replace("founder\\", "");
            using (SysDBDataContext db = new SysDBDataContext(BPMConnectionString.ConnectionString))
            {
                SYS_MappingUser obj = db.SYS_MappingUser.Where(p => p.FormUserCode == user).FirstOrDefault();
                return obj;
            }
        }

        public SYS_MappingUser UpdateState(string user, int state)
        {
            using (SysDBDataContext db = new SysDBDataContext(BPMConnectionString.ConnectionString))
            {
                SYS_MappingUser obj = db.SYS_MappingUser.Where(p => p.FormUserCode == user).FirstOrDefault();
                if (obj != null)
                {
                    obj.State = state;
                    db.SubmitChanges();
                }
                return obj;
            }
        }
    }
}
