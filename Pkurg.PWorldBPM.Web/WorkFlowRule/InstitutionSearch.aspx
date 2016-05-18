<%@ Page Title="" Language="C#" MasterPageFile="~/WorkFlowRule.master" AutoEventWireup="true"
    CodeFile="InstitutionSearch.aspx.cs" Inherits="WorkFlowRule_InstitutionSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".header_li").removeClass("header_li_current");
            $(".header_li:eq(1)").addClass("header_li_current");
        });
    </script>
   <div style="margin-top: -20px;">
        <h3 class="h-title">
            制度列表 -
            <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
        <div class="clear">
        </div>
        <div class="search_box">
            <!--List-->
            <ul class="search_box_ul">
                <asp:Repeater ID="rptRule" runat="server">
                    <ItemTemplate>
                        <li><span>[
                            <%#Eval("Category_Name") %>
                            ]-
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
