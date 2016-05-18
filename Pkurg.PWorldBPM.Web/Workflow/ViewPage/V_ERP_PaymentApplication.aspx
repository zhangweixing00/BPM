<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_ERP_PaymentApplication.aspx.cs"
    Inherits="Workflow_ViewPage_V_ERP_PaymentApplication" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看页面</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
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
            <span class="wf_title">查看流程</span>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 1060px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <%= PaymentApplication_Common.GetErpFormTitle(this) %>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <iframe id="iframe" style="width: 1050px; height: 700px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td style="width: 400px;">
                                    <asp:Label ID="ddlDepartName" runat="server"></asp:Label>
                                </td>
                                <th style="width: 160px;">
                                    突破合同内付款：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cblisoverCotract" runat="server" Enabled="false"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <th class="width: 120px;">
                                    关联流程：
                                </th>
                                <td colspan="4">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th class="width: 120px;">
                                    上传附件：
                                </th>
                                <td colspan="4">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" IsOnlyRead="true"
                                        AppId="10105" />
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
                                    相关人员意见：
                                </th>
                                <td style="vertical-align: middle" colspan="4">
                                    <%--<asp:CheckBoxList ID="cbRelatonUsers" runat="server" Visible="true" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>--%>
                                    <uc4:ApproveOpinionUC ID="Option_0" CurrentNode="false" CurrentNodeName="相关人员意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td style="vertical-align: middle" colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCDeptleader" CurrentNodeName="部门负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td style="vertical-align: middle" colspan="4">
                                    <uc3:Countersign ID="Countersign1" runat="server" ReadOnly="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCRealateDept" CurrentNodeName="会签" runat="server" />
                                    <uc4:ApproveOpinionUC ID="Option_12" CurrentNodeName="财务管理部审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门分管领导&nbsp&nbsp<br />
                                    <br />
                                    意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCLeader" CurrentNodeName="相关部门助理总裁审批" runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC4" CurrentNodeName="相关部门副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主管财务领导意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="Option_13" CurrentNodeName="财务管理部副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    常务副总裁意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_11" CurrentNodeName="常务副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:label id="lbPresident" runat="server"></asp:label>
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_4" CurrentNode="false" CurrentNodeName="总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbChairman" runat="server" Text="选择董事长" Enabled="false" />
                                    <uc4:ApproveOpinionUC ID="Option_10" CurrentNodeName="董事长审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNodeName="集团相关部门意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门主管&nbsp&nbsp<br />
                                    <br />
                                    领导意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNodeName="集团相关部门助理总裁审批" runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC22" CurrentNodeName="集团相关部门副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团CEO意见：
                                </th>
                                <td colspan="4">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC3" CurrentNodeName="集团CEO意见" runat="server" />
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
