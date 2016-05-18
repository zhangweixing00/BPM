<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebServiceDemo.aspx.cs" Inherits="WebService_WebServiceDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>触发新流程测试页面</h1>
    </div>
    <table>
    <tr>
    <td>
        原流程实例ID：<asp:TextBox ID="txtINST_ID" runat="server" Text="68372461-5fda-4455-803a-1ee6b62295e1"></asp:TextBox>
    </td>
    <td>
        新创建者ID：<asp:TextBox ID="txtUSER_ID" runat="server" Text="D0020"></asp:TextBox>
    </td>
    <td>
        存储过程：<asp:TextBox ID="txtSP" runat="server" Text="wf_usp_CreateNewForm"></asp:TextBox>
    </td>
    <td>
         <asp:Button ID="btnStart" runat="server" Text="触发" onclick="btnStart_Click" />
    </td>
    </tr>
    </table>
    </form>
</body>
</html>
