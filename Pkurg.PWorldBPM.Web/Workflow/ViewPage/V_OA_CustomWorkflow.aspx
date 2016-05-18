<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_OA_CustomWorkflow.aspx.cs"
    Inherits="Workflow_ApprovePage_V_OA_CustomWorkflow" %>

<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看页面</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <style>
        .label
        {
            word-wrap: break-word;
            word-break: keep-all;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">查看流程</span>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <asp:label ID="lbFormTitle" runat="server"></asp:label>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <!--业务表单-->
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="lbDepartName" runat="server" Text=""></asp:Label>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:Label ID="tbDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:Label ID="tbPerson" runat="server"></asp:Label>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:Label ID="tbPhone" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbTheme" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td  colspan="3" >
                                <asp:Label ID="tbContent" runat="server"></asp:Label>
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
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="false" AppId="2004"
                                        IsOnlyRead="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <asp:Repeater ID="rptList" runat="server">
                        <HeaderTemplate>
                            <table class="wf_table">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <%--                                <td>
                                    <%#Container.ItemIndex+1%>
                                </td>--%>
                                <th style="text-align: right;">
                                    <%#Eval("StepName")%>：
                                    <br />
                                    <div style="line-height: 18px; margin-top: 10px;">
                                        (
                                        <%# Eval("PartUsers")==null?"无审批人跳过":CustomWorkflowDataProcess.DisplayUserName( Eval("PartUsers").ToString())%>)
                                    </div>
                                </th>
                                <td>
                                    <AB:ApprovalBox ID="Option_c" Node='<%# Eval("StepName").ToString() %>' runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
