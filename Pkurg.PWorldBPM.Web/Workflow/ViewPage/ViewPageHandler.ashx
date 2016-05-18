<%@ WebHandler Language="C#" Class="ViewPageHandler" %>

using System.Web;

public class ViewPageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest Request = context.Request;
        string instanceId = Request["id"];
        Pkurg.BPM.Entities.WorkFlowInstance instance = new Pkurg.PWorldBPM.Business.Workflow.WF_WorkFlowInstance().GetWorkFlowInstanceById(instanceId);
        if (instance == null || string.IsNullOrEmpty(instance.AppId))
        {
            ExceptionHander.GoToErrorPage();
            return;
        }

        Pkurg.BPM.Entities.AppDict appInfo = new Pkurg.BPM.Services.AppDictService().GetByAppId(instance.AppId);
        if (appInfo == null || string.IsNullOrEmpty(appInfo.FormName))
        {
            ExceptionHander.GoToErrorPage();
            return;
        }

        string urlParams = string.Format("id={0}&InstanceID={0}", instanceId);
        string urlPage = appInfo.FormName;
        if (appInfo.FormName.ToLower().Contains(".ascx"))
        {
            urlPage = System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
        }
        else
        {

            urlPage = "/Workflow/ViewPage/V_" + urlPage;
            
        }
        string pageUrl= string.Format("{0}?{1}", urlPage, urlParams);

        context.Response.Redirect(pageUrl);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}