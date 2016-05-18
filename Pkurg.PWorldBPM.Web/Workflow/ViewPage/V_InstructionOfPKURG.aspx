<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_InstructionOfPKURG.aspx.cs"
    Inherits="Workflow_ViewPage_V_InstructionOfPKURG" %>

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
                            <td>
                                保密等级：
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                紧急程度：
                                <asp:CheckBoxList ID="cblUrgentLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                编号：
                                <asp:Label ID="tbNumber" runat="server" Width="100" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table class="wf_table" cellspacing="1" cellpadding="0">
                    <tbody>
                        <tr>
                            <td>
                                经办部门：
                                <asp:Label ID="tbDepartName" runat="server" Enabled="false"></asp:Label>
                            </td>
                            <td>
                                日期：
                                <asp:Label ID="tbData" runat="server" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                经办人：
                                <asp:Label ID="tbPerson" runat="server" Enabled="false"></asp:Label>
                            </td>
                            <td>
                                电话：
                                <asp:Label ID="tbPhone" runat="server" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                主题：
                                <asp:Label ID="tbTheme" runat="server" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                呈报内容：
                                <asp:Label ID="tbContent" runat="server" Height="80" Enabled="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                关联流程：<uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                上传附件：<uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false"  IsOnlyRead="true"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table class="wf_table" cellspacing="1" cellpadding="0">
                    <tbody>
                        <tr>
                            <td style="vertical-align: middle" colspan="2">
                                部门负责人意见：
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCDeptleader" CurrentNodeName="部门负责人审批" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                相关部门会签：
                            </td>
                            <td>
                                <uc3:Countersign ID="Countersign1" runat="server" ReadOnly="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                相关部门意见：
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCRealateDept" CurrentNodeName="会签" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                分管领导意见：
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCLeader" CurrentNodeName="部门主管领导审批" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                CEO意见：
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCCEO" CurrentNodeName="CEO审批" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                是否征求董事意见：
                                <asp:CheckBox ID="cbIsReport" runat="server" Enabled="false"></asp:CheckBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
