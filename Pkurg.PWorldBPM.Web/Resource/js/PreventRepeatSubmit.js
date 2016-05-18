$(function () {
    $("#scroll_button").find("a[href^='javascript:__doPostBack']").each(function () {
        $(this).click(function () {
            if ($(this).hasClass("disable")) {
                alert("系统处理中，请稍后");
                return false;
            }
            $("#scroll_button").find("a").each(function () {
                $(this).attr("disabled", "disabled");
            });
            $("a[disabled]").addClass("disable");
            //            alert("1111111");
            console.log("11111");
            __doPostBack($(this).attr("id"), "");
//            alert("222222");
        });
    });
});