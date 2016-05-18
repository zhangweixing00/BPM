using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Pkurg.PWorld.Business.Common;
using System.Data.SqlClient;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public class WF_WorkFlowInstances
    {
        public static DataTable GetFlowInstanceToDataTable()
        {
            //DataProvider dataProvider = new DataProvider();
            //dataProvider.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString;
            //DBHelper.Provider
            DataTable dataTable = DBHelper.ExecuteDataTable("select * from [BPM].[dbo].[WF_WorkFlowInstance] order by CreateAtTime desc", CommandType.Text);
            return dataTable;
        }

        public static bool AddWorkFlowInstance(WF_WorkFlowInstanceInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@InstanceID", SqlDbType.NVarChar,100),
					new SqlParameter("@AppID", SqlDbType.NVarChar,100),
					new SqlParameter("@FormID", SqlDbType.NVarChar,200),
					new SqlParameter("@WFInstanceId", SqlDbType.NVarChar,100),
                    //new SqlParameter("@OrderNo", SqlDbType.Int,4),
                    //new SqlParameter("@IsDel", SqlDbType.Int,4),
					new SqlParameter("@CreateByUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateByUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateAtTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateByUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateByUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateAtTime", SqlDbType.DateTime),
					new SqlParameter("@CreateDeptCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDeptName", SqlDbType.NVarChar,50),
                    //new SqlParameter("@WorkItemCode", SqlDbType.NVarChar,100),
                    //new SqlParameter("@WorkItemName", SqlDbType.NVarChar,200),
                    //new SqlParameter("@WFTaskID", SqlDbType.Int,4),
                    //new SqlParameter("@FinishedTime", SqlDbType.DateTime),
                    //new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@FormTitle", SqlDbType.NVarChar,300),
					new SqlParameter("@WFStatus", SqlDbType.NVarChar,10)
                    //,new SqlParameter("@SumitTime", SqlDbType.DateTime),
                    //new SqlParameter("@FormData", SqlDbType.Xml)
					//,new SqlParameter("@ProcessName", SqlDbType.NVarChar,200)
                                            };
                parameters[0].Value = model.InstanceID;
                parameters[1].Value = model.AppID;
                parameters[2].Value = model.FormID;
                parameters[3].Value = model.WFInstanceId;
                //parameters[4].Value = model.OrderNo;
                //parameters[5].Value = model.IsDel;
                parameters[4].Value = model.CreateByUserCode;
                parameters[5].Value = model.CreateByUserName;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = model.UpdateByUserCode;
                parameters[8].Value = model.UpdateByUserName;
                parameters[9].Value = DateTime.Now;
                parameters[10].Value = model.CreateDeptCode;
                parameters[11].Value = model.CreateDeptName;
                //parameters[14].Value = model.WorkItemCode;
                //parameters[15].Value = model.WorkItemName;
                //parameters[16].Value = model.WFTaskID;
                //parameters[17].Value = model.FinishedTime;
                //parameters[18].Value = model.Remark;
                parameters[12].Value = model.FormTitle;
                parameters[13].Value = model.WFStatus;
                //parameters[21].Value = model.SumitTime;
                //parameters[22].Value = model.FormData;
                // parameters[23].Value = model.ProcessName;

                DBHelper.ExecutedProcedure("wf_usp_WorkFlowInstance_ADD", parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static WF_WorkFlowInstanceInfo GetWorkFlowInstanceByWfId(string wfid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@WfId", SqlDbType.NVarChar,100)			};
            parameters[0].Value = wfid;

            WF_WorkFlowInstanceInfo model = new WF_WorkFlowInstanceInfo();
            DataTable dt = DBHelper.ExecutedProcedure("wf_usp_WorkFlowInstance_GetByWfId", parameters);
            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        public static WF_WorkFlowInstanceInfo DataRowToModel(DataRow row)
        {
            WF_WorkFlowInstanceInfo model = new WF_WorkFlowInstanceInfo();
            if (row != null)
            {
                if (row["InstanceID"] != null)
                {
                    model.InstanceID = row["InstanceID"].ToString();
                }
                if (row["AppID"] != null)
                {
                    model.AppID = row["AppID"].ToString();
                }
                if (row["FormID"] != null)
                {
                    model.FormID = row["FormID"].ToString();
                }
                if (row["WFInstanceId"] != null)
                {
                    model.WFInstanceId = row["WFInstanceId"].ToString();
                }
                if (row["OrderNo"] != null && row["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(row["OrderNo"].ToString());
                }
                if (row["IsDel"] != null && row["IsDel"].ToString() != "")
                {
                    model.IsDel = int.Parse(row["IsDel"].ToString());
                }
                if (row["CreateByUserCode"] != null)
                {
                    model.CreateByUserCode = row["CreateByUserCode"].ToString();
                }
                if (row["CreateByUserName"] != null)
                {
                    model.CreateByUserName = row["CreateByUserName"].ToString();
                }
                if (row["CreateAtTime"] != null && row["CreateAtTime"].ToString() != "")
                {
                    model.CreateAtTime = DateTime.Parse(row["CreateAtTime"].ToString());
                }
                if (row["UpdateByUserCode"] != null)
                {
                    model.UpdateByUserCode = row["UpdateByUserCode"].ToString();
                }
                if (row["UpdateByUserName"] != null)
                {
                    model.UpdateByUserName = row["UpdateByUserName"].ToString();
                }
                if (row["UpdateAtTime"] != null && row["UpdateAtTime"].ToString() != "")
                {
                    model.UpdateAtTime = DateTime.Parse(row["UpdateAtTime"].ToString());
                }
                if (row["CreateDeptCode"] != null)
                {
                    model.CreateDeptCode = row["CreateDeptCode"].ToString();
                }
                if (row["CreateDeptName"] != null)
                {
                    model.CreateDeptName = row["CreateDeptName"].ToString();
                }
                if (row["WorkItemCode"] != null)
                {
                    model.WorkItemCode = row["WorkItemCode"].ToString();
                }
                if (row["WorkItemName"] != null)
                {
                    model.WorkItemName = row["WorkItemName"].ToString();
                }
                if (row["WFTaskID"] != null && row["WFTaskID"].ToString() != "")
                {
                    model.WFTaskID = int.Parse(row["WFTaskID"].ToString());
                }
                if (row["FinishedTime"] != null && row["FinishedTime"].ToString() != "")
                {
                    model.FinishedTime = DateTime.Parse(row["FinishedTime"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["FormTitle"] != null)
                {
                    model.FormTitle = row["FormTitle"].ToString();
                }
                if (row["WFStatus"] != null)
                {
                    model.WFStatus = row["WFStatus"].ToString();
                }
                if (row["SumitTime"] != null && row["SumitTime"].ToString() != "")
                {
                    model.SumitTime = DateTime.Parse(row["SumitTime"].ToString());
                }
                //model.FormData=row["FormData"].ToString();
                if (row["ProcessName"] != null)
                {
                    model.ProcessName = row["ProcessName"].ToString();
                }
            }
            return model;
        }

        public static bool AddApproval_Record(WF_Approval_RecordInfo model)
        {
            try
            {
                SqlParameter[] parameters = {
					new SqlParameter("@ApprovalID", SqlDbType.NVarChar,200),
					new SqlParameter("@WFTaskID", SqlDbType.Int,4),
					new SqlParameter("@FormID", SqlDbType.NVarChar,200),
					new SqlParameter("@InstanceID", SqlDbType.NVarChar,100),
					new SqlParameter("@Opinion", SqlDbType.NVarChar,-1),
					new SqlParameter("@OrderNo", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Int,4),
					new SqlParameter("@CreateByUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateByUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateAtTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateByUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateByUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateAtTime", SqlDbType.DateTime),
					new SqlParameter("@ApproveByUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApproveByUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApproveResult", SqlDbType.NVarChar,50),
					new SqlParameter("@ApproveAtTime", SqlDbType.DateTime),
					new SqlParameter("@OpinionType", SqlDbType.NVarChar,50),
					new SqlParameter("@CurrentActiveName", SqlDbType.NVarChar,200),
					new SqlParameter("@ISSign", SqlDbType.Char,10),
					new SqlParameter("@ReceiveTime", SqlDbType.DateTime),
					new SqlParameter("@FinishedTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@CurrentActiveID", SqlDbType.NVarChar,50),
					new SqlParameter("@ApproveStatus", SqlDbType.NVarChar,10),
					new SqlParameter("@DelegateUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DelegateUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadTime", SqlDbType.DateTime)};
                parameters[0].Value = model.ApprovalID;
                parameters[1].Value = model.WFTaskID;
                parameters[2].Value = model.FormID;
                parameters[3].Value = model.InstanceID;
                parameters[4].Value = model.Opinion;
                parameters[5].Value = model.OrderNo;
                parameters[6].Value = model.IsDel;
                parameters[7].Value = model.CreateByUserCode;
                parameters[8].Value = model.CreateByUserName;
                parameters[9].Value = DateTime.Now;
                parameters[10].Value = model.UpdateByUserCode;
                parameters[11].Value = model.UpdateByUserName;
                parameters[12].Value = DateTime.Now;
                parameters[13].Value = model.ApproveByUserCode;
                parameters[14].Value = model.ApproveByUserName;
                parameters[15].Value = model.ApproveResult;
                parameters[16].Value = DateTime.Now;
                parameters[17].Value = model.OpinionType;
                parameters[18].Value = model.CurrentActiveName;
                parameters[19].Value = model.ISSign;
                parameters[20].Value = model.ReceiveTime;
                parameters[21].Value = model.FinishedTime;
                parameters[22].Value = model.Remark;
                parameters[23].Value = model.CurrentActiveID;
                parameters[24].Value = model.ApproveStatus;
                parameters[25].Value = model.DelegateUserName;
                parameters[26].Value = model.DelegateUserCode;
                parameters[27].Value = model.ReadTime;

                DBHelper.ExecutedProcedure("WF_Approval_Record_ADD", parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static WF_Approval_RecordInfo GetApproval_RecordByIdAndName(string instanceId, string activityName)
        {
            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@procId",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@activityName",System.Data.SqlDbType.NVarChar,100),
                };
            parameters[0].Value = instanceId;
            parameters[1].Value = activityName;

            DataTable dt = DBHelper.ExecutedProcedure("wf_usp_Approval_Record_GetByActivityName", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DataRowToModel_Approval_Record(dt.Rows[0]);
            }
            return null;
        }

        public static WF_Approval_RecordInfo DataRowToModel_Approval_Record(DataRow row)
        {
            WF_Approval_RecordInfo model = new WF_Approval_RecordInfo();
            if (row != null)
            {
                if (row["ApprovalID"] != null)
                {
                    model.ApprovalID = row["ApprovalID"].ToString();
                }
                if (row["WFTaskID"] != null && row["WFTaskID"].ToString() != "")
                {
                    model.WFTaskID = int.Parse(row["WFTaskID"].ToString());
                }
                if (row["FormID"] != null)
                {
                    model.FormID = row["FormID"].ToString();
                }
                if (row["InstanceID"] != null)
                {
                    model.InstanceID = row["InstanceID"].ToString();
                }
                if (row["Opinion"] != null)
                {
                    model.Opinion = row["Opinion"].ToString();
                }
                if (row["OrderNo"] != null && row["OrderNo"].ToString() != "")
                {
                    model.OrderNo = int.Parse(row["OrderNo"].ToString());
                }
                if (row["IsDel"] != null && row["IsDel"].ToString() != "")
                {
                    model.IsDel = int.Parse(row["IsDel"].ToString());
                }
                if (row["CreateByUserCode"] != null)
                {
                    model.CreateByUserCode = row["CreateByUserCode"].ToString();
                }
                if (row["CreateByUserName"] != null)
                {
                    model.CreateByUserName = row["CreateByUserName"].ToString();
                }
                if (row["CreateAtTime"] != null && row["CreateAtTime"].ToString() != "")
                {
                    model.CreateAtTime = DateTime.Parse(row["CreateAtTime"].ToString());
                }
                if (row["UpdateByUserCode"] != null)
                {
                    model.UpdateByUserCode = row["UpdateByUserCode"].ToString();
                }
                if (row["UpdateByUserName"] != null)
                {
                    model.UpdateByUserName = row["UpdateByUserName"].ToString();
                }
                if (row["UpdateAtTime"] != null && row["UpdateAtTime"].ToString() != "")
                {
                    model.UpdateAtTime = DateTime.Parse(row["UpdateAtTime"].ToString());
                }
                if (row["ApproveByUserCode"] != null)
                {
                    model.ApproveByUserCode = row["ApproveByUserCode"].ToString();
                }
                if (row["ApproveByUserName"] != null)
                {
                    model.ApproveByUserName = row["ApproveByUserName"].ToString();
                }
                if (row["ApproveResult"] != null)
                {
                    model.ApproveResult = row["ApproveResult"].ToString();
                }
                if (row["ApproveAtTime"] != null && row["ApproveAtTime"].ToString() != "")
                {
                    model.ApproveAtTime = DateTime.Parse(row["ApproveAtTime"].ToString());
                }
                if (row["OpinionType"] != null)
                {
                    model.OpinionType = row["OpinionType"].ToString();
                }
                if (row["CurrentActiveName"] != null)
                {
                    model.CurrentActiveName = row["CurrentActiveName"].ToString();
                }
                if (row["ISSign"] != null)
                {
                    model.ISSign = row["ISSign"].ToString();
                }
                if (row["ReceiveTime"] != null && row["ReceiveTime"].ToString() != "")
                {
                    model.ReceiveTime = DateTime.Parse(row["ReceiveTime"].ToString());
                }
                if (row["FinishedTime"] != null && row["FinishedTime"].ToString() != "")
                {
                    model.FinishedTime = DateTime.Parse(row["FinishedTime"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["CurrentActiveID"] != null)
                {
                    model.CurrentActiveID = row["CurrentActiveID"].ToString();
                }
                if (row["ApproveStatus"] != null)
                {
                    model.ApproveStatus = row["ApproveStatus"].ToString();
                }
                if (row["DelegateUserName"] != null)
                {
                    model.DelegateUserName = row["DelegateUserName"].ToString();
                }
                if (row["DelegateUserCode"] != null)
                {
                    model.DelegateUserCode = row["DelegateUserCode"].ToString();
                }
                if (row["ReadTime"] != null && row["ReadTime"].ToString() != "")
                {
                    model.ReadTime = DateTime.Parse(row["ReadTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获取流程序列号
        /// 如QSD201409240010
        /// </summary>
        /// <param name="workFlowCode">流程编码、流程模板名称</param>
        /// <returns></returns>
        public static string GetSerialNumber(string workFlowCode)
        {
            string date1 = DateTime.Now.ToString("yyyy-MM-dd");
            string date2 = DateTime.Now.ToString("yyyyMMdd");

            string number = "";

            int codeValue = 0;

            SqlParameter[] parameters = new SqlParameter[]{
            new SqlParameter("@Date",System.Data.SqlDbType.NVarChar,50),
            new SqlParameter("@Code",System.Data.SqlDbType.NVarChar,50)
                };
            parameters[0].Value = date1;
            parameters[1].Value = workFlowCode;

            DataTable dt = DBHelper.ExecutedProcedure("GetSerialNumber", parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                codeValue = Convert.ToInt16(dt.Rows[0][0]);
                string no = String.Format("{0:D4}", codeValue);
                //QSD201409240010
                number = workFlowCode + date2 + no;
            }
            return number;
        }

        /// <summary>
        /// 通过FormID得到WorkFlowInstance中得值
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public static DataTable GetWorkFlowInstanceByFormID(string formID)
        {
            SqlParameter[] parameters ={
                                            new SqlParameter("@FormID",SqlDbType.NVarChar,100)
                                        };
            parameters[0].Value = formID;
            DataTable dataTable = DBHelper.ExecutedProcedure("dbo.WorkFlowInstance_Get", parameters);
            return dataTable;
        }
    }
}
