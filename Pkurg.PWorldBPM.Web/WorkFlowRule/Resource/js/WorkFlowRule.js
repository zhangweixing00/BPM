/*流程制度门户*/
/// <reference path="../jquery/jquery-1.8.0-vsdoc.js" />
/// <reference path="flowItems.js" /> 

/*Jquery页面加载*/
$(function () {
    LoadItems();
    $("a.img_group").fancybox({ 'width': '100%', 'height': '100%' });
});


function LoadItems() {

    //Row1
    var divs = $("#row1>.helper-table-limit");
    var data = flowItems1;
    for (var i = 0; i < divs.length; i++) {
        var column = $(divs[i]);
        for (var j = 0; j < data.length; j++) {
            if (data[j].row == i + 1) {
                var content = createLi(data[j]);
                column.append(content);
            }
        }
    }
    //Row2
    divs = $("#row2>.helper-table-limit");
    data = flowItems2;
    for (var i = 0; i < divs.length; i++) {
        var column = $(divs[i]);
        for (var j = 0; j < data.length; j++) {
            if (data[j].row == i + 1) {
                var content = createLi(data[j]);
                column.append(content);
            }
        }
    }
    //Row3
    divs = $("#row3>.helper-table-limit");
    data = flowItems3;
    for (var i = 0; i < divs.length; i++) {
        var column = $(divs[i]);
        for (var j = 0; j < data.length; j++) {
            if (data[j].row == i + 1) {
                var content = createLi(data[j]);
                column.append(content);
            }
        }
    }
    //绑定点击事件
    //$(".thirdSpan").on("click", function () { showFlow("1", $(this)) });
}

function createLi(data) {

    //流程图
    var img = "";
    if (data.chart == "0") {
        img = "Resource/Flow/big/暂无流程图(大).jpg";
    } else if (data.big_img) {
        img = data.big_img;
    }
    else {
        //根据命名规则
        img = "Resource/Flow/big/" + data.name + ".jpg";
    }
    img = "../WorkFlowRule/" + img;
    var href = "";
    //链接
    if (data.flowId) {
        if (data.page) {
            href = "http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/Workflow/" + data.page + ".aspx?AppCode=" + data.flowId + "&ActionType=0";
        }
        if (data.page == "bpm") {
            href = data.url;
        }
    }
    if (data.flowId == 0) {
        if (data.url) {
            href = data.url;
        }
    }

    var content = "";
    content = "<li id=" + data.id + " title=" + data.name + ">";
    content = content + "<a title='' class='img_group' href=" + img + "><img title='查看流程图' src=/WorkFlowRule/Resource/images/pic.png>" + "</img></a>";
    content = content + " <a href='#' onclick=openPage(" + data.flowId + ",'" + data.page + "','" + href + "')>" + shortName(data.name, 25) + "</a>"
    content = content + "</li>";
    return content;
}

function shortName(name, shortLength) {
    return name.length > shortLength ? name.substr(0, shortLength - 1) + "..." : name;
}

function openPage(flowId, page, href) {
    if (flowId) {
        if (page == "bpm") {
            $.get("../WorkFlowRule/Ajax.ashx", { "flowId": flowId, "action": "checkpermission" }, function (data) {
                var json = eval("(" + data + ")");
                if (json == 0) {
                    alert("您没有权限发起该流程!");
                }
                else {
                    window.open(href, "_blank")
                }
            });
        }
        else {
            $.get("../WorkFlowRule/Ajax.ashx", { "flowId": flowId, "action": "checkoa" }, function (data) {
                var json = eval("(" + data + ")");
                if (json.flag == 0) {
                    alert("您没有权限发起该流程!");
                }
                else {
                    window.open(href, "_blank")
                }
            });  
        }
        
    }
    else {
        window.open(href, "_blank")
    }
}