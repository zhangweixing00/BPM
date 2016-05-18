<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JC_FinalistApproval.aspx.cs"
    Inherits="Workflow_ViewPage_V_JC_FinalistApproval" %>

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
    <style type="text/css">
        .locktxt
        {
            padding-right: 0px;
            padding-left: 6px;
            padding-bottom: 2px;
            vertical-align: middle;
            width: 250px;
            padding-top: 4px;
            font-family: Verdana, "Courier New" , "宋体";
            height: 16px;
            cursor: default;
            border-width: 0px;
            background-color: transparent;
        }
    </style>
    <style type="text/css">
        .labeltxt
        {
            cursor: default;
            border-width: 0px;
            background-color: transparent;
        }
    </style>
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
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
            <div class="wf_form" id="wf_form_title" runat="server">
                <div class="wf_form_title">
                    入围投标人资格审批表
                    <asp:Label ID="lbIsApproval" runat="server"></asp:Label>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    项目名称
                                </th>
                                <td>
                                    <asp:Label ID="tbProjectName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsImpowerProject" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                        Visible="false">
                                        <asp:ListItem Value="0">一级开发</asp:ListItem>
                                        <asp:ListItem Value="1">二级开发</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报部门
                                </th>
                                <td>
                                    <asp:Label ID="ddlDepartName" runat="server"></asp:Label>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    呈报日期
                                </th>
                                <td>
                                    <asp:Label ID="tbDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        入围投标人资格审查情况
                                    </div>
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbCheckStatus" runat="server">
                                    </asp:Label>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="2003"
                                        IsOnlyRead="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody id="tbCompany" runat="server">
                            <tr>
                                <th>
                                    招标采购部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="招标采购部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionLegalDeptLeader" CurrentNode="false" CurrentNodeName="法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbCompanyCommittee" runat="server">
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeLeader" CurrentNode="false" CurrentNodeName="招标委员会意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeChairman" CurrentNode="false" CurrentNodeName="招标委员会主任意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroup" runat="server" visible="false">
                            <tr>
                                <th>
                                    采购管理部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="集团采购管理部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团法务部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupLegalDeptLeader" CurrentNode="false" CurrentNodeName="集团法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroupCommittee" runat="server" visible="false">
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionExecutiveDirector" CurrentNode="false" CurrentNodeName="执行主任"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeLeader" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会意见" runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeChairman" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会主任意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbURL" runat="server">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl" target="_blank">点此查看相关流程</a>
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
<script type="text/javascript">
    $(function () {
        var url = window.location + "&type=1";
        $("#ViewUrl").attr("href", url);
    });
</script>
