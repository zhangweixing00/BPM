using System.Linq;

/// <summary>
///WF_FieldDataInfo 的摘要说明
/// </summary>
public class WF_FieldDataInfo
{
    public System.Collections.Specialized.NameValueCollection WorkflowFieldDatas { get; set; }
	public WF_FieldDataInfo()
	{
        WorkflowFieldDatas = new System.Collections.Specialized.NameValueCollection();
	}
    public void SetParams(string key,string value)
    {
        if (WorkflowFieldDatas.AllKeys.Contains(key))
        {
            WorkflowFieldDatas[key] = value;
        }
        else
        {
            WorkflowFieldDatas.Add(key, value);
        }
    }
}