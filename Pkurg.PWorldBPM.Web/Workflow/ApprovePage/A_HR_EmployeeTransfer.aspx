<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_HR_EmployeeTransfer.aspx.cs" Inherits="Workflow_ApprovePage_A_HR_EmployeeTransfer" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblTransferReason");
        });
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
                    员工流动审批
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" Width="180" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    姓名：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbUserName" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    性别：
                                </th>
                                <td >
                                    <asp:TextBox ID="tbSex" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    入司时间：
                                </th>
                                <td >
                                    <asp:TextBox ID="tbEntryTime" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    毕业院校及专业：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbGraduation" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    学历：
                                </th>
                                <td >
                                    <asp:TextBox ID="tbEducation" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    加入方正时间：
                                </th>
                                <td >
                                    <asp:TextBox ID="tbFounderTime" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现所在公司及部门：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbDeptName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    职务：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPost" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    职级：
                                </th>
                                <td >
                                    <asp:TextBox ID="tbPostLevel" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入公司及部门：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbToDeptName" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    职务：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToPost" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    职级：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToPostLevel" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现单位合同情况：
                                </th>
                                <td colspan="7">
                                    起始日期<asp:TextBox ID="tbLabourContractStart" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    到期日期<asp:TextBox ID="tbLabourContractEnd" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现单位薪金情况：
                                </th>
                                <td colspan="7">
                                    工资（税前）：<asp:TextBox ID="tbSalary" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:TextBox ID="tbRatio" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>%&nbsp;&nbsp;
                                    约等于目标年薪（税前）：<asp:TextBox ID="tbAnnualSalary" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入单位合同情况：
                                </th>
                                <td colspan="7">
                                    起始日期<asp:TextBox ID="tbToLabourContractStart" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    到期日期<asp:TextBox ID="tbToLabourContractEnd" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入单位薪金情况：
                                </th>
                                <td colspan="7">
                                    工资（税前）：<asp:TextBox ID="tbToSalary" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:TextBox ID="tbToRatio" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>%&nbsp;&nbsp;
                                    约等于目标年薪（税前）：<asp:TextBox ID="tbToAnnualSalary" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    本人意见：
                                </th>
                                <td colspan="7">
                                    内部流动原因：
                                    <asp:CheckBoxList ID="cblTransferReason" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="false">
                                        <asp:ListItem Value="公司行为的内部调动">公司行为的内部调动</asp:ListItem>
                                        <asp:ListItem Value="员工本人申请的内部流动">员工本人申请的内部流动</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <AB:ApprovalBox ID="ApprovalBox1" Node="员工意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="7">
                                    <asp:TextBox ID="tbRemark" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="700" Height="40" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="7">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3019" />
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
