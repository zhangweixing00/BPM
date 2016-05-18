function SetReadOnly() {
    var myForm, obj;
    myForm = document.forms[0];

    for (var i = 0; i < myForm.length; i++) {
        if ((myForm.elements[i].className.toUpperCase() == "TXTUSERREADONLY") || (myForm.elements[i].className.toUpperCase() == "TXTREADONLY")) {
            obj = myForm.elements[i];
            //alert(objRadio.name);
            obj.setAttribute("readOnly", true);
        }
    }
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
function $$(id) {
    return document.getElementById(id);
}

function ci(ctl) {
    return ctl.id;
}

function OpenURL(hlid) {
    $$(hlid).click();
}

//div
function getPageSizeWithScroll() {
    if (window.innerHeight && window.scrollMaxY) {// Firefox
        yWithScroll = window.innerHeight + window.scrollMaxY;
        xWithScroll = window.innerWidth + window.scrollMaxX;
    } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
        yWithScroll = document.body.scrollHeight;
        xWithScroll = document.body.scrollWidth;
    } else { // works in Explorer 6 Strict, Mozilla (not FF) and Safari
        yWithScroll = document.body.scrollHeight; //document.body.offsetHeight;
        xWithScroll = document.body.offsetWidth;
    }
    return [xWithScroll, yWithScroll];
}

function SuperMan() {
    var _select_cache;
    return {
        show_bg: function(hide_select) {
            var size = getPageSizeWithScroll();
            $("#bg").css({ left: 0, top: 0, width: '100%', height: size[1] }).show(); //
            if (hide_select) {
                _select_cache = $('select:visible');
                _select_cache.css('visibility', 'hidden');
            }
        },
        hide_bg: function() {
            $("#bg").hide();
            if (_select_cache) {
                _select_cache.css('visibility', 'visible');
                _select_cache = false;
            }
        }
    }
}

function getPageSizeWithScroll() {
    if (window.innerHeight && window.scrollMaxY) {// Firefox
        yWithScroll = window.innerHeight + window.scrollMaxY;
        xWithScroll = window.innerWidth + window.scrollMaxX;
    } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
        yWithScroll = document.body.scrollHeight;
        xWithScroll = document.body.scrollWidth;
    } else { // works in Explorer 6 Strict, Mozilla (not FF) and Safari
        yWithScroll = document.body.offsetHeight;
        xWithScroll = document.body.offsetWidth;
    }
    return [xWithScroll, yWithScroll];
}

window.superman = SuperMan();