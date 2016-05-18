<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JC_PurchasePaint.aspx.cs"
    Inherits="Workflow_ViewPage_V_JC_PurchasePaint" %>

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
    <title>查看流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">查看流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    新增产品审批单
                </div>
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
                    <table class="wf_table2" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="tbDepartName" runat="server"></asp:Label>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    呈报日期：
                                </th>
                                <td>
                                    <asp:Label ID="tbDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:Label ID="tbUserName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:Label ID="tbMobile" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    项目名称：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbTitle" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbContent" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="3004"
                                        IsOnlyRead="true" />
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
                                    经办部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="false" />
                                    <uc4:ApproveOpinionUC ID="OpinionCountersign" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionPresident" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionGroupAuditor" CurrentNode="false" CurrentNodeName="集团采购初审意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupProjectLeader" CurrentNode="false" CurrentNodeName="集团项目运营部意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupDeptLeader" CurrentNode="false" CurrentNodeName="集团相关部门意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupDeptManager" CurrentNode="false" CurrentNodeName="集团采购管理部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团分管领导意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionGroupLeader" CurrentNode="false" CurrentNodeName="集团分管领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    入库审核/复核意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionGroupAuditor2" CurrentNode="false" CurrentNodeName="入库审核意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupReviewer" CurrentNode="false" CurrentNodeName="入库复核意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="sn" runat="server" />
    <asp:HiddenField ID="nodeID" runat="server" />
    <asp:HiddenField ID="nodeName" runat="server" />
    <asp:HiddenField ID="taskID" runat="server" />
    <asp:HiddenField ID="hf_OpId" runat="server" />
    </form>
</body>
</html>
