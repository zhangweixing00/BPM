<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_ERP_ContractFinalAccount.aspx.cs"
    Inherits="Workflow_ApprovePage_V_ERP_ContractFinalAccount" %>

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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <%=  FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <iframe id="iframe" style="width: 1050px; height: 700px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>
                            <!--业务表单-->
                             <tr>
                               <th>
                                    原合同详情：
                                </th>
                                <td>
                                    <div id="detailContract" runat="server">
                                        <a href='<%= SupplementalAgreement_Common.GetPoUrl() %>' target="_blank">点此查看</a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="lbDeptName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <FR:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="10111" />
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
                                    部门负责人意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3861" Node="部门负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3862" Node="会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3863" Node="相关部门分管助理总裁审批" runat="server" />
                                    <AB:ApprovalBox ID="Option_3864" Node="相关部门分管副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3865" Node="总裁意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    项目运营部意见：
                                </th>
                                <td colspan='2'>
                                    <%--<AB:ApprovalBox ID="Option_3866" Node="业务对接人意见" runat="server" />--%>
                                    <AB:ApprovalBox ID="Option_3867" Node="项目运营部副总意见" runat="server" />
                                    <AB:ApprovalBox ID="Option_3868" Node="项目运营部负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团分管领导审核意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3869" Node="集团分管领导审核意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团总裁意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3870" Node="集团总裁意见" runat="server" />
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
