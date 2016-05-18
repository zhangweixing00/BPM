<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_OA_ContractAuditOfGroup.aspx.cs" 
Inherits="Workflow_ApprovePage_A_OA_ContractAuditOfGroup" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
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
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true"/>
                        </td>
                        <th>
                            保密等级：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Value="0">绝密</asp:ListItem>
                                <asp:ListItem Value="1">机密</asp:ListItem>
                                <asp:ListItem Value="2">秘密</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <th>
                            紧急程度：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Enabled="false">
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
                                     <asp:TextBox ID="tbDepartName" runat="server" CssClass="longtxt" ReadOnly="true" Width="300"></asp:TextBox>
                                    <asp:Label ID="lbDeptCode" runat="server" Visible="false"></asp:Label>
                                </td>
                                <th>
                                    日期：
                                </th>
                                 <td > 
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true" Width="200px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" class="txtshort" ReadOnly="true" ></asp:TextBox>
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
                                    <asp:TextBox ID="ddlContractType1" runat="server" ReadOnly="true" Width="200px"
                                        Height="20px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="ddlContractType2" runat="server" ReadOnly="true" Width="200px"
                                        Height="20px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="ddlContractType3" runat="server" ReadOnly="true" Width="200px"
                                        Height="20px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同标的金额：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbContractSum" runat="server" CssClass="longtxt" Width="300" ReadOnly="true" BackColor="White"></asp:TextBox>元
                                </td>
                                <th>
                                    是否补充协议：
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsSupplementProtocol" runat="server" RepeatDirection="Horizontal"
                                            Enabled="false">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left;">
                                        <asp:Label ID="lbSupplementProtocol" runat="server">若是，第</asp:Label>
                                        <asp:Label ID="tbSupplementProtocol" runat="server" Width="30"></asp:Label>
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
                                    <asp:CheckBoxList ID="cblIsFormatContract" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                    是否合同标准文本：
                                </th>
                                <td>
                                     <asp:CheckBoxList ID="cblIsNormText" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否经过招标：
                                </th>
                               <td>
                                     <asp:CheckBoxList ID="cblIsBidding" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <th>
                                    是否房地产项目：
                                </th>
                                <td>
                                     <asp:CheckBoxList ID="cblIsEstateProject" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                        <asp:ListItem Value="1">是</asp:ListItem>
                                        <asp:ListItem Value="0">否</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    房地产项目名称：
                                </th>
                                <td>
                                     <asp:TextBox ID="ddlEstateProjectName" runat="server" ReadOnly="True" Width="200px"
                                        Height="20px">
                                    </asp:TextBox>
                                </td>
                                <th>
                                    房地产项目期数：
                                </th>
                                <td>
                                <asp:TextBox ID="ddlEstateProjectNum" runat="server" ReadOnly="True" Width="100px"
                                        Height="20px">
                                    </asp:TextBox>                                    
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同主体：
                                </th>
                                <td>
                                <asp:TextBox ID="ddlContractSubject" runat="server" ReadOnly="True" Width="300px" Height="20px"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="tbContractSubject1" runat="server" CssClass="longtxt" Width="300" ReadOnly="True" BackColor="White"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                <asp:TextBox ID="tbContractSubject2" runat="server" CssClass="longtxt" Width="300" ReadOnly="True" BackColor="White"></asp:TextBox>
                                <br/>
                                <asp:TextBox ID="tbContractSubject3" runat="server" CssClass="longtxt" Width="300" ReadOnly="True" BackColor="White"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同名称：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContractTitle" runat="server" CssClass="longtxt" Width="700" ReadOnly="True" BackColor="White"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                 <td colspan="3">
                                    <asp:TextBox ID="tbContractContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="700" Height="200" ReadOnly="true" BackColor="White">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server" IsCanEdit="false"  />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="3">
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true"  AppId="3007" />
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
                                    <asp:CheckBox ID="cbDeptManager" runat="server" Text="选择部门负责人" Checked="true" Enabled="false" />
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="经办部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="true" 
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
                                    <asp:CheckBox ID="cbAP" runat="server" Text="选择相关部门主管助理总裁"  Enabled="false"/>
                                    <asp:Label ID="lbAP" runat="server"></asp:Label>
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总裁"  Enabled="false"/>
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbDirector" runat="server" Font-Bold="true" Visible="false">若还需要其他领导审批，请勾选</asp:Label>
                                    <asp:CheckBoxList ID="cblDirectors" runat="server" RepeatDirection="Horizontal" Visible="false">
                                    </asp:CheckBoxList>
                                    <uc4:ApproveOpinionUC ID="OpinionAP" CurrentNode="false" CurrentNodeName="相关部门主管助理总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionLawAP" CurrentNode="false" CurrentNodeName="法务部主管助理总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionVP" CurrentNode="false" CurrentNodeName="相关部门主管副总裁意见"
                                        runat="server" />
                                    <uc4:ApproveOpinionUC ID="OpinionDirectors" CurrentNode="false" CurrentNodeName="其他领导意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbCEO" runat="server" Text="选择总裁"  Enabled="false"/>
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
                                    <asp:CheckBox ID="cbChairman" runat="server" Text="选择董事长"  Enabled="false"/>
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
                                    <asp:CheckBox ID="cbIsReport" runat="server" Text="是否征求董事意见" Enabled="false"/>
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
</script>
