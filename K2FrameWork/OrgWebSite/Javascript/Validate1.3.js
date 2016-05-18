/*
**通用文本验证Jquery版 
**Create by Jeff
**2011-06-17
**说明：此版本基于Jquery编写，要预先引入Jquery库文件
**参数说明：
1.buttonID:发起页面验证的按钮ID; 多按钮可以用";"分割
2.lostFocus:是否在控件失去焦点时发起验证 (true：是,false：否)
3.FaultCss：控件中信息发生错误时切换的Css 如果没有则可以给""值
**使用说明：
1.在页面的onload事件中初始化 验证对象 例：$(function () { var iva = new InputValidate2(buttonID = "button1", lostFocus = false, FaultCss = ""); })
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
tel:验证电话号码
mustint:验证是正整数
mustinput:必填
checkData:时间比较
mustNumber:限制文本框必须填入数字  
*/
var isOk = false;
var isFirst = true;

function alert(msg) {top.window.ym
    top.window.ymPrompt.alert(msg.replace(/\n/g, ";").split(";")[0]);
}

/*验证器构造函数*/
function InputValidate2() {
    //是否在焦点失去时验证
    var lostFocus = true;
    //验证控件错误时切换的Class名称
    var FaultCss;
    this.Init();
}
InputValidate2.prototype.Init = function () {
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
                        $(this).css("text-align", "right");
                        $(this).keyup(function (evt) {
                            str = $(this).val();
                            evt = (evt) ? evt : ((window.event) ? window.event : "") //兼容IE和Firefox获得keyBoardEvent对象
                            var key = evt.keyCode ? evt.keyCode : evt.which; //兼容IE和Firefox获得keyBoardEvent对象的键值
                            if (key >= 33 && key <= 40) {
                                evt.preventDefault();
                                evt.returnValue = false;
                                return false;
                            }
                            $(this).val(ConvertToMoney(str));
                        });
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
                    //时间大小校验                                                                            
                    case "checkData":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                var idstart = (ruleArry[1].split(",")[0]).split("*")[0];
                                var idend = (ruleArry[1].split(",")[0]).split("*")[1];
                                if (CheckDate(idstart, idend)) {
                                    if (faultCss != "") {
                                        $("#" + inputid).removeClass(faultCss);

                                    }
                                }
                                else {
                                    if (faultCss != "") {
                                        $("#" + inputid).addClass(faultCss);

                                    }
                                    alert(ruleArry[1].split(",")[1] + "\n");
                                    $(this).focus();
                                    event.returnValue = false;
                                    return false;
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
                    //必填                                                                                                                                                 
                    case "mustinput":
                        if (lostFocus) {
                            $("#" + inputid).bind("blur", function () {
                                if ($.trim($(this).val()) != "") {
                                    if (faultCss != "") {
                                        $(this).removeClass(faultCss);
                                    }
                                }
                                else {
                                    if (faultCss != "")
                                    { $(this).addClass(faultCss); }
                                    alert(ruleArry[1]);
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
                    //必须输入字母                                                                                                                                                   
                    case "mustLetter":
                        $("#" + inputid).bind("keypress", MustLetter);
                        break;
                    default:
                        break;
                }
            }
        })

    });
  
}

