<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JC_ProjectTenderGroup.aspx.cs" Inherits="Workflow_ViewPage_V_JC_ProjectTenderGroup" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>

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
            cursor:default;
            border-width:0px;
            background-color:transparent;
            
        }
    </style>
    <style type="text/css">
        .labeltxt
        {
            cursor:default;
            border-width:0px;
            background-color:transparent;
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
                    北大资源集团项目招标需求申请
                    <asp:Label id="lbIsApproval" runat="server"></asp:Label>
                    
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:Label ID="tbReportCode" runat="server"/>
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
                                <th >
                                    经办部门：
                                </th>
                                <td >
                                    <asp:Label ID="ddlDepartName" runat="server"></asp:Label>
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
                                    <asp:Label ID="tbUserName" runat="server" ></asp:Label>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:Label ID="tbMobile" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbTitle" runat="server" ></asp:Label>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="2005"
                                        IsOnlyRead="true" />
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
                                    集团经办部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupOperateDeptleader" CurrentNode="false" CurrentNodeName="集团经办部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupRealateDept" CurrentNode="false" CurrentNodeName="集团相关部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeLeader" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会意见" runat="server" />
                                    <%--<uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeChairman" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会主任意见" runat="server" />--%>
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
    </form>
</body>
</html>
