<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_OA_ITSupport.aspx.cs" Inherits="Workflow_ApprovePage_A_OA_ITSupport" %>

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
<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox_IT.ascx" TagName="ApprovalBox_IT"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/TurnGroup/TurnGroup.ascx" TagName="TurnGroup" TagPrefix="uc2" %>
<%@ Register Src="../../Modules/ChangeSign/ChangeSign.ascx" TagName="ChangeSign"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    <asp:Label ID="lbFormTitle" runat="server"></asp:Label>
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
                                    <FR:FlowRelated ID="flowRelated" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3014" />
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
                <div id="Options_GroupStep" runat="server">
                    <li>
                        <asp:LinkButton ID="lbGet_Group" runat="server" OnClick="lbGet_Group_Click">领取</asp:LinkButton></li>
                </div>
                <div id="Options_Single" runat="server">
                    <li>
                        <asp:LinkButton ID="lbFinish" runat="server" OnClick="lbFinish_Click">处理完成</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="lbUnFinish" runat="server" OnClick="lbUnFinish_Click">结单</asp:LinkButton></li>
                    <uc2:TurnGroup ID="TurnGroup1" runat="server" />
                    <AS:AddSign ID="AddSign1" runat="server" />
                    <uc3:ChangeSign ID="ChangeSign1" runat="server" />
                </div>
                <!--可以是外部人员-->
                <div id="Options_Submit" runat="server">
                    <li>
                        <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                </div>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
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
<script src="/Resource/js/PreventRepeatSubmit.js" type="text/javascript"></script>
