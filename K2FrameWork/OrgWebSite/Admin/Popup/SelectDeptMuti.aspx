<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="SelectDeptMuti.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.SelectDeptMuti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择部门</title>
    <script type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <base target="_self" />
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
    <script type="text/javascript">
        function ChooseDept(chk) {
            var hfUserCode = cibc(chk) + "hfDeptCode";
            var hfDeptName = cibc(chk) + "hfDeptName";

            var vueSelectedUser = $$("hfSelectedDept").value;
            var vueSelectedDeptName = $$("hfSelectedDeptName").value;

            if (chk.checked) {
                vueSelectedUser += $$(hfUserCode).value + ";";
                vueSelectedDeptName += $$(hfDeptName).value + ";";
                $$("hfSelectedDept").value = vueSelectedUser;
                $$("hfSelectedDeptName").value = vueSelectedDeptName;
            }
            else {
                vueSelectedUser = vueSelectedUser.replace($$(hfUserCode).value + ";", "");
                vueSelectedDeptName = vueSelectedDeptName.replace($$(hfDeptName).value + ";", "");
                $$("hfSelectedDept").value = vueSelectedUser;
                $$("hfSelectedDeptName").value = vueSelectedDeptName;
            }
        }

        function EditChargeDept() {
            var vueSelectDept = $$("hfSelectedDept").value;
            var vueSelectDeptName = $$("hfSelectedDeptName").value;
            if (vueSelectDept == '') {
                //alert("请选择部门");
                ymPrompt.alert('请选择部门！', null, null, '错误', null)
                return false;
                return;
            }
            var retArray = new Array();
            retArray.push(vueSelectDept);
            retArray.push(vueSelectDeptName);
            window.returnValue = retArray;
            window.close();
            //            window.parent.ymPrompt.doHandler(retArray, false);
        }
    </script>
    <div style="width: 100%">
        <div class="BlankTitle" style="text-align: right;">
            <%--<asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>--%>
            <asp:HiddenField runat="server" ID="hfSelectedDept" />
            <asp:HiddenField runat="server" ID="hfSelectedDeptName" />
        </div>
        <div style="width: 100%;">
            <asp:GridView ID="gvDept" BorderColor="#F2DD81" BorderWidth="1px" Width="550px" CssClass="girdView"
                runat="server" AlternatingRowStyle-CssClass="gvAltItem" DataKeyNames="DeptCode"
                AutoGenerateColumns="false" OnRowDataBound="gvDept_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkDept" onclick="ChooseDept(this)" />
                            <asp:HiddenField runat="server" ID="hfDeptCode" Value='<%#Eval("DeptCode") %>' />
                            <asp:HiddenField runat="server" ID="hfDeptName" Value='<%#Eval("DeptName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部门名称">
                        <ItemTemplate>
                            <%#Eval("DeptName")%>
                        </ItemTemplate>
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
        </div>
        <div style="margin-left: 235px;">
            <asp:Button ID="btnSave" runat="server" Text="确 定" CssClass="btnCommon" OnClientClick="EditChargeDept();return false;"
                Style="text-align: center;" />
            <%--<asp:Button ID="btnCancel" runat="server" Text="取消" />--%>
        </div>
    </div>
    </form>
</body>
</html>
