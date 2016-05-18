<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_OA_ContractOfWY.aspx.cs"
    Inherits="Workflow_ApprovePage_V_OA_ContractOfWY" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <!--center-->
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    合同流程单（物业）<br />
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:Label ID="tbReportCode" MaxLength="50" runat="server" contentEditable="false" />
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
                                    <asp:Label ID="tbDepartName" runat="server"></asp:Label>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:Label ID="tbDateTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:Label ID="tbUserName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:Label ID="tbMobile" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="IsReportToWY" runat="server">
                                <th>
                                    是否上报物业
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsReportToWY" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr id="IsReportToGroup" runat="server">
                                <th>
                                    是否上报资源集团
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsReportToGroup" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同类型：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="ddlContractType1" runat="server"></asp:Label>
                                    <asp:Label ID="ddlContractType2" runat="server"></asp:Label>
                                    <asp:Label ID="ddlContractType3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同标的金额：
                                </th>
                                <td>
                                    <asp:Label ID="tbContractSum" runat="server"></asp:Label>元
                                </td>
                                <th>
                                    是否补充协议：
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsSupplementProtocol" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left;">
                                        <asp:Label ID="lbSupplementProtocol" runat="server">若是，第</asp:Label>
                                        <asp:Label ID="tbSupplementProtocol" runat="server" Width="30"></asp:Label>
                                        <asp:Label ID="lbSupplementProtocol1" runat="server">份</asp:Label>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否格式合同：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsFormatContract" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                    是否合同标准文本：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsNormText" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否经过招标：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsBidding" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同主体：
                                </th>
                                <td>
                                    <asp:Label ID="ddlContractSubject" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="tbContractSubject1" runat="server"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="tbContractSubject2" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="tbContractSubject3" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同名称：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbContractTitle" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbContractContent" runat="server">
                                    </asp:Label>
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
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="false" AppId="3024"
                                        IsOnlyRead="true" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1" id="Company" runat="server">
                        <tbody>
                            <tr id="Company1" runat="server">
                                <th>
                                    分公司
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    部门负责人意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4101" Node="部门负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4103" Node="会签" runat="server" IsCanSelectSelfDeptment="false" 
                                    DisableDepartments='财务管理部' DefaultCheckdDepartments="财务管理部"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4104" Node="法务部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4105" Node="主管副总经理意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4106" Node="总经理意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="wf_table" colspan="1" id="Group" runat="server">
                        <tbody>
                            <tr id="Group1" runat="server">
                                <th>
                                    物业集团
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    部门负责人意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4107" Node="集团部门负责人意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CSG:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="true" CounterSignDeptId="S366-S976" 
                                    IsCanSelectSelfDeptment="false" DisableDepartments='财务管理部' DefaultCheckdDepartments="财务管理部"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4108" Node="集团会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4109" Node="集团法务部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见:
                                </th>
                                <td colspan='2'>
                                    <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁" Checked="false" />
                                    <asp:Label ID="lbAP" runat="server"></asp:Label>
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁" Checked="false" />
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                    <AB:ApprovalBox ID="Option_4110" Node="相关部门主管助理总裁意见" runat="server" />
                                    <AB:ApprovalBox ID="Option_4111" Node="相关部门主管副总裁意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4112" Node="总裁意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                   <table class="wf_table" colspan="1" >
                        <tbody>
                            <tr>
                                <th>
                                    发起人上传最终版合同:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4113" Node="发起人上传最终版合同" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务复核意见:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4114" Node="法务复核意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员盖章:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4115" Node="印章管理员盖章" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    档案管理员归档:
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_4116" Node="档案管理员归档" runat="server" />
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
