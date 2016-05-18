/*
更新教育信息
*/

function UpdateEduButton() {
    var school = $("#dialog #Dschool").val();
    var edufrom = $("#dialog #DeduFrom").val();
    var eduto = $("#dialog #DeduTo").val();
    var Major = $("#dialog #DMajor").val();
    var xueli = $("#dialog #Dxueli").val();
    var xuelitext = $("#dialog #Dxueli").find("option:selected").text();
    var eid = $("#dialog #Eid").text();
    if (CheckDate("DeduFrom", "DeduTo") == false) {
        return;
    }
    if (school == "" || school == undefined) {
        alert("学校名称不能为空");
        return;
    }
    else if (xueli == "" || xueli == undefined) {
        alert("学历不能为空");
        return;
    }
    else if (edufrom == "" || edufrom == undefined) {
        alert("教育年限开始时间不能为空");
        return;
    }
    else if (eduto == "" || eduto == undefined) {
        alert("教育年限结束时间不能为空");
        return;
    }
    else {
        UpdateEduAjaxMethod(school, edufrom, eduto, Major, xueli, eid, xuelitext);
    }
    $("#dialog").dialog("close");
}
function UpdateEdu() {
    var content;
    var school;
    var from;
    var to;
    var Major;
    var xueli;
    var id;
    var i = 0;
    $("#T_PersonalEducation input").each(function (index) {
        if ($(this).prop("checked") == true) {
            if (index != 0) {
                i++;
            }
        }
    });
    if (i == 0) {
        alert("请选择要修改的行");
        return false;
    }
    else if (i > 1) {
        alert("一次只能修改一行数据");
        return false;
    }
    else {
        $("#T_PersonalEducation tr").each(function (index) {
            if (index != 0) {
                if ($(this).find("input").prop("checked") == true) {
                    school = $(this).find("td:eq(0)").text();
                    from = $(this).find("td:eq(1)").text();
                    to = $(this).find("td:eq(2)").text();
                    Major = $(this).find("td:eq(3)").text();
                    xueli = $(this).find("td:eq(4)").text();
                    id = $(this).find("td:eq(5)").text();
                    // debugger
                    content = "<table class=' datalist_5' border='1'bordercolor='#f2dd81'><tr bgcolor='#fef6c9'><th width='25%' scope='col'>学校名称（全称）<span class='mustinput'>*</span></th><th width='25%' scope='col' colspan='2'>教育年限<span class='mustinput'>*</span></th><th width='35%' scope='col'>专业</th><th width='15%' scope='col'>学历 </th><th style='display: none;'></th></tr><tr><td><input type='text' class='dialoginput' id='Dschool' value='" + school + "' /></td><td><input id='DeduFrom' style='height:22px;' class='Wdate' onclick='WdatePicker({readOnly:true})'type='text'   value='" + from + "' /></td><td><input id='DeduTo' style='height:22px;' class='Wdate' onclick='WdatePicker({readOnly:true})'type='text' value='" + to + "' /></td><td><input type='text' class='dialoginput' id='DMajor' value='" + Major + "' /></td><td><select name='xueli' id='Dxueli' ></select></td><td style='display: none;'><span id='Eid'>" + id + "</span></td></tr></table>";
                    //$("#dialog #Dxueli").html();

                }
            }

        });
    }
    $("#dialog").dialog({
        height: 170,
        width: 900,
        modal: true,
        title: "更新教育信息",
        hide: 'slide',
        show: 'slide',

        open: function (event, ui) {
            $("#dialogContent").html("");
            $("#dialogContent").append("<p>" + content + "</p>");
            $("#Degree").find("option").each(function (index, Value) {
                $("#dialog #Dxueli").append("<option value=" + $(Value).val() + ">" + $(Value).text() + "</option>");
            });
            $("#dialog #Dxueli").find("option").each(function (index, Value) {
                if ($.trim($(Value).text()) == $.trim(xueli)) {
                    $(Value).prop("selected", "selected");
                }
            });

        },
        close: function (event, ui) {
            $("#dialogButton").unbind("click", UpdateEduButton);
        }
    });
    $("#dialogButton").bind("click", UpdateEduButton);
}
//ajax 的方式提交数据
function UpdateEduAjaxMethod(school, edufrom, eduto, major, xueli, eid, xuelitext) {
    $.ajax({ //请求登录处理页
        url: "NewEmployeeForm.aspx",
        type: "Get",
        //传送请求数据
        data: { Update: "Edu",
            School: school,
            Edufrom: edufrom,
            Eduto: eduto,
            Major: major,
            xueli: xueli,
            Eid: eid,
            FormId: $("#FormId").val(),
            ProcessId: $("#ProcessId").val()
        },
        //插入成功后返回GUID
        success: function (strValue) {
            if (strValue != "") {
                $("#Edu_checkbox").prop("checked", false);
                var htmltr = "<tr><th scope='row'><input type='checkbox'  onclick='changeCheckBox(this)' /></th><td>" + school + "</td><td width='12%'>" + edufrom + "</td><td>" + eduto + "</td><td>" + major + "</td><td>" + xuelitext + "</td><td style='display:none;'>" + eid + "</td></tr>";
                $("#T_PersonalEducation tr").each(function (index) {

                    if ($(this).find("input").prop("checked") == true) {
                        if (index != 0) {
                            $(this).replaceWith(htmltr);
                        }
                    }
                });
            }
        },
        error: function (strValue) {
            alert("响应错误");
        }

    })
}

