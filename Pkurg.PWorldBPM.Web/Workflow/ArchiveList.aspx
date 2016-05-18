<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="ArchiveList.aspx.cs" Inherits="Workflow_ArchiveList" Trace="true" %>

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
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        归档查询</div>
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
                            <tr>
                                <th>
                                    &nbsp;</th>
                                <td>
                                    &nbsp;</td>
                                <th>
                                    状态：</th>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem>全部</asp:ListItem>
                                        <asp:ListItem>同意</asp:ListItem>
                                        <asp:ListItem>拒绝</asp:ListItem>
                                        <asp:ListItem>强制结束</asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        BPM归档列表
                    </div>
                </div>
                <div class="content">
                    <asp:Repeater ID="lblBPMList" runat="server" OnItemCommand="lblBPMList_ItemCommand"
                        OnItemDataBound="lblBPMList_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th width="50px">
                                        查看
                                    </th>
                                    <th width="50px">
                                        授权
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th width="120px">
                                        开始时间
                                    </th>
                                    <th width="120px">
                                        结束时间
                                    </th>
                                    <th width="50px">
                                        创建人
                                    </th>
                                    <th width="220px">
                                        创建部门
                                    </th>
                                    <th width="25px">
                                        源
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td>
                                <a onclick='ViewHistory("<%# Eval("bpmid")%>")'
                                href='#'>
                                <img src="/Resource/images/dg_flow_l.gif" title="查看流转过程" /></a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbnAuthorize" runat="server" Text="授权" CommandName="Authorize"
                                        CommandArgument='<%#Eval("ProcId")+","+ Eval("ProcName")%>'></asp:LinkButton>
                                </td>
                                <td title='<%#Eval("ProcName") %>'>
                                    <asp:HyperLink ID="hyperLink" Target="_blank" runat="server" Text='<%#Eval("ProcName").ToString().Length > 31 ? Eval("ProcName").ToString().Substring(0,30) + "..." : Eval("ProcName")%> '></asp:HyperLink>
                                    <asp:Label ID="lblProId" runat="server" Text='<%#Eval("ProcId") %>' Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("StartTime")%>
                                </td>
                                <td>
                                    <%#Eval("EndTime") %>
                                </td>
                                <td>
                                    <%#Eval("CreatorName") %>
                                </td>
                                <td title='<%#Eval("CreatorDeptName") %>'>
                                    <asp:Label ID="lblDeptName1" runat="server" Text='<%#Eval("CreatorDeptName").ToString().Length > 20 ? Eval("CreatorDeptName").ToString().Substring(0,20) + "..." : Eval("CreatorDeptName")%> '></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("Source") %>
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
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        OA归档列表
                    </div>
                </div>
                <div class="content">
                    <asp:Repeater ID="lblOAList" runat="server">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th width="50px">
                                        授权
                                    </th>
                                    <th>
                                        标题
                                    </th>
                                    <th width="120px">
                                        开始时间
                                    </th>
                                    <th width="120px">
                                        结束时间
                                    </th>
                                    <th width="50px">
                                        创建人
                                    </th>
                                    <th width="220px">
                                        创建部门
                                    </th>
                                    <th width="25px">
                                        源
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td>
                                    <a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/Workflow/InstanceFile.aspx"
                                        target="_blank">授权</a>
                                </td>
                                <td title='<%#Eval("ProcName") %>'>
                                    <a target="_blank" href='<%#"http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/Workflow/ProcessEventPath.ashx?caseID=" + Eval("procId") + "&taskID="+ Eval("TaskID")+"&actionType=3"%>'>
                                        <asp:Label ID="lblTitle1" runat="server" Text='<%#Eval("ProcName").ToString().Length > 31 ? Eval("ProcName").ToString().Substring(0,30) + "..." : Eval("ProcName")%> '></asp:Label></a>
                                </td>
                                </td>
                                <td>
                                    <%#Eval("StartTime") %>
                                </td>
                                <td>
                                    <%#Eval("EndTime") %>
                                </td>
                                <td>
                                    <%#Eval("CreatorName") %>
                                </td>
                                <td title='<%#Eval("CreatorDeptName") %>'>
                                    <asp:Label ID="lblDeptName1" runat="server" Text='<%#Eval("CreatorDeptName").ToString().Length > 20 ? Eval("CreatorDeptName").ToString().Substring(0,20) + "..." : Eval("CreatorDeptName")%> '></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("Source") %>
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
        </ContentTemplate>
    </asp:UpdatePanel>
     <script type="text/javascript">
         function ViewHistory(caseID) {

             var  sFeatures = "dialogHeight:450px; dialogWidth:700px; dialogTop: px; dialogLeft: px; edge:Raised; center: Yes; help: Yes; status:Yes;scroll:auto;resizable: Yes";
             var    url = "ProcessHistory.aspx?CaseID=" + caseID;
             window.showModalDialog(url, '1', sFeatures);
         }
    </script>
</asp:Content>
