<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_HR_EmployeeNeed.aspx.cs"
    Inherits="Workflow_ApprovePage_A_HR_EmployeeNeed" %>

<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    人员需求申请单
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    需求部门：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    岗位：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPosition" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    申请时间：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    需求原因：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbReason" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    人数：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbNumber" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要职责和工作目标：
                                </th>
                                <td colspan="5">
                                    <asp:TextBox ID="tbMajorDuty" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="700" Height="150" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    所属资格与条件：
                                </th>
                                <td colspan="5">
                                    <table class="wf_table2" cellspacing="1" cellpadding="0">
                                        <tr>
                                            <th>
                                                性别：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbSex" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <th>
                                                年龄：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbAge" runat="server" CssClass="txt" Width="120px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                学历最低要求：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbEducation" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <th>
                                                专业：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbSpecialty" runat="server" CssClass="txt" Width="120px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                职称最低要求：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbTitle" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <th>
                                                相关工作年限：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbWorkingLifetim" runat="server" CssClass="txt" Width="120px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                希望上班时间：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbWorkTime" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <th>
                                                专业能力概述：
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbProfessionalAbility" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                                    Width="350" Height="45" ReadOnly="true">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="5">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3016" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    用人部门意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4051" Node="用人部门意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源部意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4052" Node="人力资源部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4054" Node="相关董事分管领导意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4056" Node="董事长CEO意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--                    <AB:ApprovalBox ID="ApprovalBox1" runat="server"  />--%>
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
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign ID="AddSign1" runat="server" />
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
