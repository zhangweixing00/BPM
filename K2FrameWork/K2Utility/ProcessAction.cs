using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2Utility
{
    /// <summary>
    /// process action,即表单上的按钮类型
    /// </summary>
    public enum ProcessAction
    {

        /// <summary>
        /// 提交
        /// </summary>
        Submit,
        /// <summary>
        /// HRBP提交数据
        /// </summary>
        SubmitHRBP,

        /// <summary>
        /// 保存草稿
        /// </summary>
        SaveDraft,

        /// <summary>
        /// 重新发起提交
        /// </summary>
        Rework,

        /// <summary>
        /// 保存
        /// </summary>
        Save,

        /// <summary>
        /// 提交草稿
        /// </summary>
        SubmitDraft,

        /// <summary>
        /// 审批
        /// </summary>
        Approve,

        /// <summary>
        /// 拒绝
        /// </summary>
        Reject,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel,

        /// <summary>
        /// 确认
        /// </summary>
        Confirm,
        /// <summary>
        /// HRBP草稿提交
        /// </summary>

        SubmitHRBPDraft,

        /// <summary>
        /// 自定义流程提交
        /// </summary>
        SubmitCF,

        /// <summary>
        /// 审批保存
        /// </summary>
        ApproveSave,
        /// <summary>
        /// HR Save
        /// </summary>
        SaveHRDraft,
        /// <summary>
        /// 发起会签
        /// </summary>
        StartCounter,

        //王凤龙添加
        /// <summary>
        /// 保存可查看
        /// </summary>
        DraftHR,

        /// <summary>
        /// 保存已发起流程
        /// </summary>
        SubmitHRCB
        ///// <summary>
        ///// 离职发起流程按钮
        ///// </summary>
        //HRCBSu
    }
}
