using System;
using System.Collections.Generic;

/// <summary>
/// 集采订单状态更新
/// </summary>
public class Order
{
    /// <summary>
    /// 提交流程
    /// </summary>
    /// <param name="bpmUrl">BPM流程链接</param>
    /// <param name="orderType">订单类型(电梯=1，入户门=2)</param>
    /// <param name="orderId">订单ID(主键int)</param>
    /// <returns></returns>
    public void SubmitWorkFlow (string bpmUrl, int orderType, int orderId)
    {
        try
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["JCWebService"].ToString ();
            string menthod = "SubmitWorkFlow";
            List<object> args = new List<object> ();

            args.Add (bpmUrl);
            args.Add (orderType);
            args.Add (orderId);

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
    /// <param name="orderType">订单类型(电梯=1，入户门=2)</param>
    /// <param name="orderId">订单ID(主键int)</param>
    /// <returns></returns>
    public void FinishWorkFlow (int result, int orderType, int orderId)
    {
        string url = System.Configuration.ConfigurationManager.AppSettings["JCWebService"].ToString ();
        string menthod = "FinishWorkFlow";
        List<object> args = new List<object> ();

        args.Add (result);
        args.Add (orderType);
        args.Add (orderId);

        Dynamic.InvokeWebService (url, menthod, args.ToArray ());
    }
}