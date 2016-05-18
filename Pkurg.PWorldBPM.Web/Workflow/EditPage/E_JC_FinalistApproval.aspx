<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_JC_FinalistApproval.aspx.cs" Inherits="Workflow_EditPage_JC_FinalistApproval" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblSecurityLevel");
            selectOneCheckList("cblUrgenLevel");
            selectOneCheckList("cblRedHeadDocument");
            selectOneCheckList("cblIsPublish");
            selectOneCheckList("cblIsImpowerProject");
            selectOneCheckList("cblFirstLevel");
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
        <div class="wf_center" style="width: 1080px;">
            <!--流程主表单-->
            <div class="wf_form" id="wf_form_title" runat="server">
                <div class="wf_form_title">
                    入围投标人资格审批表
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
                                    <asp:TextBox ID="tbProjectName" runat="server" CssClass="txt edit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="tbProjectName" ErrorMessage="项目名称不能为空！"></asp:RequiredFieldValidator>
                                </td>
                                <th>
                                    <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsImpowerProject" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal" Visible="false">
                                        <asp:ListItem Value="0">一级开发</asp:ListItem>
                                        <asp:ListItem Value="1">二级开发</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th >
                                    呈报部门
                                </th>
                                <td >
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" Width="300px"
                                        OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="20px">
                                    </asp:DropDownList>
                                </td>
                                <th >
                                    呈报日期
                                </th>
                                <td > 
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                <div style="line-height:25px;">
                                    入围投标人资格审查情况
                                </div>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbCheckStatus" runat="server"  CssClass="heighttxt edit" TextMode="MultiLine" Width="700"></asp:TextBox>
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
                                    上传附件说明：最大可上传<span id="ctl21_AttachmentView1_lblMaxUpload">50</span>M的文件，请不要上传同名文件，否则会覆盖之前的文件！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr id="zbcgOpinion" runat="server">
                                <th>
                                    招标采购部意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr id="fwOpinion" runat="server">
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    采购管理部意见：
                                </th>
                                <td colspan='2'>
                                    <asp:CheckBoxList ID="cbGroupPurchaseDept" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">采购管理部</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团法务部意见：
                                </th>
                                <td colspan='2'>
                                <asp:CheckBoxList ID="cbGroupLegalDept" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">集团法务部</asp:ListItem>
                                </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
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
    <div id="scroll" style="margin-left: 520px;">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbClose" runat="server" OnClientClick="return Close_Win()">关闭</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click">终止</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
    //需要完善
//    function addListener(element, e, fn) {
//        if (element.addEventListener) {
//            element.addEventListener(e, fn, false);
//        } else {
//            element.attachEvent("on" + e, fn);
//        }
//    }
//    var gFlag = true; //设置一个全局变量，用于判断是否允许清除文本框内容
//    var textcontent = document.getElementById("tbContent");
//    addListener(textcontent, "click", function () {
//        if (gFlag == true) {
//            textcontent.value = "";
//            gFlag = false;
//        }
//    })
</script>
