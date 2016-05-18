using System.Collections.Generic;
using Pkurg.PWorldBPM.Business.Sys;

/// <summary>
///DetailInfo 的摘要说明
/// </summary>
public class DetailInfo
{
    public string FlowType { get; set; }
    public string FormTitle { get; set; }
    public string StartUserName { get; set; }
    public string StartDeptName { get; set; }
    public string StartTime { get; set; }
    public string Content { get; set; }
    public string DetailUrl { get; set; }
    public List<DetailStepInfo> StepInfos { get; set; }
    public List<BPM_Attachment> AttachmentInfos { get; set; }
}