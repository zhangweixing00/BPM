using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Business.Controls;
using Pkurg.PWorldBPM.Common.Info;

public partial class Workflow_Modules_FlowRelated_ShowCanSelectItems :UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadingData();
        }
    }

    private void LoadingData()
    {
        UserInfo userInfo = _BPMContext.CurrentUser;
        if (userInfo != null)
        {
            //IList<Pkurg.BPM.Entities.VUserProcInsts> infos = Pkurg.PWorldBPM.Business.Controls.WF_Relation.GetProcListBelongToUser("star");
           // IList<Pkurg.BPM.Entities.VUserProcInsts> infos = Pkurg.PWorldBPM.Business.Controls.WF_Relation.GetProcListBelongToUserById(userInfo.Id);
            List<WF_RelationInfo> infos = WF_Relation.GetProcListByUserCode(userInfo.PWordUser.EmployeeCode);
            //agvProcData.KeyFieldName = "ProcID";

            string keys = Request["keys"];
            if (!string.IsNullOrEmpty(keys))
            {
                List<string> ids = keys.Split(',').ToList();
                List<WF_RelationInfo>  newInfos = infos.Where(item => !ids.Contains(item.ProcId)).ToList();
                agvProcData.DataSource = newInfos.OrderByDescending(x=>x.EndTime).ToList();
            }
            else
            {
                agvProcData.DataSource = infos.OrderByDescending(x=>x.EndTime).ToList();
            }
            agvProcData.DataBind();
        }
    }
    public string GetViewProcInstPageUrl()
    {
        return System.Configuration.ConfigurationManager.AppSettings["ViewProcInstPageUrl"];
    }

    protected void lbSelected_Command(object sender, CommandEventArgs e)
    {
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();",Newtonsoft.Json.JsonConvert.SerializeObject(e.CommandArgument.ToString())));
    }
    protected void agvProcData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        agvProcData.PageIndex = e.NewPageIndex;
        LoadingData();
    }
}