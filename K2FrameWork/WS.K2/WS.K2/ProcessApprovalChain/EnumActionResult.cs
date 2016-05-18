using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.K2
{
    /// <summary>
    /// 任务操作类型
    /// </summary>
    public enum EnumActionResult
    {
        Approve,//同意
        Decline,//拒绝到发起节点
        GoBack,//退回到某个节点
        Cancel//取消
    }
}