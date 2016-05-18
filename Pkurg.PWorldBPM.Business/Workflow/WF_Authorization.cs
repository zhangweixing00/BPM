using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;

namespace Pkurg.PWorldBPM.Business.Workflow
{
    public class WF_Authorization 
    {
        /// <summary>
        /// 插入授权表（授权给招采部门全部成员）  from  XX to XX 的格式
        /// </summary>
        /// <param name="AuthorizedByUserCode">授权人的编号</param>
        /// <param name="AuthorizedByUserName">授权人的姓名</param>
        /// <param name="ProId">流程ID,需要从dispose调用过程中就取到值</param>
        /// <param name="ProcName">流程名称，需要从dispose调用过程中就取到值</param>
        /// <param name="AuthorizedUserCode">被授权人编号</param>
        /// <param name="AuthorizedUserName">被授权人姓名</param>
        /// <returns></returns>
        public static void InsertAuthorization(string AuthorizedByUserCode, string AuthorizedByUserName, string ProId, string ProcName, string AuthorizedUserCode, string AuthorizedUserName)
        {
            SqlParameter[] parameters ={
            new SqlParameter("@AuthorizedByUserCode",SqlDbType.NVarChar,100),
            new SqlParameter("@AuthorizedByUserName",SqlDbType.NVarChar,50),
            new SqlParameter("@ProId",SqlDbType.NVarChar,100),
            new SqlParameter("@ProcName",SqlDbType.NVarChar,300),
            new SqlParameter("@AuthorizedUserCode",SqlDbType.NVarChar,100),
            new SqlParameter("@AuthorizedUserName",SqlDbType.NVarChar,50) 
                                      };
            parameters[0].Value = AuthorizedByUserCode;
            parameters[1].Value = AuthorizedByUserName;
            parameters[2].Value = ProId;
            parameters[3].Value = ProcName;
            parameters[4].Value = AuthorizedUserCode;
            parameters[5].Value = AuthorizedUserName;

            DataTable dataTable = DBHelper.ExecutedProcedure("Biz.WF_Authorization_Insert", parameters);
        }
    }
}
