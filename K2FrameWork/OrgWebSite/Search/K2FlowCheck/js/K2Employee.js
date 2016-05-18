/******zlm添加*****/
$(document).ready(function ()
{
    new K2EmployeeCheck();
});

var K2Employee;
function K2EmployeeCheck()
{
    K2Employee = this;
    this.tableobj = null;
    //弹出窗口时传递过来的条件
    this.Param = "00000000-0000-0000-0000-000000000000";

    //单选：true  多选：false
    this.CheckStyle = "true";

    //自定义流程图结点的序号
    this.pos = 0;

    //返回值
    this.values = new Array();

    //员工输入的条件
    this.filter = "";

    this.Init();
}

K2EmployeeCheck.prototype.Init = function ()
{
    K2Employee.CheckStyle = $("#checkstyle").val();
    K2Employee.Param = $("#param").val();
    K2Employee.pos = $("#pos").val();

    $("#btnOk").bind("click", this.OkInfo);
    $("#btTrue").bind("click", this.TrueInfo);
    $("#btFalse").bind("click", this.FalseInfo);
    $("#txtInfo").bind("keyup", this.SearchProxy1);
    K2Employee.SearchProxy1();
}

K2EmployeeCheck.prototype.SearchProxy1 = function ()
{
    //员工输入的条件
    K2Employee.filter = $("#txtInfo").val();

    //弹出窗口时传递过来的条件
    K2Employee.param = $("#param").val();

    K2Employee.DataLoad();
    //setTimeout("K2Employee.SearchProxy1()", 100);
}

K2EmployeeCheck.prototype.DataLoad = function ()
{
    var filter = "filter=" + K2Employee.filter;
    filter += "&CheckStyle=" + K2Employee.CheckStyle;
    filter += "&Param=" + K2Employee.Param;
    $.ajax({
        type: "POST",
        url: "K2EmployeeData.ashx",
        data: filter,
        async: false,
        success: function (data)
        {
            $("#dataDiv").empty();
            if (data.length > 0)
            {
                $("#dataDiv").append(data);
                K2Employee.oldName = K2Employee.EmployeeName;
                var titlerow = 0;
                $("tr", $("#dataDiv")).each(function ()
                {
                    if (titlerow != 0)
                    {
                        $(this).bind("dblclick", K2Employee.DBClickRowE);

                        $(this).bind("mouseover", K2Employee.SelectRowE);
                        $(this).bind("mouseout", K2Employee.ClearRow);
                    }
                    titlerow++;
                });
                $("#dataDiv").find("tr:eq(0)").addClass("selectrow");
            }
            else
            {
                K2Employee.oldName = K2Employee.EmployeeName;
            }
            K2Employee.returnLoad = true;
            K2Employee.firstLoat = true;
        },
        error: function ()
        {
            K2Employee.returnLoad = false;
            alert("数据加载错误");
        }
    });
}

K2EmployeeCheck.prototype.DBClickRowE = function ()
{
    var employeeName = $(this).find("td:eq(0)").text();
    var employeeCode = $(this).find("td:eq(1)").text();
    var employeeMail = $(this).find("td:eq(2)").text();
    var deptName = $(this).find("td:eq(3)").text();
    var deptID = $(this).find("td:eq(4)").text();
    var locationName = $(this).find("td:eq(5)").text();
    var tel = $(this).find("td:eq(6)").text();
    var employeeID = $(this).find("td:eq(7)").text();
    //K2Employee.values说明
    //K2Employee.values[0]员工姓名
    //K2Employee.values[1]员工AD
    //K2Employee.values[2]邮件
    //K2Employee.values[3]员工部门名称
    //K2Employee.values[4]员工部门名称ID
    //K2Employee.values[5]员工所在地
    //K2Employee.values[6]固定电话
    //K2Employee.values[7]员工ID
    if (K2Employee.CheckStyle == "true")
    {
        K2Employee.values = new Array();
    }
    K2Employee.values.push(employeeName + ";" + employeeCode + ";" + employeeMail + ";" + deptName + ";" + deptID + ";" + locationName + ";" + tel + ";" + employeeID + ";User");
    //$("#selectuser").val($("#selectuser").val() + employeeName + " ");

    K2Employee.DisplayList();     //显示列表

    //    if (K2Employee.CheckStyle == "true")
    //    {
    //top.document.getElementById("frameContent").contentWindow.callBackOperator(K2Employee.values);
    //window.parent.ymPrompt.doHandler(K2Employee.values, false);
    //window.parent.ymPrompt.close();
    //    }
}

