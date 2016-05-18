<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_EmployeeTransfer.aspx.cs" Inherits="Workflow_EditPage_E_HR_EmployeeTransfer" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign" TagPrefix="CS" %>
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
<%--    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblisoverCotract");
        });
    </script>--%>
    <style type="text/css">
        .hide
        {
            display: none;
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
                <div class="wf_form_title">
                    员工流动审批
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                            <asp:HiddenField ID="tbToOrFrom" runat="server"></asp:HiddenField>
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
                                <td colspan="3">
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                    <asp:HiddenField ID="tbLoginID" runat="server"></asp:HiddenField>
                                    <a  href="javascript:SelectUser();">选择</a>
                                    <asp:Button ID="Button1" runat="server" Text="" Width="0px" Height="0"  CssClass="hide"
                                        onclick="Button1_Click" />
                                </td>
                                <th>
                                    性别：<span style="color: Red;">*</span>
                                </th>
                                <td >
                                    <asp:TextBox ID="tbSex" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    入司时间：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <input id="tbEntryTime" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    毕业院校及专业：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbGraduation" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    学历：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEducation" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    加入方正时间：
                                </th>
                                <td>
                                    <input id="tbFounderTime" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现所在公司及部门：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt edit" Width="300px"></asp:TextBox>
                                    <asp:HiddenField ID="tbDeptCode" runat="server" ></asp:HiddenField>
                                </td>
                                <th>
                                    职务：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPost" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    职级：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPostLevel" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入公司及部门：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbToDeptName" runat="server" CssClass="txt edit" Width="300px"></asp:TextBox>
                                    <asp:HiddenField ID="tbToDeptCode" runat="server" ></asp:HiddenField>
                                    <a  href="javascript:SelectDept();" >选择</a>
                                    <asp:Button ID="Button2" runat="server" Text="" Width="0px" Height="0"  CssClass="hide"
                                        onclick="Button2_Click" />
                                </td>
                                <th>
                                    职务：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToPost" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    职级：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbToPostLevel" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现单位合同情况：
                                </th>
                                <td colspan="7">
                                    起始日期<input id="tbLabourContractStart" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    到期日期<input id="tbLabourContractEnd" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    现单位薪金情况：
                                </th>
                                <td colspan="7">
                                    工资（税前）：<asp:TextBox ID="tbSalary" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:TextBox ID="tbRatio" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>%&nbsp;&nbsp;
                                    约等于目标年薪（税前）：<asp:TextBox ID="tbAnnualSalary" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入单位合同情况：
                                </th>
                                <td colspan="7">
                                    起始日期<input id="tbToLabourContractStart" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    到期日期<input id="tbToLabourContractEnd" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    拟调入单位薪金情况：
                                </th>
                                <td colspan="7">
                                    工资（税前）：<asp:TextBox ID="tbToSalary" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:TextBox ID="tbToRatio" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>%&nbsp;&nbsp;
                                    约等于目标年薪（税前）：<asp:TextBox ID="tbToAnnualSalary" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    本人意见：
                                </th>
                                <td colspan="7">
                                内部流动原因： 
                                <asp:CheckBoxList ID="cblTransferReason" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" Enabled="false">
                                        <asp:ListItem Value="公司行为的内部调动">公司行为的内部调动</asp:ListItem>
                                        <asp:ListItem Value="员工本人申请的内部流动">员工本人申请的内部流动</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="7">
                                    <asp:TextBox ID="tbRemark" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="7">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3019" />
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
                                    用人部门意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbDeptManager2" runat="server"></asp:Label>
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
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbDirector2" runat="server"></asp:Label>
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

    function SelectUser() {
        var sUrl = "../../Modules/BaseData/EmployeeOperation.aspx";
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);
        var dataInfo = eval(arg);
        if (typeof (dataInfo) != "undefined") {
            $('#<%= tbUserName.ClientID %>').val(dataInfo.EmployeeName);
            $('#<%= tbLoginID.ClientID %>').val(dataInfo.LoginName);
            $('#<%= tbDeptCode.ClientID %>').val(dataInfo.DepartCode);
            $('#<%= tbDeptName.ClientID %>').val(dataInfo.CompanyName + dataInfo.DepartName);
            $('#<%= Button1.ClientID %>').click();
        }
    }

    function SelectDept() {
        var sDept = "S363-S973";
        if (document.getElementById("<% =lbTitle.ClientID%>").innerText == "") {
            sDept = "S363-S969";
        }
        var sUrl = "/Modules/BaseData/DeptOperation.aspx?dept=" + sDept;
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);

        var dataInfo = eval(arg);
        if (typeof (dataInfo) != "undefined") {
            $('#<%= tbToDeptName.ClientID %>').val(dataInfo.Remark);
            $('#<%= tbToDeptCode.ClientID %>').val(dataInfo.DepartCode);
            $('#<%= Button2.ClientID %>').click();
        }
    }
</script>
