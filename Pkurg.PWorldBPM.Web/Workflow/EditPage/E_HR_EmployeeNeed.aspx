<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_EmployeeNeed.aspx.cs"
    Inherits="Workflow_EditPage_E_HR_EmployeeNeed" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
    <%--    <script type="text/javascript">
        $(function () {
            selectOneCheckList("cblisoverCotract");
        });
    </script>--%>
    <style type="text/css">
        
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
                    人员需求申请单
                </div>
                <div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
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
                        <tr>
                            <th>
                                需求部门：
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="300px"
                                    OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="20px">
                                </asp:DropDownList>
                            </td>
                            <th>
                                岗位：<span style="color: Red;">*</span>
                            </th>
                            <td>
                                <asp:TextBox ID="tbPosition" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                申请时间：<span style="color: Red;">*</span>
                            </th>
                            <td>
                                <input id="UpdatedTextBox" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                            </td>
                            <th>
                                需求原因：<span style="color: Red;">*</span>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlReason" runat="server" Height="20px">
                                    <asp:ListItem Value="编制内招聘">编制内招聘</asp:ListItem>
                                    <asp:ListItem Value="编制外招聘">编制外招聘</asp:ListItem>
                                    <asp:ListItem Value="实习生需求">实习生需求</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <th>
                                人数：<span style="color: Red;">*</span>
                            </th>
                            <td>
                                <asp:TextBox ID="tbNumber" runat="server" CssClass="txt  edit" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                主要职责和工作目标：<span style="color: Red;">*</span>
                            </th>
                            <td colspan="5">
                                <asp:TextBox ID="tbMajorDuty" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                    Width="700" Height="150">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                所属资格与条件：
                            </th>
                            <td colspan="5">
                                <table class="wf_table2" cellspacing="1" cellpadding="0">
                                    <tr>
                                        <th>
                                            性别：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlSex" runat="server" Height="20px">
                                                <asp:ListItem Value="男">男</asp:ListItem>
                                                <asp:ListItem Value="女">女</asp:ListItem>
                                                <asp:ListItem Value="不限">不限</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <th>
                                            年龄：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlAge" runat="server" Height="20px">
                                                <asp:ListItem Value="20-30">20-30</asp:ListItem>
                                                <asp:ListItem Value="30-40">30-40</asp:ListItem>
                                                <asp:ListItem Value="40以上">40以上</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            学历最低要求：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlEducation" runat="server" AutoPostBack="True" Height="20px">
                                                <asp:ListItem Value="硕士及以上">硕士及以上</asp:ListItem>
                                                <asp:ListItem Value="本科">本科</asp:ListItem>
                                                <asp:ListItem Value="大专及以下">大专及以下</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <th>
                                            专业：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlSpecialty" runat="server" AutoPostBack="True" Height="20px">
                                                <asp:ListItem Value="计算机/网络/技术类">计算机/网络/技术类</asp:ListItem>
                                                <asp:ListItem Value="市场/公关/媒介类">市场/公关/媒介类</asp:ListItem>
                                                <asp:ListItem Value="财务/审计/税务/统计类">财务/审计/税务/统计类</asp:ListItem>
                                                <asp:ListItem Value="人力资源类">人力资源类</asp:ListItem>
                                                <asp:ListItem Value="投资/金融类">投资/金融类</asp:ListItem>
                                                <asp:ListItem Value="市场营销/销售类">市场营销/销售类</asp:ListItem>
                                                <asp:ListItem Value="建筑/房地产/工民建/造价">建筑/房地产/工民建/造价</asp:ListItem>
                                                <asp:ListItem Value="法务类">法务类</asp:ListItem>
                                                <asp:ListItem Value="行政/后勤/文秘类">行政/后勤/文秘类</asp:ListItem>
                                                <asp:ListItem Value="保险/医疗医药类">保险/医疗医药类</asp:ListItem>
                                                <asp:ListItem Value="其他类">其他类</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            职称最低要求：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlTitle" runat="server" AutoPostBack="True" Height="20px">
                                                <asp:ListItem Value="不限">不限</asp:ListItem>
                                                <asp:ListItem Value="高级">高级</asp:ListItem>
                                                <asp:ListItem Value="中级">中级</asp:ListItem>
                                                <asp:ListItem Value="初级">初级</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <th>
                                            相关工作年限：
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="ddlWorkingLifetime" runat="server" AutoPostBack="True" Height="20px">
                                                <asp:ListItem Value="应届">应届</asp:ListItem>
                                                <asp:ListItem Value="1-3">1-3</asp:ListItem>
                                                <asp:ListItem Value="3-5">3-5</asp:ListItem>
                                                <asp:ListItem Value="5-8">5-8</asp:ListItem>
                                                <asp:ListItem Value="8年以上">8年以上</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            希望上班时间：
                                        </th>
                                        <td>
                                            <input id="tbWorkTime" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:false,dateFmt:'yyyy-MM-dd'})" />
                                        </td>
                                        <th>
                                            专业能力概述：
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbProfessionalAbility" runat="server" CssClass="heighttxt edit"
                                                TextMode="MultiLine" Width="350" Height="40">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上传附件：
                            </th>
                            <td colspan="5">
                                <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3016" />
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
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" OnClientClick="return Verification()">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" OnClientClick="return Verification()">提交</asp:LinkButton></li>
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

    function Verification() {
        var tb1 = document.getElementById("<% =tbPosition.ClientID%>").value;
        var tb2 = document.getElementById("<% =UpdatedTextBox.ClientID%>").value;
        var tb3 = document.getElementById("<% =tbNumber.ClientID%>").value;
        var tb4 = document.getElementById("<% =tbMajorDuty.ClientID%>").value;
        if (tb1 == null || tb1 == "") {
            alert("岗位不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("申请时间不能为空");
            return false;
        }
        else if (tb3 == null || tb3 == "") {
            alert("人数不能为空");
            return false;
        }
        else if (tb4 == null || tb4 == "") {
            alert("主要职责和工作目标不能为空");
            return false;
        }
        else {
            return true;
        }
    }
</script>
