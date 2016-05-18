using System;

/// <summary>
///ProcInst 的摘要说明
/// </summary>
namespace Pkurg.PWorldBPM.Common.Info
{
    public class ContextProcInst
    {
        public ContextProcInst()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string FormId { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ProcId { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// 额外数据
        /// </summary>
        public  string FormData { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        public string AppType { get; set; }

        /// <summary>
        /// k2
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 应用
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 发起部门
        /// </summary>
        public string StartDeptCode { get; set; }
    }
}