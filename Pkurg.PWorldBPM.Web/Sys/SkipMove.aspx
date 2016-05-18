<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SkipMove.aspx.cs" Inherits="Sys_SkipMove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="title">
    <div>

    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="i" />
            <asp:BoundField DataField="b" />
        </Columns>
    </asp:GridView>
    <asp:Button ID="Btnskip" runat="server" Text="跳转" CssClass="green_btn" />

    </form>
</body>
</html>
