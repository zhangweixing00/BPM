﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_OA_SealOfBC.aspx.cs"
    Inherits="Workflow_ApprovePage_A_OA_SealOfBC" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wf_page">
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <div class="wf_center" style="width: 980px;">
            <div class="wf_form">
                <div class="wf_form_title">
                    印章申请
                </div>
                <fieldset class="wf_fieldset">
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true"/>
                            </td>
                            <th>
                                保密等级：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                紧急程度：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="longtxt" ReadOnly="true" Width="300"></asp:TextBox>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    呈报日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true" Width="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="longtxt" ReadOnly="true" Width="100"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbMobile" runat="server" CssClass="txt" ReadOnly="true" Width="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt" ReadOnly="true" Width="700"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                         ReadOnly="true" Width="700" Height="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="3012" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
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
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan='2'>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDirector" CurrentNode="false" CurrentNodeName="分管领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbGeneralManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionGeneralManager" CurrentNode="false" CurrentNodeName="总经理意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionPresident" CurrentNode="false" CurrentNodeName="集团总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团办公室复核：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbGroupOffice" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupOffice" CurrentNode="false" CurrentNodeName="集团办公室复核"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    公章管理员盖章：
                                </th>
                                <td colspan="2">
                                <asp:Label ID="lbSealManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionSealManager" CurrentNode="false" CurrentNodeName="公章管理员盖章"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:Label ID="lblApprovers" runat="server" Visible="false"></asp:Label>
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <uc5:AddSign ID="AddSign1" runat="server" />
                <uc6:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
        <asp:HiddenField ID="sn" runat="server" />
        <asp:HiddenField ID="nodeID" runat="server" />
        <asp:HiddenField ID="nodeName" runat="server" />
        <asp:HiddenField ID="taskID" runat="server" />
        <asp:HiddenField ID="hf_OpId" runat="server" />
    </div>
    </form>
</body>
</html>
<script type="text/javascript">     

    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }

</script>