$(function () {
    //需要验证的button
    var iva = new InputValidate2( lostFocus = false, FaultCss = "wrongMessageCSS");
    ButtonValidate();

})
function ButtonValidate() {
    $("[regexbutton]").each(function () {
        var button = $(this);
        var Rules = button.attr("regexbutton").split(";");
        $.each(Rules, function (i, Value) {
            if (Value != "") {
                var ruleArry = Value.split(":");
                switch (ruleArry[0]) {
                    // 提示用户操作 
                    case "confirm":
                        button.bind("click", function (event) {
                            if (isFirst) {
                                event.preventDefault();
                            }
                            return ValidateConfirm(button.attr("id"), ruleArry[1]);
                        });
                        break;
                    //用户提交按钮 
                    case "submit":
                        button.bind("click", function (event) {
                            if (Validate() == false) {
                                event.preventDefault();
                            }
                        });

                        break;
                }
            }
        });
    });
}
function Validate() {
    var faultCss = 'cssClassName'
    inputcontrols = $("[regex]");
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
                                if (faultCss != "undefind" && faultCss != "") { $(this).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";


                            }
                        }
                        break;
                    //网址                                                                                                                                                 
                    case "url":
                        if ($("#" + inputid).val() != "") {
                            if (isURL2($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }

                        }
                        break;
                    //邮编                                                                                                                                                 
                    case "postnumber":
                        if ($(this).val() != "") {
                            if (isRightPostCode($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += (ruleArry[1]) + "\n";
                            }

                        }
                        break;
                    //区号                                                                                                                                                 
                    case "area":
                        if ($("#" + inputid).val() != "") {
                            if (area($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }
                        }
                        break;
                    //货币                                                                                                                                                 
                    case "money":
                        if ($("#" + inputid).val() != "") {
                            if (isMoney($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") {
                                    $("#" + inputid).removeClass(faultCss);

                                }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "") {
                                    $("#" + inputid).addClass(faultCss);
                                }
                                wrongMessage += ruleArry[1] + "\n";
                            }

                        }
                        break;
                    //税率                                                                                                                                                
                    case "cess":
                        if ($("#" + inputid).val() != "") {
                            if (isCess($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }

                        }
                        break;
                    //身份证号                                                                                                                                                
                    case "card":
                        if ($("#" + inputid).val() != "") {
                            if (isIdCard($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") {
                                    $("#" + inputid).removeClass(faultCss);


                                }
                            }
                            else {
                                if (faultCss != "") {

                                    $("#" + inputid).addClass(faultCss);
                                }
                                wrongMessage += ruleArry[1] + "\n";
                            }

                        }
                        break;
                    //时间                                                                                                                                                
                    case "date":
                        if ($("#" + inputid).val() != "") {
                            if (IsDateString($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }

                        }
                        break;
                    //手机                                                                                                                                               
                    case "mobile":
                        if ($("#" + inputid).val() != "") {
                            if (isMobile($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }
                        }
                        break;
                    //电话号码                                                                                                                                                  
                    case "tel":
                        if ($("#" + inputid).val() != "") {
                            if (isTel($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }
                        }
                        break;
                    //正整数                                                                                                                                                
                    case "mustint":
                        if ($("#" + inputid).val() != "") {
                            if (IsInteger($("#" + inputid).val())) {
                                if (faultCss != "undefind" && faultCss != "") { $("#" + inputid).removeClass(faultCss); }
                            }
                            else {
                                if (faultCss != "undefind" && faultCss != "")
                                { $("#" + inputid).addClass(faultCss); }
                                wrongMessage += ruleArry[1] + "\n";
                            }
                        }
                        break;
                    //必填                                                                                                                                                
                    case "mustinput":
                        if ($.trim($("#" + inputid).val()) != "") {

                            if (faultCss != "undefind" && faultCss != "") {
                                $("#" + inputid).removeClass(faultCss);
                            }
                        }
                        else {

                            if (faultCss != "undefind" && faultCss != "") {
                                $("#" + inputid).addClass(faultCss);

                            }
                            wrongMessage += ruleArry[1] + "\n";
                        }
                        break;

                    //时间比较                                                                                                                                                 
                    case "checkData":
                        var idstart = (ruleArry[1].split(",")[0]).split("*")[0];
                        var idend = (ruleArry[1].split(",")[0]).split("*")[1];
                        if (CheckDate(idstart, idend)) {
                            if (faultCss != "undefind" && faultCss != "") {
                                $("#" + inputid).removeClass(faultCss);
                            }
                        }
                        else {
                            if (faultCss != "undefind" && faultCss != "") {
                                $("#" + inputid).addClass(faultCss);
                            }
                            wrongMessage += ruleArry[1].split(",")[1] + "\n";
                        }
                        break;
                    default:
                        break;
                }
            }
        })

    });
    if ($.trim(wrongMessage) != "") {
        alert(wrongMessage);
        //event.returnValue = false;
        return false;
    } else {
        return true;
    }
}

//验证邮箱
function isEmail(src) {
    var emailPat = /^(.+)@(.+)$/;
    var matchArray = src.match(emailPat);
    if (matchArray == null) {
        return false;
    }
    return true;
}

//验证网址 
function isURL(str_url) {
    var strRegex = "^((https|http|ftp|rtsp|mms)?://)"
  + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //ftp的user@  
        + "(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP形式的URL- 199.194.52.184  
        + "|" // 允许IP和DOMAIN（域名） 
        + "([0-9a-z_!~*'()-]+\.)*" // 域名- www.  
        + "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." // 二级域名  
        + "[a-z]{2,6})" // first level domain- .com or .museum  
        + "(:[0-9]{1,4})?" // 端口- :80  
        + "((/?)|" // a slash isn't required if there is no file name  
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
    var re = new RegExp(strRegex);
    //re.test()         
    if (re.test(str_url)) {
        //        alert("正确");
        return (true);
    } else {
        //        alert("错误");  
        return (false);
    }
}
//第二种验证表达式"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"
function isURL2(str_url) {
    //var strRegex = "^http://(([a-zA-z0-9]|-){1,}\\.){1,}[a-zA-z0-9]{1,}-*"; 
    var strRegex = "^http://(www\.){0,1}.+\.(com|net|cn)$";
    var strRegex2 = "^https://(www\.){0,1}.+\.(com|net|cn)$";
    var re = new RegExp(strRegex);
    var re2 = new RegExp(strRegex2);
    //re.test()         
    if (re.test(str_url) || re2.test(str_url)) {
        //        alert("正确");
        return (true);
    } else {
        //        alert("错误");  
        return (false);
    }
}
//验证区号
function area(src) {
    var phoneAreaNum = /^\d{3,4}$/;
    if (re.test(src)) {
        return true;
    }
    else {
        return false;
    }
}
//这个是判断输入的是否为货币值 最多小数点后4位
function isMoney(src) {
    isMoneys = /^-?\d+\.{0,}\d{0,}$/;
    if (isMoneys.test(src.replace(/,/g, ""))) {
        return true;
    }
}



