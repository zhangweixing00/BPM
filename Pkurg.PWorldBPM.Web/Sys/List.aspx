<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="Sys_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="titlebg">
            <div class="title">
                业态业种</div>
            <div class="new">
                <asp:LinkButton ID="lbtnAdd" runat="server" Text="新建" OnClick="lbtnAdd_Click"></asp:LinkButton>
            </div>
        </div>
        <div class="content">
            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th>
                                序号
                            </th>
                            <th>
                                名称
                            </th>
                            <th>
                                备注
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
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td>
                            <%#Eval("Name")%>
                        </td>
                        <td>
                            <%# Eval("Remark")%>
                        </td>
                        <td>
                            <asp:LinkButton ID="lblView" runat="server" CommandName="View" CommandArgument='<%#Eval("ID") %>'
                                CausesValidation="false" Text="查看"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ID") %>'
                                CausesValidation="false" Text="编辑"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ID") %>'
                                OnClientClick="return confirm('确定要删除吗?');" CausesValidation="false" Text="删除"></asp:LinkButton>
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
