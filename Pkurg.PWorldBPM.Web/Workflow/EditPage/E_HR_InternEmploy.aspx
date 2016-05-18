<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_HR_InternEmploy.aspx.cs"
    Inherits="Workflow_EditPage_E_HR_InternEmploy" %>

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
                    <%= FormTitle%>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th>
                                姓名
                            </th>
                            <td>
                                <asp:TextBox ID="tbEmployeeName" runat="server" CssClass="txt edit"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbEmployeeName"
                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <th>
                                实习岗位
                            </th>
                            <td>
                                <asp:TextBox ID="tbPosition" runat="server" CssClass="txt edit"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPosition"
                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                实习部门
                            </th>
                            <td>
                                <asp:TextBox ID="tbDept" runat="server" CssClass="txt edit"></asp:TextBox>
                                <asp:HiddenField ID="tbDeptCode" runat="server" ></asp:HiddenField>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDept"
                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                <a  href="javascript:SelectUser();" >选择</a>
                            </td>
                            <th>
                                实习补贴
                            </th>
                            <td>
                                <asp:TextBox ID="tbInternReward" runat="server" CssClass="txt edit"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                岗位级
                            </th>
                            <td>
                                实习生
                            </td>
                            <th>
                                实习期限
                            </th>
                            <td>
                                <asp:TextBox ID="tbInternDeadline" runat="server" CssClass="txt edit"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上传附件：
                            </th>
                            <td colspan="3">
                                <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3022" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1">
                        <tbody>
                            <tr>
                                <th>
                                    人力资源部面试官意见：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    用人部门负责人意见：
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
        var sUrl = "../../Modules/BaseData/DeptOperation.aspx?dept=S363-S973,S363-S969";
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);
        //alert(arg);
        if (arg != "") {
            var dataInfo = eval(arg);
            $('#<%= tbDept.ClientID %>').val(dataInfo.DepartName);
            $('#<%= tbDeptCode.ClientID %>').val(dataInfo.DepartCode);
        }
    }
</script>
