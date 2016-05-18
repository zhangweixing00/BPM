using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.K2
{
    public enum EnumActionWeightType
    {
        P,//使用百分比
        R,//使用数量，当为C时，ActionWeight为1
        N//不使用权重
    }
}