<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_InstructionOfGroup.aspx.cs"
    Inherits="Workflow_EditPage_E_OA_InstructionOfGroup" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="uc5" %>
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
        });
    </script>
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
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form" id="wf_form_title" runat="server">
                <div class="wf_form_title">
                    流程单
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true"/>
                        </td>
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
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" Width="300px"
                                        OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="20px">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <input id="UpdatedTextBox" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbMobile" runat="server" CssClass="txt  edit" Width="100px"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt edit" Width="700"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="200">
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="3003" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr id="trDept" runat="server">
                                <th>
                                    经办部门意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbDeptManager" runat="server" Text="选择部门负责人" Checked="true" Enabled="false"/>
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr id="trDutyFree" runat="server" visible="false">
                                <th>
                                    选择相关领导：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblRelatedManager" runat="server" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <asp:CheckBox ID="cbDutyFreeManager" runat="server" Text="选择" />
                                    <asp:Label ID="lbDutyFreeManager" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trDutyFreeOpinion" runat="server" visible="false">
                                <th>
                                    相关领导意见：
                                </th>
                                <td>
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC1" CurrentNode="false" CurrentNodeName="条线负责人意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUC2" CurrentNode="false" CurrentNodeName="免税店领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="true" />
                                    <uc4:ApproveOpinionUC ID="OpinionCountersign" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan='2'>
                                    <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁"/>
                                    <asp:Label ID="lbAP" runat="server"></asp:Label>
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁"/>
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbDirector" runat="server" Font-Bold="true" Visible="false">若还需要其他领导审批，请勾选</asp:Label>
                                    <asp:CheckBoxList ID="cblDirectors" runat="server" RepeatDirection="Horizontal" Visible="false">
                                    </asp:CheckBoxList>
                                    <uc4:ApproveOpinionUC ID="OpinionAP" CurrentNode="false" CurrentNodeName="相关部门主管助理总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionVP" CurrentNode="false" CurrentNodeName="相关部门主管副总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionDirectors" CurrentNode="false" CurrentNodeName="总办会领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbPresident" runat="server" Text="选择总裁" />
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionPresident" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr style="display:none;" >
                                <th>
                                    董事长意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbChairman" runat="server" Text="选择董事长"/>
                                    <asp:Label ID="lbChairman" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionChairman" CurrentNode="false" CurrentNodeName="董事长意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    征求董事意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbIsReport" runat="server" Text="是否征求董事意见"/>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:Label ID="lblApprovers" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbIsReport" runat="server" Visible="false"></asp:Label>
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

    function Save_Verification() {
        var tb1 = document.getElementById("<% =tbTitle.ClientID%>").value;
        var tb2 = document.getElementById("<% =tbContent.ClientID%>").value;
        if (tb1 == null || tb1 == "") {
            alert("主题不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("主要内容不能为空");
            return false;
        }
        else {
            return true;
        }
    }
</script>
