$(function () {
    //leftwidth = $("#hdleftwidth").val();
	leftwidth = $("#lefttd").width();
    $(document).layoutUI('#divheader', '#divnav', '#divmenu', '#divbody', null);
    $('#spliter').splite('#lefttd', '#spliter', '#righttd', leftwidth);	
});
