using System.Web;
using System.Web.SessionState;
using SourceCode.Workflow.Client;

/// <summary>
///UPageBase 的摘要说明
/// </summary>
public class UHanderBase : IHttpHandler, IRequiresSessionState
{
    public Pkurg.PWorldBPM.Common.Context.BPMContext _BPMContext { get; set; }

    public UHanderBase()
    {
        _BPMContext = new Pkurg.PWorldBPM.Common.Context.BPMContext();
        _BPMContext.OrgService = new Pkurg.PWorldBPM.Common.Services.OrgService();
        _BPMContext.ProcService = new Pkurg.PWorldBPM.Common.Services.BPMProcService();

    }

    public void ProcessRequest(HttpContext context)
    {
        InitUser();
        DoProcessRequest(context);
    }

    public virtual void DoProcessRequest(HttpContext context)
    {

    }

    protected void InitUser()
    {
        _BPMContext.LoginId = new IdentityUser().GetEmployee().LoginId;
    }

    private WorklistItem _K2_TaskItem;

    public WorklistItem K2_TaskItem
    {
        get
        {
            Init_K2_TaskItem();
            return _K2_TaskItem;
        }
    }

    private void Init_K2_TaskItem()
    {
        if (_K2_TaskItem == null)
        {

            if (!string.IsNullOrEmpty(_BPMContext.Sn))
            {
                try
                {
                    _K2_TaskItem = WorkflowHelper.GetWorklistItemWithSN(_BPMContext.Sn, _BPMContext.CurrentUser.LoginId);
                }
                catch
                {

                }
            }
        }
    }

    protected bool IsDebug
    {
        get
        {

            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["IsDebug"]))
            {
                return System.Configuration.ConfigurationManager.AppSettings["IsDebug"].ToString() == "1";
            }
            else
            {
                return false;
            }

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}