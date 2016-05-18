<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XApprove.aspx.cs" Inherits="OrgWebSite.Process.CDF.XApprove" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register Src="../Controls/ProcessLog.ascx" TagName="ProcessLog" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>审批</title>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/juqery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/ui.jqgrid.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" src="../../JavaScript/workflowchart.js"></script>
    <script type="text/javascript" src="../../JavaScript/customeflowchart_view.js"></script>
    <script type="text/javascript" src="../../JavaScript/Tools.js"></script>
    <script charset="utf-8" src="../../Javascript/kindeditor-min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../Javascript/lang/zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var editor;
            KindEditor.ready(function (K) {
                editor = K.create('#CDF1_AppReason', {
                    resizeType: 0,
                    readonlyMode: true
                });
                editor.toolbar.hide();
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.pro_1').css('margin-left', '0px');
            $('.pro_1').css('margin-top', '0px');
            $('.nav_3 > p').css('margin-top', '-10px');
            $('.nav_3 > p').css('margin-right', '19px');
            $('.conter_right_list_input_bg').removeClass('conter_right_list_input_bg').addClass('conter_right_list_input_bg_border');
            $('#reason_read').css('display', 'none');
            document.getElementById('CDF1_hfIsForm').value = 'Form';

            $('#CDF1_Upload1_gvAttachList').attr('style', 'margin-left:5px; width: 540px; border-top-color: #f2dd81; border-right-color: #f2dd81; border-bottom-color: #f2dd81; border-left-color: #f2dd81; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; float: left; border-collapse: collapse;');
            $("#CDF1_Upload1_gvAttachList td span").each(function () {
                //if($(this).css('aspNetDisabled'))
                $(this).attr('style', '');
            });
        });
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
        <div class="conter_right_list_main">
            <div class="conter_right_list_nav" id="divComments" style="height: 26px;" runat="server">
                <span>审批意见</span>
            </div>
            <asp:Panel ID="plComments" runat="server" Width="650px">
                <div class="ItemTitle" style="float: left">
                    <span id="span1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                <div style="float: left; width: 600px; padding-left: 28px; padding-top: 10px;">
                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="537px" Rows="4"></asp:TextBox></div>
            </asp:Panel>
        </div>
        <table style="width: 640px; padding-top: 10px;">
            <tr>
                <td align="center">
                    <asp:ImageButton ID="btnApprove" runat="server" 
                        ImageUrl="~/Pic/btnImg/btnAgree_nor.png" onclick="btnApprove_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="btnReject" runat="server" 
                        ImageUrl="~/Pic/btnImg/btnRefuse_nor.png" onclick="btnReject_Click" 
                        />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
