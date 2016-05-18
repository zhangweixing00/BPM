$(document).ready(function () {
    new ClickCheckbox();
})

var ClickCheckboxObj;
function ClickCheckbox() {
    ClickCheckboxObj = this;
    this.Init();
}

ClickCheckbox.prototype.Init = function () {
    check_All(); //全选
    check_Other(); //单选
}
//全选
function check_All() {
    $("#ckb_All").click(function () {
        if ($(this).prop("checked") == true) {
            $(":checkbox", $("#MyWorklist1_gvMyWorkList")).each(function () {
                //wangfenglong添加2011-1-16 begin
                if ($(this).prop("disabled") == true) {
                    $(this).prop("checked", false);
                }
                else {
                    $(this).prop("checked", true);
                }
                //wangfenglong添加2011-1-16 end
            });
        }
        else {
            $(":checkbox").each(function () {
                $(this).prop("checked", false);
            });
        }
    });
}
//全选 parentContainer是GridView的ID，chkallname是全选框的ID 
//CreateBy：wuweimin 2011-11-17
function allCheck(parentContainer, chkallname) {
    if ($("#" + parentContainer + " [id=" + chkallname + "]").prop("checked"))
    //2012-01-13 WuWeiMin 选择符中加了 :enabled这样不会选中不可用的CheckBox
        $("#" + parentContainer + " :enabled:checkbox").prop("checked", "true");
    else {
        $("#" + parentContainer + " :checkbox").prop("checked", "");
    }

}

//单选择
function check_Other() {
    $(":checkbox", $("#GridView1")).each(function () {
        if ($(this).attr("id") != "ckb_All") {
            $(this).click(function () {
                var flage = true;
                $(":checkbox", $("#GridView1")).each(function () {
                    if ($(this).attr("id") != "ckb_All") {
                        if ($(this).prop("checked") == false) {
                            flage = false;
                        }
                    }
                });
                if (flage) {
                    $("#ckb_All").prop("checked", true);
                }
                else {
                    $("#ckb_All").removeAttr("checked");
                }
            });
        }
    });
}
function CheckAll() {
    $(":checkbox").attr("checked", $("#ckb_All").attr("checked") == "checked" ? $("#ckb_All").attr("checked") == "checked" : false);
}