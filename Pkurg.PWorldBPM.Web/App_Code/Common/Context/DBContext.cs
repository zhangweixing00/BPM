using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Sys;

/// <summary>
///DBContext 的摘要说明
/// </summary>
public class DBContext
{
	public DBContext()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static SysDBDataContext GetSysContext()
    {
        return  new SysDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
    }
    public static BizFormDBDataContext GetBizContext()
    {
        return new BizFormDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
    }

}