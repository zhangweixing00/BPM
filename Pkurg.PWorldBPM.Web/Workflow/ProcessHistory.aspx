<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessHistory.aspx.cs" Inherits="Workflow_ProcessHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="titlebg">
            <div class="title">
                流转流程</div>
            <div class="new">
            </div>
        </div>
        <div class="content">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th style="width: 80px;">
                                处理人
                            </th>
                            <th>
                                所属部门
                            </th>
                            <th style="width: 150px;">
                                完成时间
                            </th>
                            <th style="width: 50px;">
                                状态
                            </th>
                            <th>
                                处理步骤
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                        onmouseout="this.style.backgroundColor=c;">
                        <td>
                            <%#Eval("ApproveByUserName")%>
                        </td>
                        <td>
                            <%#Eval("DepartName")%>
                        </td>
                        <td>
                            <%#  Convert.ToDateTime(Eval("FinishedTime")).ToString("yyyy-MM-dd HH:mm:ss") == "9999-12-31 00:00:00" ? "" : Convert.ToDateTime(Eval("FinishedTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                        </td>
                        <td>
                            <%# Eval("ApproveResult")%>
                        </td>
                        <td>
                            <%# Eval("CurrentActiveName")%><%# Eval("ISSign").ToString().Trim()=="2"?"【加签】":""%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            
        </div>
    </div>
    </form>
</body>
</html>