/*
弹出对话框 更新工作信息
*/
function UpdateWorkButtton() {
    var company = $("#dialog #DcompanyName").val();
    var workfrom = $("#dialog #DworkFrom").val();
    var workto = $("#dialog #DworkTo").val();
    var post = $("#dialog #DPost").val();
    var reason = $("#dialog #Dreason").val();
    var eid = $("#dialog #Wid").text();
    if (CheckDate("DworkFrom", "DworkTo") == false) {
        return;
    }
    if (company == "" || company == undefined) {
        alert("公司名称不能为空");
        return;
    }
    else if (workfrom == "" || workfrom == undefined) {
        alert("工作开始时间不能为空");
        return;
    }
    else if (workto == "" || workto == undefined) {
        alert("工作结束时间不能为空");
        return;
    }
    else {
        UpdateWorkAjaxMethod(company, workfrom, workto, post, reason, eid);
    }
    $("#dialog").dialog("close");

}
function UpdateWork() {
    var content;
    var i = 0;
    $("#T_PersonalWorkingExperience input").each(function (index) {
        if ($(this).prop("checked") == true) {
            if (index != 0) {
                i++;
            }
        }
    });
    if (i == 0) {
        alert("请选择要修改的行");
        return false;
    }
    else if (i > 1) {
        alert("一次只能修改一行数据");
        return false;
    }
    else {
        $("#T_PersonalWorkingExperience tr").each(function (index) {
            if (index != 0) {
                if ($(this).find("input").prop("checked") == true) {
                    var company = $(this).find("td:eq(0)").text();
                    var workfrom = $(this).find("td:eq(1)").text();
                    var workto = $(this).find("td:eq(2)").text();
                    var post = $(this).find("td:eq(3)").text();
                    var reason = $(this).find("td:eq(4)").text();
                    var id = $(this).find("td:eq(5)").text();
                    content = "<table class=' datalist_5' border='1'bordercolor='#f2dd81'><tr bgcolor='#fef6c9'><th width='25%' scope='col'>公司名称<span class='mustinput'>*</span></th><th width='25%' scope='col' colspan='2'>起止时间<span class='mustinput'>*</span></th><th width='20%' scope='col'>职位</th><th width='30%' scope='col'>离职原因</th><th style='display: none;'></th></tr><tr><td><input  class='dialoginput' type='text' id='DcompanyName' value='" + company + "' /></td><td><input id='DworkFrom' style='height:22px;' class='Wdate' onclick='WdatePicker({readOnly:true})'type='text' value='" + workfrom + "' /></td><td><input id='DworkTo'  style='height:22px;' class='Wdate' onclick='WdatePicker({readOnly:true})'type='text' value='" + workto + "' /></td><td><input type='text'  class='dialoginput' id='DPost' value='" + post + "' /></td><td><input type='text'  class='dialoginput' id='Dreason' value='" + reason + "' /></td><td style='display: none;'><span id='Wid'>" + id + "</span></td></tr></table>";
                }
            }

        });
    }
    $("#dialog").dialog({
        height: 170,
        width: 900,
        modal: true,
        title: "更新工作信息",
        hide: 'slide',
        show: 'slide',

        open: function (event, ui) {
            $("#dialogContent").html("");
            $("#dialogContent").append("<p>" + content + "</p>");

        },
        close: function (event, ui) {
            $("#dialogButton").unbind("click", UpdateWorkButtton);
        }
    });

    $("#dialogButton").bind("click", UpdateWorkButtton);
}
//ajax 的方式提交数据
function UpdateWorkAjaxMethod(company, workfrom, workto, post, reason, eid) {
    $.ajax({ //请求登录处理页
        url: "NewEmployeeForm.aspx",
        type: "Get",
        //传送请求数据
        data: { Update: "Work",
            Company: company,
            Workfrom: workfrom,
            Workto: workto,
            Post: post,
            Reason: reason,
            Eid: eid,
            FormId: $("#FormId").val(),
            ProcessId: $("#ProcessId").val()
        },
        //更新成功后返回GUID
        success: function (strValue) {
            if (strValue != "") {
                $("#Work_checkbox").prop("checked", false);
                var htmltr = "<tr><th scope='row'><input type='checkbox'  onclick='changeCheckBox(this)' /></th><td>" + company + "</td><td width='12%'>" + workfrom + "</td><td>" + workto + "</td><td>" + post + "</td><td>" + reason + "</td><td style='display:none;'>" + eid + "</td></tr>";
                $("#T_PersonalWorkingExperience tr").each(function (index) {
                    if ($(this).find("input").prop("checked") == true) {
                        if (index != 0) {
                            $(this).replaceWith(htmltr);
                        }
                    }
                });
            }
        },
        error: function (strValue) {
            alert("响应错误");
        }

    })
}


