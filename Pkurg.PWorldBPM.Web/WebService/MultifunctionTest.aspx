<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultifunctionTest.aspx.cs" Inherits="WebService_MultifunctionTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <h1>多功能测试页面</h1>
    </div>
    <table>
    <tr>
    <td>
        文本框1：<asp:TextBox ID="txt1" runat="server"></asp:TextBox>
    </td>
    <td>
        文本框2：<asp:TextBox ID="txt2" runat="server"></asp:TextBox>
    </td>
    <td>
        文本框3：<asp:TextBox ID="txt3" runat="server"></asp:TextBox>
    </td>
    <td>
         <asp:Button ID="btnStart" runat="server" Text="测试" onclick="btnStart_Click" />
    </td>
    </tr>
    </table>
    </form>
</body>
</html>
