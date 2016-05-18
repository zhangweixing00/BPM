/// <summary>
///传递给流程的DataField
/// </summary>
public class CustomWorkflowDataField
{
    public long ActivityID { get; set; }
    public string ActivityName { get; set; }
    public string Users { get; set; }
    public int OrderId { get; set; }
    public bool IsFinished { get; set; }

    public bool IsOpenApprovalNum { get; set; }
    public int ApprovalNum { get; set; }

    /// <summary>
    /// 备用
    /// </summary>
    public bool IsOpen { get; set; }
}