<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="OrgWebSite.Process.CDF.Approve" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UCProcessAction.ascx" TagName="UCProcessAction" TagPrefix="uc3" %>
<%@ Register src="../Controls/ProcessLog.ascx" tagname="ProcessLog" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>审批</title>
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
    <script type="text/javascript" src="../../JavaScript/customeflowchart_approve.js"></script>
    <script type="text/javascript" src="../../JavaScript/Tools.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery.blockui.js"></script>
    <script charset="utf-8" src="../../Javascript/kindeditor-min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../Javascript/lang/zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //            var editor1 = KindEditor.create('#CDF1_AppReason');
            //            editor1.toolbar.hide();     //隐藏工具栏
            //            debugger;
            //            editor1.resizeType = 0;     //不能拖动高度宽度
            var editor;
            KindEditor.ready(function (K) {
                var id = 'CDF1_AppReason';
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
            var addButton = document.getElementById('addTableRow');             //加签按钮
            var counterButton = document.getElementById('addTableRowConter');   //会签按钮
            if (addButton && counterButton) {
                if ($('#hfIsMeet').val() == 'false') {//加签
                    //隐藏会签按钮
                    $(addButton).attr('style', '');
                    $(counterButton).attr('style', 'display:none');
                }
                else if ($('#hfIsMeet').val() == 'true') {
                    $(addButton).attr('style', 'display:none');
                    $(counterButton).attr('style', '');
                }
            }

//            $('#CDF1_Upload1_gvAttachList').attr('style', 'margin-left:5px; width: 540px; border-top-color: #f2dd81; border-right-color: #f2dd81; border-bottom-color: #f2dd81; border-left-color: #f2dd81; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; float: left; border-collapse: collapse;');
//            $("#CDF1_Upload1_gvAttachList td span").each(function () {
//                //if($(this).css('aspNetDisabled'))
//                $(this).attr('style', '');
//            });
        });


        function callBacks(rel) {
            if (rel != "close") {
                if (rel == null)
                    return false;
                else {
                    if (rel == 'Node')
                        selectAddNodes();
                    else if (rel == 'Counter')
                        selectCounters();
                }
            }
        }

        //显示选择
        function DisplaySelectFunction() {
            var str = '';
            var root = getRootPath();
            ymPrompt.win({ message: root + "/CDF/ChooseType.aspx", handler: callBacks, width: 450, height: 200, title: '选择加签类型', iframe: true });
        }

        function getRootPath() {
            var strFullPath = window.document.location.href;
            var strPath = window.document.location.pathname;
            var pos = strFullPath.indexOf(strPath);
            var prePath = strFullPath.substring(0, pos);
            var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
            return (prePath + postPath);
        }

        //查看流程图
        function ReViewFlow() {
            var snObj = document.getElementById('CDF1_hfProcessSN');
            if (snObj) {
                var root = getRootPath();
                ymPrompt.win({ message: root + "/CDF/ReView.aspx?SN=" + snObj.value, handler: callBackViewFlow, width: 830, height: 470, title: '流程图状态', iframe: true });
            }
        }

        function callBackViewFlow(returnVal) {
            ymPrompt.close();
        }

        function Endorsement() {
            $('hfIsMeet').val('false');
            $('#tblChart').slideDown('slow');
            selectAddNodes();
        }

        function CounterSign() {
            $('hfIsMeet').val('true');
            $('#tblChart').slideDown('slow');
            selectCounters();
        }

        //选择是会签还是加签
        function CounterOrNodeControl(wfpos) {
            if ($('#hfIsMeet').val() == 'true') //是会签
            {
                selectCounters();
            }
            else {
                selectAddNodes();
            }
        }

        //退回检查是否输入了审批意见
        function RejectCheck() {
            var retVal = false;

            if (document.getElementById('UCProcessAction1_txtComments').value == '') {
                top.window.ymPrompt.alert({ title: '提示信息', message: '请填写审批意见' });
                return retVal;
            }

            top.window.ymPrompt.confirmInfo('您确定要拒绝该申请单吗？', null, null, null, function ConFirm(tp) { tp == 'ok' ? rejectHandler() : retVal = false });
            return retVal;
        }

        function rejectHandler() {
            document.getElementById('UCProcessAction1_btnRejectCF').click();
        }
        //选择会签人
        function selectCounters() {
            //跳出选人框
            SelectUCounter(getCurrentPos());
        }

        //选择加签人
        function selectAddNodes() {
            //跳出选人框
            SelectUNodes(getCurrentPos());
        }

        //在节点中编辑人员信息（会签）
        var SelectUCounter = function (pos) {
            var b_class_guid = document.getElementById('CDF1_BBCategory');    //业务大类
            var b_sub_class_guid = document.getElementById('CDF1_BSCategory');    //业务小类
            var para = "?checkstyle=false&param = " + getDropdownlistValue(b_class_guid) + ";" + getDropdownlistValue(b_sub_class_guid) + "&pos=" + pos;
            ymPrompt.win({ message: "../Search/K2EmployeeCheck/K2EmployeeCheck.aspx" + para, handler: callbackOutUser, width: 760, height: 530, title: '选择人员', iframe: true });
        };

        function callbackOutUser(returnVal) {
            if (returnVal != "close") {
                if (returnVal && returnVal.length != 0) {
                    //设置按钮
                    $('#UCProcessAction1_btnApprove').css('display', 'none');
                    $('#UCProcessAction1_btnReject').css('display', 'none');
                    $('#UCProcessAction1_btnApproveSave').css('display', 'none');
                    $('#UCProcessAction1_btnViewFlow').css('display', 'none');
                    $('#UCProcessAction1_btnStartCounter').css('display', 'inline');

                    //设置会签标识
                    $('#hfIsMeet').val('true');

                    var isRep = false;      //标示是否重复

                    for (var i = 0; i < returnVal.length; i++) {
                        var name = returnVal[i].split(';')[0];
                        var code = returnVal[i].split(';')[1];
                        var deptname = returnVal[i].split(';')[3];
                        var deptcode = returnVal[i].split(';')[4];
                        var csnode = new CSNode(chart.getCurrentIndex());
                        csnode.enabled = true;
                        csnode.counterSignNames = name;
                        csnode.counterSignCodes = code;
                        csnode.counterDeptName = deptname;
                        csnode.counterDeptCode = deptcode;
                        var tmpnode = chart.getNode(chart.getCurrentIndex());
                        if (!isRep) {
                            isRep = tmpnode.setCounterArray(csnode, tmpnode.getCounterArrayLength());  //添加会签
                        }
                        else {
                            tmpnode.setCounterArray(csnode, tmpnode.getCounterArrayLength());  //添加会签
                        }
                    }
                    //                    if (isRep)
                    //                    {
                    //                        ymPrompt.alert({title:'提示',message:'您添加的人员有重复！'});
                    //                    }
                    document.getElementById('CDF1_hfNoCounter').value = 'false';

                    chart.paint();
                    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                    //DisplayDirectFlow();
                    ControlDisplay('counter');
                }
            }
            ymPrompt.close();
        }

        //在节点中编辑人员信息（加签）
        var SelectUNodes = function (pos) {
            var b_class_guid = document.getElementById('CDF1_BBCategory');    //业务大类
            var b_sub_class_guid = document.getElementById('CDF1_BSCategory');    //业务小类
            var para = "?checkstyle=false&param = " + getDropdownlistValue(b_class_guid) + ";" + getDropdownlistValue(b_sub_class_guid) + "&pos=" + pos;
            ymPrompt.win({ message: "../Search/K2FlowCheck/K2FlowCheck.aspx" + para, handler: callbackNodeUser, width: 760, height: 530, title: '选择人员', iframe: true });
        };

        function callbackNodeUser(returnVal) {
            if (returnVal != "close") {
                if (returnVal && returnVal.length != 0) {
                    $('#UCProcessAction1_btnViewFlow').css('display', 'none');

                    //设置加签标识
                    $('#hfIsMeet').val('false');
                    debugger;
                    var currentPos = chart.getCurrentIndex();       //取得当前节点
                    for (var i = 0; i < returnVal.length; i++) {
                        if (returnVal[i].split(';')[returnVal[i].split(';').length - 1] == 'User') {
                            var name = returnVal[i].split(';')[0];
                            var code = returnVal[i].split(';')[1];
                            var deptname = returnVal[i].split(';')[3];
                            var deptcode = returnVal[i].split(';')[4];

                            //添加一个节点到当前节点的后边
                            AddNode(currentPos++, 'User');
                            chart.getNode(currentPos).setNodeValue(name, code, deptname, deptcode, 'User', '', '');
                            chart.paint();
                            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                        }
                        else if (returnVal[i].split(';')[returnVal[i].split(';').length - 1] == 'Role') {
                            var name = '角色';
                            var code = returnVal[i].split(';')[0];
                            var deptname = returnVal[i].split(';')[1];
                            var deptcode = '';
                            AddNode(currentPos++, 'Role');
                            chart.getNode(currentPos).setNodeValue(name, code, deptname, deptcode, 'Role', returnVal[i].split(';')[1], returnVal[i].split(';')[0]);
                            chart.paint();
                            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                        }
                    }
                    //DisplayDirectFlow();        //显示流程图页面
                    ControlDisplay('add');               //控制按钮显示隐藏
                }
            }
            ymPrompt.close();
        }



        //选人并添加节点
        function SelectUAdd(pos) {
            var b_class_guid = document.getElementById('CDF1_BBCategory');    //业务大类
            var b_sub_class_guid = document.getElementById('CDF1_BSCategory');    //业务小类
            var para = "?checkstyle=false&param = " + getDropdownlistValue(b_class_guid) + ";" + getDropdownlistValue(b_sub_class_guid) + "&pos=" + pos + "&flag=true";
            ymPrompt.win({ message: "../Search/K2FlowCheck/K2FlowCheck.aspx" + para, handler: function (returnVal) {
                var tpos = pos;
                if (returnVal != "close") {
                    if (returnVal && returnVal.length != 0) {
                        for (var i = 0; i < returnVal.length; i++) {
                            debugger;
                            if (returnVal[i].split(';')[returnVal[i].split(';').length - 1] == 'User') {
                                var name = returnVal[i].split(';')[0];
                                var code = returnVal[i].split(';')[1];
                                var deptname = returnVal[i].split(';')[3];
                                var deptcode = returnVal[i].split(';')[4];

                                //添加一个节点到当前节点的后边
                                AddNode(tpos++, 'User');
                                chart.getNode(tpos).setNodeValue(name, code, deptname, deptcode, 'User', '', '');
                                chart.paint();
                                chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                            }
                            else if (returnVal[i].split(';')[returnVal[i].split(';').length - 1] == 'Role') {
                                var name = '角色';
                                var code = returnVal[i].split(';')[0];
                                var deptname = returnVal[i].split(';')[1];
                                var deptcode = '';
                                AddNode(tpos++, 'Role');
                                chart.getNode(tpos).setNodeValue(name, code, deptname, deptcode, 'Role', returnVal[i].split(';')[1], returnVal[i].split(';')[0]);
                                chart.paint();
                                chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                            }
                        }
                    }
                }
                ymPrompt.close();
                ControlDisplay('add');               //控制按钮显示隐藏
            }, width: 760, height: 530, title: '选择人员', iframe: true
            });
        }

        function PageCheck() {
            var retVal = checkcustomflowchart();
            if (retVal == 'ok')
                return true;
            else {
                top.window.ymPrompt.alert({ title: '提示信息', message: retVal });
                return false;
            }
        }

        //控制按钮
        function ControlDisplay(type) {
            var addButton = document.getElementById('addTableRow');             //加签按钮
            var counterButton = document.getElementById('addTableRowConter');   //会签按钮
            if (addButton && counterButton) {
                if (type == 'add') {//加签
                    //隐藏会签按钮
                    $(addButton).attr('style', '');
                    document.getElementById('UCProcessAction1_btnCDFEndorsement').setAttribute('onclick', 'Endorsement();return false;');
                    $(counterButton).attr('style', 'display:none');
                    document.getElementById('UCProcessAction1_btnCDFCountersign').setAttribute('onclick', 'return false;');
                }
                else if (type == 'counter') {
                    $(addButton).attr('style', 'display:none');
                    document.getElementById('UCProcessAction1_btnCDFEndorsement').setAttribute('onclick', 'return false;');
                    $(counterButton).attr('style', '');
                    document.getElementById('UCProcessAction1_btnCDFCountersign').setAttribute('onclick', 'CounterSign();return false;');
                }

                //判断是否还有加签、会签
                var pos = 0;
                var flag = false;    //标记是否存在加签、会签
                var currentNode;        //保存当前节点
                while (chart.getNode(pos)) {
                    //判断加签
                    var node = chart.getNode(pos++);
                    if (node.nodeState == 1)
                        currentNode = node;
                    if (node.nodeState == 2) {  //存在加签
                        flag = true;
                        break;
                    }
                }
                if (!flag) {
                    //再判断是否有会签
                    if (currentNode) {
                        if (currentNode.arrayCounter.length == 0) {
                            $(addButton).attr('style', '');
                            $(counterButton).attr('style', '');
                            document.getElementById('UCProcessAction1_btnCDFCountersign').setAttribute('onclick', 'CounterSign();return false;');
                            document.getElementById('UCProcessAction1_btnCDFEndorsement').setAttribute('onclick', 'Endorsement();return false;');
                        }
                        var csnodeState = true;     //是否还存在未确认的会签节点
                        for (var k = 0; k < currentNode.arrayCounter.length; k++) {
                            if (currentNode.arrayCounter[k].enabled) {
                                csnodeState = false;
                                break;
                            }
                        }
                        if (csnodeState) {
                            $(addButton).attr('style', '');
                            $(counterButton).attr('style', '');
                            document.getElementById('UCProcessAction1_btnCDFCountersign').setAttribute('onclick', 'CounterSign();return false;');
                            document.getElementById('UCProcessAction1_btnCDFEndorsement').setAttribute('onclick', 'Endorsement();return false;');
                        }
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CDF ID="CDF1" runat="server" />
        <uc2:ProcessLog ID="ProcessLog1" runat="server"></uc2:ProcessLog>
        <div class="conter_right_list_nav" style="margin-left: 15px;">
            <span style="width: 540px;">流程配置</span>
            <table style="width: 540px; margin-left: 25px;">
                <tr>
                    <td align="right">
                        <img id="Imgcontrol" alt="配置流程" style="cursor: pointer; float: right; margin-top: 5px;"
                            src="../../pic/btnImg/button_chart_nor.png" onclick="$('#tblChart').slideToggle('slow');"
                            onmouseover="SaveMouseover(this.id ,'../../pic/btnImg/button_chart_over.png')"
                            onmouseout="SaveMouseout(this.id ,'../../pic/btnImg/button_chart_nor.png')" />                        
                    </td>
                    <td style="width:80px;">
                        <img id="ImgViewFlow" alt="查看流程图" style="cursor: pointer; float: right; margin-top: 5px;"
                            src="../../pic/btnImg/chakanliuchengtu_nor.png" onclick="ReViewFlow();"
                            onmouseover="SaveMouseover(this.id ,'../../pic/btnImg/chakanliuchengtu_over.png')"
                            onmouseout="SaveMouseout(this.id ,'../../pic/btnImg/chakanliuchengtu_nor.png')" />
                    </td>
                </tr>
            </table>
            <table id="tblChart" class="datalist2" style="width: 540px; border: 1px solid #F2DD81;
                margin-left: 25px; font-weight: normal; display: none;">
                <tr>
                    <td style="background: #fef5c7; font-weight: bold;">
                        序号
                    </td>
                    <td style="background: #fef5c7; font-weight: bold; width: 160px;">
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
        <uc3:UCProcessAction ID="UCProcessAction1" runat="server" ApproveVisible="true" RejectVisible="true"
            ApproveSaveVisible="false" CloseVisible="false" StartCounterVisible="true" btnReViewFlowVisible="false"
            RejectCFVisible="true" CDFApproveBackVisible="false" CDFEndorsementVisible="false" CDFCounterSignVisible="false" Abandon="true" />
    </div>
<%--    <div id="selDiv" style="display: none;">
        <div style="width: 300px; height: 300px;">
            <input id="btnCounter" type="button" value="会签" onclick="ymPrompt.close();selectCounters()" />
            <input id="btnNode" type="button" value="加签" onclick="ymPrompt.close();selectAddNodes()" />
        </div>
    </div>--%>
    <asp:HiddenField ID="hfIsMeet" runat="server" Value="" />
    </form>
</body>
</html>