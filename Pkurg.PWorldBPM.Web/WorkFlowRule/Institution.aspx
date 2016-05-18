<%@ Page Title="制度" Language="C#" MasterPageFile="~/WorkFlowRule.master" AutoEventWireup="true"
    CodeFile="Institution.aspx.cs" Inherits="WorkFlowRule_Institution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".header_li").removeClass("header_li_current");
            $(".header_li:eq(1)").addClass("header_li_current");

        });
    </script>
    <div class="search">
        <div class="search_bar">
            <span class="inputSearch_cnt">
                <asp:TextBox ID="txtKey" runat="server" CssClass="inputSearch"></asp:TextBox>
            </span>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="buttonSearch_cnt" OnClick="btnSearch_Click" />
        </div>
    </div>
    <div>
        <div>
            <h3 class="h-title" style="float: left;">
                最新发布的制度</h3>
            <h3 class="h-title" style="float: right;">
            </h3>
        </div>
        <div class="clear">
        </div>
        <div class="search_box">
            <!--List-->
            <ul class="search_box_ul">
                <asp:Repeater ID="rptTopRule" runat="server">
                    <ItemTemplate>
                        <li><span>[
                            <%#Eval("Category_Name") %>
                            ] -
                            <%#FormatPublishDate( Eval("Publish_Date"))%>
                        </span><a href="InstitutionInfo.aspx?ID=<%#Eval("ID") %>&GUID=<%#Eval("Rule_GUID") %>">
                            <%#Eval("Title") %></a> </li>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoRecord" runat="server" Text="暂无记录" Visible="false"></asp:Label>
            </ul>
        </div>
    </div>
    <div class="clear">
    </div>
    <div>
        <h3 class="h-title" style="float: left;">
            制度列表(<asp:Label ID="lblCategoryName" Font-Bold="false" ForeColor="Green" runat="server"></asp:Label>)
            <asp:Label ID="lblCategoryId" runat="server" Visible="false" Text="-1"></asp:Label>
        </h3>
        <span style="float: left; padding-top: 12px; padding-left: 20px;">
            <asp:Repeater ID="rptCategory" runat="server">
                <ItemTemplate>
                    <a id="<%#Eval("ID") %>" href="Institution.aspx?categoryId=<%#Eval("ID") %>&categoryName=<%#Eval("Category_Name") %>">
                        <%#Eval("Category_Name") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </span>
        <div class="clear">
        </div>
        <div class="search_box">
            <!--List-->
            <ul class="search_box_ul">
                <asp:Repeater ID="rptRule" runat="server">
                    <ItemTemplate>
                        <li><span>[
                            <%#Eval("Category_Name") %>
                            ] -
                            <%#FormatPublishDate( Eval("Publish_Date"))%>
                        </span><a href="InstitutionInfo.aspx?ID=<%#Eval("ID") %>&GUID=<%#Eval("Rule_GUID") %>">
                            <%#Eval("Title") %></a> </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <pager:AspNetPager ID="AspNetPager1" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="10"
                OnPageChanged="AspNetPager1_PageChanged">
            </pager:AspNetPager>
        </div>
    </div>
</asp:Content>
