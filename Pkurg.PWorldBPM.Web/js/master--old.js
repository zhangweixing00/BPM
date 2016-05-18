var leftwidth = 210;
$(function () {
alert(6666);
    leftwidth = $("#hdleftwidth").val();

    $(document).layoutUI('#divheader', '#divnav', '#divmenu', '#divbody', null);
    $('#spliter').splite('#lefttd', '#spliter', '#righttd', leftwidth);
	
	showInfo("onload");
});
var isLeftFolded=false;//标记折叠状态，默认是展开的，所以默认折叠为flase
function moveLeft(){
	//var objleft = $("#lefttd");           
	var durationTime=300;
	showInfo("moveLeft");
	var isIE=jQuery.browser.msie;
	
	if(isIE==true){
		//如果则侧是折叠的，将其展开
		if(isLeftFolded) { 				
			//$("#lefttd").animate({width: 210}, { duration: durationTime,queue:false});
			$("#lefttd").animate({width: 210}, durationTime,"linear",showInfo("moveLeft , if isIE = "+isIE));
			
			
			$("#divmenu").animate(
				{marginLeft: 0}, { duration: durationTime,queue:false }
			);
			$("#leftTitleBg").animate(
				{marginLeft: 0}, { duration: durationTime,queue:false }
			);
							
		 }else { 
			//如果则侧是展开的，将其折叠		
			$("#lefttd").animate({width: 0}, durationTime,"linear",showInfo("moveLeft , else isIE = "+isIE));
			
			$("#divmenu").animate({marginLeft: -200}, { duration: durationTime ,queue:false});
			$("#leftTitleBg").animate({marginLeft: -200}, { duration: durationTime ,queue:false});
		 }
		 //alert("11111111111isLeftFolded = "+isLeftFolded);
	}else{
	/*
		if(isLeftFolded){
			 $("#lefttd").width( 210 ); 			
             $("#lefttd").children().each(function(i){$(this).show();});
			
		}else{
			 $("#lefttd").width(0); 
             $("#lefttd").each(function(i){$(this).hide();});
		}
		alert("22222222isLeftFolded="+isLeftFolded);
	}
	
	*/
	
	 isLeftFolded=!isLeftFolded;
                
}

function showInfo(fnName){
	var isIE=jQuery.browser.msie;
	var testInfo="";
	testInfo+=fnName+" :: lefttd.width = "+$("#lefttd").width()+" , spliter.width = "+$("#spliter").width()+" , righttd.width = "+$("#righttd").width()+" , isIE = "+isIE+"<br />";
  	$("#moveTestDebugInfo")[0].innerHTML+=testInfo;
	//$("#moveTestDebugInfo")[0].innerHTML+="------------------------------------------------------------------------------<br />";
}
function moveEle(id){
	
}