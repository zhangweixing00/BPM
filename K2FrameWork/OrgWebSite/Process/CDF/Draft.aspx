<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Draft.aspx.cs" Inherits="OrgWebSite.Process.CDF.Draft" %>

<%@ Register Src="UC/CDF.ascx" TagName="CDF" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UCProcessAction.ascx" TagName="UCProcessAction" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>草稿</title>
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
    <script charset="utf-8" src="../../Javascript/kindeditor-min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../../Javascript/lang/zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var editor1 = KindEditor.create('#CDF1_AppReason')
        });
    </script>
    <script type="text/javascript">
        var chart = new window.WFChart('dvFlowchart');       //设置一个全局变量

        function InitWFChart(Id) {
            if ($('#' + Id).val() != '') {
                //反序列化，并生成WFChart类
                var arraynodes = eval($('#' + Id).val());
                chart.Clear();
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
                    node.nodeState = 2;
                    node.isView = false;
                    node.ftitle = arraynodes[i].ftitle;
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
                        csnode.enabled = arraynodes[i].arrayCounter[j].enabled;
                        node.arrayCounter.InsertAt(csnode, 0);
                    }
                    chart.InsertAt(node, 0);        //恢复对象
                }
            }
            else {
                //默认设置一个节点
                AddNode(-1, 'User');      //添加一个节点
            }
        }

        $(document).ready(function () {
            InitWFChart('CDF1_hfjqFlowChart');
            var roleNameArray = document.getElementById('CDF1_hfRoleName').value.split(';');
            var roleArray = document.getElementById('CDF1_hfRoleCode').value.split(';');

            //查找已存在的Solid结点
            var pos = 0;
            var solidPos = 1;       //记录已存在的固定节点位置
            var solidLength = 0;    //记录已存在的固定节点长度
            while (chart.getNode(pos)) {
                var node = chart.getNode(pos);
                if (node.nodeType == 'Solid') {
                    solidPos = pos;
                    solidLength++;
                }
                pos++;
            }

            //删除相应已存在的固定节点
            if (solidLength != 0) {
                var tmpos = solidPos;
                for (var i = 0; i < solidLength; i++) {
                    chart.RemoveAt(tmpos--);
                }
            }
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
            //添加相应节点
            var solidTmpos = 0;
            if (solidLength == 0)
                solidTmpos = 1;
            else
                solidTmpos = solidPos - solidLength + 1;

            //判断是否存在其他节点
            pos = 0;
            var notSolidPos = 0;
            var notSolitLength = pos;
            while (chart.getNode(pos)) {
                var node = chart.getNode(pos);
                if (node.nodeType != 'Solid') {
                    notSolidPos = pos;
                    notSolitLength++;
                }
                pos++;
            }

            if (notSolidPos > solidTmpos)
                solidTmpos = notSolidPos + 1;
            for (var i = 0; i < roleArray.length; i++) {
                if (roleArray[i] != '') {
                    AddNode(solidTmpos, 'Solid');
                    chart.getNode(solidTmpos).setNodeValue('固定节点', roleArray[i], roleNameArray[i], roleArray[i], 'Solid', roleNameArray[i], roleArray[i]);
                    solidTmpos++;
                }
            }
            chart.paint();
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
        });


        //添加一个节点
        var AddNode = function (pos, nodeType) {
            var node = window.WFNode(nodeType);
            chart.InsertAt(node, pos + 1);        //将节点插入到绘制队列中（加在当前节点后边）
            chart.paint();                  //绘制流程图
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
        };


        //删除一个节点
        var DelNode = function (index) {
            //            if (index == 0 && chart.getNode(1) == null)
            //            {
            //                //提示不能删除
            //                alert('您不能删除此节点');
            //            }
            //            else
            //            {
            //                ymPrompt.confirmInfo({ message: '是否确认删除此节点？', handler: function (p)
            //                {
            //                    if (p == 'ok')
            //                    {
            //                        chart.RemoveAt(index);
            //                        chart.paint();
            //                        chart.Objserialize(chart, 'CDF1_hfjqFlowChart');
            //                    }
            //                }
            //                });
            //            }
            if (index == 0 && (chart.getNode(1) == null || chart.getNode(1).nodeType == 'Solid' || chart.getNode(1).nodeType == 'Role')) {
                //提示不能删除
                alert('您不能删除此节点');
            }
            else {
                ymPrompt.confirmInfo({ message: '是否确认删除此节点？', handler: function (p) {
                    if (p == 'ok') {
                        chart.RemoveAt(index);
                        chart.paint();
                        chart.Objserialize(chart, 'CDF1_hfjqFlowChart');
                    }
                }
                });
            }
        };

        //设置某个选择
        var setCheck = function (pos) {
            chart.setCheckBox(pos);
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
        };

        //在节点中编辑人员信息
        var SelectU = function (pos) {
            var b_class_guid = document.getElementById('CDF1_BBCategory');    //业务大类
            var b_sub_class_guid = document.getElementById('CDF1_BSCategory');    //业务小类
            var para = "?checkstyle=true&param = " + getDropdownlistValue(b_class_guid) + ";" + getDropdownlistValue(b_sub_class_guid) + "&pos=" + pos + "&flag=true";
            ymPrompt.win({ message: "../../Search/K2FlowCheck/K2FlowCheck.aspx" + para, handler: callBackOperator, width: 760, height: 500, title: '选择人员', iframe: true });
        };

        function callBackOperator(returnVal) {
            if (returnVal != "close") {
                if (returnVal && returnVal.length != 0) {
                    var name = returnVal[0].split(';')[0];
                    var code = returnVal[0].split(';')[1];
                    var deptname = returnVal[0].split(';')[3];
                    var deptcode = returnVal[0].split(';')[4];
                    chart.getNode(returnVal[0].split(';')[12]).setNodeValue(name, code, deptname, deptcode, 'User', '', '');
                    chart.paint();
                    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                }
            }
            ymPrompt.close();
        }

        /*选择角色回调函数*/
        function callBackRole(returnVal) {
            if (returnVal != "close") {
                if (returnVal && returnVal.length != 0) {
                    var name = '角色';
                    var code = returnVal.split(';')[0];
                    var deptname = returnVal.split(';')[1];
                    var deptcode = '';
                    chart.getNode(returnVal.split(';')[2]).setNodeValue(name, code, deptname, deptcode, 'Role', returnVal.split(';')[1], returnVal.split(';')[0]);
                    chart.paint();
                    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
                }
            }
            ymPrompt.close();
        }

        function deleteUser(rowIndex, pos) {
            chart.getNode(pos).deleteNodeValue(rowIndex);
            chart.paint();
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
        }

        //检查流程是否配置完整
        function checkcustomflowchart() {
            var pos = 0;
            while (chart.getNode(pos)) {
                var node = chart.getNode(pos++);
                if (node.usercode == '') {
                    return '流程图中存在未选审批人的节点';
                }
            }
            return 'ok';
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
            }, width: 760, height: 530, title: '选择人员', iframe: true
            });
        }
    </script>
    <script type="text/javascript" src="../../JavaScript/Tools.js"></script>
    <script type="text/javascript" src="../../JavaScript/Validate.js"></script>
    <script type="text/javascript" src="../../JavaScript/ValidateNoMustInput.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.pro_1').css('margin-left', '0px');
            $('.pro_1').css('margin-top', '0px');
            $('.nav_3 > p').css('margin-top', '-10px');
            $('.nav_3 > p').css('margin-right', '19px');
        });
    </script>
    <script type="text/javascript">
        //页面检查
        function PageCheck() {
            if ($('#CDF1_AppReason').val() == '') {
                alert('请填写申请原因');
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
            if (DisplayDirectFlow()) {
                //检查流程
                if (checkcustomflowchart() == 'ok') {
                    return true;
                }
                else {
                    alert(checkcustomflowchart());        //提示错误信息
                }
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CDF ID="CDF1" runat="server" />
        <div class="conter_right_list_nav" style="margin-left: 20px;">
            <span>流程信息</span>
            <ul style="margin-top: 0px; width: 540px;">
                <li></li>
            </ul>
            <table id="tblChart" class="datalist2" style="width: 540px; border: 1px solid #F2DD81;
                margin-left: 20px; font-weight: normal;">
                <tr>
                    <td style="background: #fef5c7; font-weight: bold;">
                        序号
                    </td>
                    <td style="background: #fef5c7; font-weight: bold;">
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
        <uc2:UCProcessAction ID="UCProcessAction1" CommentsVisible="false" runat="server"
            CloseVisible="false" SaveVisible="true" btnSubmitDraftCFVisible="true" Abandon="true" />
    </div>
    </form>
</body>
</html>
