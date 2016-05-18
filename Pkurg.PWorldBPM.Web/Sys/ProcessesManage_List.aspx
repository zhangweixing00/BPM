<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="ProcessesManage_List.aspx.cs" Inherits="Sys_ProcessesManage_List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .FormTable th
        {
            width: 80px;
        }
        .empty_label
        {
            margin: 5px;
            font-size: 15px;
            color: Red;
        }
        .anpager span
        {
            background: none;
            border: none;
        }
    </style>
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        实例管理</div>
                    <div class="new">
                        <table class="title">
                        </table>
                    </div>
                </div>
                <div class="content">
                    <table class="FormTable">
                        <tr>
                            <th>
                                创建人：
                            </th>
                            <td>
                                <asp:TextBox ID="txtCreateName" runat="server" class="txt" Style="width: 130px"></asp:TextBox>
                            </td>
                            <th>
                                流程名称：
                            </th>
                            <td>
                                <asp:TextBox ID="tbxTitle" runat="server" CssClass="txt" MaxLength="50" Width="130px"></asp:TextBox>
                            </td>
                            <th>
                                开始时间：
                            </th>
                            <td>
                                <input id="tbxBeginTime" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                至
                                <input id="tbxEndTime" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                实例号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbxNumBer" runat="server" class="txt" Style="width: 130px"></asp:TextBox>
                            </td>
                            <th>
                                状态：
                            </th>
                            <td>
                                <asp:TextBox ID="tbxStatus" runat="server" class="txt" Style="width: 130px"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnQuery" runat="server" CssClass="green_btn" Text="查询" OnClick="btnQuery_Click" />
                                <a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/Workflow/Administrator/InstanceList.aspx"
                                        target="_blank" class="new">切换至OA实例管理</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="content">
                    <asp:Label ID="lblshow" runat="server" Text="您所访问的记录不存在！" Visible="False" CssClass="empty_label"></asp:Label>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th>
                                        流程类型
                                    </th>
                                    <th>
                                        流程实例号
                                    </th>
                                    <th>
                                        流程名称
                                    </th>
                                    <th>
                                        开始时间
                                    </th>
                                    <th>
                                        结束时间
                                    </th>
                                    <th>
                                        创建人
                                    </th>
                                    <th>
                                        最新完成环节
                                    </th>
                                    <th>
                                        状态
                                    </th>
                                    <th style="width: 30px;">
                                        操作
                                    </th>
                                    <th style="width: 30px;">
                                        查看
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
                                    <%#Eval("FormID")%>
                                </td>
                                <td title='<%#Eval("FormTitle")%>' class="longstring">
                                    <asp:HyperLink ID="hyperLink" Target="_blank" runat="server" Text='<%#Eval("FormTitle").ToString().Length > 24 ? Eval("FormTitle").ToString().Substring(0,23) + "..." : Eval("FormTitle")%>'></asp:HyperLink>
                                    <asp:Label ID="lblProId" runat="server" Text='<%#Eval("InstanceID") %>' Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("CreateAtTime")%>
                                </td>
                                <td>
                                    <%#Eval("FinishedTime")%>
                                </td>
                                <td title='<%#Eval("CreateDeptName")%>'>
                                    <%#Eval("CreateByUserName")%>
                                </td>
                                <td>
                                    <%#Eval("WorkItemName")%>
                                </td>
                                <td>
                                    <%--<a onclick='ViewStatus("<%# Eval("InstanceID")%>")' href='#'></a>--%>
                                    <%# Eval("ApproveStatusName")%>
                                </td>
                                <td>
                                    <a style="display: <%# Eval("WFStatus").ToString()!="5"&& Eval("WFStatus").ToString()!="3"&& Eval("WFStatus").ToString()!="0"?"inline":"none"%>"
                                        target="_blank" href="EditDataField.aspx?procInstID=<%# Eval("WFInstanceId")%>&FormID=<%#Eval("FormID")%>">
                                        操作</a>
                                </td>
                                <td>
                                    <a target="_blank" href="ProcessDate.aspx?procInstID=<%# Eval("WFInstanceId")%>&FormID=<%#Eval("FormID")%>&InstanceID=<%#Eval("InstanceID") %>">
                                        查看</a>

                                </td>
                                <td>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <webdiyer:AspNetPager SubmitButtonClass="buttons" ID="AspNetPager1" runat="server"
                        CssClass="anpager" CurrentPageButtonClass="cpb" AlwaysShow="True" FirstPageText="首页"
                        NextPageText="下一页" PrevPageText="前一页" LastPageText="尾页" PageSize="13" ShowPageIndex="true"
                        OnPageChanged="AspNetPager1_PageChanged">
                    </webdiyer:AspNetPager>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ViewHistory(caseID, wfid) {
            var sFeatures = "dialogHeight:450px; dialogWidth:600px; dialogTop: px; dialogLeft: px; edge:Raised; center: Yes; help: Yes; status:Yes;scroll:auto;resizable: Yes";
            var keys = caseID;
            var url = "";
            sFeatures = "dialogHeight:450px; dialogWidth:1000px; dialogTop: px; dialogLeft: px; edge:Raised; center: Yes; help: Yes; status:Yes;scroll:auto;resizable: Yes";
            //url = "http://zy-bpmtest:81/ViewFlow/ViewFlow.aspx?ViewTypeName=ProcessView&K2Server=ZY-BPMTEST:5252&HostServerName=ZY-BPMTEST&HostServerPort=5555&ProcessID=" + wfid;
            url = "ProcessDate.aspx?CaseID=" + caseID;
            window.showModalDialog(url, '1', sFeatures);
        }

        function ViewStatus(CaseID) {
            var sFeatures = "dialogHeight:300px; dialogWidth:400px; dialogTop: px; dialogLeft: px; edge:Raised; center: Yes; help: Yes; status:Yes;scroll:auto;resizable: Yes";
            url = "/Sys/ProcessesStatus.aspx?ID=" + CaseID;
            window.showModalDialog(url, '1', sFeatures);
        }
    </script>
</asp:Content>
