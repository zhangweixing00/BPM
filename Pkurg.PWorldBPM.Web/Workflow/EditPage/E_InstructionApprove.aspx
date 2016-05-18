<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_InstructionApprove.aspx.cs"
    Inherits="Workflow_EditPage_E_InstructionApprove" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblSecurityLevel");
            selectOneCheckList("cblUrgenLevel");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    请示单
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th style="width: 100px;">
                                保密等级：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th style="width: 100px;">
                                紧急程度：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblUrgentLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th style="width: 50px;">
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbNumber" runat="server" CssClass="txt" Width="100"></asp:TextBox>
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
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbData" runat="server" CssClass="txt">2014-09-22</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTheme" runat="server" CssClass="longtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc4:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <uc7:UploadAttachments ID="UploadAttachments1" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td>
                                    <uc2:ApproveOpinionUC ID="ApproveOpinionUCDeptleader" CurrentNodeName="部门负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td>
                                    <uc3:Countersign ID="Countersign1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td>
                                    <uc2:ApproveOpinionUC ID="ApproveOpinionUCRealateDept" CurrentNodeName="会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td>
                                    <uc2:ApproveOpinionUC ID="ApproveOpinionUCLeader" CurrentNodeName="部门主管领导审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    CEO意见：
                                </th>
                                <td>
                                    <uc2:ApproveOpinionUC ID="ApproveOpinionUCCEO" CurrentNodeName="CEO审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否发起新流程：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbDirector" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
                <div id="Options" runat="server">
                    <li>
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Save_Click">同意</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">不同意</asp:LinkButton></li>
                </div>
                <uc5:AddSign ID="AddSign1" runat="server" />
                <uc6:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <li>
                    <asp:LinkButton ID="LinkButton4" runat="server">打印表单</asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="LinkButton5" runat="server">关闭</asp:LinkButton>
                </li>
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