/*
弹出对话框 更新家庭信息
*/

function UpdateButtonFamily() {
    var name = $("#dialog #DName").val();
    var relation = $("#dialog #Drelation").val();
    var unit = $("#dialog #Dunit").val();
    var tel = $("#dialog #Dtel").val();
    var eid = $("#dialog #Fid").text();
    if (name == "" || name == undefined) {
        alert("姓名不能为空");
        return;
    }
    else {
        UpdateHomeAjaxMethod(name, relation, unit, tel, eid);
    }
    $("#dialog").dialog("close");

}
function UpdateFamily() {
    var content;
    var i = 0;
    $("#T_PersonalFamilyInfomation input").each(function (index) {
        if ($(this).prop("checked") == true) {
            if (index != 0) {
                i++;
            }
        }
    });
    if (i == 0) {
        alert("请选择要修改的行");
        return false;
    }
    else if (i > 1) {
        alert("一次只能修改一行数据");
        return false;
    }
    else {
        $("#T_PersonalFamilyInfomation tr").each(function (index) {
            if (index != 0) {
                if ($(this).find("input").prop("checked") == true) {
                    var name = $(this).find("td:eq(0)").text();
                    var relation = $(this).find("td:eq(1)").text();
                    var unit = $(this).find("td:eq(2)").text();
                    var tel = $(this).find("td:eq(3)").text();
                    var id = $(this).find("td:eq(4)").text();
                    content = "<table class=' datalist_5' border='1'bordercolor='#f2dd81'><tr bgcolor='#fef6c9'><th width='25%' scope='col'>姓名<span class='mustinput'>*</span></th><th width='20%' scope='col'>关系</th><th width='35%' scope='col'>工作单位</th><th width='20%' scope='col'>电话</th><th style='display: none;'></th></tr><tr><td><input type='text'  class='dialoginput' id='DName' value='" + name + "'  /></td><td><input id='Drelation'  class='dialoginput' type='text' value='" + relation + "' /></td><td><input type='text'  class='dialoginput' id='Dunit' value='" + unit + "' /></td><td><input type='text'  class='dialoginput' id='Dtel' value='" + tel + "' /></td><td style='display: none;'><span id='Fid'>" + id + "</span></td></tr></table>";
                }
            }

        });
    }
    $("#dialog").dialog({
        height: 170,
        width: 900,
        modal: true,
        title: "更新家庭信息",
        hide: 'slide',
        show: 'slide',
        open: function (event, ui) {
            $("#dialogContent").html("");
            $("#dialogContent").append("<p>" + content + "</p>");
        },
        close: function (event, ui) {
            $("#dialogButton").unbind("click", UpdateButtonFamily);
        }
    });

    $("#dialogButton").bind("click", UpdateButtonFamily);
}
//ajax 的方式提交数据
function UpdateHomeAjaxMethod(name, relation, unit, tel, eid) {
    $.ajax({ //请求登录处理页
        url: "NewEmployeeForm.aspx",
        type: "Get",
        //传送请求数据
        data: { Update: "Home",
            Name: name,
            Relation: relation,
            Unit: unit,
            Tel: tel,
            Eid: eid,
            FormId: $("#FormId").val(),
            ProcessId: $("#ProcessId").val()
        },
        //更新成功后返回GUID
        success: function (strValue) {
            if (strValue != "") {
                $("#Home_checkbox").prop("checked", false);
                var htmltr = "<tr><th scope='row'><input type='checkbox'  onclick='changeCheckBox(this)' /></th><td>" + name + "</td><td>" + relation + "</td><td>" + unit + "</td><td>" + tel + "</td><td style='display:none;'>" + eid + "</td></tr>";
                $("#T_PersonalFamilyInfomation tr").each(function (index) {
                    if ($(this).find("input").prop("checked") == true) {
                        if (index != 0) {
                            $(this).replaceWith(htmltr);
                        }
                    }
                });
            }
        },
        error: function (strValue) {
            alert("响应错误");
        }

    })
}