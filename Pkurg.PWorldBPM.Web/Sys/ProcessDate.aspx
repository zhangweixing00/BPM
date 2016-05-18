<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="ProcessDate.aspx.cs" Inherits="Sys_ProcessDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function Close_Win() {
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                1、流程流转情况
            </div>
            <div class="new">
                <a href="javascript:void();" onclick="javascript:Close_Win();">关闭</a>
            </div>
        </div>
        <div class="content">
            <asp:Repeater ID="rpViewHistory" runat="server">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th style="width: 40px;">
                                序号
                            </th>
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
                            <%# Container.ItemIndex + 1%>
                        </td>
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
    <div class="container">
        <div class="titlebg">
            <div class="title">
                2、DataField
            </div>
            <div class="new">
            </div>
        </div>
        <div class="content">
            <asp:Label ID="lblException" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th style="width: 40px;">
                                序号
                            </th>
                            <th>
                                名称
                            </th>
                            <th>
                                值
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblValue" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
