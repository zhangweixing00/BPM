$(function () {

    $("#loginBtn").click(function () {

        if ($.trim($('#oa_username').val()) == "") {
            alert("请输入域账号!");
            $('#oa_username').focus();
            return;
        }

        if ($.trim($('#oa_password').val()) == "") {
            alert("请输入密码!");
            $('#oa_password').focus();
            return;
        }

        var model = { "account": $.trim($('#oa_username').val()), "password": $.trim($('#oa_password').val()) };
        $.ajax({
            type: "POST",
            url: activeUrl + "app.ashx?action=login&ref=mobile",
            dataType: "jsonp",
            jsonp: "callback",
            jsonpCallback: "handler",
            data: model,
            beforeSend: function () {
                $("#loginBtn").text("登录中...");
            },
            complete: function () {
                $("#loginBtn").text("登录");
            },
            success: function (data) {
                //todo:
                if (data) {

                    $.cookie("username", $.trim($('#oa_username').val()), { expires: 365 });
                    $.cookie("password", $.trim($('#oa_password').val()), { expires: 365 });

                    var UserName1 = uniencode("" + data.UserName + "");
                    var CompanyName1 = uniencode("" + data.CompanyName + "");
                    var DeptName1 = uniencode("" + data.DeptName + "");

                    window.location.href = "list.html?usercode=" + data.UserCode + "&userencode=" + data.UserEnCode + "&pageindex=0&username=" + UserName1 + "&companyname=" + CompanyName1 + "&deptname=" + DeptName1;
                } else {
                    alert('登录失败！');
                };

            },
            error: function (a, b, c, d, e) {
                alert('登录失败！');
            }
        });


    });

    var sReturnBtn = GetQueryString("returnBtn");
    if (sReturnBtn == "listReturnButton") {

        $.cookie("username", "");
        $.cookie("password", "");

        window.location.href = "login.html";
        return;
    }

    if ($.cookie("username") != undefined && $.cookie("password") != undefined && $.cookie("username") != "" && $.cookie("password") != "") {
        $("#oa_username").val($.cookie("username"));
        $("#oa_password").val($.cookie("password"));

        /*
        //android 自动登录
        var v = GetQueryString("v");
        if (v == "android") {
        $("#loginBtn").trigger("click");
        }
        */

        if (navigator && navigator.userAgent) {
            var userAgentInfo = navigator.userAgent;
            //alert(userAgentInfo);
            //"Android", "iPhone" ,"SymbianOS", "Windows Phone", "iPad", "iPod", "MQQBrowser"
            if (userAgentInfo.indexOf("iPhone") > -1 || userAgentInfo.indexOf("iphone") > -1 || userAgentInfo.indexOf("iPad") > -1) {

            }
            else {
                $("#loginBtn").trigger("click");
            }
        }
    }
});