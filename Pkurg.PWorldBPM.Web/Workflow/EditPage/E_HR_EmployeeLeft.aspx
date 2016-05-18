<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_EmployeeLeft.aspx.cs"
    Inherits="Workflow_EditPage_E_HR_EmployeeLeft"%>

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
            selectOneCheckList("tbLeftType");
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
                <div class="wf_form_title" id="wf_form_title" runat="server">
                     <%= FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    <strong>离职人员基本信息</strong>
                                </th>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    员工姓名：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEmployeeName" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
                                    <asp:HiddenField ID="tbEmployeeLoginName" runat="server"></asp:HiddenField>
                                    <a  href="javascript:SelectUser();">选择</a>
                                </td>
                                <th>
                                    部门：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
                                    <asp:HiddenField ID="tbDeptID" runat="server" ></asp:HiddenField>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    职位：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPosition" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
                                </td>
                                <th>
                                    离职类型：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="tbLeftType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="主动离职" Selected="True">主动离职</asp:ListItem>
                                        <asp:ListItem Value="优化">优化</asp:ListItem>
                                        <asp:ListItem Value="调动">调动</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    离职文档模板：
                                </th>
                                <td>
                                    <a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/UploadFiles/WorkFlow/工作交接单模板.doc"
                                        target="_blank">工作交接模板</a>&nbsp;、&nbsp; <a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/UploadFiles/WorkFlow/离职证明模板.doc"
                                            target="_blank">离职证明模板</a>
                                </td>
                                <th>
                                    <span>
                                        <asp:CheckBox ID="cbIsActiveLeft" runat="server" RepeatDirection="Horizontal" Checked="true"
                                            Visible="true" />
                                    </span><span>是否主动离职:</span>
                                </th>
                                <td id="ctl18_tdLeftQuestionnaire" class="td_r" colspan="3">
                                    <font color="red">请点击此链接进行《离职问卷》作答：</font> <a href="http://zyinfo.founder.com/dept/HR/Lists/Survey/NewForm.aspx?IsDlg=0"
                                        target="_blank">离职问卷</a>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td style="padding: 5px;" colspan="4">
                                    <b><font color="red">流程提醒：</font></b><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、请下载离职文档模板中的《工作交接单模板》，在您与相关同事工作交接完毕后，由其在相应交接内容后签字，您在移交人处签字，最后由部门负责人签字确认，以清晰的电子扫描件形式上传到此流程中作为附件；<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、《辞职信》（无模板，本人与主管领导手写签字）和《离职证明》（打印2份手写签字）请一起交由人力资源部。
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3015" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    员工意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbEmployee" runat="server" RepeatDirection="Horizontal" Checked="true"/>
                                    <span id="ctl18_lEmployee0">选择</span>
                                    <span id="ctl18_lEmployee1" style="display: inline-block; font-weight: bold; width: 200px;">
                                        员工意见</span><br />
                                    <span>交接人：</span>
                                    <asp:TextBox ID="tbHandover" runat="server" CssClass="txt" Width="100px" ReadOnly="true">
                                    </asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <span>承接人：</span>
                                    <asp:TextBox ID="tbRecipient" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox><br />
                                    <span id="ctl18_lEmployee2">工作内容交接 交接完毕</span><br />
                                    <span id="ctl18_lEmployee3">工作文档交接 交接完毕</span><br />
                                    <span id="ctl18_lEmployee34">应收、应付款 交接完毕</span>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    用人部门负责人意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbDeptManager" runat="server" RepeatDirection="Horizontal" Checked="true" />
                                    <span id="Span1" style="display: inline-block; font-weight: bold; width: 200px;">用人部门负责人意见</span><br />
                                    <font color="red">备注：请您审核员工上传的《工作交接单》扫描件，确认交接人和承接人工作交接完毕后，进行审批。</font>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门接口人意见：
                                </th>
                                <td colspan="2">
                                    <font color="red">备注：信息接口人审批包括:办公电脑、电子邮箱。<br />固定资产接口人审批包括:门禁卡、抽屉钥匙、办公用品、胸卡和固定资产。<br />财务接口人审批包括:归还借款、费用报销。</font>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门负责人意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源接口人意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源负责人意见：
                                </th>
                                <td colspan="2">
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
<script type="text/javascript">
    function SelectUser() {
        var sUrl = "../../Modules/BaseData/EmployeeOperation.aspx";
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);
        var dataInfo = eval(arg);
        if (typeof (dataInfo) != "undefined") {
            $('#<%= tbEmployeeName.ClientID %>').val(dataInfo.EmployeeName);
            $('#<%= tbEmployeeLoginName.ClientID %>').val(dataInfo.LoginName);
            $('#<%= tbDeptID.ClientID %>').val(dataInfo.DepartCode);
            $('#<%= tbDeptName.ClientID %>').val(dataInfo.DepartName);
        }
    }
</script>

