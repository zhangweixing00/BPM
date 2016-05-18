using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.BPM.Entities;
using Pkurg.BPM.Services;
using Pkurg.BPM.Data;
using Pkurg.PWorldBPM.Common.Log;
 

namespace Pkurg.PWorldBPM.Business.AttachmentMan
{
  public  class BPM_Attachment
    {
        private string className = "RLY_Attachment";

        /// <summary>
        /// 得到附件信息列表
        /// </summary>
        /// <returns></returns>
        public TList<Attachment> GetAttachmentList()
        {
            AttachmentService service = new AttachmentService();


            AttachmentQuery query = new AttachmentQuery();
            query.Clear();

            SqlSortBuilder<AttachmentColumn> sort = new SqlSortBuilder<AttachmentColumn>();
            sort.AppendASC(AttachmentColumn.CreateAtTime);


            return service.Find(query.GetParameters(), sort.GetSortColumns());
        }

        public Attachment GetAttachmentById(string AttachmentID)
        {
            AttachmentService drs = new AttachmentService();
            return drs.Get(new AttachmentKey(AttachmentID));
        }


        public TList<Attachment> GetAttachmentByFormID(string FormID)
        {
            AttachmentService rs = new AttachmentService();
            AttachmentQuery query = new AttachmentQuery();

            query.Clear();
            query.AppendEquals(string.Empty, AttachmentColumn.FormId, FormID);

            SqlSortBuilder<AttachmentColumn> sort = new SqlSortBuilder<AttachmentColumn>();
            sort.AppendASC(AttachmentColumn.CreateAtTime);

            return rs.Find(query.GetParameters(), sort.GetSortColumns());
        }
        public TList<Attachment> GetAttachmentByFormID(string FormID,Pkurg.PWorld.Entities.Employee currentEmployee)
        {
            AttachmentService rs = new AttachmentService();
            AttachmentQuery query = new AttachmentQuery();

            query.Clear();
            query.AppendEquals(string.Empty, AttachmentColumn.FormId, FormID);
            query.AppendEquals("and", AttachmentColumn.CreateByUserCode, currentEmployee.EmployeeCode);

            SqlSortBuilder<AttachmentColumn> sort = new SqlSortBuilder<AttachmentColumn>();
            sort.AppendASC(AttachmentColumn.CreateAtTime);

            return rs.Find(query.GetParameters(), sort.GetSortColumns());
        }
        
        #region 附件数据更新
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="rd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        //public bool AddGeneralReimburse(Entities.Reimburse rd, TList<ReceiptInfo> list, decimal Balance, string strBankAccount, Employee CurrentEmployee)
        public bool AddAttachment(TList<Attachment> list)
        {
            try
            {
                new Pkurg.BPM.Services.AttachmentService().Save(list);
                return true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="CurrentEmployee"></param>
        /// <returns></returns>
        public bool DeleteAttachment(TList<Attachment> list)
        {
            try
            {
                new Pkurg.BPM.Services.AttachmentService().Delete(list);
                return true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        #endregion

    }
}
