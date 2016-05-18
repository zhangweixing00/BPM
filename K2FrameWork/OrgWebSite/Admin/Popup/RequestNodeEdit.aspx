<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestNodeEdit.aspx.cs" Inherits="OrgWebSite.Admin.Popup.RequestNodeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑申请节点</title>
    <base target="_self" />
    <link href="/Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="/JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script src="/JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
    <link href="../../../Styles/PMS/PMS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ddlcss
        {
            border: 1px #999999 solid;
        }
        #RoleAddTitle
        {
            width: 770px;
            margin: 10px 0 0 20px;
            height: 10px;
            padding: 10px;
            color: #76650b;
            font-weight: bold;
            background: url(../../../pic/right_list_title_bg2.png) no-repeat;
        }
        .table_title
        {
            text-align: right;
        }
        .table_content
        {
            width: 350px;
            text-align: left;
        }
        .table_xing
        {
            width: 50px;
        }
        .txtcss
        {
            padding-top: 4px;
            height: 18px;
            border: 1px #999999 solid;
        }
    </style>
    <script type="text/javascript">
        function checkInput() {
            if ($('#txtNodeName').val() == '') {
                alert('请输入节点名称');
                return false;
            }
            if ($('#txtExpression').val() == '') {
                alert('请输入节点表达式');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="brand_detail" runat="server" style="margin: 20px auto auto auto; width: 500px;">
        <table border="0" cellpadding="0" width="500px" cellspacing="0">
            <tr>
                <td class="table_title">
                    入口节点名称：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtNodeName" runat="server" Width="340px" CssClass="txtcss" MaxLength="50" ToolTip=""></asp:TextBox>
                </td>
                <td class="table_xing">
                <div style="color:Red;" id="div1" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td class="table_title">
                    表达式：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtExpression" runat="server" TextMode="MultiLine" Width="340px" Height="54px"
                        Style="padding-top: 4px; border: 1px #999999 solid;" MaxLength="500"></asp:TextBox>
                </td>
                <td class="table_xing">
                    &nbsp;
                <div style="color:Red;" id="div4" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-top: 15px; text-align: center;">
                    <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnAffirm_over.png')"
                        onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnAffirm_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAffirm_nor.png" OnClick="btnSave_Click" OnClientClick="return checkInput();" />&nbsp;
                    <asp:ImageButton ID="btnCancel" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnCancel','../../../pic/btnImg/btnCancel_over.png')"
                        onmouseout="SaveMouseout('btnCancel','../../../pic/btnImg/btnCancel_nor.png')"
                        ImageUrl="~/pic/btnImg/btnCancel_nor.png" OnClientClick="top.window.ymPrompt.close(); return false;" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfProcessID" runat="server" />
    </form>
</body>
</html>