//这个是判断输入的是税率 最多小数点后2位
function isCess(src) {
    isMoneys = /^\-[0-9]+\.[0-9]{0,2}$/;
    if (isMoneys.test(src)) {
        return true;
    }
}
//这个是判断输入的是否是整数    
function isCount(src) {
    isMoney = /^[0-9]\d*$/;
    if (isMoney.test(src)) {
        return true;
    }
    else {
        return false;
    }
}
//验证时间输入格式    
function IsDateString(sDate) {
    var iaMonthDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
    var iaDate = new Array(3)
    var year, month, day

    if (arguments.length != 1) return false
    iaDate = sDate.toString().split("-")
    if (iaDate.length != 3) return false
    if (iaDate[1].length > 2 || iaDate[2].length > 2) return false

    year = parseFloat(iaDate[0])
    month = parseFloat(iaDate[1])
    day = parseFloat(iaDate[2])

    if (year < 1900 || year > 2100) return false
    if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) iaMonthDays[1] = 29;
    if (month < 1 || month > 12) return false
    if (day < 1 || day > iaMonthDays[month - 1]) return false
    return true
}

function IsDatetime(startTime, endTime) {
    var ass, aD, aS;
    var bss, bD, bS;
    var begin = startTime; //起始时间；
    var over = endTime;      //终止时间； 
    ass = begin.split("-");         //以"-"分割字符串，返回数组；
    aD = new Date(ass[0], ass[1], ass[2]);   //格式化为Date对像;
    aS = aD.getTime(); //得到从 1970 年 1 月 1 日开始计算到 Date 对象中的时间之间的毫秒数
    bss = over.split("-");
    bD = new Date(bss[0], bss[1], bss[2]);
    bS = bD.getTime();
    if (aS > bS) {
        return false;
    }
    else {
        return true;
    }
}
//-------------------------
function regInput(obj, reg, inputStr) {
    var docSel = document.selection.createRange()
    if (docSel.parentElement().tagName != "INPUT") return false
    oSel = docSel.duplicate()
    oSel.text = ""
    var srcRange = obj.createTextRange()
    oSel.setEndPoint("StartToStart", srcRange)
    var str = oSel.text + inputStr + srcRange.text.substr(oSel.text.length)
    return reg.test(str)
}

//验证邮编
function isRightPostCode(str1) {
    var sun = str1;
    var pattern = /^[0-9]{6}$/;
    if (pattern.test(sun) == false) {
        return false;
    }
    else {
        return true;
    }
}

