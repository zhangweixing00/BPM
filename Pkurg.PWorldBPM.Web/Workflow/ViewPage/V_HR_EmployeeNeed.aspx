<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_EmployeeNeed.aspx.cs"
    Inherits="Workflow_ViewPage_V_HR_EmployeeNeed" %>

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
                    人员需求申请单
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:Label ID="tbReportCode" runat="server" />
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
                                    <asp:Label ID="tbDeptName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    岗位：
                                </th>
                                <td>
                                    <asp:Label ID="tbPosition" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    申请时间：
                                </th>
                                <td>
                                    <asp:Label ID="tbDateTime" runat="server"></asp:Label>
                                </td>
                                <th>
                                    需求原因：
                                </th>
                                <td>
                                    <asp:Label ID="tbReason" runat="server"></asp:Label>
                                </td>
                                <th>
                                    人数：
                                </th>
                                <td>
                                    <asp:Label ID="tbNumber" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要职责和工作目标：
                                </th>
                                <td colspan="5">
                                    <asp:Label ID="tbMajorDuty" runat="server">
                                    </asp:Label>
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
                                                <asp:Label ID="tbSex" runat="server"></asp:Label>
                                            </td>
                                            <th>
                                                年龄：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbAge" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                学历最低要求：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbEducation" runat="server"></asp:Label>
                                            </td>
                                            <th>
                                                专业：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbSpecialty" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                职称最低要求：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbTitle" runat="server"></asp:Label>
                                            </td>
                                            <th>
                                                相关工作年限：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbWorkingLifetim" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                希望上班时间：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbWorkTime" runat="server"></asp:Label>
                                            </td>
                                            <th>
                                                专业能力概述：
                                            </th>
                                            <td>
                                                <asp:Label ID="tbProfessionalAbility" runat="server">
                                                </asp:Label>
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
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3016"
                                        IsOnlyRead="true" />
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
                                    用人部门意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4051" Node="用人部门意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源部意见:
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
