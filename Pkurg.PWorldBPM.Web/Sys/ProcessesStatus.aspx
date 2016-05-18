<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessesStatus.aspx.cs"
    Inherits="Sys_ProcessesStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>操作</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="lbtnStop" runat="server" CommandName="stop" CommandArgument='<%#Eval("InstanceID") %>'
                    OnClientClick="return confirm('确定要暂停吗?');" CausesValidation="false" Text="暂停"
                    OnClick="btnstop_Click"></asp:LinkButton>
                <asp:Button ID="btnkillstop" runat="server" CssClass="green_btn" OnClick="btnkillstop_Click"
                    Text="强制结束" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