K2EmployeeCheck.prototype.DisplayList = function ()
{
    //先删除空记录
    for (var i = 0; i < K2Employee.values.length; i++)
    {
        if (K2Employee.values[i] == null)
        {
            K2Employee.values.splice(i--, 1);
        }
    }

    if (K2Employee.tableobj != null)
    {
        if (document.getElementById('addEmployee') != null)
        {
            K2Employee.tableobj == document.getElementById('addEmployee');
        }
        $(K2Employee.tableobj).html('<table id="addEmployee" class="emalNamecsstable"></table>');
    }
    else
    {
        K2Employee.tableobj = document.createElement('table');
        K2Employee.tableobj.id = "addEmployee";
        $(K2Employee.tableobj).addClass("emalNamecsstable");
    }

    //根据选择生成列表
    for (var i = 0; i < K2Employee.values.length; i++)
    {
        var newtr = document.createElement('tr');
        if (K2Employee.values[i].split(';')[K2Employee.values[i].split(';').length - 1] == 'User')
        {
            //第一列
            var newtd1 = document.createElement('td');
            $(newtd1).text(K2Employee.values[i].split(';')[0]);
            $(newtd1).css('width', '120px');
            $(newtd1).attr('align', 'center');
        }
        else
        {
            //第一列
            var newtd1 = document.createElement('td');
            $(newtd1).text(K2Employee.values[i].split(';')[1]);
            $(newtd1).css('width', '120px');
            $(newtd1).attr('align', 'center');
        }
        //第二列
        var newtd2 = document.createElement('td');
        $(newtd2).addClass("createtd1");
        $(newtd2).attr('align', 'right');
        $(newtd2).css('width', '545px');
        var newa = document.createElement('a');
        $(newa).addClass("ahadden");
        $(newa).attr("href", "#");
        $(newa).text("删除");
        $(newa).attr("id", i);
        $(newa).bind("click", function ()
        {
            $(this).parent().parent().remove();
            K2Employee.values.splice($(this)[0].id, 1);
            K2Employee.values.splice($(this)[0].id, 0, null);       //再添加一条空记录
        });
        $(newtd2).append(newa);
        $(newtr).append(newtd1);
        $(newtr).append(newtd2);
        $(K2Employee.tableobj).append(newtr);
        $("#emalName").append(K2Employee.tableobj);

    }
}

//鼠标滑动样式
K2EmployeeCheck.prototype.SelectRowE = function ()
{
    $("tr", $("#dataDiv")).removeClass("selectrow");
    $(this).addClass("selectrow");
}


//鼠标滑动样式
K2EmployeeCheck.prototype.ClearRow = function ()
{
    $("tr", $("#dataDiv")).removeClass("selectrow");
    // $(this).addClass("selectrow");
}
//确定
K2EmployeeCheck.prototype.OkInfo = function ()
{
    //top.document.getElementById("frameContent").contentWindow.TrueInfos(K2Employee.values);
    var retValues = new Array();
    //循环取得不为null的记录
    for (var i = 0; i < K2Employee.values.length; i++)
    {
        if (K2Employee.values[i] != null)
            retValues.push(K2Employee.values[i]);
    }
    window.parent.ymPrompt.doHandler(retValues, false);
}

//清除
K2EmployeeCheck.prototype.TrueInfo = function ()
{
    $("#selectuser").val("");
    if (K2Employee.values != null && K2Employee.values.length != 0)
    {
        K2Employee.values.length = 0;
        K2Employee.values = new Array();

        K2Employee.tableobj.outerHTML = '<table id="addEmployee" class="emalNamecsstable"></table>';
    }
    //    if (K2Employee.CheckStyle == "true")
    //    {
    //        top.document.getElementById("frameContent").contentWindow.TrueInfos(K2Employee.values);
    //        window.parent.ymPrompt.doHandler(K2Employee.values, false);
    //        window.parent.ymPrompt.close();
    //    }
}
//取消
K2EmployeeCheck.prototype.FalseInfo = function ()
{
    if (K2Employee.values != null && K2Employee.values.length != 0)
    {
        K2Employee.values.length = 0;
        K2Employee.values = null;
    }
    //top.document.getElementById("frameContent").contentWindow.TrueInfos(K2Employee.values);
    window.parent.ymPrompt.doHandler("cancel", false);
}