function SelEmpCode(str) {
    document.getElementById("txt_Employee").Value = str;
}
/*******begin wfl********/

//全选择
var name = "";
var ctrl, hidCtrl;

var button;
var postleveName, postleveID, postName, postID, detailName, detailID, isleve, ispost, isdetail;
var ctrvalue = "";
var isEmail, isGroup;
var emilsplit;
var leaveName, leaveid, leaveTel, leaveCompany, leavefirstdp, leaveseconddp, leavethirddp, leaveboss, leavepostleve, leavestarttime, leaveendtime, leaveemail, leaveEmailInfo, leavePhone, leavePost, leavePayrollID, leaveViewBoss;
var pmsbtn, PMSformID, applyTypeCode, machineID, isPage;
var paybtn;

//直接主管员工转移信息
var Supervisor, Supervisorid, SPosition, SEmail, STel, SDepartment, SdivOne, SdivTwo;

//公司
function CompanyCode(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Search/Company/Company.aspx', 255, 360, "公司查询", null, null, null, true, null, null, true, false, false);
}

//角色
function RoleCode(txtCtrl, txtHidCtrl) {
    name = "role";
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Admin/RoleAction/BusinessModelAction.aspx', 708, 490, "角色选择", null, null, null, true, null, null, true, false, false);
}
//create by kangyabing
//create date:2011-08-29
//begin
function RoleCodeNew(txtCtrl, txtHideCtrl) {
    name = "rolenew";
    ctrl = txtCtrl;
    hidCtrl = txtHideCtrl;
    ymPrompt.win('/Admin/RoleAction/BusinessModelAction.aspx', 255, 360, "角色查询", null, null, null, true, null, null, true, false, false);
}
//end
//合同签署公司
function ConCompanyCode(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Search/CompactCompany/CompactCompany.aspx', 255, 360, "合同签署公司", null, null, null, true, null, null, true, false, false);
}

//大部门
function FirstDeptCode(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Search/FirstDepartment/FirstDepartment.aspx', 255, 360, "大部门查询", null, null, null, true, null, null, true, false, false);
}

//hrpb大部门
function HrpbFirstDeptCode(txtCtrl, txtHidCtrl, cName) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ctrvalue = cName;
    ymPrompt.win("/Search/HRPBDepartment/HRPBDepartment.aspx?employee=" + ctrvalue, 255, 360, "大部门查询", null, null, null, true, null, null, true, false, false);
}



//中部门
function SecondDeptCode(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Search/SecondDepartment/SecondDepartment.aspx', 255, 360, '中部门查询', null, null, null, true, null, null, true, false, false);
}
//小部门
function ThirdDeptCode(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Search/ThirdDepartment/ThirdDepartment.aspx', 255, 360, '小部门查询', null, null, null, true, null, null, true, false, false);
}
//职级
function Position(txtpostleveCtrl, txtpostleveHidCtrl, txtpostCtrl, txtpostHidCtrl, txtdetailCtrl, txtdetailHid, isleves, isposts, isdetails) {
    postleveName = txtpostleveCtrl;
    postleveID = txtpostleveHidCtrl;
    postName = txtpostCtrl;
    postID = txtpostHidCtrl;
    detailName = txtdetailCtrl;
    detailID = txtdetailHid;
    isleve = isleves;
    ispost = isposts;
    isdetail = isdetails;

    ymPrompt.win('/Search/PostionRelation/PostionRelation.aspx', 500, 504, '职级职位查询', null, null, null, true, null, null, true, false, true);
    name = "post";
}

function showModalDialoge(url, str, w, h) {
    //var s = window.showModalDialog(url, "", "dialogWidth:650px; dialogHeight :300px;scroll :no;status :no;")
    top.window.ymPrompt.win(url, w, h, str, null, null, null, true, null, null, true, false, false);
}

