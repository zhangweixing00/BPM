<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserExtAction.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.UserExtAction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扩展属性</title>
    <meta content="no-cache" http-equiv="pragma" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu_List" class="pro_1" style="padding-bottom: 30px; top: 5px; width: 600px;">
        <table style="width: 600px;">
            <tr>
                <td colspan="7" style="text-align: right; height: 30px;">
                    <asp:Button runat="server" ID="btnAdd" Text="添加属性" OnClick="btnAdd_Click" CssClass="btnCommon" />
                    <asp:Button runat="server" CssClass="btnCommon" ID="btnDelete" Text="删除属性" OnClientClick="return confirm('确认删除信息？');"
                        OnClick="btnDelete_Click" />
                    <asp:Button ID="btnSave" CssClass="btnCommon" OnClientClick="return ValidateData();"
                        runat="server" Text="保存" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="gvForms" BorderColor="#F2DD81" BorderWidth="1px" Width="600px"
                        CssClass="girdView" AutoGenerateColumns="False" DataKeyNames="UserExtPropID"
                        runat="server" AllowPaging="True" OnPageIndexChanging="gvForms_PageIndexChanging"
                        OnRowCommand="gvForms_RowCommand" EmptyDataText="&lt;div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'&gt;无扩展属性&lt;/div&gt;">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkForm" />
                                    <asp:HiddenField runat="server" ID="hfFormCode" Value='<%#Eval("UserExtPropID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="5%"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserExtProperty" HeaderText="属性名称" HeaderStyle-Width="20%">
                                <HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="属性描述" HeaderStyle-Width="45%">
                                <HeaderStyle Width="45%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Company" DataField="Company" HeaderStyle-Width="15%">
                                <HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbEdit" CommandName="E" CommandArgument='<%#Eval("UserExtPropID") %>'
                                        Text="编辑"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%"></HeaderStyle>
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
        <div style="font-size: 11px; font-family: Arial; width: 600px;">
            <table class="tbCommon" style="width: 400px;">
                <tr>
                    <td style="width: 100px">
                        属性名称:
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="txtProName" Width="300px" CssClass="tableInput" runat="server" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        属性描述:
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="txtProDes" Width="300px" CssClass="tableInput" runat="server" MaxLength="3000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Company:
                    </td>
                    <td style="width: 100px">
                        <asp:TextBox ID="txtProCom" Width="300px" CssClass="tableInput" runat="server" MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="hfSelectedExtPropID" />
        </div>
    </div>
    <script type="text/javascript" charset="gb2312">
        //验证输入完整性
        function ValidateData()
        {
            if (document.getElementById('<%=txtProName.ClientID %>').value == '')
            {
                alert('请输入名称');
                return false;
            }
            if (document.getElementById('<%=txtProDes.ClientID %>').value == '')
            {
                alert('请输入属性描述');
                return false;
            }

            return true;
        }

        function MouseOut()
        {
            var objUnknown;

            objUnknown = window.event.srcElement;

            if (objUnknown.tagName.toUpperCase() == "INPUT")
            {
                objUnknown.style.backgroundImage = "url(../pic/b_base.gif)";
                objUnknown.style.borderRightColor = "#000000";
                objUnknown.style.borderTopColor = "#000000";
                objUnknown.style.borderLeftColor = "#000000";
                objUnknown.style.borderBottomColor = "#000000";

            }
            return true;
        }

        function MouseOver()
        {

            var objUnknown;

            objUnknown = window.event.srcElement;
            //alert("enter MouseOver:"+objUnknown.tagName );

            if (objUnknown.tagName.toUpperCase() == "INPUT")
            {
                objUnknown.style.backgroundImage = "url(../pic/b_blue.gif)";
                objUnknown.style.borderRightColor = "#000000";
                objUnknown.style.borderTopColor = "#000000";
                objUnknown.style.borderLeftColor = "#000000";
                objUnknown.style.borderBottomColor = "#000000";

            }
            return true;
        }
    </script>
    </form>
</body>
</html>
