<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_ContractOfWY.aspx.cs"
    Inherits="Workflow_EditPage_E_OA_ContractOfWY" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {
             selectOneCheckList("cblSecurityLevel");
             selectOneCheckList("cblUrgenLevel");
             selectOneCheckList("cblIsReportToGroup");
             selectOneCheckList("cblIsReportToWY");
             selectOneCheckList("cblIsSupplementProtocol");
             selectOneCheckList("cblIsFormatContract");
             selectOneCheckList("cblIsNormText");
             selectOneCheckList("cblIsBidding");
         });
    </script>
    <style type="text/css">
        .style1
        {
            width: 102px;
        }
    </style>
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
                <div class="wf_form_title" id="wf_form_title" runat="server">
                    合同流程单（物业）<br />
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
                                RepeatLayout="Flow">
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
                                RepeatLayout="Flow">
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
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" Width="350px"
                                         OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="25px">
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
                                    <asp:TextBox ID="tbUserName" runat="server" class="txtshort"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbMobile" runat="server" class="txtshort"></asp:TextBox>
                                </td>
                            </tr>
                          <tr id="IsReportToWY" runat="server">
                                <th>
                                    是否上报物业
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsReportToWY" runat="server" RepeatDirection="Horizontal"
                                        Enabled="true">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr id="IsReportToGroup" runat="server">
                                <th>
                                    是否上报资源集团
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsReportToGroup" runat="server" RepeatDirection="Horizontal"
                                        Enabled="true">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同类型：
                                </th>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlContractType1" runat="server" AutoPostBack="True" Width="200px"
                                        Height="20px" OnSelectedIndexChanged="ddlContractType1_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlContractType2" runat="server" AutoPostBack="True" Width="200px"
                                        Height="20px" OnSelectedIndexChanged="ddlContractType2_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlContractType3" runat="server" AutoPostBack="True" Width="200px"
                                        Height="20px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同标的金额：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbContractSum" runat="server" CssClass="txtshort edit"></asp:TextBox>元
                                </td>
                                <th>
                                    是否补充协议：
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsSupplementProtocol" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left; padding-top: 8px;">
                                        <asp:Label ID="lbSupplementProtocol" runat="server">若是，第</asp:Label>
                                        <asp:TextBox ID="tbSupplementProtocol" runat="server" Width="30"></asp:TextBox>
                                        <asp:Label ID="lbSupplementProtocol1" runat="server">份</asp:Label>
                                    </div>
                                    <div style="clear: both;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否格式合同：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsFormatContract" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                    是否合同标准文本：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsNormText" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否经过招标：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsBidding" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同主体：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlContractSubject" runat="server" Width="310px" Height="20px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:TextBox ID="tbContractSubject1" runat="server" CssClass="longtxt edit" Width="300"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContractSubject2" runat="server" CssClass="longtxt edit" Width="300"></asp:TextBox>
                                    <br />
                                    <asp:TextBox ID="tbContractSubject3" runat="server" CssClass="longtxt edit" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同名称：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContractTitle" runat="server" CssClass="longtxt edit" Width="700"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="lbcontent" runat="server" Text="为了让便于领导审批，建议将摘要信息控制在500字以内，如摘要信息过长请放在附件中；"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="tbContractContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="200">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <FR:FlowRelated ID="flowRelated" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3024" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1" id="Company" runat="server">
                        <tbody>
                            <tr id="Company1" runat="server">
                                <th>
                                    分公司
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="false" 
                                    DisableDepartments='财务管理部' DefaultCheckdDepartments="财务管理部"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="wf_table" colspan="1" id="Group" runat="server">
                        <tbody>
                            <tr id="Group1" runat="server">
                                <th>
                                    物业集团
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CSG:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="true" 
                                        CounterSignDeptId="S366-S976" IsCanSelectSelfDeptment="false" 
                                    DisableDepartments='财务管理部' DefaultCheckdDepartments="财务管理部"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁" />
                                    <asp:Label ID="lbAP" runat="server"></asp:Label>
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁" />
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    发起人上传最终版合同：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务复核意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员盖章：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    档案管理员归档：
                                </th>
                                <td colspan="2">
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
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" OnClientClick="return Save_Verification()">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" OnClientClick="return Save_Verification()">提交</asp:LinkButton></li>
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
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }

    function Save_Verification() {
        var tb1 = document.getElementById("<% =tbContractSum.ClientID%>").value;
        var tb2 = document.getElementById("<% =tbContractSubject2.ClientID%>").value;
        var tb3 = document.getElementById("<% =tbContractTitle.ClientID%>").value;
        var tb4 = document.getElementById("<% =tbContractContent.ClientID%>").value;
        var ddl1 = document.getElementById("<% =ddlContractType1.ClientID%>");
        var ddl2 = document.getElementById("<% =ddlContractSubject.ClientID%>");

        if (tb1 == null || tb1 == "") {
            alert("合同标的金额不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("合同主体不能为空");
            return false;
        }
        else if (tb3 == null || tb3 == "") {
            alert("合同名称不能为空");
            return false;
        }
        else if (tb4 == null || tb4 == "") {
            alert("主要内容不能为空");
            return false;
        }
        else if (ddl1.selectedIndex == 0) {
            alert("请选择合同类型");
            return false;
        }
        else if (ddl2.selectedIndex == 0) {
            alert("请选择合同主体");
            return false;
        }
        else {
            return true;
        }
    }
</script>
