<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="SelectSingleUser.aspx.cs" Inherits="OrgWebSite.Admin.Popup.SelectSingleUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员选择</title>
    <base target="_self" />
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
        <div style="margin-top: 5px; margin-right: 10px;">
            <div style="float: right;">
                <asp:TextBox runat="server" CssClass="txtSearch" ID="txtFilter"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearch" CssClass="btnCommon" Text="查找" OnClick="btnSearch_Click" />
            </div>
            <div class="divSelectMain">
                <asp:GridView ID="gvUser" runat="server" HeaderStyle-CssClass="gvHeader" RowStyle-CssClass="gvRow"
                    AlternatingRowStyle-CssClass="gvAltItem" DataKeyNames="ID" AutoGenerateColumns="False"
                    CssClass="gv" OnRowDataBound="gvUser_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvUser_PageIndexChanging" PageSize="15">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton runat="server" ID="rbtnUser" ToolTip='<%#Container.DataItemIndex %>'
                                    onclick="setRadio(this)" />
                                <asp:HiddenField runat="server" ID="hfADAccount" Value='<%#Eval("ADAccount") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href='#' onclick="UserView('<%#Eval("ID") %>')">明细</a>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文名称">
                            <ItemTemplate>
                                <%#Eval("CHName") %>
                                <asp:HiddenField runat="server" ID="hfCHName" Value='<%#Eval("CHName") %>' />
                                <%--<asp:TextBox runat="server" CssClass="txtSelectCommon" ID="txtCHName" Text='<%#Eval("CHName") %>'></asp:TextBox>--%>
                            </ItemTemplate>
                            <HeaderStyle Width="35%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="英文名称">
                            <ItemTemplate>
                                <%#Eval("ENName") %>
                                <asp:HiddenField runat="server" ID="hfENName" Value='<%#Eval("ENName") %>' />
                                <%--<asp:TextBox runat="server" CssClass="txtSelectCommon" ID="txtENName" Text='<%#Eval("ENName") %>'></asp:TextBox>--%>
                            </ItemTemplate>
                            <HeaderStyle Width="30%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属地区">
                            <ItemTemplate>
                                <%#GetWorkPlace(Eval("WorkPlace").ToString()) %>
                                <%--<asp:TextBox runat="server" CssClass="txtSelectCommon" ID="txtWP" Text='<%#GetWorkPlace(Eval("WorkPlace").ToString()) %>'></asp:TextBox>--%>
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="gvRow" />
                    <HeaderStyle CssClass="gvHeader" />
                    <AlternatingRowStyle CssClass="gvAltItem" />
                </asp:GridView>
            </div>

            <script language="javascript" type="text/javascript">

                function ValidateData() {
                    var hfValue = document.getElementById('hfSelectUser').value;
                    if (hfValue == "-1") {
                        alert("请选择用户Please choose user.");
                        return false;
                    }

                    return true;
                }

                function setRadio(nowRadio) {
                    var myForm, objRadio;
                    myForm = document.forms[0];
                    document.getElementById('<%=hfSelectUser.ClientID %>').value = nowRadio.parentNode.title;

                    for (var i = 0; i < myForm.length; i++) {
                        if (myForm.elements[i].type == "radio") {
                            objRadio = myForm.elements[i];
                            if (objRadio != nowRadio && objRadio.name.indexOf("gvUser") > -1 && objRadio.name.indexOf("rbtnUser") > -1) {
                                //alert(objRadio.name);
                                if (objRadio.checked) {
                                    objRadio.checked = false;
                                }
                            }
                        }
                    }
                }

                function UserView(usercode) {
                    window.open('UserView.aspx?userCode=' + usercode, '', 'width=900,height=550,toolbar=no,scrollbars=yes');
                }
            </script>

            <asp:HiddenField runat="server" ID="hfSelectUser" />
            <asp:Button ID="btnChoose" runat="server" CssClass="btnCommon" Text="选择" OnClick="btnChoose_Click"
                OnClientClick="return ValidateData()" />
            <input type="button" id="btnCancel" value="取消" class="btnCommon" onclick="window.opener=null;window.open('','_self','');window.close();" />
            <asp:Literal runat="server" ID="litScript"></asp:Literal>
        </div>
    </form>
</body>
</html>
