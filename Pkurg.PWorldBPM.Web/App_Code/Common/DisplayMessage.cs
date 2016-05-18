using System.Web;
using System.Web.UI;

/// <summary>
///DisplayMessage 的摘要说明
/// </summary>
public class DisplayMessage
{
	public DisplayMessage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static void ExecuteJs(string js)
    {
        ScriptManager.RegisterClientScriptBlock((Page)HttpContext.Current.CurrentHandler, typeof(string), "1", js, true);
    }
}