//主管
function EmployeeCheck(txtCtrl, txtHidCtrl, cName) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ctrvalue = cName;
    ymPrompt.win("/Search/EmployeeCheck/EmployeeCheck.aspx?employee=" + ctrvalue, 500, 508, '主管查询', null, null, null, true, null, null, true, false, true);
    name = "Employeedepartment";
}

//邮件
function EmailImgeSearch(txtCtrl, txtHidCtrl, email, group, emailsp, varEmailAdd) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    isEmail = email;
    isGroup = group;
    emilsplit = emailsp
    ymPrompt.win("/Search/EmailCheck/EmailCheck.aspx?mailAdd=" + varEmailAdd, 489, 510, '邮件组检索', null, null, null, true, null, null, true, false, true);
    name = "email";
}

////hrpb 邮件组
function HrbpEmailSearch(txtCtrl, txtHidCtrl, email, group, emailsp, varEmailGroupAdd) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    isEmail = email;
    isGroup = group;
    emilsplit = emailsp
    ymPrompt.win("/Search/HRBPEmail/HRBPEmail.aspx?EmailGroupAdd=" + varEmailGroupAdd, 490, 510, '邮件组检索', null, null, null, true, null, null, true, false, true);
    name = "email";
}

//2011-9-16 王凤龙添加，代替邮件 begin
function EmailDimension(txtCtrl, txtHidCtrl, email, group, emailsp, varEmailAdd) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    isEmail = email;
    isGroup = group;
    emilsplit = emailsp
    ymPrompt.win("/Search/EmailDimension/EmailDimension.aspx?dimensionType=HR&mailAdd=" + varEmailAdd, 698, 510, '邮件组检索', null, null, null, true, null, null, true, false, true);
    name = "email";
}

function HrbpEmailDimension(txtCtrl, txtHidCtrl, email, group, emailsp, varEmailGroupAdd) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    isEmail = email;
    isGroup = group;
    emilsplit = emailsp
    ymPrompt.win("/Search/HRBPEmailDimension/HRBPEmailDimension.aspx?dimensionType=IT&EmailGroupAdd=" + varEmailGroupAdd, 698, 510, '邮件组检索', null, null, null, true, null, null, true, false, true);
    name = "email";
}
//2011-9-16 王凤龙添加，代替邮件 end

//点击姓名查询数据
function SearchNameInfo(txtName, txtId, txtTel, txtEmail, txtCompan, txtFirstdp, txtSeconddp, txtThirddp, txtBoss, txtPostleve, txtStarttime, txtEndtime, txtEmailInfo, txtPhone, txtPost, txtPayrollID, txtViewBoss) {
    leaveName = txtName;
    leaveid = txtId;
    leaveTel = txtTel;
    leaveCompany = txtCompan
    leavefirstdp = txtFirstdp;
    leaveseconddp = txtSeconddp;
    leavethirddp = txtThirddp;
    leaveboss = txtBoss;
    leavepostleve = txtPostleve;
    leavestarttime = txtStarttime;
    leaveendtime = txtEndtime;
    leaveemail = txtEmail;
    leaveEmailInfo = txtEmailInfo;
    leavePhone = txtPhone;
    leavePost = txtPost;
    leavePayrollID = txtPayrollID;
    leaveViewBoss = txtViewBoss;
    ymPrompt.win("/Search/LeveEmployee/LeveEmployee.aspx", 500, 508, '人员查询', null, null, null, true, null, null, true, false, true);
    name = "searchNameInfo";
}

