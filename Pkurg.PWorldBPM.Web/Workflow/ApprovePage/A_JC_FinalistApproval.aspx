<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_JC_FinalistApproval.aspx.cs"
    Inherits="Workflow_ApprovePage_A_JC_FinalistApproval" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<%@ Register src="../../Modules/ChangeSign/ChangeSign.ascx" tagname="ChangeSign" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form" id="wf_form_title" runat="server">
                <div class="wf_form_title">
                    入围投标人资格审批表
                    <asp:Label ID="lbIsApproval" runat="server"></asp:Label>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    项目名称
                                </th>
                                <td>
                                    <asp:TextBox ID="tbProjectName" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsImpowerProject" runat="server" RepeatDirection="Horizontal"
                                        Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                        Visible="false">
                                        <asp:ListItem Value="0">一级开发</asp:ListItem>
                                        <asp:ListItem Value="1">二级开发</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报部门
                                </th>
                                <td>
                                    <asp:TextBox ID="ddlDepartName" runat="server" CssClass="txt" ReadOnly="true" Width="300px">
                                    </asp:TextBox>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    呈报日期
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <div style="line-height: 25px;">
                                        入围投标人资格审查情况
                                    </div>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbCheckStatus" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="700" ReadOnly="true" BackColor="White">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="2003" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody id="tbCompany" runat="server">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl2" target="_blank">点此查看相关流程</a>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    招标采购部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="招标采购部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionLegalDeptLeader" CurrentNode="false" CurrentNodeName="法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbCompanyCommittee" runat="server">
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeLeader" CurrentNode="false" CurrentNodeName="招标委员会意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionTenderCommitteeChairman" CurrentNode="false" CurrentNodeName="招标委员会主任意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbURL" runat="server" visible="false">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl" target="_blank">点此查看相关流程</a>
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroup" runat="server" visible="false">
                            <tr>
                                <th>
                                    采购管理部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupPurchasadministrationDeptLeader" CurrentNode="false"
                                        CurrentNodeName="集团采购管理部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团法务部意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionGroupLegalDeptLeader" CurrentNode="false" CurrentNodeName="集团法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbGroupCommittee" runat="server" visible="false">
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionExecutiveDirector" CurrentNode="false" CurrentNodeName="执行主任"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeLeader" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会意见" runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionGroupTenderCommitteeChairman" CurrentNode="false"
                                        CurrentNodeName="集团招标委员会主任意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="sn" runat="server" />
                    <asp:HiddenField ID="nodeID" runat="server" />
                    <asp:HiddenField ID="nodeName" runat="server" />
                    <asp:HiddenField ID="taskID" runat="server" />
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:HiddenField ID="hfViewUrl" runat="server" />
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
                    <li>
                        <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                    <li id="UnOptions" runat="server">
                        <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                </div>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign ID="AddSign1" runat="server" />
                <ASI:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <uc3:ChangeSign ID="ChangeSign1" runat="server" />
                 <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
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

    $(function () {
        var url = document.getElementById("hfViewUrl").value;
        $("#ViewUrl").attr("href", url);
        $("#ViewUrl2").attr("href", url + "&type=1");
    });
</script>
