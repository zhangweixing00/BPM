<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemDetail.aspx.cs" Inherits="Sys_ItemDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发起流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源集团" style="float: left;" />
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    立项考核指标变更
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <th>
                                创建日期：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                保密等级：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <th>
                                文档等级：
                            </th>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                备注：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="TextBox3" runat="server" CssClass="longtxt"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    项目名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    所属公司：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox8" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox11" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    地块：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox12" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    城市：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox13" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    城市公司：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox14" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td>
                                    <asp:TextBox ID="TextBox10" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="LinkButton1" runat="server">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LinkButton2" runat="server">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LinkButton3" runat="server">打印</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LinkButton4" runat="server">关闭</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
