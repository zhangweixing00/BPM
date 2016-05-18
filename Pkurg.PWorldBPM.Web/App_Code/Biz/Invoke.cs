using System;
using Pkurg.PWorldBPM.Business.BIZ.JC;

/// <summary>
///终止操作调用方法
/// </summary>
public class Invoke
{
    /// <summary>
    /// 如果业务系统需要终止流程
    /// 需要根据appId实现业务系统的终止逻辑
    /// 最好在这个Invoke类新建一个方法，在case里面调用
    /// </summary>
    /// <param name="k2Sn">k2流程ID</param>
    /// <param name="instanceID">实例ID</param>
    /// <param name="formId">formId</param>
    /// <param name="appId">AppId</param>
    public void StopWorkFlow(int k2Sn, string instanceID, string formId, string appId)
    {
        switch (appId)
        {
            case "10106":
                //集采产品审批单
                StopJc(k2Sn, instanceID,formId, appId);
                break;
            case "10105":
                //ERP付款申请单
            case "10107":
                //ERP请示单
            case "10109":
                //ERP合同单
            case "2004":
                //ERP补充协议
                new AppCode.ERP.CommonService(int.Parse(appId)).NotifyStop(instanceID);
                break;
            case "2001":
                //合同租赁
                StopLeaseContract(k2Sn, instanceID, formId, appId);
                break;
            case "3027":
                //ERP合约规划平衡
                new AppCode.ERP.CommonService(int.Parse(appId)).NotifyStop(instanceID);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 集采产品审批单
    /// yanghechun 204-12-03
    /// </summary>
    /// <param name="k2Sn"></param>
    /// <param name="instanceID"></param>
    /// <param name="formId"></param>
    /// <param name="appId"></param>
    void StopJc(int k2Sn, string instanceID, string formId, string appId)
    {
        JC_ElevatorOrderInfo model = new JC_ElevatorOrder().GetElevatorOrder(formId);
        if (model != null)
        {
            int result = 2;
            int orderType =Convert.ToInt16( model.OrderType);
            int orderId =Convert.ToInt32( model.OrderID);
            new Order().FinishWorkFlow(result, orderType, orderId);
        }
    }

    void StopLeaseContract(int k2Sn, string instanceID, string formId, string appId)
    {
        BP_LeaseContractInfo model = new BP_LeaseContract().GetLeaseContract(formId);
        if (model != null)
        {
            int result = 4;
            int bizType = Convert.ToInt16(model.BizType.GetValueOrDefault());
            int bizId = Convert.ToInt32(model.BizID.GetValueOrDefault());
            new ContractClass().FinishWorkFlow(result, bizType, bizId);
        }
    }
}