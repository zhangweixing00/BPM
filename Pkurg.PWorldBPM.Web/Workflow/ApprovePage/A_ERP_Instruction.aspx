<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_ERP_Instruction.aspx.cs"
    Inherits="Workflow_ApprovePage_A_ERP_Instruction" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批页面</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblisoverCotract");
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
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                     <%= Instruction_Common.GetErpFormTitle(this)%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <iframe id="iframe" style="width: 1050px; height: 550px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                            </iframe>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr >
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <uc5:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="10107" />
                                </td>
                            </tr>
                        </tbody>
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
                                    <uc4:ApproveOpinionUC ID="Option_1" CurrentNode="false" CurrentNodeName="部门负责人审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_2" CurrentNode="false" CurrentNodeName="会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门分管领导&nbsp&nbsp<br /><br />意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_3" CurrentNode="false" CurrentNodeName="相关部门助理总裁审批"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="Option_9" CurrentNode="false" CurrentNodeName="相关部门副总裁审批"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    常务副总裁意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_11" CurrentNodeName="常务副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁审批：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_4" CurrentNode="false" CurrentNodeName="总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbChairman" runat="server" Text="选择董事长" Enabled="false" />
                                    <uc4:ApproveOpinionUC ID="Option_10" CurrentNodeName="董事长审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_5" CurrentNodeName="集团相关部门意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门主管&nbsp&nbsp<br /><br />领导意见 ：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_6" CurrentNodeName="集团相关部门助理总裁审批" runat="server" />
                                    <uc4:ApproveOpinionUC ID="Option_7" CurrentNodeName="集团相关部门副总裁审批" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团CEO意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="Option_8" CurrentNodeName="集团CEO意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
                    <li  id="UnOptions"  runat="server">
                        <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                </div>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <uc5:AddSign ID="AddSign1" runat="server" />
                <xxxxelmt></xxxxelmt>
                <uc6:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
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