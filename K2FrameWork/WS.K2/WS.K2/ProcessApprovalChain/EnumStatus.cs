using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.K2
{
    /// <summary>
    /// 节点、用户运行时状态
    /// </summary>
    public enum EnumStatus
    {
        Running,//运行中
        Available,//未执行
        Completed//已完成
    }
}