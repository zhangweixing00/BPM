<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_OA_SealOfWY.aspx.cs" Inherits="Workflow_ViewPage_V_OA_SealOfWY" %>
<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
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
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    北大资源物业集团<br />
                    印章使（借）用审批单
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:label ID="tbReportCode" runat="server"/>
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
                    <table id="tb1" class="wf_table2" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    申请部门：
                                </th>
                                <td>
                                    <asp:label ID="tbDeptName" runat="server"></asp:label>
                                </td>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:label ID="tbUserName" runat="server"></asp:label>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:label ID="tbDateTime" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    文件名称：
                                </th>
                                <td colspan="3">
                                    <asp:label ID="tbTitle" runat="server"></asp:label>
                                </td>
                                <th>
                                    备注：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblRemark" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="false">
                                        <asp:ListItem Value="即用">即用</asp:ListItem>
                                        <asp:ListItem Value="外带使用">外带使用</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="5">
                                    <asp:label ID="tbContent" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="5">
                                    <UA:uploadattachments id="UploadAttachments1" runat="server" iscanedit="false" appid="3025" IsOnlyRead="true"/>
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
                                    申请部门负责人意见：
                                </th>
                                <td colspan="2">
                                <AB:ApprovalBox ID="ApprovalBox1" Node="部门负责人意见" runat="server" />
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
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <AB:ApprovalBox ID="Option_3862" Node="会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁" Enabled="false"/>
                                    <asp:Label ID="lbAP" runat="server"></asp:Label>
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁" Enabled="false"/>
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                    <AB:ApprovalBox ID="ApprovalBox2" Node="相关部门主管助理总裁意见" runat="server" />
                                    <AB:ApprovalBox ID="ApprovalBox3" Node="相关部门主管副总裁意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                <asp:CheckBox ID="cbPresident" runat="server" Text="选择总裁" Enabled="false"/>
                                    <AB:ApprovalBox ID="ApprovalBox4" Node="总裁意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理部门审核：
                                </th>
                                <td colspan="2">

                                <asp:Label ID="lbZHDeptManager" runat="server"></asp:Label>
                                <AB:ApprovalBox ID="ApprovalBox5" Node="印章管理部门审核" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员盖章：
                                </th>
                                <td colspan="2">
                                <AB:ApprovalBox ID="ApprovalBox6" Node="印章管理员签字" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

