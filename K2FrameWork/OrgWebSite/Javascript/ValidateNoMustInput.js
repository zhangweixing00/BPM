/*
**通用文本验证Jquery版 不验证必填项
**Create by Jeff
**2011-06-17
**说明：此版本基于Jquery编写，要预先引入Jquery库文件
**参数说明：
1.buttonID:发起页面验证的按钮ID; 多按钮可以用";"分割
2.lostFocus:是否在控件失去焦点时发起验证 (true：是,false：否)
3.FaultCss：控件中信息发生错误时切换的Css 如果没有则可以给""值
**使用说明：
1.在页面的onload事件中初始化 验证对象 例：$(function () { var iva = new InputValidate(buttonID = "button1", lostFocus = false, FaultCss = ""); })
2.给页面上待验证的控件标记属性regex,多条验证规则以";"分割,格式为【验证规则:错误提示信息;验证规则:提示错误信息...】 例:<input type="text" regex="mustinput:必须输入邮箱;email:邮箱格式不正确" id="email"/>
3.时间比较验证格式 checkData:id1*id2,wrongMessage; 例 <input id="OnboardDate"  regex="checkData:OnboardDate*ContractEnd,合同结束日期必须大于入职日期;"  type="text" />
验证规则标记说明: 
email:验证邮箱
url:验证网址
postnumber:验证邮编
area:验证区号
money:验证货币
cess:验证税率
card:身份证号
date:验证时间
mobile:验证手机
tel:验证网址
mustint:验证是正整数
mustinput:必填
mustNumber:限制文本框必须填入数字 
checkData:验证时间大小;    
*/
//function alert(msg) {
//    top.window.ymPrompt.alert(msg.replace(/\n/g, "<br/>"));
//}
/*验证器构造函数*/
function InputValidateNoMustInput() {
    //验证发起的按钮ID
    var buttonID;
    //是否在焦点失去时验证
    var lostFocus = true;
    //验证控件错误时切换的Class名称
    var FaultCss;
    this.Init();
}
InputValidateNoMustInput.prototype.Init = function () {
    var faultCss = FaultCss;
    var inputcontrols = $("[regex]");
    $.each(inputcontrols, function (index, Value) {
        var inputid = $(Value).attr("id");
        var Rules = $(Value).attr("regex").split(";");
        $.each(Rules, function (i, Value) {
            if (Value != "") {
                var ruleArry = Value.split(":");
                switch (ruleArry[0]) {
                    //邮箱                                                                          
                    case "email":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isEmail($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "") {
                                            $(this).addClass(faultCss);
                                        }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //网址                                                                          
                    case "url":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isURL2($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //邮编                                                                          
                    case "postnumber":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isRightPostCode($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //区号                                                                          
                    case "area":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (area($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //货币                                                                          
                    case "money":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isMoney($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //税率                                                                         
                    case "cess":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isCess($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //身份证号                                                                         
                    case "card":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isIdCard($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //时间                                                                         
                    case "date":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (IsDateString($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //手机                                                                        
                    case "mobile":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isMobile($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //电话号码                                                                                                                                  
                    case "mustint":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (IsInteger($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //电话号码                                                                        
                    case "tel":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($(this).val() != "") {
                                    if (isTel($(this).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $(this).addClass(faultCss); }
                                        alert(ruleArry[1]);
                                        $(this).focus();
                                        event.returnValue = false;
                                        return false;
                                    }
                                }
                            });
                        }
                        break;
                    //时间比较         
                    case "checkData":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                var idstart = (ruleArry[1].split(",")[0]).split("*")[0];
                                var idend = (ruleArry[1].split(",")[0]).split("*")[1];
                                if (CheckDate(idstart, idend)) {
                                    if (faultCss != "") {
                                        $("#" + inputid).removeClass(faultCss);
                                        $("#" + inputid).parent().removeClass(faultCss);
                                        $("#" + inputid).parent().addClass("conter_right_list_input_bg");
                                    }
                                }
                                else {
                                    if (faultCss != "") {
                                        $("#" + inputid).addClass(faultCss);
                                        $("#" + inputid).parent().removeClass("conter_right_list_input_bg");
                                        $("#" + inputid).parent().addClass(faultCss);
                                    }
                                    alert(ruleArry[1].split(",")[1] + "\n");
                                    $(this).focus();
                                    event.returnValue = false;
                                    return false;
                                }
                            });
                        }
                        break;

                    //必须输入数字                                                                          
                    case "mustNumber":
                        $("#" + inputid).bind("keypress", MustNumber);
                        break;
                    default:
                        break;
                }
            }
        })

    });
    /*
    绑定验证按钮
    */
    var buttonIds = buttonID.split(";")
    $.each(buttonIds, function (index, Value) {
        if (Value == "") { return; }
        $("#" + Value).bind("click", function () {
            var wrongMessage = "";
            $.each(inputcontrols, function (index, Value) {
                var inputid = $(Value).attr("id");
                var Rules = $(Value).attr("regex").split(";");
                $.each(Rules, function (i, Value) {
                    if (Value != "") {
                        var ruleArry = Value.split(":");
                        switch (ruleArry[0]) {
                            //邮箱                                                                                 
                            case "email":
                                if ($("#" + inputid).val() != "") {
                                    if (isEmail($("#" + inputid).val())) {
                                        if (faultCss != "") { $(this).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";


                                    }
                                }
                                break;
                            //网址                                                                                 
                            case "url":
                                if ($("#" + inputid).val() != "") {
                                    if (isURL2($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //邮编                                                                                 
                            case "postnumber":
                                if ($(this).val() != "") {
                                    if (isRightPostCode($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += (ruleArry[1]) + "\n";
                                    }

                                }
                                break;
                            //区号                                                                                 
                            case "area":
                                if ($("#" + inputid).val() != "") {
                                    if (area($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }
                                }
                                break;
                            //货币                                                                                 
                            case "money":
                                if ($("#" + inputid).val() != "") {
                                    if (isMoney($("#" + inputid).val())) {
                                        if (faultCss != "") {
                                            $("#" + inputid).parent().removeClass(faultCss);
                                            $("#" + inputid).parent().addClass("conter_right_list_input_bg");
                                        }
                                    }
                                    else {
                                        if (faultCss != "") {
                                            $("#" + inputid).parent().removeClass("conter_right_list_input_bg");
                                            $("#" + inputid).addClass("inputreadonly");
                                            $("#" + inputid).parent().addClass(faultCss);
                                        }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //税率                                                                                
                            case "cess":
                                if ($("#" + inputid).val() != "") {
                                    if (isCess($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //身份证号                                                                                
                            case "card":
                                if ($("#" + inputid).val() != "") {
                                    if (isIdCard($("#" + inputid).val())) {
                                        if (faultCss != "") {
                                            $("#" + inputid).parent().removeClass(faultCss);
                                            $("#" + inputid).parent().addClass("conter_right_list_input_bg");
                                        }
                                    }
                                    else {
                                        if (faultCss != "") {
                                            $("#" + inputid).parent().removeClass("conter_right_list_input_bg");
                                            $("#" + inputid).addClass("inputreadonly");
                                            $("#" + inputid).parent().addClass(faultCss);
                                        }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //时间                                                                                
                            case "date":
                                if ($("#" + inputid).val() != "") {
                                    if (IsDateString($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //手机                                                                               
                            case "mobile":
                                if ($("#" + inputid).val() != "") {
                                    if (isMobile($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }
                                }
                                break;
                            //正整数                                                                                                                                         
                            case "mustint":
                                if ($("#" + inputid).val() != "") {
                                    if (IsInteger($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }
                                }
                                break;
                            //电话号码                                                                               
                            case "tel":
                                if ($(this).val() != "") {
                                    if (isTel($("#" + inputid).val())) {
                                        if (faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                                    }
                                    else {
                                        if (faultCss != "")
                                        { $("#" + inputid).addClass(faultCss); }
                                        wrongMessage += ruleArry[1] + "\n";
                                    }

                                }
                                break;
                            //时间比较       
                            //                            case "checkData":   

                            //                                var idstart = (ruleArry[1].split(",")[0]).split("*")[0];   
                            //                                var idend = (ruleArry[1].split(",")[0]).split("*")[1];   
                            //                                if (CheckDate(idstart, idend)) {   
                            //                                    if (faultCss != "") {   
                            //                                        $("#" + inputid).removeClass(faultCss);   
                            //                                        $("#" + inputid).parent().removeClass(faultCss);   
                            //                                        $("#" + inputid).parent().addClass("conter_right_list_input_bg");   
                            //                                    }   
                            //                                }   
                            //                                else {   
                            //                                    if (faultCss != "") {   
                            //                                        $("#" + inputid).addClass(faultCss);   
                            //                                        $("#" + inputid).parent().removeClass("conter_right_list_input_bg");   
                            //                                        $("#" + inputid).parent().addClass(faultCss);   
                            //                                    }   
                            //                                    wrongMessage += ruleArry[1].split(",")[1] + "\n";   
                            //                                }   
                            //                                break;   
                            default:
                                break;
                        }
                    }
                })

            });
            if ($.trim(wrongMessage) != "") {
                alert(wrongMessage);
                return false;
            }
        });
    });


}

//验证是否为正整数
function IsInteger(value) {
    var re = /^[0-9]+[0-9]*]*$/;    //判断字符串是否为数字      //判断正整数 /^[1-9]+[0-9]*]*$/   
    if (!re.test(value)) {
        return false;
    }
    return true;
}



