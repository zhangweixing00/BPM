<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_SalaryAdjust.aspx.cs" Inherits="Workflow_ViewPage_V_HR_SalaryAdjust" %>
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
                    薪酬调整审批单
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
                                    现任职务：
                                </th>
                                <td>
                                    <asp:label ID="tbPost" runat="server"></asp:label>
                                </td>
                                <th>
                                    现所在公司及部门：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbDeptName" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    工作地点：
                                </th>
                                <td>
                                    <asp:label ID="tbWorkPlace" runat="server"></asp:label>
                                </td>
                                <th>
                                    拟任职务：
                                </th>
                                <td>
                                    <asp:label ID="tbToPost" runat="server"></asp:label>
                                </td>
                                <th>
                                    拟调入公司及部门：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbToDeptName" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    工资（税前）：
                                </th>
                                <td>
                                    <asp:label ID="tbSalary" runat="server"></asp:label>
                                    <asp:Label id="Label1" runat="server" Text="万元/年"></asp:Label>
                                </td>
                                <th>
                                    现业绩奖金基数：
                                </th>
                                <td>
                                    <asp:label ID="tbRatio" runat="server"></asp:label>
                                    <asp:Label id="Label2" runat="server" Text="%"></asp:Label>
                                </td>
                                <th>
                                    目标年薪（税前）：
                                </th>
                                <td>
                                    <asp:label ID="tbAnnualSalary" runat="server"></asp:label>
                                    <asp:Label id="Label3" runat="server" Text="万元/年"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    建议工资（税前）：
                                </th>
                                <td>
                                    <asp:label ID="tbToSalary" runat="server"></asp:label>
                                    <asp:Label id="Label4" runat="server" Text="万元/年"></asp:Label>
                                </td>
                                <th>
                                    建议业绩奖金基数：
                                </th>
                                <td>
                                    <asp:label ID="tbToRatio" runat="server"></asp:label>
                                    <asp:Label id="Label5" runat="server" Text="%"></asp:Label>
                                </td>
                                <th>
                                    建议约等于目标年薪（税前）：
                                </th>
                                <td>
                                    <asp:label ID="tbToAnnualSalary" runat="server"></asp:label>
                                    <asp:Label id="Label6" runat="server" Text="万元/年"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟生效日期：
                                </th>
                                <td>
                                    <asp:label ID="tbEffectiveDate" runat="server"></asp:label>
                                </td>
                                <th>
                                    调薪原因：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbReason" runat="server">
                                    </asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="5">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3021" IsOnlyRead="true"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr id="trRDeptManager" runat="server">
                                <th>
                                    相关部门意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4051" Node="用人部门意见" runat="server" />
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
                                    人力资源部意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4052" Node="人力资源部审核" runat="server" />
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
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:HiddenField ID="hfIsGroup" runat="server" />
                    <asp:HiddenField ID="hfApprovers" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