function ClickRow() {
    if (name == "post") {//职级，职位，职位明细
        if (isleve == true) {
            var post_levecode = $(this).find("td:eq(0)").find("input").val();
            var post_levename = $(this).find("td:eq(0)").text();
            $(postleveName).val(post_levename);
            $(postleveID).val(post_levecode);
        }
        if (ispost == true) {
            var post_id = $(this).find("td:eq(1)").find("input").val();
            var post_name = $(this).find("td:eq(1)").text();
            $(postName).val(post_name);
            $(postID).val(post_id);
        }
        if (isdetail == true) {
            var post_deid = $(this).find("td:eq(2)").find("input").val();
            var post_dename = $(this).find("td:eq(2)").text();
            $(detailName).val(post_dename);
            $(detailID).val(post_deid);
        }

    }
    else if (name == "Employeedepartment") {
        var id = $(this).find("td:eq(0)").text();
        var value = $(this).find("td:eq(1)").text();
        $(ctrl).val(value);
        $(hidCtrl).val(id);
    }
    else if (name == "email") {//hrpb邮件组
        var groupname = $(this).find("input").val();
        var value = $(this).text();
        $(ctrl).val(value);
        $(hidCtrl).val(groupname);
    } else if (name == "searchNameInfo") {
        var ss = $(this).find("in_teleCode");
        var searchName = $(this).find("td:eq(1)").text();
        var searchID = $(this).find("td:eq(0)").text();
        var inputList = $("input", $(this));
        var searchTel = inputList[0].value;
        var searchCompany = inputList[1].value
        var searchFirstdp = inputList[2].value;
        var searchSeconddp = inputList[3].value;
        var searchThirddp = inputList[4].value;
        var searchBoss = inputList[5].value;
        var searchPostleve = inputList[6].value;
        var searchStarttime = inputList[7].value;
        var searchEndtime = inputList[8].value;
        var searchEmail = inputList[9].value;
        var searchEmailInfo = inputList[12].value;
        var searchPhone = inputList[10].value;
        var searchPost = inputList[11].value;
        var searchPayrollID = inputList[13].value;
        $(leaveName).val(searchName);
        $(leaveid).val(searchID);
        $(leaveTel).val(searchTel);
        $(leaveCompany).val(searchCompany);
        $(leavefirstdp).val(searchFirstdp);
        $(leaveseconddp).val(searchSeconddp);
        $(leavethirddp).val(searchThirddp);
        $(leaveboss).val(searchBoss);
        $(leavepostleve).val(searchPostleve);
        $(leavestarttime).val(searchStarttime);
        $(leaveendtime).val(searchEndtime);
        $(leaveemail).val(searchEmail);
        $(leaveEmailInfo).val(searchEmailInfo);
        $(leavePhone).val(searchPhone);
        $(leavePost).val(searchPost);
        $(leavePayrollID).val(searchPayrollID);
        //2011-10-17 wfl 添加
        GetEmployeeBoss(leaveViewBoss);
    }
    else if (name == 'role') {
        var id = $(this).find("input").val();
        var value = $(this).text();
        $(ctrl).val(value);
        $(hidCtrl).val(id);
        window.frames[0].SetRole(value, id);
    }
    //create by kangyabing
    //create date:2011-08-29
    //begin
    else if (name = 'rolenew') {
        var id = $(this).find("input").val();
        var value = $(this).text();
        $(ctrl).val(value);
        $(hidCtrl).val(id);
    } //end
    else {
        var id = $(this).find("input").val();
        var value = $(this).text();
        $(ctrl).val(value);
        $(hidCtrl).val(id);
    }
    name = "";
    ymPrompt.close();
}

