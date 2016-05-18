<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Submit.aspx.cs" Inherits="OrgWebSite.Process.CDF.Submit" ValidateRequest="false" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register src="../Controls/UCProcessAction.ascx" tagname="UCProcessAction" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>Custom</title>
    <style type="text/css">
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal !important;
            height: auto;
            vertical-align: text-top;
            padding-top: 2px;
        }
    </style>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/juqery-ui.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="../../JavaScript/json2.js"></script>
    <script type="text/javascript" src="../../JavaScript/ArrayPrototype.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../../JavaScript/Tools.js"></script>
    <script type="text/javascript" src="../../JavaScript/Validate.js"></script>
    <script type="text/javascript" src="../../JavaScript/ValidateNoMustInput.js"></script>
    <script type="text/javascript" src="../../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script type="text/javascript" src="../../JavaScript/smokescreen.js"></script>
    <script type="text/javascript" src="../../JavaScript/metadata.js"></script>
    <script charset="utf-8" src="../../Javascript/kindeditor-min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../Javascript/lang/zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var editor1 = KindEditor.create('#CDF1_AppReason')
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#CDF1_AppReason').val() == '')
                $('#CDF1_AppReason').smokescreen({ smoke: '请在此处填写您的申请原因并尽可能详细描述您的需求' });
            else {
                var tmp = $('#CDF1_AppReason').val();
                $('#CDF1_AppReason').smokescreen({ smoke: '请在此处填写您的申请原因并尽可能详细描述您的需求' });
                $('#CDF1_AppReason').val(tmp);
                $('#CDF1_AppReason').focus();
            }
        });
        //页面检查
        function PageCheck() {
//            if (document.getElementById('CDF1_BSCategory').selectedIndex == -1) {
//                alert('未选择业务小类！');
//                return false;
//            }
//            if ($('#CDF1_AppReason').val() == '' || $('#CDF1_AppReason').val() == '请在此处填写您的申请原因并尽可能详细描述您的需求') {
//                alert('请填写申请原因');
//                return false;
//            }
//            if ($('#CDF1_hfNeedVerification').val() == 'true') {
//                var models = $('#CDF1_hfTemplateCounts').val();     //取得模板个数
//                if (models != 0 && $('#CDF1_Upload1_hfAttachmentCounts').val() == 0) {
//                    alert('请上传附件');
//                    return false;
//                }
//                if ($('#CDF1_Upload1_hfAttachmentCounts').val() < models) {
//                    alert('附件个数少于模板个数');
//                    return false;
//                }
//            }

//            //检查流程
//            if (checkcustomflowchart() == 'ok') {
//                var employeeCode = document.getElementById('CDF1_hfEmployeeCode').value;
//                var operator = '<%=EmployeeCode%>';
//                if (employeeCode != operator) {
//                    alert('由于您是代他人申请，流程会先送到申请人确认');
//                }
//                return true;
//            }
//            else {
//                alert(checkcustomflowchart());        //提示错误信息
//            }
            //            return false;
            return true;
        }
    </script>
</head>
<body style="overflow-x: hidden; margin-top: 0px; width: 798px;">
    <form id="form1" runat="server">
    <div style="margin-top: 0;">
        <uc1:CDF ID="CDF1" runat="server" />
        <%--<div class="conter_right_list_nav" style="margin-left: 20px;">
            <span>流程配置</span>
            <ul style="margin-top: 0px; width: 540px; ">
                <li>
                    
                </li>
            </ul>
            <table id="tblChart" class="datalist2" style="width: 540px; border: 1px solid #F2DD81;
                margin-left: 48px; font-weight: normal;">
                <tr>
                    <td style="background: #fef5c7; font-weight: bold;">
                        序号
                    </td>
                    <td style="background: #fef5c7; font-weight: bold; width:160px;">
                        审批人
                    </td>
                    <td style="background: #fef5c7; font-weight: bold;">
                        节点名称
                    </td>
                    <td style="background: #fef5c7; font-weight: bold;">
                        状态
                    </td>
                    <td style="background: #fef5c7; font-weight: bold;">
                        操作
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>
    <div>
        <%--<asp:Button ID="btnSubmit" runat="server" Text="提 交" 
            onclick="btnSubmit_Click" />--%>
        <uc2:UCProcessAction ID="UCProcessAction1" runat="server" SubmitCFVisible="true"
            Abandon="true" SaveToDraft="true" CloseVisible="false" CommentsVisible="false" CDFStartBackVisible="false"/>
    </div>
    </form>
</body>
</html>