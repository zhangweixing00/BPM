<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_OA_ITSupport.aspx.cs" Inherits="Workflow_ApprovePage_V_OA_ITSupport" %>

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
<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox_IT.ascx" TagName="ApprovalBox_IT"
    TagPrefix="uc1" %>
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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <%=  FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <!--业务表单-->
                            <tr>
                                <th>
                                    所属公司：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbCompany" MaxLength="80" runat="server" ReadOnly="true" CssClass="txt" />
                                </td>
                                <th>
                                    申请人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    所属部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                    <%--
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                </td>
                                <th>
                                    申请日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDate" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    邮箱：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEmail" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    联系电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    常见问题：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbQuestion" MaxLength="80" runat="server" ReadOnly="true" CssClass="txt" />
                                </td>
                                <th>
                                    系统名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbType" MaxLength="80" runat="server" ReadOnly="true" CssClass="txt" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    问题描述：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        ReadOnly="true" ValidationGroup="su"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbContent"
                                        ErrorMessage="*" ValidationGroup="su"></asp:RequiredFieldValidator>
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
                    <table class="wf_table" colspan="1" style="height: 100px;">
                        <tbody>
                            <tr>
                                <th>
                                    IT顾问处理：
                                </th>
                                <td colspan='2'>
                                    <uc1:ApprovalBox_IT ID="ApprovalBox_IT1" runat="server" Node="待领取,待处理" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
