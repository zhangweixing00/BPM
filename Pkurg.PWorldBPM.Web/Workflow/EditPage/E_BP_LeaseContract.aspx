<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_BP_LeaseContract.aspx.cs"
    Inherits="Workflow_EditPage_E_BP_LeaseContract" %>

<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发起租赁合同流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblSecurityLevel");
            selectOneCheckList("cblUrgentLevel");
            selectOneCheckList("cblCompensationContract");
            selectOneCheckList("cblDecorationContract");
            selectOneCheckList("cblServiceContract");
            selectOneCheckList("cblModificationContract");
            selectOneCheckList("cblSupplementContract");
            selectOneCheckList("cblLesseeContract");
            //Iframe动态加载Url
            //            var url = getUrlParam("url");
            //            if (url) {
            //                $("#iframe1").attr("src", url);
            //            }
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    租赁合同审批
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th style="width: 90px;">
                                保密等级：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
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
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="0">加急</asp:ListItem>
                                    <asp:ListItem Value="1">紧急</asp:ListItem>
                                    <asp:ListItem Value="2">一般</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th style="width: 70px;">
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="130" ReadOnly="true"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlDepartName" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <input id="UpdatedTextBox" readonly="readonly" runat="server" class="txt" style="width: 250px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"
                                        disabled="disabled" />
                                    <asp:TextBox ID="tbData" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
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
                                    <asp:TextBox ID="tbBizType" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="tbBizID" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="tbApproveFlag" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th rowspan="3">
                                    上报集团原因：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbReason" runat="server" CssClass="longtxt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>商户撤柜/调整赔偿10万元以上：
                                    <asp:CheckBoxList ID="cblCompensationContract" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>支付商户装修补贴：
                                    <asp:CheckBoxList ID="cblDecorationContract" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>引进商户产生服务费用：
                                    <asp:CheckBoxList ID="cblServiceContract" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>对集团下发合同文本进行修订的：
                                    <asp:CheckBoxList ID="cblModificationContract" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>补充协议：
                                    <asp:CheckBoxList ID="cblSupplementContract" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>使用承租方合同文本：
                                    <asp:CheckBoxList ID="cblLesseeContract" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="0">是</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbRemark" runat="server" CssClass="heighttxt edit"></asp:TextBox>
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
                                    <iframe id="iframe1" style="width: 100%; height: 300px; border: none;" src='<%=GetUrl() %>'>
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
                                    招商管理部意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNode="false" CurrentNodeName="部门总监意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNode="false" CurrentNodeName="部门助理总意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC3" CurrentNode="false" CurrentNodeName="招商管理部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    工程管理部意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC4" CurrentNode="false" CurrentNodeName="工程审核意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC5" CurrentNode="false" CurrentNodeName="工程管理部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    财务管理部意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC6" CurrentNode="false" CurrentNodeName="财务审核意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC7" CurrentNode="false" CurrentNodeName="财务管理部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    法务部意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC8" CurrentNode="false" CurrentNodeName="法务审核意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC9" CurrentNode="false" CurrentNodeName="法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC10" CurrentNode="false" CurrentNodeName="分管副总意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC11" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            </tbody>
                            <tbody id="tbGroup" runat="server" visible="false">
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC12" CurrentNode="false" CurrentNodeName="集团商业地产管理部意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC13" CurrentNode="false" CurrentNodeName="集团财务管理部意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC14" CurrentNode="false" CurrentNodeName="集团法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团分管领导意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC15" CurrentNode="false" CurrentNodeName="集团分管助理总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC16" CurrentNode="false" CurrentNodeName="集团法务部主管领导意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC17" CurrentNode="false" CurrentNodeName="集团分管副总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团总裁意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC18" CurrentNode="false" CurrentNodeName="集团总裁意见"
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
    <div id="scroll" style="margin-left: 490px;">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click">保存</asp:LinkButton></li>
                <%--<li>
                    <asp:LinkButton ID="lbArchive" runat="server" OnClick="Archive_Click">归档</asp:LinkButton></li>--%>
                <li>
                    <asp:LinkButton ID="lbClose" runat="server" OnClientClick="return Close_Win()">关闭</asp:LinkButton></li>
            </ul>
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
