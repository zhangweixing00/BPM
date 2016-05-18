
//CheckBoxList只选择一项 
//yanghechun 2014-09-18
function selectOneCheckList(key) {
    $("input[id*=" + key + "]").each(function (index) {
        $("#" + this.id).unbind("click");
        $("#" + $("input[id*=" + key + "]")[index].id).bind('click', function () {
            var currid = this.id;
            $("input[id*=" + key + "]").each(function (ind) {
                if (this.id != currid) {
                    // $("#" + this.id).attr('checked', false);
                   //2015.8.12因为测试中发现在火狐浏览器中单选是成功的，但是在IE浏览器中是不成功的，所以更改之前的脚本为下面新的脚本
                    $("#" + this.id).removeAttr("checked");
                }
            });
        });
    });
}

//获取Url参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//禁用按钮
//2015-4-3
function disabledButton(btn)
{
    if (document.getElementById(btn) != null) {
        document.getElementById(btn).disabled = 'disabled'
    }
}

//启用按钮
//2015-4-3
function enableButton(btn) {
    if (document.getElementById(btn) != null) {
        document.getElementById(btn).disabled = ''
    }
}
