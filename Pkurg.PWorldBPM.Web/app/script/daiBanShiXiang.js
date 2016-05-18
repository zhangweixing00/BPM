$(function () {

    var sUsercode = GetQueryString("usercode");
    var sUserencode = GetQueryString("userencode");
    var nPageindex = GetQueryString("pageindex");
    var sUsername = uniencode(GetQueryString("username"));
    var sCompanyname = uniencode(GetQueryString("companyname"));
    var sDeptname = uniencode(GetQueryString("deptname"));

    var nQh_num = GetQueryString("qh_num");

    if (nQh_num == 0) {
        $(".commonHeader2 h1").html("待办事项");
    } else if (nQh_num == 1) {
        $(".commonHeader2 h1").html("已办事项");
    } else if (nQh_num == 2) {
        $(".commonHeader2 h1").html("归档事项");
    }

    if (nQh_num == 0) {
        $(".commonHeader h1").html("待办事项");
    } else if (nQh_num == 1) {
        $(".commonHeader h1").html("已办事项");
    } else if (nQh_num == 2) {
        $(".commonHeader h1").html("归档事项");
    }



    $(".return_btn").attr("href", "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "&qh_num=" + nQh_num + "");


    $(".index_btn").attr("href", "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "");




    /* 待办事项详情 */
    var sInstanceid = GetQueryString("id");
    var model00 = { "id": sInstanceid };

    CheckLogin();

    var aBtns = $(".sCWrapContent1_btns span");


    var sSn = GetQueryString("sn");
    if (sSn != null) {
        $(".daiBanShiXiang .sCWrap_dis").show();
    };

    //不同意
    function disagreeFn() {

        var r = confirm("确定不同意吗？")
        if (r == true) {

            aBtns.eq(0).find("a").addClass("disabled").unbind("click");
            aBtns.eq(1).find("a").addClass("disabled").unbind("click");
            $('#btnAddSign').addClass("disabled").unbind("click");

            var model000 = { "userencode": sUserencode, "sn": sSn, "id": sInstanceid, "result": "N", "remark": $(".sCWrap_content1 textarea").val() };

            $.ajax({
                type: "POST",
                url: activeUrl + "app.ashx?action=approve&ref=mobile",
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "handler",
                data: model000,
                success: function (data) {
                    if (data) {
                        alert("审批成功");
                        window.location.href = "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "&qh_num=" + nQh_num + "";
                        //basicModal.show(popup_true);

                    } else {
                        alert("审批失败");
                        //basicModal.show(popup_false);
                    }
                },
                error: function (a, b, c, d, e) {
                    alert("审批失败");
                    //basicModal.show(popup_false);
                }
            });
        }
    }
    
    //同意
    function agreeFn() {
        aBtns.eq(1).find("a").addClass("disabled").unbind("click");
        aBtns.eq(0).find("a").addClass("disabled").unbind("click");
        $('#btnAddSign').addClass("disabled").unbind("click");

        var model000 = { "userencode": sUserencode, "sn": sSn, "id": sInstanceid, "result": "Y", "remark": $(".sCWrap_content1 textarea").val() };

        $.ajax({
            type: "POST",
            url: activeUrl + "app.ashx?action=approve&ref=mobile",
            dataType: "jsonp",
            jsonp: "callback",
            jsonpCallback: "handler",
            data: model000,
            success: function (data) {
                if (data) {
                    alert("审批成功");
                    window.location.href = "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "&qh_num=" + nQh_num + "";

                    //basicModal.show(popup_true);

                } else {
                    alert("审批失败");
                    //basicModal.show(popup_false);
                }
            },
            error: function (a, b, c, d, e) {
                alert("审批失败");
                //basicModal.show(popup_false);
            }
        });

    }

    //加签
    function addsignFn() {
        aBtns.eq(1).find("a").addClass("disabled").unbind("click");
        aBtns.eq(0).find("a").addClass("disabled").unbind("click");

        var model000 = { "userencode": sUserencode, "sn": sSn, "id": sInstanceid, "tousercode": "被加签域账号", "remark": $(".sCWrap_content1 textarea").val() };

        $.ajax({
            type: "POST",
            url: activeUrl + "app.ashx?action=addsign&ref=mobile",
            dataType: "jsonp",
            jsonp: "callback",
            jsonpCallback: "handler",
            data: model000,
            success: function (data) {
                if (data) {
                    alert("加签成功");
                    window.location.href = "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "&qh_num=" + nQh_num + "";

                    //basicModal.show(popup_true);

                } else {
                    alert("加签失败");
                    //basicModal.show(popup_false);
                }
            },
            error: function (a, b, c, d, e) {
                alert("审批失败");
                //basicModal.show(popup_false);
            }
        });

    }

    /* 不同意 */
    aBtns.eq(0).find("a").bind("click", disagreeFn);


    /* 同意 */
    aBtns.eq(1).find("a").bind("click", agreeFn);


});