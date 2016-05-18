<%@ WebHandler Language="C#" Class="ApprovePageHandler" %>

using System.Web;

public class ApprovePageHandler : UHanderBase
{
    public override void  DoProcessRequest(HttpContext context)
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

        string status = Request["status"];
        if (status != "拟稿" && status != "发起申请")
        {
            //验证sn
            if (K2_TaskItem == null)
            {
                //System.Web.UI.ScriptManager.RegisterClientScriptBlock(null, typeof(string), "1", "alert('该环节已审批结束'); window.close();", true);
                context.Response.Write("<script>alert('该环节已审批结束'); window.close();</script>");
                return;
            }
        }
        else
        {
            if (_BPMContext.ProcInst==null||_BPMContext.ProcInst.Status=="5")
            {
                context.Response.Write("<script>alert('该环节不存在'); window.close();</script>");
                return;
            }
        }

        string urlParams = string.Format("sn={1}&id={0}", instanceId, _BPMContext.Sn, "");
        string urlPage = appInfo.FormName;
        if (appInfo.FormName.ToLower().Contains(".ascx"))
        {
            if (status == "拟稿" || status == "发起申请")
            {
                urlPage = System.Configuration.ConfigurationManager.AppSettings["StartProcInstPageUrl"];
            }
            else
                urlPage = System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
        }
        else
        {
            if (status == "拟稿" || status == "发起申请")
            {
                urlPage = "/Workflow/EditPage/E_" + urlPage;
            }
            else
            {
                urlPage = "/Workflow/ApprovePage/A_" + urlPage;
            }
        }
        string pageUrl = string.Format("{0}?{1}", urlPage, urlParams);

        context.Response.Redirect(pageUrl);
    }


}