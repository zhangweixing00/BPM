using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace K2Utility
{
    /// <summary>
    /// 定义流程状态
    /// </summary>
    public enum ProcessStatus
    {
        [Description("草稿")]
        Draft,
        [Description("已提交")]
        HRBPSubmit,
        [Description("处理中")]
        Running,
        [Description("拒绝")]
        Rejected,
        [Description("取消")]
        Cancelled,
        [Description("已完成")]
        Finished,
        //王凤龙添加
        [Description("保存可查看")]
        SaveDraft,
        [Description("HRCB")]
        SubmitHRCB,
        //PMS状态
        [Description("未生效")]
        Unvalid

    }



    public enum ActivityStatus
    {

        Unhandled,
        Processing,
        Finished

    }

    public enum StateString
    {
        [Description("未处理")]
        未处理,
        [Description("处理中")]
        处理中,
        [Description("已完成")]
        已完成
    };
}