//清除或者确定
function TrueInfo() {
    if (name == "post") {//职级，职位，职位明细
        if (isleve == true) {
            $(postleveName).val("");
            $(postleveID).val("");
        }
        if (ispost == true) {
            $(postName).val("");
            $(postID).val("");
        }
        if (isdetail == true) {

            $(detailName).val("");
            $(detailID).val("");
        }
    }
    else if (name == "email") {
        if (emilsplit == true) {
            var emailTr = "";
            // var btnname = $(ymPrompt.getPage().contentWindow.document.all['emalName']).val();
            var btnname = $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName")));
            //var btnname = $("tr", $(ymPrompt.getPage().contentWindow.document.all['emalName']));
            if (btnname.length > 0 && btnname != null) {
                $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName"))).each(function () {
                    var vard = $(this).find("td:eq(0)").text();
                    emailTr += vard;

                });
                if (emailTr.length > 0) {
                    $(ctrl).val(emailTr);
                }
            }
            else {
                $(ctrl).val("");
            }
        }
        else {
            //邮件组
            var emailGroupTr = "";
            var btnname = $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName")));
            if (btnname.length > 0 && btnname != null) {
                $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName"))).each(function () {
                    var vard = $(this).find("td:eq(0)").text();
                    emailGroupTr += vard;

                });
                if (emailGroupTr.length > 0) {
                    $(hidCtrl).val(emailGroupTr);
                }
            }
            else {
                $(hidCtrl).val("");
            }
            //            if (isEmail == true) {
            //                $(ctrl).val(btnname);
            //            }
            //            if (isGroup == true) {
            //                $(hidCtrl).val(groupname);
            //            }
        }
    } else if (name == "searchNameInfo") {
        $(leaveName).val("");
        $(leaveid).val("");
        $(leaveTel).val("");
        $(leaveCompany).val("");
        $(leavefirstdp).val("");
        $(leaveseconddp).val("");
        $(leavethirddp).val("");
        $(leaveboss).val("");
        $(leavepostleve).val("");
        $(leavestarttime).val("");
        $(leaveendtime).val("");
        $(leaveemail).val("");
        $(leaveEmailInfo).val("");
        $(leavePhone).val("");
        $(leavePost).val("");
        $(leavePayrollID).val("");
    }
    else {
        $(ctrl).val("");
        $(hidCtrl).val("");
    }
    name = "";
    ymPrompt.close();
}
//取消
function FalseInfo() {
    name = "";
    ymPrompt.close();
}
//验证页码
function CheckPageIndex() {
    var varPageAll = $("#lblPage").text();
    var varALL = varPageAll.split('/');
    var varone = varALL[1].replace('共', '');
    //总页码
    var vartwo = varone.replace('页', '');
    //页码索引
    var varPageIndex = $("#txt_PageIndex").val();
    //没有中页码报错
    if (vartwo != "") {
        if (varPageIndex == "" || varPageIndex <= 0) {
            alert("请输入页码!");
            return false;
        }
        else if (varPageIndex > vartwo) {
            alert("页码不能大于总页面!");
            return false;
        }
        else {
            return true;
        }
    }
    else {
        alert("没有总页面！");
        return false;
    }
}
function showLoading(objbody) {
    var txtdiv = '<div style="width:100%;height:100%;"><img src="/pic/loading.gif" width="38" height="38" style="margin-right:8px;" align="absmiddle"/></div>';
    var divNode = document.createElement("div");
    divNode.setAttribute("id", "divloading");
    divNode.setAttribute("style", "width:100%;height:100%;text-align:center;height:auto;position:absolute;z-index:20001;background-color:white");
    divNode.innerHTML = txtdiv;
    objbody.appendChild(divNode);
}
function hideLoading(objbody) {
    objbody.removeChild($('#divloading')[0]);
}



////////////wfl添加begin
//合同签署公司
function TempCompany(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    var returnInfoID = $(hidCtrl).val();
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempCompany/TempCompany.aspx?tempcom=" + returnInfo + "&tempcomID=" + returnInfoID, 273, 480, '合同签署公司选择', null, null, null, true, null, null, true, false, false);
}

//payroll公司
function TempPayroll(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    var returnInfoID = $(hidCtrl).val();
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempPayrollCompany/TempPayrollCompany.aspx?tempayroll=" + returnInfo + "&tempayrollID=" + returnInfoID, 273, 480, 'Payroll公司选择', null, null, null, true, null, null, true, false, false);
}

