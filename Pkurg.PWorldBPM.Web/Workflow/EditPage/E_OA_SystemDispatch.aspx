<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_SystemDispatch.aspx.cs"
    Inherits="Workflow_EditPage_E_OA_SystemDispatch" %>

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
        });
    </script>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wf_page">
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <div class="wf_center" style="width: 1000px;">
            <div class="wf_form">
                <div class="wf_form_title">
                    发文审批单（制度）
                    <br />
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            保密等级：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="0">绝密</asp:ListItem>
                                <asp:ListItem Value="1">机密</asp:ListItem>
                                <asp:ListItem Value="2">秘密</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <th>
                            紧急程度：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="0">加急</asp:ListItem>
                                <asp:ListItem Value="1">紧急</asp:ListItem>
                                <asp:ListItem Value="2">一般</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" MaxLength="50" runat="server" contentEditable="false" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tr>
                            <th style="width: 15%">
                                经办部门：
                            </th>
                            <td style="width: 30%">
                                <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" Width="300" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged"> 
                                </asp:DropDownList>
                            </td>
                            <th style="width: 15%">
                                日期：
                            </th>
                            <td style="width: 30%">
                                <input id="UpdatedTextBox" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                经办人：
                            </th>
                            <td>
                                <asp:TextBox ID="tbUserName" runat="server" class="txtshort" ReadOnly="true"></asp:TextBox>
                            </td>
                            <th>
                                电话：
                            </th>
                            <td>
                                <asp:TextBox ID="tbMobile" runat="server" class="txtshort" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                文件名称：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt edit" Width="700"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                发布红头文件：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblRedHeadDocument" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">是</asp:ListItem>
                                    <asp:ListItem Value="1">否</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th>
                                公布于公司内部网：
                            </th>
                            <td>
                                <asp:CheckBoxList ID="cblIsPublish" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">是</asp:ListItem>
                                    <asp:ListItem Value="1">否</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                主要内容：
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                    Width="700"  height="200"></asp:TextBox>
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
                                上传附件：
                            </th>
                            <td colspan="3">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="1001" />
                                说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbDeptleader" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptleader" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionRealateDept" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导/董事意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBoxList ID="cblTopLeaders" runat="server" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <uc4:ApproveOpinionUC ID="OpinionTopLeaders" CurrentNode="false" CurrentNodeName="总办领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbCEO" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionCEO" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;" >
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbChairman" runat="server" Text="选择董事长" />
                                    <asp:Label ID="lbChairman" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionChairman" CurrentNode="false" CurrentNodeName="董事长意见"
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
    <div id="scroll" style="margin-left: 520px;">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" OnClientClick="return Save_Verification()">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" OnClientClick="return Save_Verification()">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click">终止</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
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

    function Save_Verification() {
        var pw1 = document.getElementById("<% =tbTitle.ClientID%>").value;
        if (pw1 != null && pw1 != "") {
            return true;
        }
        else {
            alert("文件名称不能为空");
            return false;
        }
    }
</script>
