var Sys = {};
var ua = navigator.userAgent.toLowerCase();
var s;
(s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
(s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
(s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
(s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
(s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

//if (Sys.ie) document.write('IE: ' + Sys.ie);
//if (Sys.firefox) document.write('Firefox: ' + Sys.firefox);
//if (Sys.chrome) document.write('Chrome: ' + Sys.chrome);
//if (Sys.opera) document.write('Opera: ' + Sys.opera);
//if (Sys.safari) document.write('Safari: ' + Sys.safari);

function W() {
    return window.screen.width;
}

function H() {
    return window.screen.height < document.body.scrollHeight ? document.body.scrollHeight : window.screen.height;
}

function ShowDiv(str, state) {
    var msgw, msgh, bordercolor;
    msgw = 400; //提示窗口的宽度 
    msgh = 150; //提示窗口的高度 
    bordercolor = "#336699"; //提示窗口的边框颜色 
    titlecolor = "#99CCFF"; //提示窗口的标题颜色 

    var sWidth, sHeight;
    sWidth = W();
    sHeight = H(); //document.documentElement.clientHeight; 

    var bgObj = document.createElement("div");
    bgObj.setAttribute('id', 'bgDiv');
    bgObj.style.position = "absolute";
    bgObj.style.top = "0";
    bgObj.style.background = "#777";
    bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
    bgObj.style.opacity = "0.6";
    bgObj.style.left = "0";
    //bgObj.style.width=sWidth + "px"; 
    //bgObj.style.height=sHeight + "px"; 
    document.body.appendChild(bgObj);
    var msgObj = document.createElement("div")
    msgObj.setAttribute("id", "msgDiv");
    msgObj.setAttribute("align", "center");
    msgObj.style.position = "absolute";
    msgObj.style.background = "white";
    msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
    msgObj.style.border = "1px solid " + bordercolor;
    msgObj.style.width = msgw + "px";
    msgObj.style.height = msgh + "px";
    //msgObj.style.top=(document.body.clientHeight/2) + "px"; //+ (sHeight-msgh)/2) 
    //msgObj.style.left=(document.body.clientWidth/2) + "px"; 
    var title = document.createElement("h4");
    title.setAttribute("id", "msgTitle");
    title.setAttribute("align", "right");
    title.style.margin = "0";
    title.style.padding = "3px";
    title.style.background = titlecolor;
    title.style.filter = "progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);";
    title.style.opacity = "0.75";
    title.style.border = "1px solid " + titlecolor;
    title.style.height = "18px";
    title.style.font = "12px Verdana, Geneva, Arial, Helvetica, sans-serif";
    title.style.color = "white";
    title.style.cursor = "pointer";
    title.innerHTML = "关闭Close";

    if (state != "Close")
        title.onclick = closeDiv;
    else
        title.onclick = closeWindow;

    document.body.appendChild(msgObj);
    document.getElementById("msgDiv").appendChild(title);
    var txt = document.createElement("div");
    txt.style.margin = "1em 0"
    txt.style.color = "red";
    txt.setAttribute("id", "msgTxt");
    txt.innerHTML = str;
    document.getElementById("msgDiv").appendChild(txt);
    var btn = document.createElement("div");
    btn.style.margin = "1em 0";
    btn.style.padding = "3px";
    btn.style.background = "white";
    btn.setAttribute("id", "msgBtn");
    btn.innerHTML = "<input type='button' value='关闭' onclick='closeDiv()' />";
    document.getElementById("msgDiv").appendChild(btn);
}

function closeDiv() {
    //window.close();
    //Close Div 
    document.body.removeChild(document.getElementById("bgDiv"));
    document.getElementById("msgDiv").removeChild(document.getElementById("msgTitle"));
    document.body.removeChild(document.getElementById("msgDiv"));
    //window.opener=null;
    //window.open('','_self','');
    //window.close();
}

function closeWindow() {
    window.opener = null;
    window.open('', '_self', '');
    window.close();
}

function MyAlert(txt, state) {
    var shield = GetShieldDiv();
    var strHtml = "<div class='atclose' id='imgclose'></div></td></tr>\n"
    + "<tr><td class=\"alerttxt\" colspan='2'>" + txt + "</td></tr>\n"
    + "<tr><td colspan='2' valign='top' height='41' style=\"text-align:center;\">"
    + "<input type=\"button\" value=\"确定OK\" id=\"do_OK\" class='alert_btn' />";

    var alertFram = GetShieldAlertFram(strHtml);
    var ifm = GetShieldIfm();
    GetShieldMain(shield, alertFram, ifm);
    if (state != "Close") {
        document.getElementById("do_OK").value = "确定OK";
        document.getElementById("do_OK").attachEvent("onclick", new Function("doClose()"));
        document.getElementById("imgclose").attachEvent("onclick", new Function("doClose()"));
    }
    else {
        document.getElementById("do_OK").value = "关闭Close";
        document.getElementById("do_OK").attachEvent("onclick", new Function("doCloseWindow()"));
        document.getElementById("imgclose").attachEvent("onclick", new Function("doCloseWindow()"));
    }

    return false;
}

//Confirm提示框
function MyConfirm(txt) {
    //alert(docallback);
    var ctrl = (document.all) ? window.event.srcElement : arguments[1];
    var shield = GetShieldDiv();
    var strHtml = "<div class='atclose' id='imgclose'></div></td></tr>\n"
    + "<tr><td class=\"alerttxt\" colspan='2'>" + txt + "</td></tr>\n"
    + "<tr><td colspan='2' valign='top' height='41' style=\"text-align:center;\">"
    + "<input type=\"button\" value=\"确定OK\" id=\"do_OK\" class=\"alert_btn\" />"
    + " &nbsp; <input type=\"button\" value=\"取消Cancel\" id=\"do_Cancel\" class=\"alert_btn\" />";
    var alertFram = GetShieldAlertFram(strHtml);
    var ifm = GetShieldIfm();
    GetShieldMain(shield, alertFram, ifm, ctrl);
    document.getElementById("do_Cancel").attachEvent("onclick", new Function("doClose()"));
    document.getElementById("do_OK").attachEvent("onclick", new Function("doOK()"));
    document.getElementById("imgclose").attachEvent("onclick", new Function("doClose()"));
    return false;
}

//page height
function $WH() {
    return window.screen.height < document.body.scrollHeight ? document.body.scrollHeight : window.screen.height;
    //return window.screen.height<document.body.scrollHeight?document.body.scrollHeight+18:window.screen.height;
}
//page width
function $WW() {
    return window.screen.width;
    //return window.screen.width-20;
}

function GetShieldDiv() {
    var shield = document.createElement("DIV");
    shield.id = "shield";
    shield.className = "shield";
    shield.style.height = $WH() + 20 + "px";
    shield.style.width = $WW() + "px";
    return shield;
}
function GetShieldAlertFram(strHtml) {
    var alertFram = document.createElement("DIV");
    alertFram.id = "alertFram";
    alertFram.className = "alertFram";
    alertFram.style.marginTop = document.documentElement.scrollTop + "px";
    var str = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"alerttb\">\n"
    + "<tr><td class='alertfil'>&nbsp;</td><td align='right' class=\"alertbg\">";
    alertFram.innerHTML = str + strHtml + "</td></tr>\n</table>\n";
    return alertFram;
}
function GetShieldIfm() {
    var ifm = document.createElement("IFRAME");
    ifm.id = "ifm";
    ifm.className = "ifm";
    ifm.style.height = $WH() + "px";
    ifm.style.width = $WW() + "px";
    return ifm;
}
function GetShieldMain(shield, alertFram, ifm, ctrl) {
    document.body.appendChild(ifm);
    document.body.appendChild(alertFram);
    document.body.appendChild(shield);
    this.setOpacity = function(obj, opacity) {
        if (opacity >= 1)
            opacity = opacity / 100;
        try {
            obj.style.opacity = opacity;
        }
        catch (e) { }
        try {
            if (obj.filters.length > 0 && obj.filters("alpha"))
                obj.filters("alpha").opacity = opacity * 100;
            else
                obj.style.filter = "alpha(opacity=\"" + (opacity * 100) + "\")";
        }
        catch (e) { }
    }
    var c = 0;
    this.doAlpha = function() {
        if (++c > 20) {
            clearInterval(ad);
            return 0;
        }
        setOpacity(shield, c);
    }
    var ad = setInterval("doAlpha()", 1);
    this.doClose = function() {
        RemoveShield(shield, alertFram, ifm);
        //return false;
        //        if(hfUrl.length>0&&$(hfUrl)!=null&&$(hfUrl).value.length>0)
        //        {
        //            window.location.href=$(hfUrl).value;
        //            Loading();
        //        }
    }
    this.doCloseWindow = function() {
        closeWindow();
    }
    this.doOK = function() {
        RemoveShield(shield, alertFram, ifm);
        //alert("test");
        //return 1;
        //alert(docallback);
        if (ctrl != null) {
            SubmitFormByOk();
        }
        //	    {
        //	        ctrl.onclick="";
        //		    if(ctrl.id.indexOf("txtEandLRequestID")>-1)
        //		    {
        //		        ctrl.attachEvent("onclick",new Function("ChooseEandLRequest();"));
        //		    }
        //		    else
        //		    {
        //		        ctrl.click();
        //		    }
        //		    else if(btn.id.indexOf("btnDecline")>-1)
        //		        btn.attachEvent("onclick",new Function("return ValidateComment();"));
        //		    else if(btn.id.indexOf("btnCancel")>-1)
        //		        btn.attachEvent("onclick",new Function("return ValidateComment();"));
        //		    else if(btn.id=="libtnDelete")
        //		    {
        //		        btn.parentNode.onclick="";
        //		        btn.parentNode.click();
        //		    }
        //alert(ctrl.id);
        //ctrl.click();

        //evel(docallback);
        //}
    }

    document.getElementById("do_OK").focus();
    document.body.onselectstart = function() { return false; }
    document.body.oncontextmenu = function() { return false; }
}
function RemoveShield(shield, alertFram, ifm) {
    document.body.removeChild(ifm);
    document.body.removeChild(alertFram);
    document.body.removeChild(shield);
    document.body.onselectstart = function() { return true; }
    document.body.oncontextmenu = function() { return true; }

}

//get control's client id by control
function cibc(ctl) {
    var ctlID = ci(ctl);
    return ctlID.substring(0, ctlID.lastIndexOf("_") + 1);
}

//get control's client id by control id
function cibci(ctlID) {
    return ctlID.substring(0, ctlID.lastIndexOf("_") + 1);
}

// return element by it's id
function $(id) {
    return document.getElementById(id);
}

function ci(ctl) {
    return ctl.id;
}

function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})[\/-](\d{1,2})[\/-](\d{1,2}) *$/);
        if (results && results.length > 3)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));

        //results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);          
        //if(results && results.length>3)          
        //    return new Date(parseInt(results[1]),parseInt(results[2]) -1,parseInt(results[3]));           

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));

        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));
    }
    return null;
}

