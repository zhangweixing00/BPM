
var qhBtn = true;
var nIndex0 = 0;


var bFnBtn0 = true;



function daiBanListFn(sUserencode, nPageindex, bBtn, qh_num) {
    var model0 = { "userencode": sUserencode, "pageindex": nPageindex };
    var oUl0 = $(".daiBan0 ul");

    CheckLogin();

    $.ajax({
        type: "POST",
        url: activeUrl + "app.ashx?action=gettodolist&ref=mobile",
        dataType: "jsonp",
        jsonp: "callback",
        jsonpCallback: "handler",
        data: model0,
        beforeSend: function () {
            $(".loadMore0").find("b").html("加载中<i></i>");
        },
        complete: function () {

        },
        success: function (data) {

            //todo:
            if (data) {


                if (nPageindex == 0 && data.length == 0) {

                    $(".loadMore0").find("b").html("暂无记录");
                    $(".loadMore0").unbind("click");

                } else if (data.length == 0) {

                    $(".loadMore0").find("b").html("没有更多待办事项");
                    $(".loadMore0").unbind("click");

                }
                else {
                    $(".loadMore0").find("b").html("加载更多");
                }

                if (data.length == 0) {
                    bFnBtn0 = false;

                    qhBtn = true;


                    return;
                }



                var str0 = "";



                for (var i = 0; i < data.length; i++) {

                    nIndex0++;

                    str0 += "<li><a href=\"Details.html?usercode=" + GetQueryString("usercode") + "&pageindex=0&username=" + uniencode(GetQueryString("username")) + "&companyname=" + uniencode(GetQueryString("companyname")) + "&deptname=" + uniencode(GetQueryString("deptname")) + "&userencode=" + sUserencode + "&id=" + data[i].InstanceId + "&sn=" + data[i].SN + "&qh_num=" + qh_num + "\"><p class=\"p1\"><span class=\"num\">" + nIndex0 + "</span>" + data[i].FormTitle + "</p><p class=\"p2\">发起人：" + data[i].CreateByUserName + " </p><p class=\"p3\">接收时间：" + data[i].ReceiveTime + " </p></a></li>";

                }


                if (bBtn) {
                    oUl0.html(str0);
                } else {
                    oUl0.append(str0);
                }


                qhBtn = true;
                bFnBtn0 = true;



            } else {
                alert('获取失败！');
            };

        },
        error: function (a, b, c, d, e) {
            alert('获取失败！');
        }
    });
}



var bFnBtn1 = true;


function yiBanListFn(sUserencode, nPageindex, bBtn, qh_num) {
    var model1 = { "userencode": sUserencode, "pageindex": nPageindex };
    var oUl1 = $(".daiBan1 ul");

    CheckLogin();

    $.ajax({
        type: "POST",
        url: activeUrl + "app.ashx?action=getdonelist&ref=mobile",
        dataType: "jsonp",
        jsonp: "callback",
        jsonpCallback: "handler",
        data: model1,
        beforeSend: function () {
            $(".loadMore1").find("b").html("加载中<i></i>");
        },
        complete: function () {
            
        },
        success: function (data) {

            //todo:
            if (data) {


                if (nPageindex == 0 && data.length == 0) {

                    $(".loadMore1").find("b").html("暂无记录");
                    $(".loadMore1").unbind("click");

                } else if (data.length == 0) {

                    $(".loadMore1").find("b").html("没有更多已办事项");
                    $(".loadMore1").unbind("click");

                }
                else {
                    $(".loadMore1").find("b").html("加载更多");
                }


                if (data.length == 0) {
                    bFnBtn1 = false;

                    qhBtn = true;


                    return;
                }

                var str1 = "";


                for (var i = 0; i < data.length; i++) {

                    nIndex0++;

                    str1 += "<li><a href=\"Details.html?usercode=" + GetQueryString("usercode") + "&pageindex=0&username=" + uniencode(GetQueryString("username")) + "&companyname=" + uniencode(GetQueryString("companyname")) + "&deptname=" + uniencode(GetQueryString("deptname")) + "&userencode=" + sUserencode + "&id=" + data[i].InstanceId + "&qh_num=" + qh_num + "\"><p class=\"p1\"><span class=\"num\">" + nIndex0 + "</span>" + data[i].FormTitle + "</p><p class=\"p2\">待处理人：" + data[i].TodoUser + " </p><p class=\"p3\">处理时间：" + data[i].ApproveAtTime + " </p></a></li>";

                }


                if (bBtn) {
                    oUl1.html(str1);
                } else {
                    oUl1.append(str1);
                }

                qhBtn = true;
                bFnBtn1 = true;



            } else {
                alert('获取失败！');
            };

        },
        error: function (a, b, c, d, e) {
            alert('获取失败！');
        }
    });
}




var bFnBtn2 = true;



