<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToDoListMini.aspx.cs" Inherits="ToDoListMini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>待办列表</title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            border: 0;
            font-size: 12px;
            font-family: 宋体,Arial, Helvetica, sans-serif;
            background-color: #fff;
        }
        a
        {
            color: #003399;
            text-decoration: none;
        }
        .container
        {
            border-top: 1px solid #eaeaea;
            margin-bottom: 5px;
        }
        .container table
        {
            border-collapse: collapse;
            border-spacing: 0;
        }
        .titlebg
        {
            background: #cdd6dd;
            height: 27px;
            margin-bottom: 1px;
        }
        .title
        {
            color: #005bac;
            font-size: 13px;
            padding-top: 5px;
            display: inline;
            float: left;
            padding-left: 5px;
            line-height: 20px;
            height: 20px;
        }
        .content
        {
            text-align: left;
        }
        /*List*/
        .List
        {
            width: 100%;
        }
        
        .List th, .List td
        {
            border: 1px solid #dedede;
            padding-top: 3px;
            padding-left: 3px;
            height: 23px;
            line-height: 23px;
        }
        .List th
        {
            font-weight: bold;
            text-align: left;
            background: #f1f1f1;
        }
        .new
        {
            float: right;
            display: inline;
            font-size: 13px;            
            height: 20px;
            line-height: 20px;
            padding-right: 10px;
            padding-top: 4px;
        }
        .new A
        {
            color: #005bac;
        }
    </style>
    <script type="text/javascript">

        function refresh() {
            //<%=Page.ClientScript.GetPostBackEventReference(btRefresh,"") %>
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="titlebg" style="background-color: rgb(246, 244, 245); height: 30px; line-height: 30px;">
            <div class="title">
                待办事项(<span style="cursor: pointer;" onclick="javascript:top.location.href='http://zybpm.founder.com/Default.aspx';">
                    <asp:Label ID="lblCount" runat="server" ForeColor="Red"></asp:Label>
                </span>)</div>
            <div class="new">
                <asp:LinkButton ID="lbtnReload" runat="server" Text="刷新" OnClick="lbtnReload_Click"></asp:LinkButton>
            </div>
            <div>
                <asp:Button ID="btRefresh" Style="display: none" OnClick="btRefresh_Click" runat="server"
                    Text="Button" />
            </div>
        </div>
        <div class="content">
            <table class="List">
                <tr>
                    <th style="text-align: center;">
                        标题
                    </th>
                    <th style="width: 100px; text-align: center;">
                        流程类型
                    </th>
                    <th style="width: 120px; text-align: center;">
                        接收时间
                    </th>
                </tr>
                <asp:Repeater ID="rptERP" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href=' <%# Eval("FlowUrl")%>' target='_blank'>
                                    <%# Eval("FlowTitle")%>
                                </a>
                            </td>
                            <td>
                                <%# Eval("FlowType")%>
                            </td>
                            <td>
                                <%# Eval("FlowDateTime")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="gvDataList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a title="<%# Eval("FormTitle").ToString()%>" target="_blank" href='<%# GetApprovalPageUrl(Eval("flowFrom").ToString(),Eval("VirtualPath").ToString(),Eval("InstanceID").ToString(),Eval("taskid").ToString(),Eval("Step").ToString(),Eval("FormName").ToString(),Eval("sn").ToString())+"&u="+Eval("partLoginId").ToString() %>'>
                                    <%# Eval("FormTitle").ToString()%></a>
                            </td>
                            <td>
                                <%# Eval("AppName")%>
                            </td>
                            <td>
                                <%#FormatDateTime( Eval("ReceiveTime"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
