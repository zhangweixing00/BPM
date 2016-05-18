<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkPlaceEdit.aspx.cs" Inherits="OrgWebSite.Admin.Popup.WorkPlaceEdit" %>

<%@ Register src="../../Process/Controls/Sitemap.ascx" tagname="Sitemap" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑工作地点</title>
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
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { window.location.href = '/Admin/WorkplaceManage.aspx'; } } });
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
    </style>
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
    <div class="rightTop">
        <p>
            <uc1:Sitemap ID="Sitemap1" runat="server" />
        </p>
    </div>
    <div style="padding-left: 18px;">
        <div class="rightTitle">
            <span>工作地点信息</span></div>
        <table style="width: 769px;">
            <tbody>
                <tr>
                    <td class=" text">
                        &nbsp;
                    </td>
                    <td colspan="5" style="text-align: right;">
                        <asp:ImageButton ID="ibtAdd" ClientIDMode="Static" runat="server" ImageUrl="~/pic/btnImg/save_nor.png"
                            onmouseover="this.src='/pic/btnImg/btnsave_over.png'" onmouseout="this.src='/pic/btnImg/save_nor.png'"
                            OnClick="btnSave_Click" />
                        <asp:ImageButton ID="btnReturnList" runat="server" onmouseover="this.src='/pic/btnImg/btnBackList_over.png'"
                            onmouseout="this.src='/pic/btnImg/btnBackList_nor.png'" OnClientClick="javascript: window.location.href='/Admin/WorkPlaceManage.aspx';window.close();return false;"
                            ImageUrl="~/pic/btnImg/btnBackList_nor.png" Style="margin-left: 15px;" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table style="width: 769px;">
            <tr>
                <td width="80" height="20">
                    工作地点名称
                </td>
                <td width="190">
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid; width:150px;" ID="txtPlaceName" runat="server"></asp:TextBox>
                </td>
                <td width="80">
                    工作地点编号
                </td>
                <td width="190">
                    <asp:TextBox ID="txtPlaceCode" Style="height: 22px; border: 1px #999999 solid; width:150px;"
                        runat="server"></asp:TextBox>
                </td>
                <td width="200" align="center" bgcolor="#FFFFFF">
                </td>
            </tr>
            </table>
        <div class="divCommand" style="text-align: center;">
            <asp:Literal runat="server" ID="litScript"></asp:Literal>
        </div>
    </div>
    <asp:HiddenField ID="hfID" runat="server" />
    <asp:HiddenField ID="hfStatus" runat="server" />
    </form>
</body>
</html>
