using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_WorkflowRelation
    {
        public static IList<Pkurg.BPM.Entities.FlowRelated> GetRelatedInstsByInstId(string instId)
        {
            return new Pkurg.BPM.Services.FlowRelatedService().Find(string.Format("FlowID='{0}'", instId));
        }

        public static IList<Pkurg.BPM.Entities.VUserProcInsts> GetProcListBelongToUser(string userName)
        {
            return new Pkurg.BPM.Services.VUserProcInstsService().Get(string.Format("PartName='{0}' and Status='3'", userName), "");
        }

        public static IList<Pkurg.BPM.Entities.VUserProcInsts> GetProcListBelongToUserById(string userId)
        {
            return new Pkurg.BPM.Services.VUserProcInstsService().Get(string.Format("PartID='{0}' ", userId), "");
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

        public static bool DelRelatedFlowInfo(int relatId)
        {
            return new Pkurg.BPM.Services.FlowRelatedService().Delete(relatId);
        }

        public static DataTable GetProcListByUserID(string userId)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@userId",System.Data.SqlDbType.NVarChar,50)
            };
            parameters[0].Value = userId;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetProcListByUserID", parameters);
            return dataTable;
        }

        public static DataTable GetFlowRelatedListByFlowID(string flowId)
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@flowId",System.Data.SqlDbType.VarChar,64)
            };
            parameters[0].Value = flowId;
            DataTable dataTable = DBHelper.ExecutedProcedure("wf_usp_GetFlowRelatedListByFlowID", parameters);
            return dataTable;
        }

    }
}
