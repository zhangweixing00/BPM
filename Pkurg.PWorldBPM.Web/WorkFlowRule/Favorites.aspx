<%@ Page Title="我的收藏" Language="C#" MasterPageFile="~/WorkFlowRule.master" AutoEventWireup="true"
    CodeFile="Favorites.aspx.cs" Inherits="WorkFlowRule_Favorites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".header_li").removeClass("header_li_current");
            $(".header_li:eq(2)").addClass("header_li_current");
        });
    </script>
    <div style="margin-top: -20px;">
        <h3 class="h-title">
            我收藏的制度(<asp:Label ID="lblCount" runat="server" ToolTip="总数"></asp:Label>)</h3>
        <div class="search_box">
            <!--List-->
            <ul class="search_box_ul">
            </ul>
            <asp:Repeater ID="rptRule" runat="server" OnItemCommand="rptRule_ItemCommand">
                <HeaderTemplate>
                    <table style="width: 100%;">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                        onmouseout="this.style.backgroundColor=c;">
                        <td style="height: 25px; font-size: 13px;">
                            <%# Container.ItemIndex + 1+this.AspNetPager1.PageSize*(this.AspNetPager1.CurrentPageIndex-1)%>.<a
                                href="InstitutionInfo.aspx?ID=<%#Eval("ID") %>&GUID=<%#Eval("Rule_GUID") %>">
                                <%#Eval("Title") %></a>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Cancel" CommandArgument='<%#Eval("ID") %>'
                                ForeColor="Green" OnClientClick="return confirm('确定要取消收藏吗?');" CausesValidation="false"
                                Text="取消收藏"></asp:LinkButton>
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
