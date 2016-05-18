<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="OrgWebSite.Process.CDF.View" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UCProcessAction.ascx" TagName="UCProcessAction" TagPrefix="uc2" %>
<%@ Register src="../Controls/ProcessLog.ascx" tagname="ProcessLog" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>查看</title>
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
    <script type="text/javascript" src="../../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script type="text/javascript" src="../../JavaScript/workflowchart.js"></script>
    <script type="text/javascript" src="../../JavaScript/customeflowchart_view.js"></script>
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
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CDF ID="CDF1" runat="server" />
        <uc3:ProcessLog ID="ProcessLog1" runat="server" />
        <%--<uc2:UCProcessAction ID="UCProcessAction1" runat="server" CDFViewBackVisible="true" CommentsVisible="false" />--%>
    </div>
        <div style="text-align: center; padding-bottom: 20px; padding-top:20px; float:left; width:80%;">
            <img id="imgClose" src='/pic/btnImg/btnBack_nor.png' alt="关闭页面" onmouseover="this.src='/pic/btnImg/btnBack_over.png'"
                onmouseout="this.src='/pic/btnImg/btnBack_nor.png'" onclick="window.history.go(-1);window.close();" />
        </div>
    </form>
</body>
</html>
