using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

public partial class Modules_TurnGroup_DoTurnGroup : UPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlGroups.DataSource = BizContext.V_ITSupport_ITGroup.ToList();
            ddlGroups.DataTextField = "Name";
            ddlGroups.DataValueField = "Id";
            ddlGroups.DataBind();
            ShowGroupUsers();

        }
    }
    protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowGroupUsers();
    }

    private void ShowGroupUsers()
    {
        StringBuilder displayUsers = new StringBuilder();
        var userList = ITSupportCommon.GetUserListByGroupId(int.Parse(ddlGroups.SelectedItem.Value));
        foreach (var item in userList)
        {
            displayUsers.AppendFormat("{0},", item.EmployeeName);
        }
        Div_groupUsers.InnerHtml = "组内成员：" + displayUsers.ToString().TrimEnd(',');
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        NameValueCollection kvs = new NameValueCollection();
        kvs.Add("GroupUsers", ITSupportCommon.GetK2DataFieldByGroupId(int.Parse(ddlGroups.SelectedItem.Value)));
        WorkflowHelper.UpdateDataFields(_BPMContext.Sn, kvs);
        string Opinion = Request["optionTxt"];
        var appInfo = AppflowFactory.GetAppflow(_BPMContext.ProcID);
        appInfo.IsFromPC = true;
        string groupId=BizContext.V_ITSupport_Catalog.FirstOrDefault(x => x.Id.ToString() == ddlGroups.SelectedValue).GroupId.Value.ToString();
        appInfo.AssistObject = groupId;
        bool isSuccess = appInfo.StartApproval(_BPMContext.Sn, _BPMContext.ProcID, Opinion, "转出");
        if (isSuccess)
        {
            var formInfo = BizContext.OA_ITSupport_Form.FirstOrDefault(x => x.FormID == _BPMContext.ProcInst.FormId);
            formInfo.ProcessGroupId = groupId;

            BizContext.SubmitChanges();
        }
        DisplayMessage.ExecuteJs(string.Format("window.returnValue = {0};window.close();", isSuccess ? "1" : "0"));
    }
}