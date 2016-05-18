<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WF_Approval.ascx.cs" Inherits="Workflow_Forms_WF_Approval" %>
<%@ Register src="../../../Modules/FlowRelated/FlowRelated.ascx" tagname="FlowRelated" tagprefix="uc3" %>
<%@ Register src="../../../Modules/Countersign/Countersign.ascx" tagname="Countersign" tagprefix="uc4" %>
<%@ Register src="../../../Modules/UploadAttachments/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc1" %>
<div class="wf_form">
    <div class="wf_form_title">
        请示单
    </div>
    <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
        <tbody>
            <tr>
                <th>
                    保密等级：
                </th>
                <td>
                    <asp:CheckBoxList ID="CBLSecurityLevel" runat="server" RepeatDirection="Horizontal"  CBind="SecurityLevel">
                        <asp:ListItem Value="0">绝密</asp:ListItem>
                        <asp:ListItem Value="1">机密</asp:ListItem>
                        <asp:ListItem Value="2">秘密</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <th>
                    紧急程度：
                </th>
                <td>
                    <asp:CheckBoxList ID="CBLUrgentLevel" runat="server" RepeatDirection="Horizontal" CBind="UrgentLevel">
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
                    <th>
                        经办部门：
                    </th>
                    <td>
                        <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt" CBind="DeptName"></asp:TextBox>
                    </td>
                    <th>
                        经办人：
                    </th>
                    <td>
                        <asp:TextBox ID="tbUser" runat="server" CssClass="txt" CBind="UserName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        主题：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tbTitle" runat="server" CssClass="txt" Width="80%" CBind="Title"></asp:TextBox>
                    &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        呈报内容：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tbContent" runat="server" CssClass="txt" Height="182px" 
                            TextMode="MultiLine" Width="80%" CBind="Content"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        关联流程：</th>
                    <td colspan="3">
                        <uc3:FlowRelated ID="FlowRelated1" runat="server"  IsCanEdit="true"/>
                    </td>
                </tr>
                <tr>
                    <th>
                        上传附件：</th>
                    <td colspan="3">
                        
                        <uc1:UploadAttachments ID="UploadAttachments1" runat="server" />
                        
                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>
    <fieldset class="wf_fieldset">
        <legend class="wf_legend">审批流程</legend>
        <table class="wf_table" cellspacing="1" cellpadding="0">
            <tbody>
                <tr>
                    <th>
                        相关部门会签：
                    </th>
                    <td>
                        <uc4:Countersign ID="Countersign1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        相关部门意见：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        分管领导意见：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
    </fieldset>
</div>
