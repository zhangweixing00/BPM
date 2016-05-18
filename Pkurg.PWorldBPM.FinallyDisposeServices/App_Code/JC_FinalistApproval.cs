using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pkurg.PWorldBPM.FinallyDisposeServices;
using Pkurg.PWorldBPM.Business.BIZ.JC;
using Pkurg.BPM.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using System.Data;
using Pkurg.PWorld.Business.Permission;
using Pkurg.PWorld.Entities;

/// <summary>
///JC_FinalistApproval 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class JC_FinalistApproval : Pkurg.PWorldBPM.FinallyDisposeServices.FinallyDisposeServiceBase
{
    //部门编号都写在web配置里，在这里需要调用
    string CGDeptCode = System.Configuration.ConfigurationManager.AppSettings["CGDeptCode"];
    static string url = System.Configuration.ConfigurationManager.AppSettings["URL"];
    /// <summary>
    /// 重写流程结束方法
    /// </summary>
    public override Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo DoServiceEvent(int k2_workflowId, SerializableDictionary<string, string> dataFields)
    {
        Logger.logger.DebugFormat("Params:{0},{1}", k2_workflowId, dataFields.Keys.Count);
        foreach (KeyValuePair<string, string> item in dataFields)
        {
            Logger.logger.DebugFormat("df:{0}-{1}", item.Key, item.Value);
        }
        Pkurg.PWorldBPM.FinallyDisposeServices.ExecuteResultInfo info = new ExecuteResultInfo();
        try
        {
            //根据流程ID获取FormId
            //string jcFormCode = Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.GetJC_FinalistApprovalInfoByWfId(k2_workflowId.ToString()).FormID;
            JC_FinalistApprovalInfo companyInfo = Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.GetJC_FinalistApprovalInfoByWfId(k2_workflowId.ToString());
            string jcFormCode = companyInfo.FormID;
            string jcFormName = companyInfo.ProjectName;
            string StartUserName = companyInfo.ArchiveName;
            //判断是否审批通过,存储0，1表示通过与不通过
            string resultString = dataFields["IsPass"];

            //添加判断在招标委员会的节点上，如果时间过去两天，还有人没有审批，
            //则根据相关的字段给数据库插入“默认同意”字段，并且时间为截至时间[三个表关联]
            //条件=招标委员会意见/集团招标委员会意见，同时时间=2天，option.value=null
            //option.value="同意" 时间=当前时间
            //方法 insertinto:加意见     instanceid
            //先得到节点，再把节点上的datefield的人分隔开，再挨个判断他有没有意见，没有意见的再插入
            //if (dataFields["IsPass"] == "1")
            //{
            //    string currentActiveName = "";
            //    string persons = "";
            //    if (dataFields["IsReportToResource"] == "0")
            //    {
            //        persons = dataFields["GroupTenderCommitteeManager"];

            //        currentActiveName = "集团招标委员会意见";
            //    }
            //    else
            //    {
            //        persons = dataFields["TenderCommitteeManager"];

            //        currentActiveName = "招标委员会意见";
            //    }
            //    //把上面节点的datefield的人分隔开，再挨个判断他有没有意见，没有意见的再插入
            //    foreach (string person in persons.Split(','))
            //    {
            //            //通过一个方法定义三个变量得到
            //            // l WF_InstanogionnameceId CurrentActiveName
            //            //loginName, wfInstanceId, currentActiveName
            //        string LoginName = person.ToLower().Replace("k2:founder\\", "");
            //        string WFInstanceId = k2_workflowId.ToString();
            //        //给表中插入数据，并且option字段值默认为“默认同意”
            //        if (!string.IsNullOrEmpty(LoginName))
            //        {
            //        Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.InsertWF_Approval_RecordByInstanceID(currentActiveName, LoginName, WFInstanceId);
            //    }
            //    }
            //}

            //添加判断在招标委员会主任的节点上，如果时间过去两天，还有人没有审批，
            //则根据相关的字段给数据库插入“同意”字段，并且时间为截至时间[三个表关联]
            //条件=招标委员会主任意见/集团招标委员会主任意见，同时时间=2天，option.value=null
            //option.value="同意" 时间=当前时间
            //instanceid
            //先得到节点，再把节点上的datefield的人分隔开，再挨个判断他有没有意见，没有意见的再插入
            //if (dataFields["IsPass"] == "1")
            //{
            //    string currentActiveName = "";
            //    string person = "";
            //    if (dataFields["IsReportToResource"] == "0")
            //    {
            //        person = dataFields["GroupTenderCommitteeChairman"];

            //        currentActiveName = "集团招标委员会主任意见";
            //    }
            //    else
            //    {
            //        person = dataFields["TenderCommitteeChairman"];

            //        currentActiveName = "招标委员会主任意见";
            //    }

            //    //定义三个变量得到
            //    //loginName, wfInstanceId, currentActiveName
            //    string LoginName = person.ToLower().Replace("k2:founder\\", "");
            //    string WFInstanceId = k2_workflowId.ToString();
            //    //给表中插入数据，并且option字段值默认为“默认同意”
            //    Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.InsertWF_Approval_RecordByInstanceID(currentActiveName, LoginName, WFInstanceId);

            //}
            //这部分是根据自己的流程来判定的，现在只是通过流程ID得到FormId来进行判断结束
            Logger.logger.DebugFormat("Proc:{0},{1}", jcFormCode, resultString);

            JC_FinalistApprovalInfo model = new JC_FinalistApprovalInfo();
            model.FormID = jcFormCode;
            model.IsApproval = resultString;
            //两个文件名字最好不能起的一样，尤其是增删改查操作的时候文件名不能一样
            Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.UpdateJC_FinalistApprovalInfoInfoByModel(model);


            //流程结束之后授权给集团招采部门所有人
            //得到各参数的值：AuthorizedByUserCode：授权人编号；AuthorizedByUserName：授权人姓名；ProId：流程ID；
            //ProcName：流程编号；AuthorizedUserCode：被授权部门编号；AuthorizedUserName：被授权部门名称
            DataTable dt = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetWorkFlowInstanceByFormID(jcFormCode);
            DataRow dataRow = dt.Rows[0];

            string AuthorizedByUserCode = dataRow["CreateByUserCode"].ToString();
            string AuthorizedByUserName = dataRow["CreateByUserName"].ToString();
            string ProId = jcFormCode;
            string ProcName = jcFormName;

            InsertAuthorizationToJCGroup(AuthorizedByUserCode, AuthorizedByUserName, ProId, ProcName, CGDeptCode);



            info.IsSuccess = true;
        }
        catch (Exception ex)
        {
            info.ExecException = ex.StackTrace;
            info.IsSuccess = false;
        }
        Logger.logger.DebugFormat("Result:{0}", info.IsSuccess);
        Logger.logger.DebugFormat("Result-e:{0}", info.ExecException);
        return info;
    }
    /// <summary>
    /// 得到集团采购管理部的成员
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private static IList<VEmployeeAndAdditionalInfo> GetUserListByDeptCode(string code)
    {
        BFPmsUserRoleDepartment bfurd = new BFPmsUserRoleDepartment();
        IList<VEmployeeAndAdditionalInfo> ds = new List<VEmployeeAndAdditionalInfo>();

        DataTable dtUsers = bfurd.GetSelectRoleUser(code, "招采流程收文人");

        if (dtUsers == null || dtUsers.Rows.Count <= 0)
        {
            ds = bfurd.GetNotSelectRoleUser("0");
            ds = ds.Where(x => x.DepartCode == code).ToList();
        }
        else
        {
            foreach (DataRow item in dtUsers.Rows)
            {
                ds.Add(new VEmployeeAndAdditionalInfo()
                {
                    LoginName = item["LoginName"].ToString(),
                    DepartName = item["DepartName"].ToString(),
                    EmployeeName = item["EmployeeName"].ToString(),
                    Email = item["LoginName"].ToString() + "@jtmail.founder.com",
                    EmployeeCode = item["EmployeeCode"].ToString()
                });
            }
        }
        return ds;
    }


    /// <summary>
    /// 插入授权表（授权给招采部门全部成员）  from  XX to XX 的格式
    /// 在该部分中的被授权人是一个部门的集合，所以需要获取部门的编号，遍历得到该部门的所有成员，并对每个成员进行授权
    /// </summary>
    /// <param name="AuthorizedByUserCode">授权人的编号</param>
    /// <param name="AuthorizedByUserName">授权人的姓名</param>
    /// <param name="ProId">流程ID,需要从dispose调用过程中就取到值</param>
    /// <param name="ProcName">流程名称，需要从dispose调用过程中就取到值</param>
    /// <param name="AuthorizedUserCode">被授权人编号(部门编号)</param>
    /// <param name="AuthorizedUserName">被授权人姓名（部门名称）</param>
    /// <returns></returns>
    public static void InsertAuthorizationToJCGroup(string AuthorizedByUserCode, string AuthorizedByUserName, string ProId, string ProcName, string AuthorizeDeptCode)
    {
        //发送邮件
        MailService mailService = new MailService();

        IList<VEmployeeAndAdditionalInfo> ds = GetUserListByDeptCode(AuthorizeDeptCode);
        foreach (var item in ds)
        {
            string AuthorizedUserCode = item.EmployeeCode;
            string AuthorizedUserName = item.EmployeeName;

            Pkurg.PWorldBPM.Business.Workflow.WF_Authorization.InsertAuthorization(AuthorizedByUserCode, AuthorizedByUserName, ProId, ProcName, AuthorizedUserCode, AuthorizedUserName);
            //发送邮件
            //定义两个变量，将title以及body使用字符串方式拼接出来
            JC_FinalistApprovalInfo finalistApprovalInfo = Pkurg.PWorldBPM.Business.BIZ.JC.JC_FinalistApproval.GetJC_FinalistApprovalInfoByFormId(ProId);

            string emailTitle = finalistApprovalInfo.ReportDept + finalistApprovalInfo.ArchiveName + "于" + finalistApprovalInfo.ArchiveDate + "发起的" + "【入围申请单】" + finalistApprovalInfo.ProjectName + "流程已审批结束！";
            //得到实例ID号
            DataTable dt = Pkurg.PWorldBPM.Business.Controls.WF_WorkFlowInstances.GetWorkFlowInstanceByFormID(ProId);
            DataRow dataRow = dt.Rows[0];
           
            string WorkFlowInstancesID= dataRow["InstanceID"].ToString();

            string ID =url + "/Workflow/ViewPage/V_JC_FinalistApproval.aspx?id=" + WorkFlowInstancesID;

            //string emailFinallyTitleFormat = @"emailTitle";
            string emailFinallyBodyFormat = @"您好！
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + emailTitle + @"
       <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;若要查看审批意见点击此处：&nbsp;&nbsp;&nbsp;&nbsp;<a href='" + ID + @"'>查看</a>
         <br/>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;谢谢
          <br/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";


            bool eResult = mailService.SendEmailCustom(item.Email, emailTitle, emailFinallyBodyFormat);
        }
    }
    /// <summary>
    /// 正式系统要开启
    /// </summary>
    /// <param name="k2_workflowId"></param>
    private void DoSendEmail(int k2_workflowId)
    {
        try
        {
            MailService mailService = new MailService();
            bool eResult = mailService.SendEndEmail(k2_workflowId);
            Logger.logger.DebugFormat("Email--Result:{0}", eResult);
        }
        catch (Exception ex)
        {
            Logger.logger.DebugFormat("Email--Result:{0}", false);
            Logger.logger.DebugFormat("Email--Result-e:{0}", ex.StackTrace);
        }
    }
}
