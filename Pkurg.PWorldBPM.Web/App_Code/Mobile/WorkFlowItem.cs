/// <summary>
/// 移动OA返回到前台的实体
/// </summary>
public class WorkFlowItem
{
    //公共属性
    public string InstanceId{ get; set; }
    public string FormTitle { get; set; }
    public string DetailUrl { get; set; }

    /// <summary>
    /// 待办特有属性
    /// </summary>
    public string SN{ get; set; }
    public string ReceiveTime { get; set; }
    public string CreateByUserName { get; set; }

    //已办特有属性
    public string TodoUser { get; set; }
    public string ApproveAtTime { get; set; }

    //归档
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string CreatorName { get; set; }
}