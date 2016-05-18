using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.K2
{
    /// <summary>
    /// 节点的审批类型
    /// </summary>
    public enum EnumActivityType
    {
        SP,//审批
        JQB,//前加签
        JQA,//后加签
        HQ//会签
    }
}