//验证身份证号
function isIdCard(idcard) {
    idcard = idcard.toUpperCase();
    var Errors = new Array(
"验证通过!",
"身份证号码位数不对!",
"身份证号码出生日期超出范围或含有非法字符!",
"身份证号码校验错误!",
"身份证地区非法!"
);
    var area = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }

    var idcard, Y, JYM;
    var S, M;
    var idcard_array = new Array();
    idcard_array = idcard.split("");
    //地区检验
    if (area[parseInt(idcard.substr(0, 2))] == null) {
        //alert(Errors[4]);
        return false;
    }
    //身份号码位数及格式检验
    switch (idcard.length) {
        case 15:
            if ((parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0 || ((parseInt(idcard.substr(6, 2)) + 1900) % 100 == 0 && (parseInt(idcard.substr(6, 2)) + 1900) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$/; //测试出生日期的合法性
            } else {
                ereg = /^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$/; //测试出生日期的合法性
            }
            if (ereg.test(idcard)) return true;
            else {
                //alert(Errors[2]);
                return false;
            }
            break;
        case 18:
            //18位身份号码检测
            //出生日期的合法性检查 
            //闰年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))
            //平年月日:((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))
            if (parseInt(idcard.substr(6, 4)) % 4 == 0 || (parseInt(idcard.substr(6, 4)) % 100 == 0 && parseInt(idcard.substr(6, 4)) % 4 == 0)) {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$/; //闰年出生日期的合法性正则表达式
            } else {
                ereg = /^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$/; //平年出生日期的合法性正则表达式
            }
            if (ereg.test(idcard)) {//测试出生日期的合法性
                //计算校验位
                S = (parseInt(idcard_array[0]) + parseInt(idcard_array[10])) * 7
+ (parseInt(idcard_array[1]) + parseInt(idcard_array[11])) * 9
+ (parseInt(idcard_array[2]) + parseInt(idcard_array[12])) * 10
+ (parseInt(idcard_array[3]) + parseInt(idcard_array[13])) * 5
+ (parseInt(idcard_array[4]) + parseInt(idcard_array[14])) * 8
+ (parseInt(idcard_array[5]) + parseInt(idcard_array[15])) * 4
+ (parseInt(idcard_array[6]) + parseInt(idcard_array[16])) * 2
+ parseInt(idcard_array[7]) * 1
+ parseInt(idcard_array[8]) * 6
+ parseInt(idcard_array[9]) * 3;
                Y = S % 11;
                M = "F";
                JYM = "10X98765432";
                M = JYM.substr(Y, 1); //判断校验位
                if (M == idcard_array[17]) return true; //检测ID的校验位
                else {
                    //alert(Errors[3]);
                    return false;
                }
            }
            else {
                //alert(Errors[2]);
                return false;
            }
            break;
        default:
            //alert(Errors[1]);
            return false;
            break;
    }
}
//验证手机号
function isMobile(infor) {
    var reg = /^0*(13|15)\d{9}$/;
    if (reg.test(infor)) {
        return true;
    }
}
//验证电话
function isTel(tel) {
    var str = tel;
    var Expression = /^\d+(\.\d+)?$/;
    var objExp = new RegExp(Expression);
    if (objExp.test(str) == true) {
        return true;
    } else {
        return false;
    }
}
//必须输入数字
function MustNumber()
{ if (((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46)) { event.returnValue = true; } else { event.returnValue = false; } }

//必须输入字母

function MustLetter() {
    if (((event.keyCode >= 65) && (event.keyCode <= 90)) || (event.keyCode == 46)) { event.returnValue = true; } else { event.returnValue = false; }
}

//比较时间
function CheckDate(bdate, edate) {
    var bvalue = document.getElementById(bdate).value;
    var evalue = document.getElementById(edate).value;
    if (bvalue != null && bvalue != "undefind" && evalue != null && evalue != "undefind") {
        if (bvalue.length > 9 && evalue.length > 9) {
            var bvalus = bvalue.split("-");
            var evalus = evalue.split("-");
            var d1 = new Date(bvalus[0], bvalus[1], bvalus[2]);
            var d2 = new Date(evalus[0], evalus[1], evalus[2]);
            if (Date.parse(d1) - Date.parse(d2) >= 0) {
                //alert("开始时间应该大于结束时间");
                return false;
            }
            else {
                return true;
            }
        }
    }
    else {
        return true;
    }
}

//验证是否为正整数
function IsInteger(value) {
    var re = /^[0-9]+[0-9]*]*$/;    //判断字符串是否为数字      //判断正整数 /^[1-9]+[0-9]*]*$/   
    if (!re.test(value)) {
        return false;
    }
    return true;
}


function ValidateConfirm(button, msg) {

    if (isOk == true && isFirst == false) {
        isOk = false;
        isFirst = true;
        return true;
    }
    top.window.ymPrompt.confirmInfo({ title: '提示',
        message: msg,

        handler: function ConFirm(tp) {
            if (tp == 'ok') {
                isOk = true;
                isFirst = false;
                $("#" + button).click();
            }

        }
    });
    return isOk;
}
function ConvertToMoney(amtStr) {
    var a, renum = '';
    var j = 0;
    var a1 = '', a2 = '', a3 = '';
    var tes = /^-/;
    a = amtStr.replace(/,/g, "");
    a = a.replace(/[^-\.,0-9]/g, ""); //删除无效字符
    a = a.replace(/(^\s*)|(\s*$)/g, ""); //trim 
    if (tes.test(a)) a1 = '-';
    else a1 = '';
    a = a.replace(/-/g, "");
    if (a != "0" && a.substr(0, 2) != "0.") a = a.replace(/^0*/g, "");
    j = a.indexOf('.'); if (j < 0) j = a.length; a2 = a.substr(0, j); a3 = a.substr(j); j = 0;
    for (i = a2.length; i > 3; i = i - 3) {
        renum = "," + a2.substr(i - 3, 3) + renum;
        j++;
    }
    renum = a1 + a2.substr(0, a2.length - j * 3) + renum + a3;

    return renum;
}