function guiDangListFn(sUserencode, nPageindex, bBtn, qh_num) {
    var model2 = { "userencode": sUserencode, "pageindex": nPageindex };
    var oUl2 = $(".daiBan2 ul");

    CheckLogin();

    $.ajax({
        type: "POST",
        url: activeUrl + "app.ashx?action=getarchivelist&ref=mobile",
        dataType: "jsonp",
        jsonp: "callback",
        jsonpCallback: "handler",
        data: model2,
        beforeSend: function () {
            $(".loadMore2").find("b").html("加载中<i></i>");
        },
        complete: function () {
         
        },
        success: function (data) {

            //todo:
            if (data) {

                if (nPageindex == 0 && data.length == 0) {

                    $(".loadMore2").find("b").html("暂无记录");
                    $(".loadMore2").unbind("click");

                } else if (data.length == 0) {

                    $(".loadMore2").find("b").html("没有更多归档事项");
                    $(".loadMore2").unbind("click");

                }
                else {
                    $(".loadMore2").find("b").html("加载更多");
                }

                if (data.length == 0) {
                    bFnBtn2 = false;

                    qhBtn = true;



                    return;
                }

                var str2 = "";


                for (var i = 0; i < data.length; i++) {

                    nIndex0++;

                    str2 += "<li><a href=\"Details.html?usercode=" + GetQueryString("usercode") + "&pageindex=0&username=" + uniencode(GetQueryString("username")) + "&companyname=" + uniencode(GetQueryString("companyname")) + "&deptname=" + uniencode(GetQueryString("deptname")) + "&userencode=" + sUserencode + "&id=" + data[i].InstanceId + "&qh_num=" + qh_num + "\"><p class=\"p1\"><span class=\"num\">" + nIndex0 + "</span>" + data[i].FormTitle + "</p><p class=\"p2\">发起人：" + data[i].CreatorName + " </p><p class=\"p3\">时间：" + data[i].StartTime + " - " + data[i].EndTime + "</p></a></li>";

                }


                if (bBtn) {
                    oUl2.html(str2);
                } else {
                    oUl2.append(str2);
                }

                qhBtn = true;
                bFnBtn2 = true;


            } else {
                alert('获取失败！');
            };

        },
        error: function (a, b, c, d, e) {
            alert('获取失败！');
        }
    });
}

$(function () {



    var sUsername = GetQueryString("username");
    $(".gr_username").html(sUsername);

    var sUsercode = GetQueryString("usercode");
    $(".gr_usercode").html(sUsercode);

    var sCompanyname = GetQueryString("companyname");
    $(".gr_companyname").html(sCompanyname);

    var sDeptname = GetQueryString("deptname");
    $(".gr_deptname").html(sDeptname);






    var sUserencode = GetQueryString("userencode");
    var nPageindex = GetQueryString("pageindex");





    /* 待办 - 加载更多 */
    $(".loadMore0").find("b").html("加载中<i></i>");
    $(".loadMore0").bind("click", function () {
        if (bFnBtn0) {

            nPageindex0++;
            daiBanListFn(sUserencode, nPageindex0, false, 0);

        };
        bFnBtn0 = false;
    });


    /* 已办 - 加载更多 */
    $(".loadMore1").find("b").html("加载中<i></i>");
    $(".loadMore1").bind("click", function () {
        if (bFnBtn1) {
            nPageindex1++;
            yiBanListFn(sUserencode, nPageindex1, false, 1);

        };
        bFnBtn1 = false;
    });

    /* 归档 - 加载更多 */
    $(".loadMore2").find("b").html("加载中<i></i>");
    $(".loadMore2").bind("click", function () {
        if (bFnBtn2) {
            nPageindex2++;
            guiDangListFn(sUserencode, nPageindex2, false, 2);

        };
        bFnBtn2 = false;
    });


    var tabsSwiper = new Swiper('#tabs-container', {
        touchRatio: 0,
        speed: 500
    });


    var nPageindex0 = nPageindex;

    var nPageindex1 = nPageindex;

    var nPageindex2 = nPageindex;

    $(".wrap01_nav ul li").click(function () {

        if ($(this).index() == 3) {
            $(this).addClass("active").siblings().removeClass("active");
            tabsSwiper.slideTo($(this).index());

            qhBtn = true;
        }


        if (qhBtn) {


            if ($(this).index() == 0) {

                /* 待办 */
                $(".commonHeader2>h1").text("待办列表");
                $(".commonHeader>h1").text("待办列表");
                qhBtn = false;
                nPageindex0 = 0;
                nIndex0 = 0;
                daiBanListFn(sUserencode, nPageindex0, true, 0);


            } else if ($(this).index() == 1) {

                /* 已办 */
                $(".commonHeader2>h1").text("已办列表");
                $(".commonHeader>h1").text("已办列表");
                qhBtn = false;
                nPageindex1 = 0;
                nIndex0 = 0;
                yiBanListFn(sUserencode, nPageindex1, true, 1);


            } else if ($(this).index() == 2) {

                /* 归档 */
                $(".commonHeader2>h1").text("归档列表");
                $(".commonHeader>h1").text("归档列表");
                qhBtn = false;
                nPageindex2 = 0;
                nIndex0 = 0;
                guiDangListFn(sUserencode, nPageindex2, true, 2);

            }
            else if ($(this).index() == 3) {
                $(".commonHeader2>h1").text("个人中心");
                $(".commonHeader>h1").text("个人中心");
            }

            $(this).addClass("active").siblings().removeClass("active");
            tabsSwiper.slideTo($(this).index());


        }

    });





    var nQh_num = GetQueryString("qh_num");

    if (nQh_num == null) {
        $(".wrap01_nav ul li").eq(0).trigger("click");
    } else {
        $(".wrap01_nav ul li").eq(nQh_num).trigger("click");
    };

});