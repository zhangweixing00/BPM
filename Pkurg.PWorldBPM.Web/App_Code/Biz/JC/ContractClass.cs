using System;
using System.Collections.Generic;

/// <summary>
/// 租赁合同状态更新
/// </summary>
public class ContractClass
{
    /// <summary>
    /// 流程最后确认，更新合同编号
    /// 2015-08-05 yanghechun
    /// </summary>
    /// <param name="bizType">合同类型</param>
    /// <param name="bizId">合同ID(主键int)</param>
    /// <param name="contract_No">合同编号</param>
    /// <returns></returns>
    public void UpdateContractNo(int bizType, int bizId, string contract_No)
    {
        try
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["BPWebService"].ToString();
            string menthod = "UpdateContractNo";
            List<object> args = new List<object>();

            args.Add(bizType);
            args.Add(bizId);
            args.Add(contract_No);

            Dynamic.InvokeWebService(url, menthod, args.ToArray());
        }
        catch (Exception)
        {

        }
    }

    /// <summary>
    /// 提交流程
    /// </summary>
    /// <param name="bpmUrl">合同链接</param>
    /// <param name="bizType">合同类型</param>
    /// <param name="bizId">合同主键</param>
    /// <returns></returns>
    public void SubmitWorkFlow(string bpmUrl, int BizType, int BizId)
    {
        try
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["BPWebService"].ToString ();
            string menthod = "SubmitWorkFlow";
            List<object> args = new List<object> ();

            args.Add (bpmUrl);
            args.Add (BizType);
            args.Add(BizId);

            Dynamic.InvokeWebService (url, menthod, args.ToArray ());
        }
        catch (Exception)
        {

        }
    }

    /// <summary>
    /// 结束流程
    /// </summary>
    /// <param name="result">审批结果(审批通过=4，审批不通过=5,终止=6)</param>
    /// <param name="bizType">合同类型</param>
    /// <param name="bizId">合同主键</param>
    /// <returns></returns>
    public void FinishWorkFlow(int result, int BizType, int BizId)
    {
        string url = System.Configuration.ConfigurationManager.AppSettings["BPWebService"].ToString();
        string menthod = "FinishWorkFlow";
        List<object> args = new List<object> ();

        args.Add (result);
        args.Add(BizType);
        args.Add(BizId);

        Dynamic.InvokeWebService (url, menthod, args.ToArray ());
    }
}