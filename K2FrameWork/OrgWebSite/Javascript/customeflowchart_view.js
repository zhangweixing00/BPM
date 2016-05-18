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
            node.nodeState = arraynodes[i].nodeState;
            node.ftitle = arraynodes[i].ftitle;
            node.isOpen = arraynodes[i].isOpen;
            node.isView = true;
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
                csnode.counterDeptName = arraynodes[i].arrayCounter[j].counterDeptName;
                csnode.counterDeptCode = arraynodes[i].arrayCounter[j].counterDeptCode;
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