function DateDiff(beginDate, endDate) {
    var sign = '-';

    if (beginDate.indexOf('/') > 0)
        sign = '/';

    var arrbeginDate, sDate, eDate, arrendDate, iDays

    arrbeginDate = beginDate.split(sign)
    sDate = new Date(arrbeginDate[1] + '-' + arrbeginDate[2] + '-' + arrbeginDate[0])
    arrendDate = endDate.split(sign)
    eDate = new Date(arrendDate[1] + '-' + arrendDate[2] + '-' + arrendDate[0])
    iDays = parseInt(Math.abs(sDate - eDate) / 1000 / 60 / 60 / 24)

    return iDays
}


function DateDiff2(sDate1, sDate2) {     //sDate1和sDate2是2002-12-18格式 
    var aDate, oDate1, oDate2, iDays;
    aDate = sDate1.split("-");
    oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]);   //转换为12-18-2002格式 
    aDate = sDate2.split("-");
    oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]);
    //    document.diff.date1change.value=oDate1; 
    //    document.diff.date2change.value=oDate2; 
    if ((oDate2 - oDate1) < 0) {
        return -1;
    }
    if ((oDate2 - oDate1) == 0) {
        return 0;
    }
    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);     //把相差的毫秒数转换为天数 
    return iDays;
}

