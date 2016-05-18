<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="AuthorizationSelectPersons.aspx.cs" Inherits="Workflow_AuthorizationSelectPersons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function checkList() {
                    if ($('#<%=tbxChosenEmployees.ClientID %>').val() == "") {
                        alert("请选择人员!");
                        return false;
                    }
                }
            </script>
            <div class="container">
                <div class="titlebg">
                    <div class="title">
                        授权 - 选择人员
                    </div>
                </div>
                <table class="FormTable">
                    <tbody>
                        <tr>
                            <th>
                                员工名称：
                            </th>
                            <td>
                                <asp:TextBox ID="tbxEmployeeName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            </td>
                            <th>
                                员工登录名：
                            </th>
                            <td>
                                <asp:TextBox ID="tbxLoginName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblProcId" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblProcName" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblTitle" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblDate1" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblDate2" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="btnQuery" runat="server" CssClass="green_btn" Text="查询" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="content">
                    <asp:Repeater ID="lblUserList" runat="server" OnItemDataBound="lblUserList_ItemDataBound">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th style="width: 40px;">
                                        选择
                                    </th>
                                    <th>
                                        员工名称
                                    </th>
                                    <%--<th>
                                        所在部门
                                    </th>--%>
                                    <th>
                                        登录名
                                    </th>
                                    <th>
                                        E-mail
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbUser" runat="server" OnCheckedChanged="cbUser_CheckedChanged"
                                        AutoPostBack="true" />
                                </td>
                                <td>
                                    <asp:Label ID="lblEmployeeCode" runat="server" Text='<%#Eval("EmployeeCode") %>'
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName") %>'></asp:Label>
                                </td>
                                <%--<td>
                                    <%#Eval("DeparName") %>
                                </td>--%>
                                <td>
                                    <asp:Label ID="lblLoginName" runat="server" Text='<%#Eval("LoginName") %>'></asp:Label>
                                </td>
                                <td>
                                    <%#Eval("Email") %>
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
                <div class="content">
                    <div>
                        <span style="font-weight: bold;">已选中人员：</span>
                        <asp:TextBox ID="tbxChosenEmployees" runat="server" TextMode="MultiLine" Enabled="false"
                            Width="100%" Height="50px"></asp:TextBox>
                    </div>
                </div>
                <div style="margin-top: 5px;">
                    <div class="title">
                    </div>
                    <div class="new" style="padding-top: 0px;">
                        <asp:Button ID="btnConfirm" runat="server" CssClass="green_btn" Text="确定" OnClick="btnConfirm_Click"
                            OnClientClick="javascript:return checkList();" />
                        <asp:Button ID="btnClose" runat="server" CssClass="green_btn" Text="返回" OnClick="btnClose_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
