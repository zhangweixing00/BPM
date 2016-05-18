<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowCanSelectItems.aspx.cs"
    Inherits="Workflow_Modules_FlowRelated_ShowCanSelectItems" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>选择关联流程</title>
    <link href="/Resource/css/bpm.css" type="text/css" rel="Stylesheet" />
    <link href="/Resource/css/Default.css" type="text/css" rel="Stylesheet" />
    <link href="/Resource/css/Layout.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/Resource/jquery/jquery-1.8.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="dcontainer" class="container">
            <div class="titlebg">
                <div class="title">
                    选择关联流程</div>
                <div class="new">
                    <a href="#" onclick="javascript:window.close();" class="linkbtn"><span>关闭</span></a>
                </div>
            </div>
            <div class="search">
            </div>
            <div class="content">
                <asp:GridView ID="agvProcData" runat="server" AutoGenerateColumns="False" 
                    Width="100%" CssClass="List" AllowPaging="True" onpageindexchanging="agvProcData_PageIndexChanging"
                   >
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                操作
                            </HeaderTemplate>
                            <ItemTemplate >
                                <asp:LinkButton ID="lbSelected" runat="server" CommandArgument='<%#Eval("ProcId") %>'
                                    OnCommand="lbSelected_Command" Text="选择"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标题">
                            <ItemTemplate>
                                <a href='<%# "/Workflow/ViewPage/ViewPageHandler.ashx?id="+Eval("ProcId").ToString()  %>'
                                    target="_blank">
                                    <%# Eval("ProcName") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatorName" ItemStyle-Width="100px" 
                            HeaderText="创建人" >
<ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EndTime" ItemStyle-Width="150px" HeaderText="完成时间" >
<ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        没有可关联流程！
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
    <input type="hidden" id="hidProcIDList" value='' />
</body>
</html>
