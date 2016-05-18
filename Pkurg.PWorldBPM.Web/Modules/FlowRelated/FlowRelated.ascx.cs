using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Common;

public partial class Workflow_Modules_FlowRelated_FlowRelated : UControlBase
{
    /// <summary>
    /// 服务器load时设置ID，控件再load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh();
        }
    }
    public string GetViewProcInstPageUrl(string instance)
    {
        return System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
    }
    public string GetViewProcInstPageUrl()
    {
        return System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
    }

    protected override void OnPreRender(EventArgs e)
    {
        hidProcIDForRelated.Value = _BPMContext.ProcID;
        StringBuilder content = new StringBuilder();
        if (RelationInfos != null)
        {
            foreach (var item in RelationInfos)
            {
                content.AppendFormat("{0},", item.RelatedFlowId);
            }
        }
        content.AppendFormat("{0},", _BPMContext.ProcID);
        hidRelatedFlowProcIdList.Value = content.ToString().TrimEnd(',');

        base.OnPreRender(e);
    }

    private void Refresh()
    {
        if (string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            //新建或者参数错误

            return;
        }

        IList<Pkurg.BPM.Entities.FlowRelated> infos = Pkurg.PWorldBPM.Business.Controls.WF_Relation.GetRelatedInstsByInstId(_BPMContext.ProcID);
        RelationInfos = infos;
        dlRelatedFlow.DataSource = infos;
        dlRelatedFlow.DataBind();
    }

    public IList<Pkurg.BPM.Entities.FlowRelated> RelationInfos { get; set; }

    protected void dlRelatedFlow_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        string rId = e.CommandArgument.ToString();
        Pkurg.PWorldBPM.Business.Controls.WF_Relation.DelRelatedFlowInfo(int.Parse(rId));
        Refresh();
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {

    }
    //public string VirtualRootPath { get; set; }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_BPMContext.ProcID))
        {
            return;
        }
        string currentInstId = _BPMContext.ProcID;
        string[] procIds = hidRelatedFlowProcIdList.Value.Split(',');
        //当前流程已有的关联流程

        List<string> ListProcId = new List<string>();
        foreach (string procid in procIds)
        {
            Pkurg.BPM.Entities.FlowRelated info = Pkurg.PWorldBPM.Business.Controls.WF_Relation.GetRelatedFlowInfo(currentInstId.ToString(), procid);
            if (info == null)
            {
                Pkurg.BPM.Entities.WorkFlowInstance inst = new Pkurg.BPM.Services.WorkFlowInstanceService().GetByInstanceId(procid);
                if (inst == null)
                {
                    continue;
                }
                info = new Pkurg.BPM.Entities.FlowRelated()
                {
                    FlowId = currentInstId,
                    RelatedFlowId = procid,
                    CreateTime = DateTime.Now,
                    // CreatorId=_BPMContext.CurrentUser.Id,
                    CreatorName = _BPMContext.CurrentUser.Name,
                    RelatedFlowName = inst.FormTitle,
                    RelatedFlowCreator = inst.CreateByUserName,
                    RelatedFlowEndTime = inst.FinishedTime
                };
                Pkurg.PWorldBPM.Business.Controls.WF_Relation.AddRelatedFlowInfo(info);
            }
        }

        Refresh();
    }
}