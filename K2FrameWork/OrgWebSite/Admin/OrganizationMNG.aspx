<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizationMNG.aspx.cs"
    Inherits="OrgWebSite.Admin.OrganizationMNG" %>

<%@ Register Src="~/Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组织管理</title>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .text
        {
            text-align: right;
            height: 30px;
            width: 110px;
        }
        .text2
        {
            text-align: left;
        }
    </style>
</head>
<body onload="SetReadOnly()">
    <form id="form1" runat="server">
    <div id="container" style="width: 790px;">
        <div class="rightTop">
            <p>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
        <div style="width: 200px; float: left; padding-left: 18px;">
            <asp:DropDownList ID="ddlOrg" runat="server" Width="150" AutoPostBack="True" CssClass="ddlcss"
                OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TreeView ID="tvDept" runat="server" ForeColor="Black" HoverNodeStyle-Font-Underline="true"
                HoverNodeStyle-ForeColor="#dd5555" SelectedNodeStyle-ForeColor="#dd5555" SelectedNodeStyle-Font-Underline="true"
                ShowExpandCollapse="true" NodeIndent="10" ShowLines="true">
                <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                <SelectedNodeStyle BorderStyle="Dashed" Font-Underline="True" ForeColor="#DD5555" />
            </asp:TreeView>
        </div>
        <div style="width: 570px; float: left;">
            <div class="nav_1" style="width: 560px;">
                <p>
                    组织信息</p>
            </div>
            <div class="divDeptInfo">
                <table style="width: 570px;">
                    <tr>
                        <td colspan="7" style="text-align: right; height: 30px;">
                            <input type="button" class="btnCommon" value="添加组织" id="Button1" onclick="OrgAction('new');" />
                            <input type="button" class="btnCommon" value="编辑组织" id="Button2" onclick="OrgAction('edit');" />
                            <asp:Button ID="btnDelOrg" CssClass="btnCommon" runat="server" Text="删除组织" 
                                OnClientClick="return ValidateData('确认删除组织及其部门信息？','btnDelOrgHF');" 
                                onclick="btnDelOrg_Click" />
                            <asp:Button ID="btnDelOrgHF" runat="server" Text="删除组织" OnClick="btnDelOrg_Click"
                                Style="display: none;" />
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lbReload_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table style="width: 550px;">
                                <tr>
                                    <td class="text" style="width: 15%">
                                        组织编号
                                    </td>
                                    <td class="text2">
                                        <asp:TextBox runat="server" ID="txtOrgCode" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text" style="width: 15%">
                                        组织名称
                                    </td>
                                    <td class="text2">
                                        <asp:TextBox runat="server" ID="txtOrgName" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text" style="width: 15%">
                                        序号
                                    </td>
                                    <td class="text2">
                                        <asp:TextBox runat="server" ID="txtOrgOrder" MaxLength="5" Text="10" Style="height: 22px;
                                            border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="nav_1" style="width: 560px;">
                <p>
                    部门信息</p>
            </div>
            <div class="divDeptInfo">
                <table style="width: 570px;">
                    <tr>
                        <td colspan="7" style="text-align: right; height: 30px;">
                            <input type="button" class="btnCommon" value="添加部门" id="btnAddDept" onclick="DeptAction('new');" />
                            <input type="button" class="btnCommon" value="编辑部门" id="btnEditDept" onclick="DeptAction('edit');" />
                            <asp:Button ID="btnDelDept" CssClass="btnCommon" runat="server" Text="删除部门" 
                                OnClientClick="return ValidateData('确认删除部门及其子部门信息？','btnDelDeptHF');" 
                                onclick="btnDelDept_Click" />
                            <asp:Button ID="btnDelDeptHF" Style="display: none;" runat="server" Text="删除部门" OnClick="btnDelDept_Click" />
                            <asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table style="width: 550px;">
                                <tr>
                                    <td class="text" style="width: 15%">
                                        部门编号
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCode" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                    <td class="text" style="width: 15%">
                                        部门名称
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDepartment" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text" style="width: 15%">
                                        部门类型
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDeptType" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                    <td class="text" style="width: 15%">
                                        缩写
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAbbreviation" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text" style="width: 15%">
                                        状态
                                    </td>
                                    <td>
                                        <asp:RadioButtonList runat="server" ID="rblState" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="启用" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td class="text" style="width: 15%">
                                        序号
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtOrderNo" MaxLength="5" Text="10" Style="height: 22px;
                                            border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="nav_1" style="width: 560px;">
                <p>
                    用户信息 <span class="spanRed">[<asp:Literal runat="server" ID="txtUserCount"></asp:Literal>]</span></p>
            </div>
            <div class="divDeptInfo">
                <table style="width: 570px;">
                    <tr>
                        <td colspan="7" style="text-align: right; height: 30px;">
                            <input type="button" value="添加用户" class="btnCommon" id="btnAddUser" runat="server"
                                onclick="UserAction('','new')" />
                            <asp:HiddenField ID="hfSelectUserAD" runat="server" />
                            <asp:Button ID="btnAddUserHF" Style="display: none;" runat="server" Text="添加用户" OnClick="btnAddUserHF_Click" />
                            <asp:Button ID="btnDeleteUser" CssClass="btnCommon" runat="server" Text="删除用户" OnClientClick="return ValidateData('确认删除人员信息？','btnDeleteUserHF');" />
                            <asp:Button ID="btnDeleteUserHF" Style="display: none;" runat="server" Text="删除用户"
                                OnClick="btnDeleteUser_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="padding-left: 18px;">
                            <asp:GridView BorderColor="#F2DD81" BorderWidth="1px" Width="550px" CssClass="girdView"
                                runat="server" ID="gvUser" AlternatingRowStyle-CssClass="gvAltItem" DataKeyNames="ID"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkUser" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CHName" HeaderText="中文名称">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ENName" HeaderText="英文名称">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ADAccount" HeaderText="AD账号" Visible="false">
                                        <ItemStyle Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="邮箱">
                                        <ItemStyle Width="140px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="70px" />
                                        <HeaderTemplate>
                                            工作地点
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl1" Text='<%#GetWorkPlace(Eval("WorkPlace").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CostCenter" HeaderText="成本中心" Visible="false">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href="#" onclick="UserAction('<%#Eval("ID") %>','edit')">查看</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BorderColor="#F2DD81" BorderWidth="1px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        function ValidateData(msg, ctl) {
            top.window.ymPrompt.confirmInfo(
                        {
                            message: msg,
                            title: '删除',
                            handler: function ConFirm(tp) {
                                if (tp == "ok") {
                                    document.getElementById(ctl).click();
                                }
                            }
                        });
            return false;
        }
        function DeptAction(state) {
            var deptCode = '<%=DeptCode %>';
            var orgCode = '<%=OrgCode %>';
            var random = Math.round(Math.random() * 10000);
            var title = '编辑部门';
            if (state == 'new') {
                title = '添加部门';
            }
            var url = '../Admin/Popup/DeptEdit.aspx?';
            var para = "random=" + random + "&deptCode=" + deptCode + "&action=" + state + "&orgCode=" + orgCode;
            top.ymPrompt.win({ message: url + para, width: 360, height: 280, title: title, handler: TrueInfos, iframe: true, titleBar: true });

        }
        function UserAction(userCode, state) {
            var deptCode = '<%=DeptCode %>';

            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&deptCode=" + deptCode + "&userCode=" + userCode + "&action=" + state + "&from=ORG";
            if (state == 'new') {
                para = "?checkstyle=false&random=" + random;
                top.ymPrompt.win({ message: '../Search/K2FlowCheck/K2FlowCheck.aspx' + para, width: 760, height: 560, title: "人员选择", handler: AddUserTrueInfos, iframe: true, titleBar: true });
            }
            else {
                top.ymPrompt.win({ message: '../Admin/Popup/UserView.aspx?' + para, width: 830, height: 550, title: '查看用户', handler: null, iframe: true, titleBar: true });
            }
        }
        function AddUserTrueInfos(retValue) {
            switch (retValue) {
                case "close": top.ymPrompt.close(); break;
                case "cancel": top.ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length > 0) {
                        var userad = '';
                        for (var i = 0; i < retValue.length; i++) {
                            if (userad == '') {
                                userad = retValue[i].split(';')[7];
                            }
                            else {
                                userad += ";" + retValue[i].split(';')[7];
                            }
                        }
                        document.getElementById('hfSelectUserAD').value = userad;
                        document.getElementById('btnAddUserHF').click();

                    }
                    top.ymPrompt.close();
                    break;
            }
        }
        function OrgAction(state) {
            var orgCode = '<%=OrgCode %>';
            var height = '650px';
            var title = '编辑组织';
            if (state == 'new') {
                title = '添加组织';
            }
            var url = '../Admin/Popup/OrgEdit.aspx?';
            var random = Math.round(Math.random() * 10000);

            var para = "random=" + random + "&orgCode=" + orgCode + "&action=" + state;
            top.ymPrompt.win({ message: url + para, width: 360, height: 260, title: title, handler: TrueInfos, iframe: true, titleBar: true });

        }
        function TrueInfos(retValue) {
            switch (retValue) {
                case "close":
                    top.ymPrompt.close();
                    break;
                case "failed":
                    top.ymPrompt.alert({ title: '提示信息', message: "失败" });
                    break;
                case "ok":
                    top.ymPrompt.alert({ title: '提示信息', message: "成功" });
                    document.getElementById('lbReload').click();
                    break;
            }
        }
    </script>
</body>
</html>
