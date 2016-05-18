<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_ContractAuditOfEToI.aspx.cs" 
Inherits="Workflow_EditPage_E_OA_ContractAuditOfEToI" %>

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
            selectOneCheckList("cblIsSupplementProtocol");
            selectOneCheckList("cblIsFormatContract");
            selectOneCheckList("cblIsNormText");
            selectOneCheckList("cblIsBidding");
            selectOneCheckList("cblIsEstateProject");
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
                    合同流程单
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
                                        Height="25px" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged">
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
                                    是否房地产项目：
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblIsEstateProject" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    房地产项目名称：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlEstateProjectName" runat="server" Width="200px" Height="20px">
                                        <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">博雅CC</asp:ListItem>
                                        <asp:ListItem Value="2">北大资源·江山名门</asp:ListItem>
                                        <asp:ListItem Value="3">北大资源·理城</asp:ListItem>
                                        <asp:ListItem Value="4">北大资源·首座</asp:ListItem>
                                        <asp:ListItem Value="5">北大资源·尚品燕园</asp:ListItem>
                                        <asp:ListItem Value="6">北大资源·尚品清河</asp:ListItem>
                                        <asp:ListItem Value="7">北大资源·御湾</asp:ListItem>
                                        <asp:ListItem Value="8">北大资源·北大时代</asp:ListItem>
                                        <asp:ListItem Value="9">北大资源·时光</asp:ListItem>
                                        <asp:ListItem Value="10">北大资源·梦想城</asp:ListItem>
                                        <asp:ListItem Value="11">北大资源·博雅</asp:ListItem>
                                        <asp:ListItem Value="12">北大资源·燕南</asp:ListItem>
                                        <asp:ListItem Value="13">北大资源·缤纷广场</asp:ListItem>
                                        <asp:ListItem Value="14">北大资源·G70</asp:ListItem>
                                        <asp:ListItem Value="15">北大资源·山水年华</asp:ListItem>
                                        <asp:ListItem Value="16">北大资源·莲湖锦城</asp:ListItem>
                                        <asp:ListItem Value="17">北大资源·北大科技园上地项目</asp:ListItem>
                                        <asp:ListItem Value="18">北大资源·方正医药研究院B-6c地块项目</asp:ListItem>
                                        <asp:ListItem Value="19">北大资源·平谷科教园项目</asp:ListItem>
                                        <asp:ListItem Value="20">北大资源·李遂项目</asp:ListItem>
                                        <asp:ListItem Value="21">北大资源·博雅东</asp:ListItem>
                                        <asp:ListItem Value="22">北大资源·悦来项目</asp:ListItem>
                                        <asp:ListItem Value="23">西溪海港城</asp:ListItem>
                                        <asp:ListItem Value="24">浙商财富中心</asp:ListItem>
                                        <asp:ListItem Value="25">宜昌中央文艺区</asp:ListItem>
                                        <asp:ListItem Value="26">昆明医大广场项目</asp:ListItem>
                                        <asp:ListItem Value="27">北大资源·博雅滨江</asp:ListItem>
                                        <asp:ListItem Value="28">余政储出（2015）35号地块</asp:ListItem>
                                        <asp:ListItem Value="29">北大科技园·博雅苑</asp:ListItem>
                                        <asp:ListItem Value="30">北大资源城·燕园</asp:ListItem>
                                        <asp:ListItem Value="31">北大资源.株洲天池项目</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    房地产项目期数：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlEstateProjectNum" runat="server" Width="100px" Height="20px">
                                        <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                        <asp:ListItem Value="1">第一期</asp:ListItem>
                                        <asp:ListItem Value="2">第二期</asp:ListItem>
                                        <asp:ListItem Value="3">第三期</asp:ListItem>
                                        <asp:ListItem Value="4">第四期</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同主体：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlContractSubject" runat="server" Width="310px"
                                        Height="20px">
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
                                    <asp:Label ID="lbFlowRelated1" runat="server" Text="经过招标的合同，请添加相关的定标流程！" ForeColor="Red"></asp:Label>
                                    <br />
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="3006" />
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
                                    <asp:CheckBoxList ID="cblDeptDirectors" runat="server" RepeatDirection="Horizontal" />
                                    <asp:CheckBox ID="cbDeptManager" runat="server" Text="选择部门负责人" Checked="true" Enabled="false" />
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptDiretor" CurrentNode="false" CurrentNodeName="部门经理意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="经办部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="false" 
                                    DisableDepartments='法务部,财务管理部' DefaultCheckdDepartments="财务管理部"  />
                                    <uc4:ApproveOpinionUC ID="OpinionCountersign" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    法务部意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionLawDept" CurrentNode="false" CurrentNodeName="法务部意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan='2'>
                                    <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁" />
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁" />
                                    <asp:CheckBox ID="cbCFO" runat="server" Text="选择主管财务领导" />
                                    <asp:Label ID="lbCFO" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbDirector" runat="server" Font-Bold="true">若还需要其他领导审批，请勾选</asp:Label>
                                    <asp:CheckBoxList ID="cblDirectors" runat="server" RepeatDirection="Horizontal">
                                    </asp:CheckBoxList>
                                    <uc4:ApproveOpinionUC ID="OpinionAP" CurrentNode="false" CurrentNodeName="相关部门主管助理总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionVP" CurrentNode="false" CurrentNodeName="相关部门主管副总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionDirectors" CurrentNode="false" CurrentNodeName="其他领导意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionCFO" CurrentNode="false" CurrentNodeName="主管财务领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr id="trEVP" runat="server" visible="false">
                                <th>
                                    常务副总裁意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbEVP" runat="server" Text="选择常务副总裁" />
                                    <asp:Label ID="lbEVP" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionEVP" CurrentNode="false" CurrentNodeName="常务副总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁/总经理意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbPresident" runat="server" Text="选择总裁" />
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionPresident" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
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
                            <tr>
                                <th>
                                    征求董事意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbIsReport" runat="server" Text="是否征求董事意见" />
                                </td>
                            </tr>
                            <tr id="trStartToFinallyContract" runat="server">
                                <th>
                                    发起人上传最终版合同：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionStartToFinallyContract" CurrentNode="false" CurrentNodeName="发起人上传最终版合同"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr id="trLawAuditOpinion" runat="server">
                                <th>
                                    法务复核意见：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionLawAuditOpinion" CurrentNode="false" CurrentNodeName="法务复核意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr id="trSealAdministrator" runat="server">
                                <th>
                                    印章管理员盖章：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionSealAdministrator" CurrentNode="false" CurrentNodeName="印章管理员盖章"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr id="trFileManager" runat="server">
                                <th>
                                    档案管理员归档：
                                </th>
                                <td colspan="2">
                                    <uc4:ApproveOpinionUC ID="OpinionFileManager" CurrentNode="false" CurrentNodeName="档案管理员归档"
                                        runat="server" />
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
