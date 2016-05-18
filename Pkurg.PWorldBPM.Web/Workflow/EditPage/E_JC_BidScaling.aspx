<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_JC_BidScaling.aspx.cs"
    Inherits="Workflow_EditPage_E_JC_BidScaling" %>

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
            selectOneCheckList("cblIsAccreditByGroup");
            selectOneCheckList("cblFirstLevel");
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
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt edit" Width="700"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报部门：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" Width="300px"
                                        OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="20px">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    呈报日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" ReadOnly="true" Width="170px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    评标内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="200">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    要求进场时间：
                                </th>
                                <td>
                                    <input id="UpdatedTextBox" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    <asp:TextBox ID="tbEntranceTime" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                               <th>
                                   <asp:Label ID="lbIsImpowerProject" runat="server" Text="集团授权项目"></asp:Label>
                                </th>
                                <td>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblIsAccreditByGroup" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">是</asp:ListItem>
                                            <asp:ListItem Value="1">否</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div style="float: left;">
                                        <asp:CheckBoxList ID="cblFirstLevel" runat="server" RepeatDirection="Horizontal"
                                            Visible="false">
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
                                    第一入围单位：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbFirstUnit" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="300"></asp:TextBox>
                                </td>
                                <th>
                                    第二入围单位：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbSecondUnit" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="300"></asp:TextBox>
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
                                    <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="true" AppId="1003" />
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
                            <tr id="trCounterSign" runat="server">
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <uc3:Countersign ID="Countersign1" runat="server" IsCanEdit="true" IsCanSelectSelfDeptment="true" DisableDepartments='法务部'/>
                                </td>
                            </tr>
                            <tr id="trCounterSignGroup" runat="server">
                                <th>
                                    集团相关部门意见
                                </th>
                                <td colspan="2">
                                    <uc5:Countersign_Group ID="Countersign_Group1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    招标委员会意见：
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    定标结果：
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

    function addListener(element, e, fn) {
        if (element.addEventListener) {
            element.addEventListener(e, fn, false);
        } else {
            element.attachEvent("on" + e, fn);
        }
    }

    function Save_Verification() {
        var tb1 = document.getElementById("<% =tbTitle.ClientID%>").value;
        var tb2 = document.getElementById("<% =tbContent.ClientID%>").value;
        var tb3 = document.getElementById("<% =UpdatedTextBox.ClientID%>").value;
        var tb4 = document.getElementById("<% =tbFirstUnit.ClientID%>").value;
        var tb5 = document.getElementById("<% =tbSecondUnit.ClientID%>").value;
        if (tb1 == null || tb1 == "") {
            alert("文件名称不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("评标内容不能为空");
            return false;
        }
        else if (tb3 == null || tb3 == "") {
            alert("进场时间不能为空");
            return false;
        }
        else if (tb4 == null || tb4 == "") {
            alert("第一入围单位不能为空");
            return false;
        }
        else if (tb5 == null || tb5 == "") {
            alert("第二入围单位不能为空，若没有请填“无”");
            return false;
        }
        else {
            return true;
        }
    }
</script>
