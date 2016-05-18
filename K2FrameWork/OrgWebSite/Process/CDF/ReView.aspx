<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReView.aspx.cs" Inherits="OrgWebSite.Process.CDF.ReView" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
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
    <script type="text/javascript" >
        var chartview = new window.WFChart('dvFlowchart');       //设置一个全局变量

        function InitWFChart(Id) {
            if ($('#' + Id).val() != '') {
                //反序列化，并生成WFChart类
                var arraynodes = eval($('#' + Id).val());
                chartview.Clear();
                for (var i = 0; i < arraynodes.length; i++) {
                    //创建每个节点
                    var node = window.WFNode();
                    node.usercode = arraynodes[i].usercode;
                    node.username = arraynodes[i].username;
                    node.deptcode = arraynodes[i].deptcode;
                    node.deptname = arraynodes[i].deptname;
                    node.isChekced = arraynodes[i].isChekced;
                    node.isCounterSign = arraynodes[i].isCounterSign;
                    node.nodeGuid = arraynodes[i].nodeGuid;
                    node.nodeState = arraynodes[i].nodeState;
                    node.ftitle = arraynodes[i].ftitle;
                    node.isOpen = arraynodes[i].isOpen;
                    node.isView = true;
                    node.nodeType = arraynodes[i].nodeType;
                    node.roleCode = arraynodes[i].roleCode;
                    node.roleName = arraynodes[i].roleName;
                    for (var j = 0; j < arraynodes[i].arrayCounter.length; j++) {
                        var csnode = window.CSNode();
                        csnode.csnodeGuid = arraynodes[i].arrayCounter[j].csnodeGuid;
                        csnode.isChecked = arraynodes[i].arrayCounter[j].isChecked;
                        csnode.csState = arraynodes[i].arrayCounter[j].csState;
                        csnode.csTitle = arraynodes[i].arrayCounter[j].csTitle;
                        csnode.counterSignCodes = arraynodes[i].arrayCounter[j].counterSignCodes;
                        csnode.counterSignNames = arraynodes[i].arrayCounter[j].counterSignNames;
                        csnode.counterDeptName = arraynodes[i].arrayCounter[j].counterDeptName;
                        csnode.counterDeptCode = arraynodes[i].arrayCounter[j].counterDeptCode;
                        csnode.enabled = arraynodes[i].arrayCounter[j].enabled;
                        node.arrayCounter.InsertAt(csnode, 0);
                    }
                    chartview.InsertAt(node, 0);        //恢复对象
                }
            }
        }

        function setElementStatus(currentObj, wfpos) {
            if (chartview.getNode(wfpos).isOpen) {
                $(currentObj).attr('src', '../../pic/icon_arrowup.png');
                for (var i = 0; i < $(currentObj).parent().parent().parent().parent().parent().next().children().children().length; i++) {
                    $($(currentObj).parent().parent().parent().parent().parent().next().children().children()[i]).css('display', 'none');
                }
                chartview.setOpen(wfpos, false);
                //chartview.paint();                  //绘制流程图
            }
            else if (!chartview.getNode(wfpos).isOpen) {
                $(currentObj).attr('src', '../../pic/icon_arrowdown.png');
                for (var i = 0; i < $(currentObj).parent().parent().parent().parent().parent().next().children().children().length; i++) {
                    $($(currentObj).parent().parent().parent().parent().parent().next().children().children()[i]).css('display', 'inline');
                }
                chartview.setOpen(wfpos, true);
                //chartview.paint();                  //绘制流程图
            }
        }

        $(document).ready(function () {
            InitWFChart('hfjqFlowChart');
            chartview.paint();
            chartview.Objserialize(chartview, 'hfjqFlowChart');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="width:820px;">
    <div style="width:820px;">
        <div id="dvFlowChart_OUT" style="margin: 0 auto;">
            <table cellspacing="0" cellpadding="0" style="width: 820px;">
                <tr style="height: 20px;">
                    <td colspan="3" width="20">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/kaishi.png" alt="开始" style="padding-right: 30px;" />
                    </td>
                    <td rowspan="4" valign="top" style="width:200px;">
                        图例
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/arrow.png" alt="" style="padding-right: 30px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <div id="dvFlowchart" style="width: 100%; height: 100%; margin: 0 auto; height: auto!important;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/jieshu.png" alt="结束" style="padding-right: 30px;" />
                    </td>
                </tr>
                <tr class="bottom">
                    <td colspan="3">
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfjqFlowChart" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
