/******************
弹出正在发送Div
******************/

//弹出
function OpenWaitDiv(bgDiv,imgId){
    //取得图片实际大小
    var theimage = new Image();
    theimage.src = $('#' + imgId).attr('src');
    var imgHeight = theimage.width;
    var imgWidth = theimage.height;
    $('#' + imgId).css({'padding-left':(window.screen.availWidth/2 - imgWidth/2)  + 'px','padding-top':(window.screen.availHeight/2 - imgHeight/2) + 'px'});
    $('#' + bgDiv).css({'z-index':'5','background-color':'gray','display':'inline','position':'fixed','top':'0px','left':'0px','width':$(document.body).outerWidth(true) + 'px','height': $(document).height() + 'px','filter':'alpha(opacity=50)','-moz-opacity':'0.5','opacity':'0.5','margin':'0 auto'});
}

//关闭
function CloseWaitDiv(bgDiv){
    $('#' + bgDiv).css({'display':'none'});
}
