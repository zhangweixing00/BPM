<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StepOperation.aspx.cs" Inherits="Workflow_CustomWorkFlow_StepOperation" %>

<%@ Register Src="../../Modules/Custom/ShowUserNames.ascx" TagName="ShowUserNames"
    TagPrefix="uc1" %>
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
    <style type="text/css">
        .style1
        {
            width: 59px;
        }
    </style>
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
                        添加流程步骤</div>
                    <div style="float: right">
                        <asp:Button ID="btnYes" runat="server" Text=" 确定 " CssClass="gray_btn2" OnClick="btnYes_Click" ValidationGroup="s"/>
                        &nbsp;<asp:Button ID="Button2" runat="server" Text=" 关闭 " CssClass="gray_btn2" OnClientClick="javascript:window.returnValue=2;window.close();" />
                    </div>
                </div>
                <div class="content">
                    <table class="SFormTable">
                        <tbody>
                            <tr>
                                <td>
                                    步骤名称（必填）:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbStepName" runat="server" Width="300px" ValidationGroup="s"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbStepName"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="s"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    选择结果（可多选）：
                                </td>
                                <td>
                                    <uc1:ShowUserNames ID="ShowUserNames1" runat="server" IsShowDelete="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                    <table class="FormTable">
                                        <tbody>
                                            <tr>
                                                <th style="width: 50px;">
                                                    姓名： 
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt" Width="100"></asp:TextBox>
                                                </td>
                                                <th class="style1">
                                                    登录名： 
                                                </th>
                                                <td>
                                                    <asp:TextBox ID="tbLoginName" runat="server" CssClass="txt" Width="100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <%--green_btn--%>
                                                    <asp:Button ID="btnQuery" runat="server" CssClass="green_btn2" 
                                                        OnClick="btnQuery_Click" Text=" 查询 " />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="content">
                                        <asp:GridView ID="gvData" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" CssClass="List" 
                                            HeaderStyle-HorizontalAlign="Center" 
                                            OnPageIndexChanging="gvData_PageIndexChanging" PagerSettings-FirstPageText="首页" 
                                            PagerSettings-LastPageText="尾页" PagerSettings-Mode="NumericFirstLast" 
                                            PagerStyle-CssClass="anpager" PagerStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="50px">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbSelected" runat="server" 
                                                            CommandArgument='<%#Eval("LoginName") %>' OnCommand="lbSelected_Command" 
                                                            Text="选择"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeName" HeaderText="姓名" />
                                                <asp:BoundField DataField="LoginName" HeaderText="登录名" />
                                                <asp:BoundField DataField="CompanyName" HeaderText="公司"><ItemStyle wrap="false" /></asp:BoundField>
                                                <asp:BoundField DataField="DepartName" HeaderText="部门" ></asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div>
                </div>
                <input id="hd_SelectType" type="hidden" runat="server" value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
