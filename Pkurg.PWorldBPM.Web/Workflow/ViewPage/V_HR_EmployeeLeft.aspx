<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_EmployeeLeft.aspx.cs" Inherits="Workflow_ViewPage_V_HR_EmployeeLeft" %>
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
                    <%--员工离职\调动转单--%>
                    <%=  FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    <strong>离职人员基本信息</strong>
                                </th>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    员工姓名：
                                </th>
                                <td>
                                    <asp:Label ID="tbEmployeeName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    部门：
                                </th>
                                <td>
                                    <asp:Label ID="tbDept" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    职位：
                                </th>
                                <td>
                                    <asp:Label ID="tbPosition" runat="server"></asp:Label>
                                </td>
                                <th>
                                    离职类型：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="tbLeftType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                        <asp:ListItem Value="主动离职">主动离职</asp:ListItem>
                                        <asp:ListItem Value="优化">优化</asp:ListItem>
                                        <asp:ListItem Value="调动">调动</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <span>
                                        <asp:CheckBox ID="cbIsActiveLeft" runat="server" RepeatDirection="Horizontal" onclick="return false;" checked="true"/>
                                    </span><span>是否主动离职:</span>
                                </th>
                                <td id="ctl18_tdLeftQuestionnaire" class="td_r" colspan="3">
                                    <font color="red">请点击此链接进行《离职问卷》作答：</font> <a href="http://zyinfo.founder.com/dept/HR/Lists/Survey/NewForm.aspx?IsDlg=0"
                                        target="_blank">离职问卷</a>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="false" AppId="3015" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    员工意见
                                </th>
                                <td colspan='2'>
                                    <span>交接人：</span>
                                    <asp:Label ID="TextBox1" runat="server">
                                    </asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span>承接人：</span>
                                    <asp:Label ID="TextBox2" runat="server"></asp:Label><br />
                                    <span id="ctl18_lEmployee2">工作内容交接 交接完毕</span><br />
                                    <span id="ctl18_lEmployee3">工作文档交接 交接完毕</span><br />
                                    <span id="ctl18_lEmployee34">应收、应付款 交接完毕</span>
                                    <AB:ApprovalBox ID="Option_4034" Node="员工意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    用人部门负责人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4035" Node="用人部门负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门接口人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4036" Node="相关部门接口人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门负责人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4037" Node="相关部门负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源接口人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4038" Node="人力资源接口人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源负责人意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4039" Node="人力资源负责人意见" runat="server" />
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

