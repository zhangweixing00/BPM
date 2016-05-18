using Pkurg.PWorldBPM.Business.Sys;

/// <summary>
///CustomWorkflowUserInfo 的摘要说明
/// </summary>
public class CustomWorkflowUserInfo
{
    public bool IsApproval { get; set; }
    public V_Pworld_UserInfo UserInfo { get; set; }

    public string LoginName
    {
        get
        {
            if (UserInfo==null)
            {
                return "";
            }
            return UserInfo.LoginName;
        }
    }

    public string EmployeeName
    {
        get
        {
            if (UserInfo==null)
            {
                return "";
            }
            return UserInfo.EmployeeName;
        }
    }

}