//工作地点
function TempLocation(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    var returnInfoID = $(hidCtrl).val();
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempWorkState/TempWorkState.aspx?tempLocation=" + returnInfo + "&tempLocationID=" + returnInfoID, 273, 480, '工作地点选择', null, null, null, true, null, null, true, false, false);
}

//员工类型
function TempEmployeeType(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    var returnInfoID = $(hidCtrl).val();
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempEmplyeeType/TempEmplyeeType.aspx?tempEmployeeType=" + returnInfo + "&tempEmployeeTypeID=" + returnInfoID, 273, 480, '员工类型选择', null, null, null, true, null, null, true, false, false);
}

//职级
function TempLevelSelect(txtCtrl) {
    ctrl = txtCtrl;

    ymPrompt.win("/Template/TemplateSearch/TempPostLeave/TempPostLeave.aspx?temLevel=cancel&Select=One", 273, 330, '职级选择', null, null, null, true, null, null, true, false, false);
    name = "TempLevel";
}
//职级
function TempLevel(txtCtrl) {
    ctrl = txtCtrl;
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempPostLeave/TempPostLeave.aspx?temLevel=" + returnInfo, 280, 495, '职级选择', null, null, null, true, null, null, true, false, false);
    name = "TempLevel";
}

//大部门
function TempFirstDept(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    var returnInfoID = $(hidCtrl).val();
    var returnInfo = $(ctrl).val();
    ymPrompt.win("/Template/TemplateSearch/TempDepartment/TempDepartment.aspx?temFirstDept=" + returnInfo + "&temFirstDeptID=" + returnInfoID, 273, 480, '大部门选择', null, null, null, true, null, null, true, false, false);
}



function TempTrueButton() {
    var tempCompanycode = "";
    var tempCompanyName = "";

    var btnname = $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("dataID")));
    if (btnname.length > 0 && btnname != null) {
        btnname.each(function () {
            var inputtype = $(this).find("input").val();
            var tdtype = $(this).find("td:eq(0)").text();
            if (tdtype != "") {
                tempCompanycode += inputtype;
                tempCompanyName += tdtype;
            }
        });
    }
    if (name == "TempLevel") {
        $(ctrl).val(tempCompanyName);
    }
    else {
        $(ctrl).val(tempCompanyName);
        $(hidCtrl).val(tempCompanycode);
    }
    name = "";
    ymPrompt.close();
}

//部门领导
function DeptLeaderSearch(txtCtrl, txtHidCtrl, varName, varCode) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win("/Search/LeaveDepartmentBoss/LeaveDirector.aspx?varName=" + varName + "&varCode=" + varCode + "&departFlag=DeptLead", 486, 510, '部门领导查询', null, null, null, true, null, null, true, false, true);
    name = "deptleader";
}
//部门助理
function DeptAcessSearch(txtCtrl, txtHidCtrl, varName, varCode) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win("/Search/LeaveDepartmentBoss/LeaveDirector.aspx?varName=" + varName + "&varCode=" + varCode + "&departFlag=DepartAcess", 486, 510, '部门助理查询', null, null, null, true, null, null, true, false, true);
    name = "DeptAcessSearch";
}
//部门领导，助理确认按钮
function DeptLederAndAccess() {
    var deptname = "";
    var deptcode = "";
    var btnname = $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName")));
    if (btnname.length > 0 && btnname != null) {
        $("tr", $(ymPrompt.getPage().contentWindow.document.getElementById("emalName"))).each(function () {
            var vard = $(this).find("td:eq(0)").text();
            var varhidcode = $(this).find("input").val();
            if (vard != "" && varhidcode != "") {
                deptname += vard;
                deptcode += varhidcode + ";";
            }
        });
        $(ctrl).val(deptname);
        $(hidCtrl).val(deptcode);

        //添加员工查询  ---王红福
        try {

            //添加一个回调方法
            if (button != null && button != "undefind") {
                $(button).click();
            }

        } catch (e) {

        }
    }
    else {
        $(ctrl).val("");
        $(hidCtrl).val("");
    }
    ymPrompt.close();
}
//添加员工查询  ---王红福
function EmployeesSearch(txtCtrl, txtHidCtrl, btnSave, varName, varCode) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    button = btnSave;
    ymPrompt.win("/Search/AllEmployee/AllEmployees.aspx?varName=" + varName + "&varCode=" + varCode + "&departFlag=DepartAcess", 486, 510, '添加员工到角色', null, null, null, true, null, null, true, false, true);
    name = "DeptAcessSearch";
}
//添加参数  ---王红福
function ParameterSearch(txtCtrl, txtHidCtrl, ruleId) {

    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;

    //ymPrompt.win('/Search/FirstDepartment/FirstDepartment.aspx', 255, 360, "大部门查询", null, null, null, true, null, null, true, false, false);
    //ymPrompt.win('/Search/Company/Company.aspx', 255, 360, "公司查询", null, null, null, true, null, null, true, false, false);

    ymPrompt.win('/Process/ADF/ADFRuleMange/Search/Search.aspx?ruleId=' + ruleId, 255, 360, "选择可选参数", null, null, null, true, null, null, true, false, false);
    name = "ParameterSearch";
}
///////////wfl添加end

