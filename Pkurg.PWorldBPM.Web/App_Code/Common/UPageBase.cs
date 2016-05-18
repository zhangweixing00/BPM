using System;
using System.Linq;
using Pkurg.PWorldBPM.Business.BIZ;
using Pkurg.PWorldBPM.Business.Sys;
using Pkurg.PWorldBPM.Common.Info;
using SourceCode.Workflow.Client;

/// <summary>
///UPageBase 的摘要说明
/// </summary>
public class UPageBase : System.Web.UI.Page
{
    public Pkurg.PWorldBPM.Common.Context.BPMContext _BPMContext { get; set; }

    public BizFormDBDataContext BizContext { get; set; }
    public SysDBDataContext SysContext { get; set; }
    public UPageBase()
    {
        _BPMContext = new Pkurg.PWorldBPM.Common.Context.BPMContext();
        _BPMContext.OrgService = new Pkurg.PWorldBPM.Common.Services.OrgService();
        _BPMContext.ProcService = new Pkurg.PWorldBPM.Common.Services.BPMProcService();

        //
        BizContext = new BizFormDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
        SysContext = new SysDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["BPMConnectionString"].ConnectionString);
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        //移动OA的流程查看页面，开启匿名登录，是不能获取用户信息的。
        //yanghechun 2015-11-03 可能会影响个别的View页面的获取用户。
        if (!Request.CurrentExecutionFilePath.Contains("/Workflow/ViewPage"))
        {
            _BPMContext.LoginId = new IdentityUser().GetEmployee().LoginId;
        }

        //_BPMContext.LoginId = new IdentityUser().GetEmployee().LoginId;
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Init_K2_TaskItem();
    }
    protected override void LoadViewState(object savedState)
    {
        Object[] datas = savedState as Object[];
        _BPMContext.ProcID = datas[1] == null ? null : datas[1].ToString();
        base.LoadViewState(datas[0]);
    }

    protected override object SaveViewState()
    {
        return new Object[]
        {
            base.SaveViewState(),
            _BPMContext.ProcID
        };
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

    /// <summary>
    /// 根据登录名获取用户实体
    /// 有缓存
    /// </summary>
    /// <returns></returns>
    protected UserInfo GetEmployee()
    {
        IdentityUser identityUser = new IdentityUser();
        return identityUser.GetEmployee();
    }


    public Pkurg.PWorld.Entities.Employee CurrentEmployee
    {
        get
        {
            return new IdentityUser().GetEmployee().PWordUser;
        }
    }

    /// <summary>
    /// 是否流程制度管理员(不是分类管理员)
    /// left.ascx也有
    /// </summary>
    public bool IsRuleAdmin
    {
        get
        {
            bool flag = false;
            //全局管理员
            Pkurg.PWorldBPM.WorkFlowRule.Setting db = new Pkurg.PWorldBPM.WorkFlowRule.Setting();
            string admin = db.GetValueByName("Rule_Admin");
            string userCode = GetEmployee().LoginId;
            flag = admin.Split(',').Contains(userCode);
            if (!flag)
            {
                //流程分类管理员
                //Pkurg.PWorldBPM.WorkFlowRule.Rule rule = new Pkurg.PWorldBPM.WorkFlowRule.Rule();
                //flag = rule.CheckIsRuleAdmin(CurrentEmployee.EmployeeCode);
            }
            return flag;
        }
    }

    private System.Collections.Specialized.NameValueCollection _DFS;

    public System.Collections.Specialized.NameValueCollection DFS
    {
        get
        {
            if (_DFS == null)
            {
                try
                {
                    if (string.IsNullOrEmpty(_BPMContext.WorkflowId))
                    {
                        throw new Exception("UnStart");
                    }
                    _DFS = WorkflowHelper.GetProcessInstanceAllDataFields(int.Parse(_BPMContext.WorkflowId));
                }
                catch
                {
                    return new System.Collections.Specialized.NameValueCollection();
                }
            }
            return _DFS;
        }
        set { _DFS = value; }
    }
}