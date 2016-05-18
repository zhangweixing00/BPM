<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_SalaryAdjust.aspx.cs"
    Inherits="Workflow_EditPage_E_HR_SalaryAdjust" %>

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
            selectOneCheckList("cblRDeptManager");
            selectOneCheckList("cblDirector");
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
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    薪酬调整审批单
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table2" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    姓名：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt edit" Width="105px"></asp:TextBox>
                                </td>
                                <th>
                                    现任职务：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPost" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    现所在公司及部门：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt edit" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    工作地点：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbWorkPlace" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    拟任职务：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToPost" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    拟调入公司及部门：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbToDeptName" runat="server" CssClass="txt edit" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    工资（税前）：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbSalary" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>
                                    <asp:Label id="Label1" runat="server" Text="万元/年"></asp:Label>
                                </td>
                                <th>
                                    现业绩奖金基数：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbRatio" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>
                                    <asp:Label id="Label2" runat="server" Text="%"></asp:Label>
                                </td>
                                <th>
                                    目标年薪（税前）：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbAnnualSalary" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                    <asp:Label id="Label3" runat="server" Text="万元/年"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    建议工资（税前）：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToSalary" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>
                                    <asp:Label id="Label4" runat="server" Text="万元/年"></asp:Label>
                                </td>
                                <th>
                                    建议业绩奖金基数：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToRatio" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>
                                    <asp:Label id="Label5" runat="server" Text="%"></asp:Label>
                                </td>
                                <th>
                                    建议约等于目标年薪（税前）：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToAnnualSalary" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                    <asp:Label id="Label6" runat="server" Text="万元/年"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟生效日期：
                                </th>
                                <td>
                                    <input id="tbEffectiveDate" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:false,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                                <th>
                                    调薪原因：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbReason" runat="server" CssClass="heighttxt edit"
                                        TextMode="MultiLine" Width="350" Height="40">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="5">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3021" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr id="trRDeptManager" runat="server">
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <asp:Label id="lbRDeptManager2" runat="server" Text="选择"></asp:Label>
                                    <asp:CheckBoxList ID="cblRDeptManager" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <asp:Label id="lbDirector2" runat="server" Text="选择"></asp:Label>
                                    <asp:CheckBoxList ID="cblDirector" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源部意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbHRDeptManager2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbPresident2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:HiddenField ID="hfIsGroup" runat="server" />
                    <asp:HiddenField ID="hfApprovers" runat="server" />
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
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
