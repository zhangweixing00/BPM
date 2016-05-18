<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="RoleManage.aspx.cs"
    Inherits="OrgWebSite.Admin.RoleManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>
    <script type="text/javascript" src="../Javascript/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="BlankTitle" style="text-align: right;">
            <input type="button" class="btnCommon" onclick="RoleAction('','new')" onmouseover="this.className='btnCommonOver'"
                onmouseout="this.className='btnCommon'" id="btnAdd" value="添加角色" />
            <asp:Button runat="server" CssClass="btnCommon" ID="btnDelete" Text="删除角色" onmouseover="this.className='btnCommonOver'"
                onmouseout="this.className='btnCommon'" OnClientClick="return confirm('确认删除？');"
                OnClick="btnDelete_Click" />
            <asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>
        </div>
        <div>
            <asp:GridView ID="gvRole" CssClass="gv" HeaderStyle-CssClass="gvHeader" AlternatingRowStyle-CssClass="gvAltItem"
                DataKeyNames="ID" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                OnPageIndexChanging="gvRole_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkRole" />
                            <asp:HiddenField runat="server" ID="hfRoleCode" Value='<%#Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleName" HeaderText="角色名称" HeaderStyle-Width="60%" />
                    <asp:BoundField DataField="ProcessType" HeaderText="流程名称" HeaderStyle-Width="30%" />
                    <asp:TemplateField HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <a href="#" onclick="RoleAction('<%#Eval("ID") %>','edit')">
                                <img src="../Img/edit.png" alt="编辑" /></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:HiddenField runat="server" ID="hfSelectedRoleCode" />
            <script language="javascript" type="text/javascript">
                function RoleAction(roleCode, action) {
                    var random = Math.round(Math.random() * 10000);
                    var para = "random=" + random + "&roleCode=" + roleCode + "&action=" + action;
                    var retValue = window.showModalDialog('Popup/RoleAction.aspx?' + para, window, 'dialogHeight: 100px; scroll:yes; dialogWidth: 250px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
                    if (typeof (retValue) != "undefined") {
                        $$('lbReload').click();
                    }
                }
            </script>
        </div>
    </div>
    </form>
</body>
</html>
