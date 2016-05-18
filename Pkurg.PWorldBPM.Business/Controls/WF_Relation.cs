using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_Relation
    {
        public static IList<Pkurg.BPM.Entities.FlowRelated> GetRelatedInstsByInstId(string instId)
        {
            return new Pkurg.BPM.Services.FlowRelatedService().Find(string.Format("FlowID='{0}'", instId));
        }

        public static IList<Pkurg.BPM.Entities.VUserProcInsts> GetProcListBelongToUser(string userName)
        {
            return new Pkurg.BPM.Services.VUserProcInstsService().Get(string.Format("PartName='{0}' and WFStatus='3'", userName), "");
        }

        public static IList<Pkurg.BPM.Entities.VUserProcInsts> GetProcListBelongToUserById(string userId)
        {
            return new Pkurg.BPM.Services.VUserProcInstsService().Get(string.Format("PartID='{0}' and Status='3' ", userId), "");
        }

        public static Pkurg.BPM.Entities.FlowRelated GetRelatedFlowInfo(string instId, string beInstId)
        {
            IList<Pkurg.BPM.Entities.FlowRelated> infos = new Pkurg.BPM.Services.FlowRelatedService().Find(string.Format("FlowID='{0}' AND  RelatedFlowID='{1}'", instId, beInstId));
            if (infos == null || infos.Count == 0)
            {
                return null;
            }
            return infos[0];
        }
        public static Pkurg.BPM.Entities.FlowRelated AddRelatedFlowInfo(Pkurg.BPM.Entities.FlowRelated info)
        {
            return new Pkurg.BPM.Services.FlowRelatedService().Save(info);
        }

        /// <summary>
        /// 关联流程：新增其他数据库调用方式
        /// star
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="beRelationFlowId"></param>
        /// <param name="opUser"></param>
        public static void AddRelatedFlowInfo(string flowId, string beRelationFlowId, string opUser)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@flowId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@beRelationFlowId",System.Data.SqlDbType.VarChar,100),
            new SqlParameter("@opUser",System.Data.SqlDbType.VarChar,100)
            };
            parameters[0].Value = flowId;
            parameters[1].Value = beRelationFlowId;
            parameters[2].Value = opUser;
            DBHelper.ExecutedProcedure("wf_usp_FlowRelated_Add", parameters);
        }


        /// <summary>
        /// 关联流程：新增其他数据库调用方式
        /// star
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static List<WF_RelationInfo> GetProcListByUserCode(string userCode)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@userCode",System.Data.SqlDbType.VarChar,50)
            };
            parameters[0].Value = userCode;
            DataTable dt = DBHelper.ExecutedProcedure("wf_usp_FlowRelated_GetCanRelateByUserCode", parameters);
            List<WF_RelationInfo> infos = new List<WF_RelationInfo>();
            if (dt==null||dt.Rows.Count==0)
            {
                return infos;
            }

            return dt.DataTableToList<WF_RelationInfo>();
        }

        public static bool DelRelatedFlowInfo(int relatId)
        {
            return new Pkurg.BPM.Services.FlowRelatedService().Delete(relatId);
        }

    }
}
