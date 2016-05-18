<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_InternEmploy.aspx.cs"
    Inherits="Workflow_ViewPage_V_HR_InternEmploy" %>

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
                   <%-- 实习生录用审批--%>
                   <%=  FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th>
                                姓名
                            </th>
                            <td>
                                <asp:Label ID="tbEmployeeName" runat="server"></asp:Label>
                            </td>
                            <th>
                                实习岗位
                            </th>
                            <td>
                                <asp:Label ID="tbPosition" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                实习部门
                            </th>
                            <td>
                                <asp:Label ID="tbDept" runat="server"></asp:Label>
                                <asp:TextBox ID="tbDeptCode" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <th>
                                实习补贴
                            </th>
                            <td>
                                <asp:Label ID="tbInternReward" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                岗位级
                            </th>
                            <td>
                                实习生
                            </td>
                            <th>
                                实习期限
                            </th>
                            <td>
                                <asp:Label ID="tbInternDeadline" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上传附件：
                            </th>
                            <td colspan="3">
                                <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="false" AppId="3022" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    人力资源部面试官意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4060" Node="人力资源部面试官意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    用人部门负责人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4061" Node="用人部门负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源负责人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4062" Node="人力资源负责人意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
