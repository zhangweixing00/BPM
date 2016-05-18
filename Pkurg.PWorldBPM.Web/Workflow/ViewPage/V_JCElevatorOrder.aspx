<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_JCElevatorOrder.aspx.cs"
    Inherits="Workflow_ViewPage_V_JCElevatorOrder" %>

<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>集采产品审批</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        //需要引用/Resource/js/helper.js
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
            <span class="wf_title">查看流程</span>
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
                <table  class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
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
                                <asp:CheckBoxList ID="cblUrgentLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" Enabled="false">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                编号：
                            </th>
                            <td>
                                <asp:Label ID="tbNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr >
                                <th >
                                    经办部门：
                                </th>
                                <td>
                                    <asp:Label ID="tbDepartName" runat="server"></asp:Label>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <asp:Label ID="tbData" runat="server"></asp:Label>
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
                                    <asp:Label ID="tbTitle" runat="server"></asp:Label>
                                    <asp:Label ID="tbOrderType" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="tbOrderID" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    目标成本限额：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="txtMaxCost" runat="server"></asp:Label>(元)
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="tbNote" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                                <th>
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="DeptManagerApproveOpinion" CurrentNode="false" CurrentNodeName="部门负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" ReadOnly="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="RealateDeptApproveOpinion" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    城市公司负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="CityCompanyLeaderApproveOpinion" CurrentNode="false" CurrentNodeName="城市公司负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部初审意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="JCFirstApprovalApproveOpinion" CurrentNode="false" CurrentNodeName="采购管理部初审"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    研发设计部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="DesignerApproveOpinion" CurrentNode="false" CurrentNodeName="研发设计部负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    项目运营部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ProjectOperatorApproveOpinion" CurrentNode="false" CurrentNodeName="项目运营部负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部复审意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="JCReApprovalApproveOpinion" CurrentNode="false" CurrentNodeName="采购管理部复审"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部负责人意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="PurchaserApproveOpinion" CurrentNode="false" CurrentNodeName="采购管理部负责人"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="COOApproveOpinion" CurrentNode="false" CurrentNodeName="COO"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部下单意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="JCMakeOrderApproveOpinion" CurrentNode="false" CurrentNodeName="采购管理部下单"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部复核意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="JCFinalApprovalApproveOpinion" CurrentNode="false" CurrentNodeName="采购管理部复核"
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
    </form>
</body>
</html>
