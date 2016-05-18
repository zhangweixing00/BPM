using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Pkurg.PWorldBPM.Common;

public partial class Modules_Custom_ShowUserNames : UControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitUserNames();
        }
    }

    private void InitUserNames()
    {
        if (string.IsNullOrWhiteSpace(UserList))
        {
            return;
        }

        List<CustomWorkflowUserInfo> userList = UserList.ToUserList();

        if (userList != null)
        {
            DLUserData.DataSource = userList;
            DLUserData.DataBind();
            Div_NoUsers.Visible = userList.Count <= 0;
        }
        else
        {
            Div_NoUsers.Visible = true;
        }

        //StringBuilder content = new StringBuilder();
        //string[] userIds = UserList.Split(',');
        //foreach (var item in userList)
        //{
        //    string displayUserTxt = GetDisplayText(item);
        //    if (!string.IsNullOrWhiteSpace(displayUserTxt))
        //    {
        //        content.AppendFormat("{0},", displayUserTxt);
        //    }
        //}
        //UserFlow.InnerHtml = content.ToString().TrimEnd(',');
    }

    //private string GetDisplayText(V_Pworld_UserInfo item)
    //{
    //    StringBuilder displayTxt = new StringBuilder();
    //    displayTxt.AppendFormat("<span id='U_{0}' uid='{0}'>{1}</span>", item.LoginName, item.EmployeeName);
    //    if (IsShowDelete)
    //    {
    //        displayTxt.AppendFormat("<a");
    //    }
    //}

    private string userList;

    public string UserList
    {
        get { return userList; }
        set
        {
            userList = value;
            InitUserNames();
        }
    }

    /// <summary>
    /// 用户xml数据
    /// </summary>
    //public string UserList { get; set; }

    /// <summary>
    /// 是否要显示删除按钮
    /// </summary>
    private bool isShowDelete = false;
    public bool IsShowDelete
    {
        get { return isShowDelete; }
        set { isShowDelete = value; }
    }

    protected override void LoadViewState(object savedState)
    {
        object[] stateData = savedState as object[];
        if (stateData == null)
        {
            base.LoadViewState(savedState);
        }
        else
        {
            base.LoadViewState(stateData[0]);
        }

        if (stateData.Length > 1 && stateData[1] != null)
        {
            UserList = stateData[1].ToString();
        }
        if (stateData.Length > 2 && stateData[2] != null)
        {
            IsShowDelete = bool.Parse(stateData[2].ToString());
        }
    }
    protected override object SaveViewState()
    {
        return new object[] { base.SaveViewState(), UserList, IsShowDelete };
    }


    protected void lbtnDelte_Command(object sender, CommandEventArgs e)
    {
        List<CustomWorkflowUserInfo> userList = UserList.ToUserList();
        CustomWorkflowUserInfo beDeleteInfo = userList.FirstOrDefault(x => x.LoginName == e.CommandArgument.ToString());
        userList.Remove(beDeleteInfo);
        UserList = userList.ToXml();
        InitUserNames();
    }

    public void AddUser(string loginName)
    {
        List<CustomWorkflowUserInfo> userList = UserList.ToUserList();
        CustomWorkflowUserInfo info = userList.FirstOrDefault(x => x.UserInfo.LoginName == loginName);
        if (info == null)
        {
            userList.Add(new CustomWorkflowUserInfo()
            {
                UserInfo = CustomWorkflowHelper.GetUserByLoginId(loginName),
                IsApproval = false
            });
            UserList = userList.ToXml();
            InitUserNames();
        }
    }

}