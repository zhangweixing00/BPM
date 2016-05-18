<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_ERP_Instruction.aspx.cs"
    Inherits="Workflow_ViewPage_V_ERP_Instruction" %>

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
        <div class="wf_center" style="width: 1050px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <%= Instruction_Common.GetErpFormTitle(this)%>
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0">
                    <tbody>
                        <iframe id="iframe" style="width: 1050px; height: 550px; border: none;" src='<%= IFrameHelper.GetErpUrl() %>'>
                        </iframe>
                        <tr>
                            <th>
                                经办部门：
                            </th>
                            <td>
                                <asp:Label ID="ddlDepartName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <th>
                                关联流程：
                            </th>
                            <td colspan="4">
                                <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上传附件：
                            </th>
                            <td colspan="4">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" IsOnlyRead="true"
                                    AppId="10107" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table class="wf_table" cellspacing="1" cellpadding="0">
                    <tbody>
                        <tr>
                            <th>
                                部门负责人意见：
                            </th>
                            <td style="vertical-align: middle" colspan="4">
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCDeptleader" CurrentNodeName="部门负责人审批" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                相关部门会签：
                            </th>
                            <td>
                                <uc3:Countersign ID="Countersign1" runat="server" ReadOnly="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                相关部门意见：
                            </th>
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCRealateDept" CurrentNodeName="会签" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                相关部门分管领导&nbsp&nbsp<br /><br />意见：
                            </th>
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCLeader" CurrentNodeName="相关部门助理总裁审批" runat="server" />
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUC4" CurrentNodeName="相关部门副总裁审批" runat="server" />
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
                                总裁意见：
                            </th>
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUCCEO" CurrentNodeName="总裁审批" runat="server" />
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
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNodeName="集团相关部门意见" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                集团相关部门主管&nbsp&nbsp<br /><br />领导意见：
                            </th>
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNodeName="集团相关部门助理总裁审批" runat="server" />
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUC22" CurrentNodeName="集团相关部门副总裁审批" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                集团CEO意见：
                            </th>
                            <td colspan="4">
                                
                                <uc4:ApproveOpinionUC ID="ApproveOpinionUC3" CurrentNodeName="集团CEO意见" runat="server" />
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
