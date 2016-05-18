<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_JC_BidScaling.aspx.cs"
    Inherits="Workflow_ApprovePage_A_JC_BidScaling" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="uc7" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<%@ Register src="../../Modules/ChangeSign/ChangeSign.ascx" tagname="ChangeSign" tagprefix="uc8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>招标定标审批单</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblSelectUnit");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wf_page">
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <div class="wf_center" style="width: 980px;">
            <div class="wf_form">
                <div class="wf_form_title">
                    招标定标审批单
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    项目名称：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt" ReadOnly="true" Width="700"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt" ReadOnly="true" Width="300px"></asp:TextBox>
                                </td>
                                <th>
                                    呈报日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true" Width="170px"></asp:TextBox>
                                    <asp:Label runat="server" Width="50"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    评标内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        ReadOnly="true" Width="700" Height="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    要求进场时间：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEntranceTime" runat="server" CssClass="txt" ReadOnly="true" Width="170px"></asp:TextBox>
                                </td>
                                <th>
                                    <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsAccreditByGroup" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false">
                                            <asp:ListItem Value="0" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="1">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false" Visible="false">
                                            <asp:ListItem Value="0">一级开发</asp:ListItem>
                                            <asp:ListItem Value="1">二级开发</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="1003" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody id="tbCompany" runat="server">
                            <tr>
                                <td colspan="3">
                                    <a id="ViewUrl2" target="_blank">点此查看相关流程</a>
                                </td>
                            </tr>
                            <tr id="trCounterSign" runat="server">
                                <th style="width: 150px;">
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="false" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNode="false" CurrentNodeName="会签"
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
                            <tr id="trCounterSignGroup" runat="server">
                                <th style="width: 150px;">
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc7:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="false" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNode="false" CurrentNodeName="集团会签"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbCommittee" runat="server">
                            <tr>
                                <th style="width: 150px;" rowspan="4">
                                    招标委员会意见：
                                </th>
                                <td>
                                    第一入围单位：<br />
                                    <asp:Label ID="tbFirstUnit" runat="server" Width="300"></asp:Label>
                                </td>
                                <td>
                                    第二入围单位：<br />
                                    <asp:Label ID="tbSecondUnit" runat="server" Width="300"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFirstList" runat="server" Width="300" Style="text-align: center"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSecondList" runat="server" Width="300" Style="text-align: center"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdSelectUnit" runat="server" colspan="2">
                                    请选择入围单位
                                    <asp:CheckBoxList ID="cblSelectUnit" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">第一入围单位</asp:ListItem>
                                        <asp:ListItem Value="1">第二入围单位</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionExecutiveDirector" CurrentNode="false" CurrentNodeName="执行主任"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC3" CurrentNode="false" CurrentNodeName="招标委员会意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC4" CurrentNode="false" CurrentNodeName="招标委员会主任意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th style="width: 150px;">
                                    定标结果：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <uc5:AddSign ID="AddSign1" runat="server" />
                <uc6:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <uc8:ChangeSign ID="ChangeSign1" runat="server"/>
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
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

    function Check() {
        if (document.getElementById("lbAgree") != null) {
            document.getElementById("lbAgree").disabled = 'disabled';
        }
        if (document.getElementById("lbSubmit") != null) {
            document.getElementById("lbSubmit").disabled = 'disabled';
        }
        if (document.getElementById("lbReject") != null) {
            document.getElementById("lbReject").disabled = 'disabled';
        }
    }

    $(function () {
        var url = document.getElementById("hfViewUrl").value;
        $("#ViewUrl").attr("href", url);
        $("#ViewUrl2").attr("href", url+ "&type=1");
    });

</script>
