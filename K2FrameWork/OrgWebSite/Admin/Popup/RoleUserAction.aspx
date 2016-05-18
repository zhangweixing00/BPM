<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="RoleUserAction.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.RoleUserAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色人员</title>
    <script type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
    <script type="text/javascript">
        //选部门
        function OpenDeptSelect() {
            var deptcodes = document.getElementById('<%=hfDeptCode.ClientID %>').value;
            var random = Math.round(Math.random() * 10000);
            var parm = "random=" + random + "&DeptCode=" + deptcodes;
            var retValue = window.showModalDialog("SelectDeptMuti.aspx?" + parm, window, "dialogHeight: 260px; scroll:yes; dialogWidth: 550px; edge: Raised; center: Yes; help: No; resizable: no; status: No;");
            if (retValue) {
                document.getElementById('hfDeptCode').value = retValue[0];
                document.getElementById('txtDeptName').value = retValue[1];
            }
        }

        //        function OpenDeptSelect() {
        //            top.ymPrompt.resizeWin(760, 550);
        //            var deptcodes = document.getElementById('<%=hfDeptCode.ClientID %>').value;
        //            var random = Math.round(Math.random() * 10000);
        //            var parm = "random=" + random + "&DeptCode=" + deptcodes;
        //            var title = "配置角色";
        //            top.ymPrompt.win({ message: "../../Admin/Popup/SelectDeptMuti.aspx?" + parm, width: 500, height: 400, title: title, handler: TrueInfos, iframe: true, titleBar: true });
        //        }

        //        function TrueInfos(retValue) {
        //            switch (retValue) {
        //                case "close": ymPrompt.close(); break;
        //                case "cancel": ymPrompt.close(); break;
        //                default:
        //                    document.getElementById("AddDelegation1_txtToUser").value = retValue[0].split(';')[0];
        //                    document.getElementById("AddDelegation1_hfToUser").value = retValue[0].split(';')[1];

        //                    ymPrompt.close();
        //                    break;
        //            }

        //            //document.getElementById("AddDelegation1_txtToUser").blur();
        //            top.ymPrompt.resizeWin(380, 330);
        //        }

        //选人
        function OpenUserSelect() {
            var random = Math.round(Math.random() * 10000);
            var parm = "random=" + random;
            var retValue = window.showModalDialog("SelectSingleUser.aspx?" + parm, window, "dialogHeight: 500px; scroll:yes; dialogWidth: 600px; edge: Raised; center: Yes; help: No; resizable: no; status: No;");
            if (retValue) {
                var retArray = retValue.split(';');
                document.getElementById('txtRoleUser').value = retArray[1];
                document.getElementById('hfRoleUser').value = retArray[2];
            }
        }
    </script>
    <div class="divCommon">
        <table style="font-size: 8pt;" class="tbCommon">
            <tr>
                <td>
                    所属角色
                </td>
                <td>
                    <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" ReadOnly="true" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
                <td>
                    角色人员
                </td>
                <td>
                    <asp:TextBox ID="txtRoleUser" runat="server" MaxLength="50" ReadOnly="true" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                    <asp:HiddenField ID="hfRoleUser" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    负责区域
                </td>
                <td>
                    <%--<asp:DropDownList ID="ddlDutyRegion" runat="server" Width="120">
                    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    <asp:ListItem Value="BJ">北京</asp:ListItem>
                    <asp:ListItem Value="HK">香港</asp:ListItem>
                </asp:DropDownList>--%>
                    <asp:TextBox ID="ddlDutyRegion" runat="server" MaxLength="50" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
                <td>
                    负责部门
                </td>
                <td>
                    <asp:TextBox ID="txtDeptName" runat="server" MaxLength="1000" Style="height: 22px;
                        border: 1px #999999 solid;" onclick="OpenDeptSelect(); return false;"></asp:TextBox>
                    <asp:HiddenField ID="hfDeptCode" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    扩展属性1
                </td>
                <td>
                    <asp:TextBox ID="txtExpand1" runat="server" MaxLength="100" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
                <td>
                    扩展属性2
                </td>
                <td>
                    <asp:TextBox ID="txtExpand2" runat="server" MaxLength="100" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    扩展属性3
                </td>
                <td>
                    <asp:TextBox ID="txtExpand3" runat="server" MaxLength="100" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
                <td>
                    扩展属性4
                </td>
                <td>
                    <asp:TextBox ID="txtExpand4" runat="server" MaxLength="100" Style="height: 22px;
                        border: 1px #999999 solid;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="btnCommon" />
                    &nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" Text="关闭" OnClientClick="window.close();return false;" CssClass="btnCommon" />--%>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfID" runat="server" />
    </div>
    </form>
</body>
</html>
