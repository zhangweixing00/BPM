$(document).ready(function () {
    new FunctionClientClass();
});

var FunctionClientObj;
function FunctionClientClass() {
    FunctionClientObj = this;
    this.Init();
}

FunctionClientClass.prototype.Init = function () {
    //大部门
    $("#txtFirstPartment").bind("click", this.ClickClientFirstDepartment);

    //中部门
    $("#txtSecontPartment").bind("click", this.ClickClientSecondDepartment);

    //小部门
    $("#txtThirdPartment").bind("click", this.ClickClientThirdDepartment);

    //主管
    $("#CompetentName").bind("click", this.ClickClientEmployeeCheck);

    //公司
    $("#CompanyName").bind("click", this.ClickClientCompany);

    //合同签署公司
    $("#ContractCompanyName").bind("click", this.ClickClientContractCompany);

    //职位职级职位详细
    $("#ipt_Level").bind("click", this.ClickClientPostionRelation);

    //邮箱检索
    $("#ipt_GetEmailAdd").bind("click", this.EmailClientSearch);

    //邮箱检索
    $("#ipt_GetEmailTwo").bind("click", this.EmailClientSearchTwo);

    //邮箱检索
    $("#ipt_GetEmailThree").bind("click", this.EmailClientSearchThree);
}

//大部门
FunctionClientClass.prototype.ClickClientFirstDepartment = function () {
    top.window.FirstDeptCode($("#txtFirstPartment"), $("#hidFirstPartment"));
}

//中部门
FunctionClientClass.prototype.ClickClientSecondDepartment = function () {
    top.window.SecondDeptCode($("#txtSecontPartment"), $("#hidSecontPartment"));
}

//小部门
FunctionClientClass.prototype.ClickClientThirdDepartment = function () {
    top.window.ThirdDeptCode($("#txtThirdPartment"), $("#hidThirdPartment"));
}

//主管 有参数
FunctionClientClass.prototype.ClickClientEmployeeCheck = function (varEmployeeCode) {
    top.window.EmployeeCheck($("#CompetentName"), $("#CompetentCode"), $("").text());
}

//公司
FunctionClientClass.prototype.ClickClientCompany = function () {
    top.window.CompanyCode($("#CompanyName"), $("#CompanyCode"));
}

//合同签署公司
FunctionClientClass.prototype.ClickClientContractCompany = function () {
    top.window.ConCompanyCode($("#ContractCompanyName"), $("#ContractCompanyCode"));
}

//职位职级职位详细
FunctionClientClass.prototype.ClickClientPostionRelation = function () {
    top.window.Position($("#ipt_Level"), $("#hid_Level"), null, null, null, null, true, false, false);
}

//邮件检索
FunctionClientClass.prototype.EmailClientSearch = function () {
    var varmail = $("#divMail").val();

    //top.window.EmailImgeSearch($("#divMail"), null, false, false, true, varmail);
    top.window.EmailDimension($("#divMail"), null, false, false, true, varmail);
}

//邮件检索
FunctionClientClass.prototype.EmailClientSearchTwo = function () {
    //    top.window.EmailImgeSearch($("#divMailTwo"), null, false, false, true);
    var varmail = $("#divMailTwo").val();

    top.window.EmailImgeSearch($("#divMailTwo"), null, false, false, true, varmail);
}

//邮件检索
FunctionClientClass.prototype.EmailClientSearchThree = function () {
    // top.window.EmailImgeSearch($("#divMailThree"), null, false, false, true);
    var varmail = $("#divMailThree").val();

    top.window.EmailImgeSearch($("#divMailThree"), null, false, false, true, varmail);
}