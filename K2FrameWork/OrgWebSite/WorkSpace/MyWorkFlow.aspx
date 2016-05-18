<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyWorkFlow.aspx.cs" Inherits="K2Organization.WorkSpace.MyWorkFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发起流程</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:DataList ID="dlWorkFlow" runat="server" GridLines="Horizontal" 
            HorizontalAlign="Center" RepeatColumns="2" RepeatDirection="Horizontal" 
            Width="100%" BorderStyle="None" BorderWidth="0px" Font-Size="12px">
            <ItemTemplate>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <a href='../SysModel/SubmitModel.aspx?ID=<%#Eval("ID") %>' target="_blank"><%#Eval("ProcessType") %></a>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        
    </div>
    </form>
</body>
</html>
