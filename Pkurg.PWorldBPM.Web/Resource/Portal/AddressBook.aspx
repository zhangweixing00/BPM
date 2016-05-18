<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddressBook.aspx.cs" Inherits="Portal_AddressBook" %>

<%@ Register Src="/UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>通讯录</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <style>
        .txt 
        {
            width: 100px;
        }
        .FormTable th
        {
            width: 50px;
        }
        .anpager span
        {
            background: none;
            border: none;
        }
    </style>
</head>
<body id="top">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="page">
        <uc1:Header ID="Header1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="container">
                    <div id="left" style="width: 250px; border: 1px solid #eaeaea;">
                        <div class="content" style="height: 450px; overflow: auto;">
                            <asp:TreeView ID="tvDepts" SelectedNodeStyle-Font-Bold="true" SelectedNodeStyle-ForeColor="Green"
                                runat="server" OnSelectedNodeChanged="tvDepts_SelectedNodeChanged" ShowLines="True">
                            </asp:TreeView>
                        </div>
                    </div>
                    <div id="right" style="width: 930px;">
                        <div class="container">
                            <div class="titlebg">
                                <div class="title">
                                    通讯录(<asp:Label ID="lblCount" runat="server" ToolTip="总数"></asp:Label>)</div>
                                <div class="new">
                                </div>
                            </div>
                            <table class="FormTable">
                                <tbody>
                                    <tr>
                                        <th colspan="9" style="text-align: left; padding-left: 10px; font-size: 13px;">
                                            当前位置：
                                            <asp:Label ID="lblCompanyName" runat="server" Text="北大资源"></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            姓名：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <th>
                                            座机：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtTelephone" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <th>
                                            手机：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <th>
                                            邮箱：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox><span style="font-size: 13px;">@founder.com</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCompanyCode" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                            <asp:TextBox ID="txtDeptCode" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                            <asp:Button ID="btnQuery" runat="server" CssClass="green_btn" Text=" 查询 " OnClick="btnQuery_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="content">
                                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="List">
                                            <tr>
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    姓名
                                                </th>
                                                <th>
                                                    公司
                                                </th>
                                                <th>
                                                    部门
                                                </th>
                                                <th>
                                                    职位
                                                </th>
                                                <th>
                                                    座机
                                                </th>
                                                <th>
                                                    手机
                                                </th>
                                                <th>
                                                    电子邮箱
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                            onmouseout="this.style.backgroundColor=c;">
                                            <td>
                                                <%# Container.ItemIndex + 1+this.AspNetPager1.PageSize*(this.AspNetPager1.CurrentPageIndex-1)%>
                                            </td>
                                            <td>
                                                <%#Eval("EmployeeName")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepartName" runat="server" Text='<%# Eval("DepartName")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#GetPosition(Container.DataItem)%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOfficePhone" runat="server" Text='<%# Eval("OfficePhone")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMobilePhone" runat="server" Text='<%# Eval("MobilePhone")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <a href="mailto:<%# Eval("Email")%>">
                                                    <%# Eval("Email")%></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <pager:AspNetPager ID="AspNetPager1" runat="server" CssClass="anpager" CurrentPageButtonClass="cpb"
                                    FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" PageSize="10"
                                    OnPageChanged="AspNetPager1_PageChanged">
                                </pager:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="footer" style="height: 20px;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tr>
                    <td class="Foot_bg" align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                        Copyright &copy; 2014 北大资源
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
