<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="RuleList.aspx.cs" Inherits="WorkFlowRule_Rule_RuleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .anpager span
        {
            background: none;
            border: none;
        }
    </style>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                制度列表(<asp:Label ID="lblCount" runat="server" ToolTip="总数"></asp:Label>)</div>
            <div class="new">
                <asp:LinkButton ID="lbtnAdd" runat="server" Text="新建" OnClick="lbtnAdd_Click"></asp:LinkButton>
            </div>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            分类：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="全部" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <th>
                            标题：
                        </th>
                        <td>
                            <asp:TextBox ID="txtKey" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="green_btn" OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
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
                                分类
                            </th>
                            <th>
                                标题
                            </th>
                            <th>
                                施行时间
                            </th>
                            <th>
                                创建人
                            </th>
                            <th>
                                创建时间
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
                            <%# Container.ItemIndex + 1+this.AspNetPager1.PageSize*(this.AspNetPager1.CurrentPageIndex-1)%>
                        </td>
                        <td>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%#Eval("Category_Name")%>'></asp:Label>
                        </td>
                        <td>
                            <%# Eval("Title")%>
                        </td>
                        <td>
                            <%# FormatDate(Eval("Publish_Date"))%>
                        </td>
                        <td>
                            <%# Eval("Created_By_Name")%>
                        </td>
                        <td>
                            <%# Eval("Created_On")%>
                        </td>
                        <td>
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
            <pager:AspNetPager ID="AspNetPager1" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="10"
                OnPageChanged="AspNetPager1_PageChanged">
            </pager:AspNetPager>
        </div>
    </div>
</asp:Content>
