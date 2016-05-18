function WaitMsg(strleft, message, divheight) {
    var sWidth, sHeight;
    sWidth = window.parent.document.body.scrollWidth;
    if (divheight == undefined || divheight == null) {
        if ($.browser.mozilla) {
            sHeight = window.parent.document.body.scrollHeight;
            if (sHeight == 0)
            { sHeight = 1300 }
        }
        else {
            sHeight = window.parent.document.body.scrollHeight;
        }
    }
    else {
        sHeight = divheight;
    }

    if (window.parent != null && window.parent != undefined) {
        var maskObj = window.parent.document.createElement("div");
    }
    else {
        var maskObj = window.document.createElement("div");
    }

    maskObj.setAttribute('id', 'BigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#777";
    maskObj.style.filter = "Alpha(opacity=50)";
    maskObj.style.opacity = "1";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.backgroundColor = "white";
    maskObj.style.zIndex = 1000;
    maskObj.style.display = "none";
    var centerheight = (window.screen.height / 2) + window.parent.document.documentElement.scrollTop - 150;
    maskObj.innerHTML = "<center><div style='width:150px; height:100px;left:42%; top:" + centerheight + "px;position:absolute;text-align:center'><center><img src='/pic/sohu-waiting.gif'><br /><span style='font-size:13px;font-weight:bolder;'>" + message + "</span></center></div></center>";
    window.parent.document.body.appendChild(maskObj);

    this.begin = function () {
        maskObj.style.display = '';
    }
    this.end = function () {
        maskObj.style.display = 'none';
    }
}