//根据离职人员查询上级领导

function GetEmployeeBoss(leaveViewBoss) {
    if ($(leaveid).val() != "") {
        var param = "leaveEmployeeCode=" + $(leaveid).val();
        $.ajax({
            type: "POST",
            url: "/Search/LeveEmployee/LeaveViewBoss.ashx",
            data: param,
            success: function (data) {
                $(leaveViewBoss).val(data);
            },
            error: function () {
                $(leaveViewBoss).val("");
            }
        });
    }
}



//选择员工用以查询下属
//主管
function ShiftEmployeeCheck(VSupervisor, VSupervisorid, VSPosition, VSEmail, VSDepartment, VSdivOne, VSdivTwo) {
    Supervisor = VSupervisor;
    Supervisorid = VSupervisorid;
    SPosition = VSPosition;
    SEmail = VSEmail;
    SDepartment = VSDepartment;
    SdivOne = VSdivOne;
    SdivTwo = VSdivTwo;

    ymPrompt.win("/Search/ShiftEmployee/ShiftEmployeeCheck.aspx", 500, 508, '员工查询', null, null, null, true, null, null, true, false, true);
}

//选择行后 返回员工基本数据 在执行其它查询赋值数据
function ClickShiftRow() {
    var code = $(this).find("td:eq(0)").text(); //员工编号
    var name = $(this).find("td:eq(1)").text(); //员工姓名
    var ment = $(this).find("td:eq(2)").text(); //机构

    var vmail = $(this).find("td:eq(0)").find("#hidmail").val();
    var vleve = $(this).find("td:eq(0)").find("#hidleve").val();
    $(Supervisor).val(name + "(" + code + ")");
    $(Supervisorid).val(code);
    $(SDepartment).val(ment);
    $(SEmail).val(vmail);
    $(SPosition).val(vleve);
    ymPrompt.close();

    DivShowOne(code);
    DivShowTwo(code);
}
//查找直接主管下的员工
function DivShowOne(varEmoployeeCode) {
    var param = "param=" + varEmoployeeCode + "&flag=1";
    $.ajax({
        type: "POST",
        url: "../Search/ShiftEmployee/ShiftDivOne.ashx",
        data: param,
        async: false,
        success: function (data) {
            $(SdivOne).empty();
            if (data.length > 0) {
                $(SdivOne).append(data);
            }
        },
        error: function () {
            top.window.alert({ title: '提示信息', message: '数据加载错误！' });
        }
    });
}


