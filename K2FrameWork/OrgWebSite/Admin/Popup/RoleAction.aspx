<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAction.aspx.cs" Theme="Common"
    Inherits="OrgWebSite.Admin.Popup.RoleAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色操作</title>
    <base target="_self" />
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../JavaScript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery.query-2.1.7.js"></script>
    <script src="../../JavaScript/CheckBox.js" type="text/javascript"></script>
    <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/Validate1.3.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Javascript/DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg });
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { window.location.href = '/Admin/UserManage.aspx'; } } });
        }
    </script>
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
        
        .login, .login_form
        {
            padding: 3px 0 0 10px;
            float: left;
        }
        li
        {
            list-style: none;
        }
    </style>
    <style type="text/css">
        .text
        {
            text-align: right;
            height: 30px;
            width: 110px;
        }
        .text2
        {
            text-align: left;
            width: 120px;
        }
        .bordercss
        {
            width: 110px;
            border-bottom: 1px solid #333333;
            padding-bottom: 2px;
        }
        .roleUserList
        {
            text-align: right;
        }
        .roleUserList td
        {
            text-align: right;
        }
        .displayNone
        {
            display: none;
        }
        .ddlcss
        {
            height: 22px;
            border: 1px #999999 solid;
        }
        .datalist3 TD A
        {
            color: #0066ff;
            text-decoration: underline;
        }
    </style>
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
    <div class="divCommon">
        <table class="tbCommon" style="">
            <tr>
                <td style="width: 70px">
                    角色名称
                </td>
                <td style="width: 150px">
                    <asp:TextBox ID="txtRoleName" CssClass="txtCommon" runat="server" Width="200px" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 70px;">
                    组织名称
                </td>
                <td>
                    <asp:DropDownList ID="ddlOrg" runat="server"  Width="200px" DataTextField="OrgName"
                        DataValueField="ID" CssClass="ddlcss">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    流程名称
                </td>
                <td>
                    <asp:DropDownList ID="ddlProcess"  Width="200px" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="divCommand">
            <asp:Button ID="btnSave" CssClass="btnCommon" OnClientClick="return ValidateData();"
                runat="server" Text="保 存" OnClick="btnSave_Click" />
            <asp:Literal runat="server" ID="litScript"></asp:Literal>
        </div>
        <script language="javascript" type="text/javascript">
            function ValidateData() {
                var roleName = $$('txtRoleName').value;

                if (roleName == '') {
                   // alert("Type a role name");
                    ymPrompt.alert('请填写角色名称！', null, null, '错误', null);
                    return false;
                }

                return true;
            }
        </script>
    </div>
    </form>
</body>
</html>
