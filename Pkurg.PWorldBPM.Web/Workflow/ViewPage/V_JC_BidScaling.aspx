<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JC_BidScaling.aspx.cs"
    Inherits="Workflow_ViewPage_V_JC_BidScaling" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="uc7" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>招标定标审批单</title>
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
                    招标定标审批单
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
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
                                    呈报部门：
                                </th>
                                <td>
                                    <asp:Label ID="tbDepartName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    呈报日期：
                                </th>
                                <td>
                                    <asp:Label ID="tbDateTime" runat="server"></asp:Label>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    评标内容：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbContent" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    要求进场时间：
                                </th>
                                <td>
                                    <asp:Label ID="tbEntranceTime" runat="server"></asp:Label>
                                </td>
                                <th>
                                    <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsAccreditByGroup" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false">
                                            <asp:ListItem Value="0" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="1">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false" Visible="false">
                                            <asp:ListItem Value="0">一级开发</asp:ListItem>
                                            <asp:ListItem Value="1">二级开发</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="1003"
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
                            <tr id="trCounterSign" runat="server">
                                <th style="width: 150px;">
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="false" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroup" runat="server" visible="false">
                            <tr id="trCounterSignGroup" runat="server">
                                <th style="width: 150px;">
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc7:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="false" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNode="false" CurrentNodeName="集团会签"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbBidCommittee" runat="server">
                            <tr>
                                <th style="width: 150px;" rowspan="3">
                                    招标委员会意见：
                                </th>
                                <td>
                                    第一入围单位：<br />
                                    <asp:Label ID="tbFirstUnit" runat="server" Width="300"></asp:Label>
                                </td>
                                <td>
                                    第二入围单位：<br />
                                    <asp:Label ID="tbSecondUnit" runat="server" Width="300"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFirstList" runat="server" Width="300" Style="text-align: center"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSecondList" runat="server" Width="300" Style="text-align: center"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionExecutiveDirector" CurrentNode="false" CurrentNodeName="执行主任"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC3" CurrentNode="false" CurrentNodeName="招标委员会意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC4" CurrentNode="false" CurrentNodeName="招标委员会主任意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    定标结果：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="tbScalingResult" runat="server" Width="300"></asp:Label>
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
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
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