//查找直接主管下的员工
function DivShowTwo(varEmoployeeCode) {
    var param = "param=" + varEmoployeeCode + "&flag=2";
    $.ajax({
        type: "POST",
        url: "../Search/ShiftEmployee/ShiftDivOne.ashx",
        data: param,
        async: false,
        success: function (data) {
            $(SdivTwo).empty();
            if (data.length > 0) {
                $(SdivTwo).append(data);
            }
        },
        error: function () {
            top.window.alert({ title: '提示信息', message: '数据加载错误！' });
        }
    });
}

//2011-11-26采购wfl添加
function PMSmachine(btn, formid, applyType, page) {
    pmsbtn = btn;
    PMSformID = formid;
    applyTypeCode = applyType;
    isPage = page;
    ymPrompt.win("/Process/PMS/PMSMachineApplyInset.aspx?FormID=" + PMSformID + "&applyType=" + applyTypeCode + "&page=" + isPage, 780, 500, '整机添加', null, null, null, true, null, null, true, false, true);
}

function PMSmachineEdit(btn, formid, applyType, gID, page) {
    pmsbtn = btn;
    PMSformID = formid;
    applyTypeCode = applyType;
    machineID = gID;
    isPage = page;
    ymPrompt.win("/Process/PMS/PMSMachineApplyEdit.aspx?FormID=" + PMSformID + "&applyType=" + applyTypeCode + "&machineID=" + machineID + "&page=" + isPage, 780, 500, '整机修改', null, null, null, true, null, null, true, false, true);
}

//添加
function PMSMachineAdd() {
    $(pmsbtn)[0].click();
    //$("#StartPMS_allPrice").val($("#StartPMS_machihidPrice").val());
    top.window.ymPrompt.close();
}

//继续添加
function PMSMachineAddgo() {
    $(pmsbtn)[0].click();
    $("#StartPMS_allPrice").val($("#StartPMS_machihidPrice").val());
}



//网络设备
function PMSnetWorkadd(btn, formid, applyType, page) {
    pmsbtn = btn;
    PMSformID = formid;
    applyTypeCode = applyType;
    isPage = page;
    ymPrompt.win("/Process/PMS/PMSNetWorkApplyInsert.aspx?formID=" + PMSformID + "&applyTypeCode=" + applyTypeCode + "&page=" + isPage, 725, 420, '配件和网络设备添加', null, null, null, true, null, null, true, false, true);
}

//pay
function PMSpayadd(btn, reUrl) {
    paybtn = btn;
    top.ymPrompt.win(reUrl, 720, 330, '编辑付款信息', null, null, null, true, null, null, true, false, true);
}

//pay
function PMSpayaddSearch() {
    $(paybtn)[0].click();
    top.window.ymPrompt.close();
}


function PMSnetWorkEdit(btn, formid, applyType, gID, page) {
    pmsbtn = btn;
    PMSformID = formid;
    applyTypeCode = applyType;
    machineID = gID;
    isPage = page;
    ymPrompt.win("/Process/PMS/PMSNetWorkApplyEdit.aspx?FormID=" + PMSformID + "&applyType=" + applyTypeCode + "&machineID=" + machineID + "&page=" + isPage, 725, 420, '配件和网络设备修改', null, null, null, true, null, null, true, false, true);
}

//添加
function PMSMnetWorkAdd() {
    $(pmsbtn).click();
    //$("#StartPMS_allPrice").val($("#StartPMS_machihidPrice").val());
    top.window.ymPrompt.close();
}

//继续添加
function PMSMnetWorkAddgo() {
    $(pmsbtn).click();
    //$("#StartPMS_allPrice").val($("#StartPMS_machihidPrice").val());
}



//RFID部门
function Department(txtCtrl, txtHidCtrl) {
    ctrl = txtCtrl;
    hidCtrl = txtHidCtrl;
    ymPrompt.win('/Process/PMS/ESConfirm/RFIDDepartment.aspx', 255, 360, "部门查询", null, null, null, true, null, null, true, false, false);

}