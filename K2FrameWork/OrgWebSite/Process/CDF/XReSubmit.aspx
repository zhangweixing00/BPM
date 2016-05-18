<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XReSubmit.aspx.cs" Inherits="OrgWebSite.Process.CDF.XReSubmit" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UCProcessAction.ascx" TagName="UCProcessAction" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ProcessLog.ascx" TagName="ProcessLog" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>重提交</title>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/juqery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal !important;
            height: auto;
            vertical-align: text-top;
            padding-top: 2px;
        }
    </style>
    <style type="text/css">
        /*文本框只读*/
        
        .conter_right_list_nav ul li.conter_right_list_input_bg_border
        {
            border-bottom: 1px solid #333333;
            width: 177px;
            text-align: left;
            vertical-align: middle;
            height: 22px;
        }
        .conter_right_list_nav ul li.conter_right_list_input_bg_border_red
        {
            border: 1px solid red;
            width: 177px;
            text-align: left;
            vertical-align: middle;
            height: 22px;
        }
        
        .conter_right_list_nav ul li.conter_right_list_input_bg_textare_border
        {
            height: 55px;
            border: #999999 none 1px;
            vertical-align: middle;
        }
        
        .inputreadonly
        {
            border: 0px none #9a9a9a;
            vertical-align: top;
            color: #333333;
            overflow: hidden;
            padding-top: 4px;
        }
        select
        {
            width: 100%;
            border: 0;
        }
        
        input[type="text"]
        {
            width: 170px;
        }
        .mustinput
        {
            color: Red;
        }
        
        .conter_right
        {
            float: left;
            width: 798px;
            height: 100%;
            min-height: 580px;
        }
        
        textarea
        {
            font-size: 12px;
            font-weight: normal;
        }
        
        .FaultClass
        {
            background-color: Red;
        }
        
        .inputbackimg
        {
            background: url(../../pic/menu1.png) no-repeat right;
        }
    </style>
    <script type="text/javascript" src="../../JavaScript/json2.js"></script>
    <script type="text/javascript" src="../../JavaScript/ArrayPrototype.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script type="text/javascript" src="../../JavaScript/workflowchart.js"></script>
    <script type="text/javascript" src="../../JavaScript/customflowchart_rework.js"></script>
    <script type="text/javascript" src="../../JavaScript/Tools.js"></script>
    <script type="text/javascript" src="../../JavaScript/Validate.js"></script>
    <script type="text/javascript" src="../../JavaScript/ValidateNoMustInput.js"></script>
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
            $('.pro_1').css('margin-left', '0px');
            $('.pro_1').css('margin-top', '0px');
            $('.nav_3 > p').css('margin-top', '-10px');
            $('.nav_3 > p').css('margin-right', '19px');
            $('#UCProcessAction1_divComments').css('display', 'none');

            //$('#CDF1_AppReason').smokescreen({ smoke: '请在此处填写您的申请原因并尽可能详细描述您的需求' });
            if ($('#CDF1_AppReason').val() == '')
                $('#CDF1_AppReason').smokescreen({ smoke: '请在此处填写您的申请原因并尽可能详细描述您的需求' });
            else {
                var tmp = $('#CDF1_AppReason').val();
                $('#CDF1_AppReason').smokescreen({ smoke: '请在此处填写您的申请原因并尽可能详细描述您的需求' });
                $('#CDF1_AppReason').val(tmp);
                $('#CDF1_AppReason').focus();
            }


            $('#CDF1_Upload1_gvAttachList').attr('style', 'margin-left:5px; width: 540px; border-top-color: #f2dd81; border-right-color: #f2dd81; border-bottom-color: #f2dd81; border-left-color: #f2dd81; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; float: left; border-collapse: collapse;');
            $("#CDF1_Upload1_gvAttachList td span").each(function () {
                //if($(this).css('aspNetDisabled'))
                $(this).attr('style', '');
            });
        });
    </script>
    <script type="text/javascript">
        //页面检查
        function PageCheck() {
            if ($('#CDF1_AppReason').val() == '') {
                alert('请填写申请原因');
                return false;
            }
            if ($('#CDF1_AppExplain').val() == '') {
                alert('请填写申请说明');
                return false;
            }
            if ($('#CDF1_hfNeedVerification').val() == 'true') {
                //var models = $('#CDF1_BSCategory > option').length;     //取得模板个数
                var models = $('#CDF1_hfTemplateCounts').val();     //取得模板个数
                //if (models != 0 && $('#CDF1_Upload1_gvAttachList_lbtnName_0').length == 0)
                if (models != 0 && $('#CDF1_Upload1_hfAttachmentCounts').val() == 0) {
                    alert('请上传附件');
                    return false;
                }
                //if (models != 0 && $('#CDF1_Upload1_gvAttachList_lbtnName_' + (models - 1)).length == 0)
                if ($('#CDF1_Upload1_hfAttachmentCounts').val() < models) {
                    alert('附件个数少于模板个数');
                    return false;
                }
            }
            //检查流程
            if (checkcustomflowchart() == 'ok') {
                var employeeCode = document.getElementById('CDF1_hfEmployeeCode').value;
                var operator = '<%=EmployeeCode%>';
                if (employeeCode != operator) {
                    alert('由于您是代他人申请，流程会先送到申请人确认');
                }
                return true;
            }
            else {
                alert(checkcustomflowchart());        //提示错误信息
            }
            return false;
        }

        function CancelProcess() {
            top.window.ymPrompt.confirmInfo('您确定要取消该申请单吗？', null, null, null, ConFirm);
            document.getElementById('UCProcessAction1_btnCancel').click();
            return false;
        }

        function ConFirm(tp) {
            if (tp == 'ok') {
                //document.getElementById('UCProcessAction1_btnCancel').click();
                __doPostBack('UCProcessAction1_btnCancel', '');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CDF ID="CDF1" runat="server" />
        <uc3:ProcessLog ID="ProcessLog1" runat="server" />
        <div class="conter_right_list_nav" style="margin-left: 15px;">
            <span style="width: 540px;">流程配置</span>
            <table style="width: 540px; margin-left: 25px;">
                <tr>
                    <td align="right">
                        <img id="Imgcontrol" alt="配置流程" runat="server" style="cursor: pointer; float: right; margin-top: 5px;"
                            src="../../pic/btnImg/button_chart_nor.png" onclick=""/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <table style="width: 640px; padding-top: 10px;">
            <tr>
                <td align="center">
                    <asp:ImageButton ID="btnApprove" runat="server" 
                        ImageUrl="~/Pic/btnImg/btnSubmit_nor.png" onclick="btnApprove_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="btnReject" runat="server" 
                        ImageUrl="~/Pic/btnImg/12.png" onclick="btnReject_Click" 
                        />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

