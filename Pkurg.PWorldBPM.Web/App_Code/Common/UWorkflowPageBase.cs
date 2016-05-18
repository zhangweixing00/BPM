using System;

/// <summary>
///UWorkflowPageBase 的摘要说明
/// </summary>
public class UWorkflowPageBase : UPageBase
{
    public Pkurg.BPM.Entities.AppDict AppInfo { get; set; }
    public UWorkflowFormBase FromControl { get; set; }

    public UWorkflowPageBase()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        InitWorkFlowForm();
    }

    private void InitWorkFlowForm()
    {
        string appId = "";
        if (_BPMContext.ProcInst == null)
        {
            appId = Request["appid"];
            if (string.IsNullOrEmpty(appId))
            {
                //appid 
                //ExceptionHander.GoToErrorPage();
                return;
            }
        }
        else
        {
            appId = _BPMContext.ProcInst.AppCode;
        }
        //新建流程
        Pkurg.BPM.Entities.AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId(appId);
        if (appInfo == null)
        {
            //appID错误
            ExceptionHander.GoToErrorPage();
            return;
        }

        AppInfo = appInfo;
        string appControlPath = "";

        if (Request.Url.AbsoluteUri.Contains("Start.aspx"))
        {
            appControlPath = "~/Workflow/EditPage/Forms/" + AppInfo.FormName;
        }
        else
            appControlPath = "~/Workflow/ViewPage/Forms/" + AppInfo.FormName;

        UWorkflowFormBase control = LoadControl(appControlPath) as UWorkflowFormBase;
        FromControl = control;
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        //加载之前先设置控件是否可用
        if (K2_TaskItem != null)
        {
            ControlHelper helper = new ControlHelper(K2_TaskItem.ActivityInstanceDestination.Name);
            helper.SettingPagePesmission(this);
        }
    }
}