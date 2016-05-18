<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_OA_SystemDispatch.aspx.cs"
    Inherits="Workflow_ViewPage_V_OA_SystemDispatch" %>

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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_center" style="width: 1000px;">
            <div class="wf_form">
                <div class="wf_form_title" >
                    发文审批单（制度）
                    <br />
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
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
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" MaxLength="50" runat="server" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tr>
                            <th>
                                经办部门：
                            </th>
                            <td >
                                <asp:Label ID="tbDepartName" runat="server"></asp:Label>
                            </td>
                            <th>
                                日期：
                            </th>
                            <td >
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
                                文件名称：
                            </th>
                            <td colspan="3">
                                <asp:Label ID="tbTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                发布红头文件：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblRedHeadDocument" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">是</asp:ListItem>
                                    <asp:ListItem Value="1">否</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                公布于公司内部网：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblIsPublish" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">是</asp:ListItem>
                                    <asp:ListItem Value="1">否</asp:ListItem>
                                </asp:CheckBoxList>
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
                                上传附件：
                            </th>
                            <td colspan="3">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="1001" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionDeptleader" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionRealateDept" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导/董事意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionTopLeaders" CurrentNode="false" CurrentNodeName="总办会意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionCEO" CurrentNode="false" CurrentNodeName="CEO意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;" >
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionChairman" CurrentNode="false" CurrentNodeName="董事长意见"
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
    </div>
    </form>
</body>
</html>
