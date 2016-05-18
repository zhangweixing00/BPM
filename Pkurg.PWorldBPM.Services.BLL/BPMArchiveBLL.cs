using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Services.DAL;

namespace Pkurg.PWorldBPM.Services.BLL
{
    public class BPMArchiveBLL
    {
        #region Return

        /// <summary>
        /// 获取需要归档的数据列表
        /// </summary>
        /// <returns></returns>
        public List<V_BPM_Archive> GetList()
        {
            using(Linq2BPMDataContext db=new Linq2BPMDataContext ()){
                return db.V_BPM_Archive.ToList();
            }
        }

        #endregion Return

    }
}
