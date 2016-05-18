<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_ERP_SupplementalAgreement.aspx.cs"
    Inherits="Workflow_ApprovePage_A_ERP_SupplementalAgreement" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/PreventRepeatSubmit.js" type="text/javascript"></script>
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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <%= FormTitle%>
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
                                <th>
                                    上报资源集团：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbIsReportResource" runat="server" Enabled="false"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="lbDeptName" runat="server" Text=""></asp:Label>
                                </td>
                                <th>
                                    上报方正集团：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbIsReportFounder" runat="server" Enabled="false"/>
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
                                    附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="2004" />
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
                                    <AB:ApprovalBox ID="Option_2249" Node="部门负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2250" Node="会签" runat="server" />
                                    <AB:ApprovalBox ID="Option_2251" Node="法务部负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2252" Node="相关部门分管助理总裁审批" runat="server" />
                                    <AB:ApprovalBox ID="Option_2253" Node="相关部门分管副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2254" Node="总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2255" Node="董事长审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CSG:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2248" Node="集团会签" runat="server" />
                                    <AB:ApprovalBox ID="Option_2256" Node="集团法务部总监审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        集团相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2257" Node="集团相关部门助理总裁审批" runat="server" />
                                    <AB:ApprovalBox ID="Option_2265" Node="集团主管法务领导审批" runat="server" />
                                    <AB:ApprovalBox ID="Option_2258" Node="集团相关部门副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团CEO意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2259" Node="集团CEO意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    流程发起人意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2260" Node="流程发起人" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    公司法务部负责人意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2261" Node="公司法务部负责人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2262" Node="盖章人审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    档案管理员意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_2263" Node="归档人审批" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%--                    <AB:ApprovalBox ID="ApprovalBox1" runat="server"  />--%>
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
                        <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                    <li id="UnOptions" runat="server">
                        <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                </div>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign ID="AddSign1" runat="server" />
                <ASI:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
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
