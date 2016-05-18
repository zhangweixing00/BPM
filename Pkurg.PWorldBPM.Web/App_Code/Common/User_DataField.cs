/// <summary>
///K2流程参数实体
/// </summary>
public class K2_DataFieldInfo
{
    public K2_DataFieldInfo()
    {
        IsHaveToExsit = false;
        IsRepeatIgnore = false;
        OrderId = -1;//默认不参与排序（去重）
    }

    /// <summary>
    /// DataField名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 部门ID
    /// </summary>
    public string DeptCode { get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; }
    /// <summary>
    /// 角色必须存在
    /// </summary>
    public bool IsHaveToExsit { get; set; }
    /// <summary>
    /// 如果角色重复是否忽略
    /// </summary>
    public bool IsRepeatIgnore { get; set; }

    /// <summary>
    /// 角色不存在提示；如果不设置取角色名称
    /// </summary>
    private string noExsitRoleTip;
    public string NoExsitRoleTip
    {
        get
        {
            if (string.IsNullOrEmpty(noExsitRoleTip))
            {
                return string.Format("{0}尚未配置！", RoleName);
            }
            return noExsitRoleTip;
        }
        set { noExsitRoleTip = value; }
    }
    /// <summary>
    /// 最终传到K2的值
    /// </summary>
    public string Result { get; set; }

    public int OrderId { get; set; }
}