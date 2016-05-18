<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="ProcessEndManage.aspx.cs" Inherits="Sys_ProcessEndManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="container">
            <div class="titlebg">
                <div class="title">
                    流程管理</div>
                <div class="new">
                    <table class="title">
                    </table>
                </div>
            </div>
            <div class="content">
                <table class="FormTable">
                    <tr>
                        <th>
                            应用名称：
                        </th>
                        <td>
                            <asp:TextBox ID="txtAppName" runat="server" class="txt" Style="width: 130px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" CssClass="green_btn" Text="查询" OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="content" style="height: 500px; overflow: scroll">
                <asp:Label ID="lblshow" runat="server" Text="您所访问的记录不存在！" Visible="False" CssClass="empty_label"></asp:Label>
                <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                    <HeaderTemplate>
                        <table class="List">
                            <tr>
                                <th>
                                    应用名称
                                </th>
                                <th>
                                    服务地址
                                </th>
                                <th>
                                    新增时间
                                </th>
                                <th>
                                    表单名称
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                            onmouseout="this.style.backgroundColor=c;">
                            <td>
                                <%#Eval("AppName")%>
                            </td>
                            <td>
                                <%#Eval("AddressToLink")%>
                            </td>
                            <td>
                                <%#Eval("CreateTime")%>
                            </td>
                            <td>
                                <%#Eval("ClassName")%>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("AppId") %>'
                                    CausesValidation="false" Text="编辑"></asp:LinkButton>
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
