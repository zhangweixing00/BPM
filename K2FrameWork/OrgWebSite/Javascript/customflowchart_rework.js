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
            node.ftitle = arraynodes[i].ftitle;
            node.isView = false;
            node.nodeType = arraynodes[i].nodeType;
            node.roleCode = arraynodes[i].roleCode;
            node.roleName = arraynodes[i].roleName;
            node.nodeId = arraynodes[i].nodeId;
            node.URL = arraynodes[i].URL;
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
}

$(document).ready(function () {
    InitWFChart('CDF1_hfjqFlowChart');
    chart.paint(); 
    chart.Objserialize(chart, 'CDF1_hfjqFlowChart');
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

//检查流程是否配置完整
function checkcustomflowchart()
{
    var pos = 0;
    while (chart.getNode(pos))
    {
        var node = chart.getNode(pos++);
        if (node.usercode == '')
        {
            return '流程图中存在未选审批人的节点';
        }
    }
    return 'ok';
}

//选人并添加节点
function SelectUAdd(pos)
{
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


$(document).ready(function () {
    //        var roleArray = roleCodes.split(';');
    //        var roleNameArray = roleNames.split(';');
    
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