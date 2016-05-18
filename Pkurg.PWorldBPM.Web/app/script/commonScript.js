/* 转码 */
function uniencode(text) {
    text = escape(text.toString()).replace(/\+/g, "%2B");
    var matches = text.match(/(%([0-9A-F]{2}))/gi);
    if (matches) {
        for (var matchid = 0; matchid < matches.length; matchid++) {
            var code = matches[matchid].substring(1, 3);
            if (parseInt(code, 16) >= 128) {
                text = text.replace(matches[matchid], '%u00' + code);
            }
        }
    }
    text = text.replace('%25', '%u0025');

    return text;
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}


function CheckLogin() {

    if ($.cookie("username") == undefined || $.cookie("username") == "") {
        window.location.href = "login.html?returnBtn=listReturnButton";
        return;
    }
    else if ($.cookie("username").indexOf(GetQueryString("usercode")) < 0) {
        window.location.href = "login.html?returnBtn=listReturnButton";
        return;
    }

}


var activeUrl = "/";

//var activeUrl = "http://zybpm.founder.com/";
//var activeUrl = "http://172.25.20.47/";