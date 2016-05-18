var chart = new window.WFChart('dvFlowchart');       //设置一个全局变量

function InitWFChart(Id)
{
    if ($('#' + Id).val() != '')
    {
        //反序列化，并生成WFChart类
        var arraynodes = eval($('#' + Id).val());
        chart.Clear();
        for (var i = 0; i < arraynodes.length; i++)
        {
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
            node.nodeType = arraynodes[i].nodeType;
            node.roleCode = arraynodes[i].roleCode;
            node.roleName = arraynodes[i].roleName;
            node.isOpen = arraynodes[i].isOpen;
            node.nodeId = arraynodes[i].nodeId;
            node.URL = arraynodes[i].URL;
            node.isView = false;
            for (var j = 0; j < arraynodes[i].arrayCounter.length; j++)
            {
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
            chart.InsertAt(node, 0);        //恢复对象
        }
    }
}

$(document).ready(function ()
{
    InitWFChart('CDF1_hfjqFlowChart');
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
var DelNode = function (index)
{
    if (index == 0 && chart.getNode(1) == null)
    {
        //提示不能删除
        alert('您不能删除此节点');
    }
    else
    {
        ymPrompt.confirmInfo({ message: '是否确认删除此节点？', handler: function (p)
        {
            if (p == 'ok')
            {
                chart.RemoveAt(index);
                chart.paint();
                chart.Objserialize(chart, 'CDF1_hfjqFlowChart');
                ControlDisplay('add');               //控制按钮显示隐藏
            }
        }
        });
    }
};

//设置某个选择
var setCheck = function (pos)
{
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

//设置会签（cspos:表示是第几个节点的会签）
var setMeet = function (cspos)
{
    var b_class_guid = document.getElementById('CDF1_BBCategory');    //业务大类
    var b_sub_class_guid = document.getElementById('CDF1_BSCategory');    //业务小类
    var para = "?checkstyle=false&param = " + getDropdownlistValue(b_class_guid) + ";" + getDropdownlistValue(b_sub_class_guid) + "&pos=" + cspos;
    ymPrompt.win({ message: "../Search/K2EmployeeCheck/K2EmployeeCheck.aspx" + para, handler: callBackMeet, width: 760, height: 560, title: '选择人员', iframe: true });
};

function callBackMeet(returnVal)
{
    if (returnVal != "close")
    {
        if (returnVal && returnVal.length != 0)
        {
            var names = '';
            var codes = '';
            var deptnames = '';
            var deptcodes = '';
            for (var j = 0; j < returnVal.length; j++)
            {
                names += returnVal[j].split(';')[0] + ';';
                codes += returnVal[j].split(';')[1] + ';';
                deptnames += returnVal[j].split(';')[3] + ';';
                deptcodes += returnVal[j].split(';')[4] + ';';
            }

            var name = names.split(';');
            var code = codes.split(';')
            var deptname = deptnames.split(';');
            var deptcode = deptcodes.split(';');

            for (var i = 0; i < name.length; i++)
            {
                if (name[i] != '')
                {
                    var csnode = window.CSNode(returnVal[0].split(';')[12]);        //创建一个会签节点
                    csnode.counterSignNames = name[i];
                    csnode.counterSignCodes = code[i];
                    csnode.counterDeptName = deptname[i];
                    csnode.counterDeptCode = deptcodes[i];
                    chart.getNode(returnVal[0].split(';')[12]).setCounterArray(csnode, returnVal[0].split(';')[12]);
                }
            }
            chart.paint();
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
        }
    }
    ymPrompt.close();
}

//删除会签（cspos:会签数组索引;wfpos:审批节点索引）
var delMeetSign = function (cspos, wfpos) {
    ymPrompt.confirmInfo({ message: '是否确认要删除此会签人？', handler: function (p) {
        if (p == 'ok') {
            chart.getNode(wfpos).delCounter(cspos);
            //            if (!chart.getNode(wfpos).ExistEnableCounterNode()) //无正在发起会签
            //            {
            //                location.reload();      //刷新页面
            //            }

            if (!chart.getNode(wfpos).ExistEnableCounterNode()) //无正在发起会签
            {
                document.getElementById('CDF1_hfNoCounter').value = 'true';
            }
            else {
                document.getElementById('CDF1_hfNoCounter').value = 'false';
            }
            chart.paint();
            chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
            ControlDisplay('counter');               //控制按钮显示隐藏
        }
    }
    });
};

var clearMeetSign = function (wfpos)
{
    chart.getNode(wfpos).clearCounterArray();
    chart.paint();
    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
};

//删除一个审批人
function deleteUser(rowIndex, pos)
{
    chart.getNode(pos).deleteNodeValue(rowIndex);
    chart.paint();
    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');     //序列化
}

//检查流程是否配置完整
function checkcustomflowchart()
{
    var pos = 0;
    while (chart.getNode(pos))
    {
        var node = chart.getNode(pos++);
        if (node.nodeType != 'Solid' && node.usercode == '')
        {
            return '流程图中存在未选审批人的节点';
        }
    }
    if (document.getElementById('CDF1_hfNoCounter').value == 'true')
        return '请选择会签人';

    return 'ok';
}

//取得当前节点的Index
function getCurrentPos()
{
    var pos = 0;
    while (chart.getNode(pos))
    {
        var node = chart.getNode(pos++);
        if (node.nodeStatus == 1)
            return pos - 1;
    }
}