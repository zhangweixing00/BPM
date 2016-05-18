<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_Employment.aspx.cs" Inherits="Workflow_ViewPage_V_HR_Employment" %>
<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看页面</title>
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
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    员工录用审批单
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:label ID="tbReportCode" runat="server"/>
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
                                <td>
                                    <asp:label ID="tbUserName" runat="server"></asp:label>
                                </td>
                                <th>
                                    部门：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbDeptName" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    目标岗位：
                                </th>
                                <td>
                                    <asp:label ID="tbGoalPost" runat="server"></asp:label>
                                </td>
                                <th>
                                    岗位级别：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbPostLevel" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    薪资情况：
                                </th>
                                <td colspan="5">
                                    工资（税前）：<asp:label ID="tbSalary" runat="server"></asp:label>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:label ID="tbRatio" runat="server"></asp:label>%&nbsp;&nbsp;
                                    目标年薪（税前）：<asp:label ID="tbAnnualSalary" runat="server"></asp:label>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同期限：
                                </th>
                                <td colspan="5">
                                    <asp:label ID="tbIsLabourContract" runat="server"></asp:label>
                                    <asp:label ID="tbLabourContractStart" runat="server"></asp:label>
                                    至<asp:label ID="tbLabourContractEnd" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    试用期待遇：
                                </th>
                                <td colspan="5">
                                    <asp:label ID="tbIsProbationPeriod" runat="server"></asp:label>试用期
                                    <asp:label ID="tbProbationPeriodStart" runat="server"></asp:label>
                                    至<asp:label ID="tbProbationPeriodEnd" runat="server"></asp:label>
                                    (注：试用期待遇：月固定工资80%，试用期纳入年终综合考评。)
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="5">
                                    <asp:label ID="tbRemark" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="5">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3017" />
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
    </form>
</body>
</html>