//判断是否是合法日期
function IsDate(DateString, Dilimeter) {
    if (DateString == null) {
        return false;
    }
    if (Dilimeter == ' ' || Dilimeter == null) {
        Dilimeter = '-';
    }
    var tempy = ' ';
    var tempm = ' ';
    var tempd = ' ';
    var tempArray;
    if (DateString.length < 8 || DateString.length > 10) {
        return false;
    }

    tempArray = DateString.split(Dilimeter);
    if (tempArray.length != 3) {
        return false;
    }

    if (tempArray[0].length != 4) {
        return false;
    }
    else {
        tempy = tempArray[0];
        tempm = tempArray[1];
        tempd = tempArray[2];
    }

    if (isNaN(tempy)) {
        return false;
    }
    if (isNaN(tempm)) {
        return false;
    }
    if (isNaN(tempd)) {
        return false;
    }

    var tDateString = tempy + '/ ' + tempm + '/ ' + tempd;
    var tempDate = new Date(tDateString);

    if (tempDate.getFullYear() != tempy) {
        return false;
    }

    if (tempDate.getMonth() != tempm - 1) {
        return false;
    }

    if (tempDate.getDate() != tempd) {
        return false;
    }

    if (isNaN(Date.parse(tempDate))) {
        return false;
    }

    return true;
}


//获取元素的纵坐标
function getTop(e) {
    var offset = e.offsetTop;
    if (e.offsetParent != null) offset += getTop(e.offsetParent);
    return offset;
}
//获取元素的横坐标
function getLeft(e) {
    var offset = e.offsetLeft;
    if (e.offsetParent != null) offset += getLeft(e.offsetParent);
    return offset;
}