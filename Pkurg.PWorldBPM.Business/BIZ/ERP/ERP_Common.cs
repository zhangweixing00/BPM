using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pkurg.PWorld.Business.Common;
using System.Data;

namespace Pkurg.PWorldBPM.Business.BIZ.ERP
{
    public class ERP_Common
    {
        static string sqlFormat = @"SELECT *
                            FROM Biz.[{1}] A
                            join WF_WorkFlowInstance B
                            on A.[FormID]=B.FormID and B.WFStatus=1-- and AppID='10109'
                            where A.ErpFormId='{0}'";

        /// <summary>
        ///  判断是否有正在审批中的单子
        /// </summary>
        public static bool IsExsitRunFlow(string erpFormId, ERP_WF_T_Name t_Name)
        {
            string sql = string.Format(sqlFormat, erpFormId, t_Name);
            DataTable dt = DBHelper.ExecuteDataTable(sql, System.Data.CommandType.Text);
            if (dt == null || dt.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

    }
    public enum ERP_WF_T_Name
    {
        ERP_ContractApproval,
        ERP_Instruction,
        ERP_PaymentApplication,
        ERP_SupplementalAgreement,
        ERP_ContractFinalAccount
    }
}
