using System;
using System.Collections.Generic;
using System.Web.UI;
using Pkurg.PWorld.Entities;
using Pkurg.PWorldBPM.Business.Workflow;
using Pkurg.PWorldBPM.Common;

/// <summary>
///流程查看页面基类
/// </summary>
public class V_WorkflowFormBase : UPageBase
{
    public string AppID;

    protected WF_WorkFlowInstance wf_WorkFlowInstance = new WF_WorkFlowInstance();

    /// <summary>
    /// 流程FormId
    /// </summary>
    public string FormId
    {
        get
        {
            return ViewState["FormID"].ToString();
        }
        set
        {
            ViewState["FormID"] = value;
        }
    }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string FormTitle
    {
        get
        {
            return ViewState["FormTitle"].ToString();
        }
        set
        {
            ViewState["FormTitle"] = value;
        }
    }


    /// <summary>
    /// 发起部门
    /// </summary>
    public string StartDeptId
    {
        get
        {
            return ViewState["StartDeptId"].ToString();
        }
        set
        {
            ViewState["StartDeptId"] = value;
        }
    }


    public V_WorkflowFormBase()
    {
        Object[] objs = GetType().GetCustomAttributes(typeof(BPMAttribute), true);
        if (objs != null && objs.Length > 0)
        {
            BPMAttribute attribute = objs[0] as BPMAttribute;
            this.AppID = attribute.AppId;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (string.IsNullOrEmpty(AppID))
        {
            throw new NotImplementedException("请设置APPID");
        }
    }

    /// <summary>
    /// 标题设定
    /// </summary>
    /// <returns></returns>
    protected virtual string GetFormTitle()
    {
        if (!string.IsNullOrEmpty(FormTitle))
        {
            return FormTitle;
        }
        throw new NotImplementedException("请设置发起标题");
    }


    /// <summary>
    /// 表单的通用控件会签、集团会签、附件上传
    /// </summary>
    public List<UControlBase> CommonControlList { get; set; }

    #region 意见框处理

    private string GetApproveOpinion()
    {
        if (K2_TaskItem == null)
        {
            return "";
        }

        string opinion = "";
        List<IApprovalBox> options = GetOptions();
        foreach (var item in options)
        {
            if (item.Node == K2_TaskItem.ActivityInstanceDestination.Name)
            {
                opinion = item.OptionText;
                break;
            }
        }
        return opinion;

    }

    private List<IApprovalBox> GetOptions()
    {
        List<IApprovalBox> options = new List<IApprovalBox>();

        if (CommonControlList.Count == 0)
        {
            LoadCommonControl(null);
        }
        foreach (var item in CommonControlList)
        {
            if (item is IApprovalBox)
            {
                options.Add(item as IApprovalBox);
            }
        }
        return options;
    }

    private void LoadCommonControl(ControlCollection controls)
    {
        if (controls == null)
        {
            controls = Page.Controls;
        }

        foreach (Control item in controls)
        {
            if (item is UControlBase)
            {
                CommonControlList.Add(item as UControlBase);
            }
            else if (item.HasControls() && (item.Controls.Count > 0))
            {
                LoadCommonControl(item.Controls);
            }
        }
    }

    #endregion

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        if (_BPMContext.ProcInst != null)
        {
            FormId = _BPMContext.ProcInst.FormId;
            StartDeptId = _BPMContext.ProcInst.StartDeptCode;
            Department deptInfo = new Pkurg.PWorld.Services.DepartmentService().GetByDepartCode(StartDeptId);
            if (deptInfo != null)
            {
                StartDeptName = deptInfo.Remark;
            }
        }
    }

    public string StartDeptName { get; set; }


}
