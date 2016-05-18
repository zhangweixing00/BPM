<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveTemplation.aspx.cs" Inherits="Workflow_CustomWorkFlow_SaveTemplation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>保存模板</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //点击回车，自动查询

        }); 
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container" style="margin: 5px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="titlebg">
                    <div class="title">
                        设置模板信息</div>
                    <div style="float: right">
                        <asp:Button ID="btnYes" runat="server" Text=" 确定 " CssClass="gray_btn2" 
                            onclick="btnYes_Click" ValidationGroup="s"  />
                        &nbsp;<asp:Button ID="Button2" runat="server" Text=" 关闭 " CssClass="gray_btn2" OnClientClick="javascript:window.returnValue=2;window.close();" />
                    </div>
                </div>
                <div class="content">
                    <table class="FormTable">
                        <tbody>
                            <tr>
                                <td>
                                    名称（必填）：</td>
                                <td>
                                    <asp:TextBox ID="tbStepName" runat="server" Width="300px" ValidationGroup="s"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="tbStepName" Display="Dynamic" ErrorMessage="*" 
                                        SetFocusOnError="True" ValidationGroup="s"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    描述：
                                </td>
                                <td>
                                    <asp:TextBox ID="tbDes" runat="server" Width="450px" height="250px" Rows="10" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr_cbIsOpen" runat="server">
                                <td>
                                    是否公开：
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbIsOpen" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="cbIsOpen_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBoxList ID="cblOpenList" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div>
                </div>
                <input id="hd_SelectType" type="hidden" runat="server" value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
