<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_JC_PurchasePaint.aspx.cs"
    Inherits="Workflow_EditPage_E_JC_PurchasePaint" %>

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
                    新增产品审批单
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true"/>
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table2" cellspacing="1" cellpadding="0">
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
                                    项目名称：
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
                                    备注：
                                </th>
                                <td colspan="3">
                                    <asp:Label ID="lbRemark" runat="server" Text="须写明新增产品原因、价格情况说明、产品详细信息等" ForeColor="Red"></asp:Label>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="3007" />
                                    说明：所有文件请先解密再上传，最大可上传50M的文件！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="部门负责人意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="false" />
                                    <uc4:ApproveOpinionUC ID="OpinionCountersign" CurrentNode="false" CurrentNodeName="会签"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总裁意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionPresident" CurrentNode="false" CurrentNodeName="总裁意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团相关部门意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbDoorManager" runat="server" onclick = 'check(this)'/>
                                    <asp:CheckBox ID="cbPaintManager" runat="server" onclick = 'check(this)'/>
                                    <asp:CheckBox ID="cbJJuManager" runat="server" onclick = 'check(this)'/>
                                    <asp:Label ID="lbJJuManager" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbProjectDiretor" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbPurchaseDiretor" runat="server"></asp:Label><br />
                                    <asp:Label ID="lbGroupDeptManager" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    集团分管领导：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbGroupLeader" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    入库审核/复核意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
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
            alert("项目名称不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("主要内容不能为空");
            return false;
        }
        else if (!(document.getElementById('cbDoorManager').checked || document.getElementById('cbPaintManager').checked || document.getElementById('cbJJuManager').checked))
        {
            alert("请选择集团相关审批人");
            return false;
        }
        else {
            return true;
        }
    }

    function check(obj) {
        
        if (obj.id == 'cbDoorManager' && obj.checked == true) {
            document.getElementById('cbPaintManager').checked = false;
            document.getElementById('cbJJuManager').checked = false;
        }
        else if (obj.id == 'cbPaintManager' && obj.checked == true) {
            document.getElementById('cbDoorManager').checked = false;
            document.getElementById('cbJJuManager').checked = false;
        }
        else if (obj.id == 'cbJJuManager' && obj.checked == true) {
            document.getElementById('cbDoorManager').checked = false;
            document.getElementById('cbPaintManager').checked = false;
        }
    }
</script>
