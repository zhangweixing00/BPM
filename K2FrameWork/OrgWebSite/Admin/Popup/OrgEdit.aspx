<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="OrgEdit.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.OrgEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑组织</title>
    <base target="_self" />
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ValidateData()
        {
            return true;
        }
        window.alert = function (msg) {
            top.ymPrompt.alert({ title: '提示信息', message: msg })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divCommon">
        <table class="tbCommon" style="padding-left: 28px; padding-top: 16px;">
            <tr>
                <td style="width: 100px;">
                    组织编号
                </td>
                <td style="">
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtOrgCode"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    组织名称
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtOrgName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    组织描述
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtOrgDesc"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    组织状态
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblState" RepeatDirection="Horizontal">
                        <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    组织序号
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNo" runat="server" MaxLength="5" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">&nbsp;
                  </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <asp:HiddenField runat="server" ID="hfAction" />
                    <asp:Button runat="server" CssClass="btnCommon" ID="btnSave" Text="保存" OnClientClick="return ValidateData();"
                        OnClick="btnSave_Click" />
                    <asp:Literal runat="server" ID="litScript"></asp:Literal>
                </td>
            </tr>
        </table>
        <div class="divCommand">
        </div>
    </div>
    </form>
</body>
</html>
