using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorldBPM.Services.DAL;

namespace Pkurg.PWorldBPM.Services.BLL
{
    public class AttachmentBLL
    {
        /// <summary>
        /// 获取需要归档的数据列表
        /// </summary>
        /// <returns></returns>
        public List<BPM_Attachment> GetList(string formID)
        {
            using (Linq2BPMDataContext db = new Linq2BPMDataContext())
            {
                return db.BPM_Attachment.Where(m=>m.FormID.Trim().ToLower()==formID.Trim().ToLower()).ToList();
            }
        }
    }
}
