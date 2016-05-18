using System;
using System.Collections.Generic;
using System.Web.UI;
using Pkurg.PWorldBPM.Common;

/// <summary>
///UWorkflowFormBase 的摘要说明
/// </summary>
public class UWorkflowFormBase : UControlBase
{
    public UWorkflowFormBase()
    {
        FlowParams = new WF_FieldDataInfo();
        ApprovalControls = new List<System.Web.UI.UserControl>();
    }

    public string PartComment { get; set; }

    /// <summary>
    /// 为审批表单设置
    /// </summary>
    public List<System.Web.UI.UserControl> ApprovalControls { get; set; }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    public string ApprovalText
    {
        get
        {
            if (K2_TaskItem == null)
            {
                return "";
            }
            foreach (UserControl item in ApprovalControls)
            {
                IApprovalBox box = item as IApprovalBox;
                if (box.Node == K2_TaskItem.ActivityInstanceDestination.Name)
                {
                    return box.OptionText;
                }
            }
            return "";
        }
    }

    public string ApprovalTextBoxId
    {
        get
        {
            if (K2_TaskItem == null)
            {
                return "";
            }
            foreach (UserControl item in ApprovalControls)
            {
                IApprovalBox box = item as IApprovalBox;
                if (box.Node == K2_TaskItem.ActivityInstanceDestination.Name)
                {
                    return box.OptionId;
                }
            }
            return "";
        }
    }

    public string ProcName { get; set; }

    protected override void LoadViewState(object savedState)
    {
        Object[] datas = savedState as Object[];
        base.LoadViewState(datas[0]);
        if (datas[1] != null)
        {
            ProcName = datas[1].ToString();
        }
        if (datas[2] != null)
        {
            SerialNumber = datas[2].ToString();
        }
    }
    protected override object SaveViewState()
    {
        return new Object[] { base.SaveViewState(), ProcName, SerialNumber };
    }

    public string SerialNumber { get; set; }

    public WF_FieldDataInfo FlowParams { get; set; }

    public void AppendParams()
    {
        SetParams();
        if (!string.IsNullOrEmpty(_BPMContext.Sn) && K2_TaskItem != null)
        {
            if (FlowParams.WorkflowFieldDatas.Count != 0)
            {
                WorkflowHelper.UpdateDataFields(_BPMContext.Sn, FlowParams.WorkflowFieldDatas);
            }
        }
    }
    
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        InitFormData();

        if (!IsPostBack)
        {
            if (_BPMContext.ProcInst == null)
            {
                SerialNumber = BPMHelp.GetSerialNumber("SQ_");
            }
            else
            {
                SerialNumber = _BPMContext.ProcInst.FormId;
            }
           
        }
    }

    /// <summary>
    /// 初始化业务表单数据
    /// </summary>
    protected virtual void InitFormData()
    {
        ///还原数据
        if (_BPMContext.InstDataInfo != null)
        {
            PageParseHelper parseHelper = new PageParseHelper(_BPMContext.InstDataInfo.DataInfo);
            parseHelper.BindValue(this);
        }
    }

    /// <summary>
    /// 保存业务表单数据
    /// </summary>
    public virtual void SaveFormData()
    {
        if (_BPMContext.InstDataInfo != null)
        {
            _BPMContext.InstDataInfo.DataInfo.Clear();
        }
        else
            _BPMContext.InstDataInfo = new Pkurg.PWorldBPM.Common.Info.ProcInstDataInfo();

        PageParseHelper parseHelper = new PageParseHelper(_BPMContext.InstDataInfo.DataInfo);
        parseHelper.CollectionValue(this);
    }

    /// <summary>
    /// 参数处理
    /// </summary>
    public virtual void SetParams()
    {

    }
}