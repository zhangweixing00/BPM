<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeptOperation.aspx.cs" Inherits="Modules_BaseData_DeptOperation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择人员</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //点击回车，自动查询
            $(document).keydown(function (e) {
                if (!e)
                    e = window.event;
                if ((e.keyCode || e.which) == 13) {
                    $('#<%=btnQuery.ClientID%>').click();
                }
            })
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container" style="margin: 5px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="titlebg">
                    <div class="title">
                        查询</div>
                </div>
                <div class="content">
                    <table class="FormTable">
                        <tbody>
                            <tr>
                                <th style="width: 80px;">
                                    部门名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt" Width="70"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnQuery" runat="server" Text=" 查询 " CssClass="green_btn2" OnClick="btnQuery_Click" />
                                    <asp:Button ID="btnClose" runat="server" Text=" 关闭 " CssClass="gray_btn2" OnClientClick="javascript:window.returnValue=2;window.close();" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="content">
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" CssClass="List"
                        AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" PagerSettings-FirstPageText="首页"
                        PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="尾页" PagerStyle-CssClass="anpager"
                        PagerStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    操作
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelected" runat="server" CommandArgument='<%#Eval("Remark") %>'
                                        OnCommand="lbSelected_Command" Text="选择"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="Remark" HeaderText="部门名称"><ItemStyle wrap="false" /></asp:BoundField>
                            <%--<asp:BoundField DataField="DepartName" HeaderText="部门" />--%>
                        </Columns>
                    </asp:GridView>
                </div>
                <input id="hd_SelectType" type="hidden" runat="server" value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
