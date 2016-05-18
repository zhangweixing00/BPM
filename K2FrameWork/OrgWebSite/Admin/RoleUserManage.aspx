<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="RoleUserManage.aspx.cs"
    Inherits="OrgWebSite.Admin.RoleUserManage" %>

<%@ Register Src="~/Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色人员管理</title>
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
    <script type="text/javascript">
        function EditRoleUser(roleCode) {
            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&roleCode=" + roleCode;
            var retValue = window.showModalDialog("Popup/RoleUserEdit.aspx?" + para, window, 'dialogHeight: 500px; scroll:yes; dialogWidth: 600px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
            if (typeof (retValue) != "undefined") {
                document.getElementById('lbReload').click();
            }
        }

        //添加用户
        //        function UserAction() {
        //            var random = Math.round(Math.random() * 10000);
        //            var para = "random=" + random;
        //            var retValue = window.showModalDialog("Popup/SelectSingleUser.aspx?" + para, window, 'dialogHeight: 500px; scroll:yes; dialogWidth: 600px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
        //            if (typeof (retValue) != "undefined") {
        //                if (retValue.split(';')[2]) {
        //                    document.getElementById('hfSelectUserAD').value = retValue.split(';')[2];
        //                    document.getElementById('lbAddUser').click();
        //                }
        //            }
        //        }


        function UserAction() {
            var random = Math.round(Math.random() * 10000);
            var para = "?checkstyle=true&random=" + random;
            top.ymPrompt.win({ message: '../Search/K2FlowCheck/K2FlowCheck.aspx' + para, width: 760, height: 560, title: "人员选择", handler: AddUserRoleTrueInfos, iframe: true, titleBar: true });
        }

        function AddUserRoleTrueInfos(retValue) {
            switch (retValue) {
                case "close": top.ymPrompt.close(); break;
                case "cancel": top.ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length > 0) {
                        var userad = '';
                        for (var i = 0; i < retValue.length; i++) {
                            if (userad == '') {
                                userad = retValue[i].split(';')[1];
                            }
                            else {
                                userad += ";" + retValue[i].split(';')[1];
                            }
                        }
                        document.getElementById('hfSelectUserAD').value = userad;
                        document.getElementById('lbAddUser').click();
                    }
                    top.ymPrompt.close();
                    break;
            }
        }


        //编辑用户角色信息
        function EditRoleUserAttribute(userCode, roleCode, Id, op) {
            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&UserCode=" + userCode + "&RoleCode=" + roleCode + "&ID=" + Id + "&op=" + op;
            top.ymPrompt.win({ message: '../Admin/Popup/RoleUserAction.aspx?' + para, width: 400, height: 200, title: "编辑角色", handler: AddUserTrueInfos, iframe: true, titleBar: true });
        }

        //回调函数
        function AddUserTrueInfos(retValue) {
            switch (retValue) {
                case "close": top.ymPrompt.close(); break;
                case "cancel": top.ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length > 0) {
                        document.getElementById('lbReload').click();
                    }
            }
        }


        //创建（编辑）角色
        function RoleAction(action) {
            var roleCode = '';
            var random = Math.round(Math.random() * 10000);
            if (action == 'edit')
                roleCode = document.getElementById('<%=hfSelectNode.ClientID %>').value;
            if (action == 'edit' && roleCode == '')
                return;
            var para = "random=" + random + "&roleCode=" + roleCode + "&action=" + action;
            var url = "../Admin/Popup/RoleAction.aspx?";
            var title = "配置角色";

            top.ymPrompt.win({ message: url + para, width: 300, height: 200, title: title, handler: TrueInfos, iframe: true, titleBar: true });
        }

        function TrueInfos(retValue) {
            top.ymPrompt.close();
            switch (retValue) {
                case "close":
                    top.ymPrompt.close();
                    break;
                case "ok":
                    top.ymPrompt.close();
                    top.ymPrompt.alert({ title: '提示信息', message: "操作成功" });
                    document.getElementById('lbReload').click();
                    break;
                default:
                    top.ymPrompt.close();
                    document.getElementById('lbReload').click();
                    break;
            }
        }
    </script>
    <div id="container" style="width: 790px;">
        <div class="rightTop">
            <p>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
        <div style="width: 200px; float: left; padding-left: 18px;">
            <asp:TreeView ID="tvRole" runat="server" Font-Names="Verdana" ForeColor="Black" HoverNodeStyle-Font-Underline="true"
                HoverNodeStyle-ForeColor="#dd5555" SelectedNodeStyle-ForeColor="#dd5555" SelectedNodeStyle-Font-Underline="true"
                ShowExpandCollapse="true" NodeIndent="10" ShowLines="true" Font-Size="8pt" OnSelectedNodeChanged="tvDept_SelectedNodeChanged">
                <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
                <Nodes>
                    <asp:TreeNode Text="角色" Value="角色"></asp:TreeNode>
                </Nodes>
                <SelectedNodeStyle BorderStyle="Dashed" Font-Underline="True" ForeColor="#DD5555" />
            </asp:TreeView>
        </div>
        <div style="width: 570px; float: left;">
            <div class="nav_1" style="width: 560px;">
                <p>
                    角色基本信息</p>
            </div>
            <div class="divDeptInfo" style="margin-top: 5px;">
                <table style="width: 570px;">
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" class="btnCommon" value="添加角色" id="btnAddDept" onclick="RoleAction('new');" /><input
                                type="button" class="btnCommon" value="编辑角色" id="btnEditDept" onclick="RoleAction('edit');" /><asp:Button
                                    ID="btnDelDept" CssClass="btnCommon" runat="server" Text="删除角色" OnClientClick="return confirm('确认删除相关角色信息？');"
                                    OnClick="btnDelDept_Click" />
                            <asp:LinkButton runat="server" ID="LinkButton1" OnClick="lbReload_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table style="width: 550px;">
                                <tr>
                                    <td class="text">
                                        角色名称
                                    </td>
                                    <td class="text2">
                                        <asp:TextBox runat="server" ID="txtRoleName" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                        <asp:HiddenField ID="hfRoleCode" runat="server" />
                                    </td>
                                    <td class="text">
                                        所属流程
                                    </td>
                                    <td class="text2">
                                        <asp:TextBox ID="txtProcessType" runat="server" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="nav_1" style="width: 560px;">
                <p>
                    角色人员信息</p>
            </div>
            <div class="divDeptInfo">
                <table style="width: 570px;">
                    <tr>
                        <td colspan="7" style="text-align: right; height: 30px;">
                            <input type="button" value="添加用户" class="btnCommon" id="btnAddUser" runat="server"
                                onclick="UserAction()" />
                            <asp:Button ID="btnDeleteUser" CssClass="btnCommon" runat="server" Text="删除用户" OnClientClick="return confirm('确认删除人员信息？');"
                                OnClick="btnDeleteUser_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:GridView BorderColor="#F2DD81" BorderWidth="1px" Width="550px" CssClass="girdView"
                                runat="server" ID="gvUser" AlternatingRowStyle-CssClass="gvAltItem" DataKeyNames="UserCode"
                                AutoGenerateColumns="false" OnRowDataBound="gvUser_RowDataBound" Style="margin-left: 20px;">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkUser" />
                                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CHName" HeaderText="中文名称">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AD" HeaderText="AD账号">
                                        <ItemStyle Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="部门">
                                        <ItemStyle Width="140px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DutyRegion" HeaderText="区域">
                                        <ItemStyle Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpandField1" HeaderText="扩展字段1">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpandField2" HeaderText="扩展字段2">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpandField3" HeaderText="扩展字段3">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExpandField4" HeaderText="扩展字段4">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptCode"></asp:BoundField>
                                    <asp:TemplateField HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <a href="#" onclick="EditRoleUserAttribute('<%#Eval("UserCode") %>','<%#Eval("RoleCode") %>','<%#Eval("ID") %>','edit')">
                                                配置</a>
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
                <asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbAddUser" OnClick="lbAddUser_Click"></asp:LinkButton>
                <asp:HiddenField ID="hfSelectUserAD" runat="server" />
                <asp:HiddenField ID="hfSelectNode" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
