<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessRuleEdit.aspx.cs" Inherits="OrgWebSite.Admin.Popup.ProcessRuleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程节点规则</title>
    <base target="_self" />
    <link href="../../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script src="../../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
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
            height:18px;
            border: 1px #999999 solid;
        }
    </style>
    <script type="text/javascript">
        function checkInput() {
            if ($('#txtDisplayName').val() == '') {
                alert('请输入节点显示名称');
                return false;
            }
            if ($('#txtTableName').val() == '') {
                alert('请输入节点表名');
                return false;
            }
            if ($('#txtSPName').val() == '') {
                alert('请输入节点存储过程名');
                return false;
            }
            if ($('#txtConditionExpression').val() == '') {
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
            <tr style="  display:none;">
                <td class="table_title">
                    查询字段名：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtDisplayName" runat="server" Width="340px" Text="RoleID" CssClass="txtcss" MaxLength="50" ToolTip=""></asp:TextBox>
                </td>
                <td class="table_xing">
                <div style="color:Red;" id="div1" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td class="table_title" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr style=" display:none;">
                <td class="table_title">
                    表名：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtTableName" runat="server" Width="340px" Text="PWorld.dbo.T_PmsRole" CssClass="txtcss" MaxLength="100"></asp:TextBox>
                </td>
                <td class="table_xing">
                <div style="color:Red;" id="div2" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td class="table_title" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="table_title">
                    选择存储过程名：</td>
                <td class="table_content">
                <asp:DropDownList ID="dplistSPName" runat="server">
                    <asp:ListItem Text="获得审批人员" Value="SProc_GetRoleUserByFilter"></asp:ListItem>
                     <asp:ListItem Text="获得CEO" Value="SProc_GetRoleUserByFilter_CEO"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSPName" runat="server" MaxLength="50" Visible="false" Width="340px" CssClass="txtcss"></asp:TextBox>
                    
                </td>
                <td class="table_xing">
                <div style="color:Red;" id="div3" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_title">
                    请选择节点角色：
                </td>
                <td class="table_content">
                 <asp:DropDownList ID="dplist" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtConditionExpression" runat="server" Visible="false" TextMode="MultiLine" Width="340px" Height="54px"
                        Style="padding-top: 4px; border: 1px #999999 solid;" MaxLength="100"></asp:TextBox>
                </td>
                <td class="table_xing">
                    &nbsp;
                <div style="color:Red;" id="div4" runat="server">*</div>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
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
        <asp:HiddenField ID="hfID" runat="server" />
    </div>
    </form>
</body>
</html>