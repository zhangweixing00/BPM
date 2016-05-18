<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_OA_SealOfEWY.aspx.cs" Inherits="Workflow_ApprovePage_A_OA_SealOfEWY" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>

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
                    印章使（借）用审批单
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                        </td>
                        <th>
                            保密等级：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Value="绝密">绝密</asp:ListItem>
                                <asp:ListItem Value="机密">机密</asp:ListItem>
                                <asp:ListItem Value="秘密">秘密</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <th>
                            紧急程度：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Value="加急">加急</asp:ListItem>
                                <asp:ListItem Value="紧急">紧急</asp:ListItem>
                                <asp:ListItem Value="一般">一般</asp:ListItem>
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
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt" Width="350px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt" Width="60px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    文件名称：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="txt" Width="500" ReadOnly="true"></asp:TextBox>
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
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="700" Height="200" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="5">
                                    <UA:uploadattachments id="UploadAttachments1" runat="server" iscanedit="true" appid="3025" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件！
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
                                    副总经理意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总" Enabled="false"/>
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                    <AB:ApprovalBox ID="ApprovalBox3" Node="副总经理意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见：
                                </th>
                                <td colspan="2">
                                <asp:CheckBox ID="cbPresident" runat="server" Text="选择总经理" Enabled="false"/>
                                    <AB:ApprovalBox ID="ApprovalBox4" Node="总经理意见" runat="server" />
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
    <!--快捷菜单-->
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <div id="Div1">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign ID="AddSign1" runat="server" />
                <ASI:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
        </div>
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
