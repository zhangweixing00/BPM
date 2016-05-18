<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JC_ProjectTenderCityCompany.aspx.cs"
    Inherits="Workflow_ViewPage_V_JC_ProjectTenderCityCompany" %>

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
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    招标需求申请
                    <asp:Label ID="lbIsApproval" runat="server"></asp:Label>
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:Label ID="tbReportCode" runat="server" />
                            </td>
                            <th>
                                保密等级：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                紧急程度：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="ddlDepartName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    日期：
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
                                    集团授权项目：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsImpowerProject" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false" Visible="false">
                                        <asp:ListItem Value="0">一级开发</asp:ListItem>
                                        <asp:ListItem Value="1">二级开发</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
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
                                    备注：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbRemark" runat="server"></asp:Label>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="10113"
                                        IsOnlyRead="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody id="tbCompany" runat="server">
                            <tr>
                                <th>
                                    经办部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionOperateDeptleader" CurrentNode="false" CurrentNodeName="经办部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合约审算部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionContractReviewDeptleaader" CurrentNode="false" CurrentNodeName="合约审算部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        分管业务/成本领导意见：
                                    </div>
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionBranchDeptLeader" CurrentNode="false" CurrentNodeName="分管领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionRealateDept" CurrentNode="false" CurrentNodeName="相关部门意见"
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
                                    <div style="line-height: 25px;">
                                        集团业务/成本相关部门领导意见：
                                    </div>
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupCompetentDeptLeader" CurrentNode="false" CurrentNodeName="集团主管部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        集团采购管理部意见：
                                    </div>
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="集团采购管理部意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbPresident" runat="server" visible="false">
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeLeader" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroupCommittee" runat="server" visible="false">
                            <tr>
                                <th>
                                    CPO意见：
                                </th>
                                <td colspan='2'>
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
