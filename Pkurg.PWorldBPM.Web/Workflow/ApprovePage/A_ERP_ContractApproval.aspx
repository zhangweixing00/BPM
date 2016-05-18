<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_ERP_ContractApproval.aspx.cs"
    Inherits="Workflow_ApprovePage_A_ERP_ContractApproval" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="AO" %>
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
        <script src="/Resource/js/PreventRepeatSubmit.js" type="text/javascript"></script>
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
                            <iframe id="iframe" style="width: 1050px; height: 900px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlDepartName" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <th class="style1">
                                    <%--突破合同内付款：--%>
                                </th>
                                <td>
                                    <asp:CheckBox ID="cblisoverCotract" runat="server" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上报资源集团:
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbIsReportResource" runat="server" Enabled="false" />
                                </td>
                                <th>
                                    上报方正集团：
                                </th>
                                <td>
                                    <asp:CheckBox ID="cbIsReportFounder" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <!--<tr></tr>-->
                            <tr style="display: none">
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <FR:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="10109" />
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
                                    部门负责人意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1326" CurrentNode="false" CurrentNodeName="部门负责人审批"
                                        runat="server" />
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
                                    相关部门意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1327" CurrentNode="false" CurrentNodeName="会签" runat="server" />
                                    <AO:ApproveOpinionUC ID="Option_1328" CurrentNode="false" CurrentNodeName="法务部负责人审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1329" CurrentNode="false" CurrentNodeName="相关部门分管助理总裁审批"
                                        runat="server" />
                                    <AO:ApproveOpinionUC ID="Option_1330" CurrentNode="false" CurrentNodeName="相关部门分管副总裁审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1331" CurrentNode="false" CurrentNodeName="总裁审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1332" CurrentNode="false" CurrentNodeName="董事长审批"
                                        runat="server" />
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
                                    集团相关部门意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1333" CurrentNode="false" CurrentNodeName="集团会签"
                                        runat="server" />
                                    <AO:ApproveOpinionUC ID="Option_1334" CurrentNode="false" CurrentNodeName="集团法务部总监审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p style="margin-top: 5px; margin-right: 10px; margin-bottom: 5px; line-height: 20px;">
                                        集团相关部门<br />
                                        主管领导意见：</p>
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1335" CurrentNode="false" CurrentNodeName="集团相关部门助理总裁审批"
                                        runat="server" />
                                    <%--                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团主管法务领导意见:
                                </th>
                                <td colspan='2'>--%>
                                    <AO:ApproveOpinionUC ID="Option_1343" CurrentNode="false" CurrentNodeName="集团主管法务领导审批"
                                        runat="server" />
                                    <%--</td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门副总裁意见:
                                </th>
                                <td colspan='2'>--%>
                                    <AO:ApproveOpinionUC ID="Option_1336" CurrentNode="false" CurrentNodeName="集团相关部门副总裁审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团CEO意见：
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1337" CurrentNode="false" CurrentNodeName="集团CEO意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    流程发起人意见：
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1338" CurrentNode="false" CurrentNodeName="流程发起人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    公司法务部负责人意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1339" CurrentNode="false" CurrentNodeName="公司法务部负责人审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1340" CurrentNode="false" CurrentNodeName="盖章人审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    档案管理员意见:
                                </th>
                                <td colspan='2'>
                                    <AO:ApproveOpinionUC ID="Option_1341" CurrentNode="false" CurrentNodeName="归档人审批"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="sn" runat="server" />
                    <asp:HiddenField ID="nodeID" runat="server" />
                    <asp:HiddenField ID="nodeName" runat="server" />
                    <asp:HiddenField ID="taskID" runat="server" />
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
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
    //    function ProfectMutiSubmit(id) {
    //        // alert($(id).attr("disable")); //OnClick="Submit_Click"
    //        // OnClientClick='ProfectMutiSubmit(this)'
    //        $(id).attr("disable", "true");
    //    }

</script>
