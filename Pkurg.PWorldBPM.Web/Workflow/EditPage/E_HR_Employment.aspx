<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_Employment.aspx.cs"
    Inherits="Workflow_EditPage_E_HR_Employment" %>

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
            selectOneCheckList("cblIsLabourContract");
            selectOneCheckList("cblIsProbationPeriod");
        });
    </script>
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
                    员工录用审批
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
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    部门：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt edit" Width="300px"></asp:TextBox>
                                    <asp:HiddenField ID="tbDeptCode" runat="server" ></asp:HiddenField>
                                    <a  href="javascript:SelectDept();" >选择</a>
                                    <asp:Button ID="Button1" runat="server" Text="" Width="0px" Height="0"  CssClass="hide"
                                        onclick="Button1_Click" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    目标岗位：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbGoalPost" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    岗位级别：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbPostLevel" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    薪资情况：
                                </th>
                                <td colspan="5">
                                    工资（税前）：<asp:TextBox ID="tbSalary" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>万元/年&nbsp;&nbsp;
                                    业绩奖金基数：<asp:TextBox ID="tbRatio" runat="server" CssClass="txt edit" Width="55px"></asp:TextBox>%&nbsp;&nbsp;
                                    目标年薪（税前）：<asp:TextBox ID="tbAnnualSalary" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>万元/年
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    合同期限：
                                </th>
                                <td colspan="5">
                                    <asp:CheckBoxList ID="cblIsLabourContract" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="劳动合同期限">劳动合同期限</asp:ListItem>
                                        <asp:ListItem Value="聘用协议期限">聘用协议期限</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <input id="tbLabourContractStart" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd',onpicked:inputBlur})" />
                                    至<input id="tbLabourContractEnd" runat="server" class="txt" style="width: 100px"
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    试用期待遇：
                                </th>
                                <td colspan="5">
                                    试用期：<asp:CheckBoxList ID="cblIsProbationPeriod" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="有">有</asp:ListItem>
                                        <asp:ListItem Value="无">无</asp:ListItem>
                                    </asp:CheckBoxList>
                                    <input id="tbProbationPeriodStart" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    至<input id="tbProbationPeriodEnd" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    (注：试用期待遇：月固定工资80%，试用期纳入年终综合考评。)
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    备注：
                                </th>
                                <td colspan="5">
                                    <asp:TextBox ID="tbRemark" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="40"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="5">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3017" />
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

    function SelectDept() {
        var sDept = "S363-S973";
        if (document.getElementById("<% =lbTitle.ClientID%>").innerText == "") {
            sDept = "S363-S969";
        }
        var sUrl = "/Modules/BaseData/DeptOperation.aspx?dept="+sDept;
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);

        var dataInfo = eval(arg);
        if ( typeof(dataInfo) != "undefined") {
            $('#<%= tbDeptName.ClientID %>').val(dataInfo.DepartName);
            $('#<%= tbDeptCode.ClientID %>').val(dataInfo.DepartCode);
            $('#<%= Button1.ClientID %>').click();
        }
    }

    function inputBlur() {
        var StartTime = document.getElementById("tbLabourContractStart").value;
        document.getElementById("tbProbationPeriodStart").value = StartTime;

        var EndTime = new Date(StartTime.substr(0, 4), StartTime.substr(5, 2), StartTime.substr(8, 2));
        var end = new Date(EndTime.getTime() + 30 * 24 * 3600 * 1000);
        var endMonth = (end.getMonth() + 1 < 10 ? "0" : "") + (end.getMonth() + 1);
        var endDay = (end.getDate() < 10 ? "0" : "") + end.getDate();
        document.getElementById("tbProbationPeriodEnd").value = end.getFullYear() + "-" + endMonth + "-" + endDay; 
    }

</script>
