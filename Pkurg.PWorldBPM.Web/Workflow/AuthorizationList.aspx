<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="AuthorizationList.aspx.cs" Inherits="Workflow_AuthorizationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <style>
        .anpager span
        {
            background: none;
            border: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--授权查询-->
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        授权查询</div>
                    <a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/Workflow/FlowAccredit.aspx"
                        target="_blank" class="new">切换至OA授权列表</a>
                </div>
                <div class="content">
                    <table class="FormTable">
                        <tbody>
                            <tr>
                                <th>
                                    标题：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbxTitle" runat="server" CssClass="txt" MaxLength="50" Width="150px"></asp:TextBox>
                                </td>
                                <th>
                                    开始时间：
                                </th>
                                <td>
                                    <input id="tbxBeginTime" runat="server" class="txt" style="width: 150px" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    至
                                    <input id="tbxEndTime" runat="server" class="txt" style="width: 150px" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <td>
                                    <asp:Button ID="btnQuery" runat="server" CssClass="green_btn" Text="查询" OnClick="btnQuery_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <!--我授权的流程BPM-->
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        我授权的流程
                    </div>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="new" Text="撤销授权" OnClick="lbtnDelete_Click"
                        OnClientClick="return confirm('确定要撤销授权吗?');" CausesValidation="false"></asp:LinkButton>
                </div>
                <div class="content">
                    <asp:Repeater ID="lblList1" runat="server" OnItemDataBound="lblList1_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th width="50px">
                                        选择
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th width="80px">
                                        被授权人
                                    </th>
                                    <th width="50px">
                                        授权人
                                    </th>
                                    <th width="130px">
                                        授权时间
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td>
                                    <asp:CheckBox ID="cbxChoose" runat="server" OnCheckedChanged="cbxChoose_CheckedChanged" />
                                </td>
                                <td title='<%#Eval("ProcName")%>'>
                                    <asp:HyperLink ID="hyperLink" runat="server" Target="_blank" Text='<%#Eval("ProcName").ToString().Length > 40 ?  Eval("ProcName").ToString().Substring(0,40) + "..." : Eval("ProcName")%>'></asp:HyperLink>
                                    <asp:Label ID="lblProcId" runat="server" Text='<%#Eval("ProcId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAuthorizationID" runat="server" Text='<%#Eval("AuthorizationID")%>'
                                        Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedUserName")%>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedByUserName")%>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedOn")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <pager:AspNetPager ID="AspNetPager1" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="5"
                        OnPageChanged="AspNetPager1_PageChanged">
                    </pager:AspNetPager>
                </div>
            </div>
            <!--授权我的流程BPM-->
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        授权我的流程
                    </div>
                </div>
                <div class="content">
                    <asp:Repeater ID="lblList2" runat="server" OnItemDataBound="lblList2_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th>
                                        标题
                                    </th>
                                    <th width="80px">
                                        被授权人
                                    </th>
                                    <th width="50px">
                                        授权人
                                    </th>
                                    <th width="130px">
                                        授权时间
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td title='<%#Eval("ProcName")%>'>
                                    <asp:HyperLink ID="hyperLink2" runat="server" Target="_blank" Text='<%#Eval("ProcName").ToString().Length > 40 ?  Eval("ProcName").ToString().Substring(0,40) + "..." : Eval("ProcName")%>'></asp:HyperLink>
                                    <asp:Label ID="lblProcId2" runat="server" Text='<%#Eval("ProcId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAuthorizationID2" runat="server" Text='<%#Eval("AuthorizationID")%>'
                                        Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedUserName")%>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedByUserName")%>
                                </td>
                                <td>
                                    <%#Eval("AuthorizedOn")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <pager:AspNetPager ID="AspNetPager2" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="5"
                        OnPageChanged="AspNetPager2_PageChanged">
                    </pager:AspNetPager>
                </div>
            </div>
            <!--我授权的流程OA-->
            <div class="container" style="margin-top: 10px;">
                <div class="titlebg">
                    <div class="title">
                        我授权的流程(OA)
                    </div>
                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="new" Text="撤销授权" OnClick="lbtnCancel_Click"
                        OnClientClick="return confirm('确定要撤销授权吗?');" CausesValidation="false"></asp:LinkButton>
                </div>
                <div class="content">
                    <asp:Repeater ID="rpt3" runat="server" OnItemDataBound="rpt3_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th width="50px">
                                        选择
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th width="80px">
                                        被授权人
                                    </th>
                                    <th width="50px">
                                        授权人
                                    </th>
                                    <th width="130px">
                                        授权时间
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td>
                                    <asp:CheckBox ID="cbxChoose2" runat="server" OnCheckedChanged="cbxChoose2_CheckedChanged" />
                                </td>
                                <td title='<%#Eval("FlowName")%>'>
                                    <asp:HyperLink ID="hyperLink" runat="server" Target="_blank" Text='<%#Eval("FlowName").ToString().Length > 40 ?  Eval("FlowName").ToString().Substring(0,40) + "..." : Eval("FlowName")%>'></asp:HyperLink>
                                    <asp:Label ID="lblProcId" runat="server" Text='<%#Eval("FlowId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAuthorizationID" runat="server" Text='<%#Eval("AccreditID")%>'
                                        Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("AccreditName")%>
                                </td>
                                <td>
                                    <%#Eval("CreateName")%>
                                </td>
                                <td>
                                    <%#Eval("CreateTime")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <pager:AspNetPager ID="AspNetPager3" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="5"
                        OnPageChanged="AspNetPager3_PageChanged">
                    </pager:AspNetPager>
                </div>
            </div>
            <!--授权我的流程OA-->
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        授权我的流程(OA)
                    </div>
                </div>
                <div class="content">
                    <asp:Repeater ID="rpt4" runat="server" OnItemDataBound="rpt4_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th>
                                        标题
                                    </th>
                                    <th width="80px">
                                        被授权人
                                    </th>
                                    <th width="50px">
                                        授权人
                                    </th>
                                    <th width="130px">
                                        授权时间
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td title='<%#Eval("FlowName")%>'>
                                    <asp:HyperLink ID="hyperLink2" runat="server" Target="_blank" Text='<%#Eval("FlowName").ToString().Length > 40 ?  Eval("FlowName").ToString().Substring(0,40) + "..." : Eval("FlowName")%>'></asp:HyperLink>
                                    <asp:Label ID="lblProcId2" runat="server" Text='<%#Eval("FlowId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblAuthorizationID2" runat="server" Text='<%#Eval("AccreditID")%>'
                                        Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("AccreditName")%>
                                </td>
                                <td>
                                    <%#Eval("CreateName")%>
                                </td>
                                <td>
                                    <%#Eval("CreateTime")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <pager:AspNetPager ID="AspNetPager4" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                        FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="5"
                        OnPageChanged="AspNetPager4_PageChanged">
                    </pager:AspNetPager>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
