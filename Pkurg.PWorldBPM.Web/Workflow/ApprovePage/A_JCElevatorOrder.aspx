<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_JCElevatorOrder.aspx.cs"
    Inherits="Workflow_ApprovePage_A_JCElevatorOrder" %>

<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>集采产品审批</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <%--script type="text/javascript">
        $(function () {
            //Iframe动态加载Url
            var url = $('#<%=tbContent.ClientID %>').val();
            if (url) {
                $("#iframe").attr("src", url);
            }
        });
    </script>--%>
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
        <div class="wf_center" style="width: 1100px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    集采产品审批单
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th style="width: 90px;">
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
                            <th style="width: 90px;">
                                紧急程度：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblUrgentLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th style="width: 70px;">
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbNumber" runat="server" CssClass="txt" Width="130" ReadOnly="true"></asp:TextBox>
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
                                    <asp:Label ID="lblLoginName" runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbData" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt" ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="tbOrderType" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="tbOrderID" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    目标成本限额：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMaxCost" runat="server" CssClass="longtxt" ReadOnly="true"></asp:TextBox>(元)
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbNote" runat="server"  Font-Bold="true" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报内容：
                                </th>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="tbContent" runat="server" Height="80" Visible="false"></asp:Label>
                                    <iframe id="iframe1" style="width: 100%; height: 300px; border: none;" src='<%=GetJCUrl() %>'>
                                    </iframe>
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
                                <th style="width: 150px;">
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="DeptManagerApproveOpinion" CurrentNodeName="部门负责人" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td>
                                    <uc3:Countersign ID="Countersign1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td>
                                    <uc2:ApproveOpinionUC ID="RealateDeptApproveOpinion" CurrentNodeName="会签" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    城市公司负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="CityCompanyLeaderApproveOpinion" CurrentNodeName="城市公司负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部初审意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="JCFirstApprovalApproveOpinion" CurrentNodeName="采购管理部初审"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    研发设计部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="DesignerApproveOpinion" CurrentNodeName="研发设计部负责人" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    项目运营部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="ProjectOperatorApproveOpinion" CurrentNodeName="项目运营部负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部复审意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="JCReApprovalApproveOpinion" CurrentNodeName="采购管理部复审" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="PurchaserApproveOpinion" CurrentNodeName="采购管理部负责人" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="COOApproveOpinion" CurrentNodeName="COO" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部下单意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="JCMakeOrderApproveOpinion" CurrentNodeName="采购管理部下单" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部复核意见：
                                </th>
                                <td colspan="2">
                                    <uc2:ApproveOpinionUC ID="JCFinalApprovalApproveOpinion" CurrentNodeName="采购管理部复核"
                                        runat="server" />
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
                    <div id="AgreeOption" runat="server">
                        <li>
                            <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                    </div>
                    <div id="DisAgreeOption" runat="server">
                        <li>
                            <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                    </div>
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
