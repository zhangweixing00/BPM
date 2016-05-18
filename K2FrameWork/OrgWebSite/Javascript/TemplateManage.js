$(document).ready(function () {
    new TempManage();
});

var ManageObj;
function TempManage() {

    ManageObj = this;
    this.Init();

}

TempManage.prototype.Init = function () {
    $("#CompanyName").bind("click", ManageObj.TempCompanyInfo);
    $("#PayrollConmpanyName").bind("click", ManageObj.TempPayrollInfo);
    $("#LocationName").bind("click", ManageObj.TempLocationInfo);
    $("#EmployeeTypeName").bind("click", ManageObj.TempEmployeeTypeInfo);
    $("#Level").bind("click", ManageObj.TempLevelInfo);
    $("#FirstDeptName").bind("click", ManageObj.TempFirstDeptInfo);

    $("#ckbPayroll").bind("click", ManageObj.chkclickPayroll);
    $("#ckbCompanyName").bind("click", ManageObj.chkclickCompanyName);
    $("#ckbLocationName").bind("click", ManageObj.chkclickLocationName);
    $("#ckbEmployeeTypeName").bind("click", ManageObj.chkclickEmployeeTypeName);
    $("#ckbLevel").bind("click", ManageObj.chkclickLevel);
    $("#ckbFirstDeptName").bind("click", ManageObj.chkclickFirstDeptName);


}

//合同签署公司
TempManage.prototype.TempCompanyInfo = function () {
    top.window.TempCompany($("#CompanyName"), $("#Company"));
}

//payroll公司
TempManage.prototype.TempPayrollInfo = function () {
    top.window.TempPayroll($("#PayrollConmpanyName"), $("#PayrollConmpany"));
}

//工作地点
TempManage.prototype.TempLocationInfo = function () {
    top.window.TempLocation($("#LocationName"), $("#Location"));
}

//员工类型
TempManage.prototype.TempEmployeeTypeInfo = function () {
    top.window.TempEmployeeType($("#EmployeeTypeName"), $("#EmployeeType"));
}

//职级
TempManage.prototype.TempLevelInfo = function () {
    top.window.TempLevel($("#Level"));
}

//大部门
TempManage.prototype.TempFirstDeptInfo = function () {
    top.window.TempFirstDept($("#FirstDeptName"), $("#FirstDept"));
}

//清空Payroll数据 移除click
TempManage.prototype.chkclickPayroll = function () {
    if ($("#ckbPayroll").prop("checked") == true) {

        $("#PayrollConmpanyName").val("");
        $("#PayrollConmpany").val("");

        $("#PayrollConmpanyName").addClass("adbackground");

        $("#PayrollConmpanyName").unbind("click");
    }
    else {
        $("#PayrollConmpanyName").bind("click", ManageObj.TempPayrollInfo);
        $("#PayrollConmpanyName").removeClass("adbackground");
    }
}

//清空合同签署公司数据 移除click
TempManage.prototype.chkclickCompanyName = function () {
    if ($("#ckbCompanyName").prop("checked") == true) {

        $("#CompanyName").val("");
        $("#Company").val("");

        $("#CompanyName").addClass("adbackground");

        $("#CompanyName").unbind("click");
    }
    else {
        $("#CompanyName").bind("click", ManageObj.TempCompanyInfo);
        $("#CompanyName").removeClass("adbackground");
    }
}


//清空工作地点数据 移除click
TempManage.prototype.chkclickLocationName = function () {
    if ($("#ckbLocationName").prop("checked") == true) {

        $("#LocationName").val("");
        $("#Location").val("");

        $("#LocationName").addClass("adbackground");

        $("#LocationName").unbind("click");
    }
    else {
        $("#LocationName").bind("click", ManageObj.TempLocationInfo);
        $("#LocationName").removeClass("adbackground");
    }
}

//清空员工类型数据 移除click
TempManage.prototype.chkclickEmployeeTypeName = function () {
    if ($("#ckbEmployeeTypeName").prop("checked") == true) {

        $("#EmployeeTypeName").val("");
        $("#EmployeeType").val("");

        $("#EmployeeTypeName").addClass("adbackground");

        $("#EmployeeTypeName").unbind("click");
    }
    else {
        $("#EmployeeTypeName").bind("click", ManageObj.TempEmployeeTypeInfo);
        $("#EmployeeTypeName").removeClass("adbackground");
    }
}

//清空 职级数据 移除click
TempManage.prototype.chkclickLevel = function () {
    if ($("#ckbLevel").prop("checked") == true) {

        $("#Level").val("");
        $("#Level").unbind("click");

        $("#Level").addClass("adbackground");
    }
    else {
        $("#Level").bind("click", ManageObj.TempLevelInfo);
        $("#Level").removeClass("adbackground");
    }
}

//清空大部门数据 移除click
TempManage.prototype.chkclickFirstDeptName = function () {
    if ($("#ckbFirstDeptName").prop("checked") == true) {

        $("#FirstDeptName").val("");
        $("#FirstDept").val("");
        $("#FirstDeptName").addClass("adbackground");


        $("#FirstDeptName").unbind("click");
    }
    else {
        $("#FirstDeptName").bind("click", ManageObj.TempFirstDeptInfo);
        $("#FirstDeptName").removeClass("adbackground");
    }
}