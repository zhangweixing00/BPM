<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowUserNames.ascx.cs" Inherits="Modules_Custom_ShowUserNames" %>

<asp:DataList ID="DLUserData" runat="server" RepeatColumns="8"  CssClass='ShowName'>
    <ItemTemplate>
        <span  style='margin-left:20px;' id='U_<%# Eval("LoginName").ToString() %>' uid='<%# Eval("LoginName").ToString() %>'><%# Eval("EmployeeName").ToString()%></span>
        <asp:LinkButton ID="lbtnDelte" runat="server"  Visible='<%# IsShowDelete  %>'
            CommandArgument='<%# Eval("LoginName").ToString() %>'  ToolTip="删除"
            oncommand="lbtnDelte_Command">×</asp:LinkButton>
    </ItemTemplate>
</asp:DataList>
<div id="Div_NoUsers" runat="server">当前没有审批人员</div>