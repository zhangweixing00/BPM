 /// <reference path="jquery-1.4.2-vsdoc.js" />
var isLeftFolded				= false;//右侧折叠状态，默认为展开，即折叠为false
;(function ($) {
    $.fn.layoutUI = function (header, nav, menu, body, footer) {
		var resizeAble=false;
        function _getDocumentClientHeight() {
            if ($.browser.msie) {
                return $(document.documentElement)[0].clientHeight; 
            } else {
                return self.innerHeight;
            }
        };
		
		function _getDocumentClientWidth() {
            if ($.browser.msie) {
                return $(document.documentElement)[0].clientWidth; 
            } else {
                return self.innerWidth;
            }
        };
		
        function _initPage(header, nav, menu, body, footer) {
            var vheight = _getDocumentClientHeight();
            //var titleheight = $(header).height();
			//计算header部分的外部高度（包含margin和border的高度）
			var titleheight 	= header	== null ?0 : $(header).height()+($(header).outerHeight(true)-$(header).innerHeight());
			
            var pageheight 		= vheight - titleheight;
            //var navheight = $(nav).height();
			//计算nav部分的外部高度（包含margin和border的高度）
			var navheight 		= nav		== null ?0 : $(nav).height()+($(nav).outerHeight(true)-$(nav).innerHeight());
            var footerheight 	= footer	== null ?0 : $(footer).height()+($(footer).outerHeight(true)-$(footer).innerHeight());
			
			var cw=_getDocumentClientWidth();
			var leftWidth;
			if(isLeftFolded){
				leftWidth=0;
			}else{
				leftWidth=$("#lefttd").width()+($("#lefttd").outerWidth(true)-$("#lefttd").innerWidth());
			}
			
			var spliterWidth=$("#spliter").width()+($("#spliter").outerWidth(true)-$("#spliter").innerWidth());
			var bodyWidth		= cw-leftWidth-spliterWidth;
			
			var bodyHeight		= pageheight - navheight - footerheight;
            //if(footer) { footerheight = (footer).height();}

            $(menu).height(pageheight - footerheight);
            $(body).height(bodyHeight);
			$(body).width(bodyWidth);			
		}; 
        
        _initPage(header, nav, menu, body, footer) ;
		
        $(window).resize(function () { 			
			_initPage(header, nav, menu, body, footer) ; 
			resizeAble=false;
		});
		
		
    }  
})( jQuery );

;(function ($) {
    $.fn.splite = function (lefte, splitere, righte, leftwidth) {		
        var orignbkcolor 				= $(splitere).css("background-color");
		
		var durationTime				= 300;//动画时间，这个值就暂时不对外开放了
		var isIE						= $.browser.msie;//检查IE，只在IE下实现动画
		var spliteBarBgColor			= "#e8edf2";	//正常状态下中间的竖条背景颜色
		var spliteBarBgColorMouseOver	= "#c2d1ff";	//鼠标经过中间的竖条时背景颜色
		$(this).css("background-color", spliteBarBgColor);
		$(splitere).html("<img src='/Pkurg.PWorldTemp.Web/App_Themes/Aqua/images/spliterBar_unFolded.jpg' />");	
        function _active(lefte, splitere, righte, leftwidth){
            var objspliter 	= $(splitere);
            var objleft 	= $(lefte);
            var objright 	= $(righte);
        };

        function initspliter(lefte, splitere, leftwidth)
        {			
            var objleft 	= $(lefte);
            var objspliter 	= $(splitere);			
            objspliter.bind("click",function(){				
				
				toggleLeft(objleft);
				if(isLeftFolded){
				    $(splitere).html("<img src='/Pkurg.PWorldTemp.Web/App_Themes/Aqua/images/spliterBar_unFolded.jpg' />");
				}else{
				$(splitere).html("<img src='/Pkurg.PWorldTemp.Web/App_Themes/Aqua/images/spliterBar_Folded.jpg' />");
				}
				isLeftFolded=!isLeftFolded;
				
				$(document).layoutUI('#divheader', '#divnav', '#divmenu', '#divbody', null);
				
				
            });            

            objspliter.bind("mouseon", function(){
                $(this).css("background-color", spliteBarBgColor);
            });

            objspliter.bind("mouseover", function(){
                $(this).css("background-color", spliteBarBgColorMouseOver);
            });

            objspliter.bind("mouseout", function(){
                $(this).css("background-color", spliteBarBgColor);
            });
			
        };
		
		/**
		* 移动左侧区域的方法
		* @param	{Object}	objleft	需要移动的对象
		* @date 	2010.12.19
		*/
		function moveLeft(objleft){
			//如果是折叠的，将其展开
			if(isLeftFolded) { 
				objleft.animate({width: leftwidth}, durationTime);				
				objleft.children().each(function(i){
					$(this).animate({marginLeft: 0}, { duration: durationTime,queue:false });				
				});							
			 }else { 
				//如果是展开的，将其折叠		
				$("#lefttd").show().animate({width: 0}, durationTime);				
				objleft.children().each(function(i){
					$(this).animate({marginLeft: -leftwidth}, { duration: durationTime ,queue:false});				
				});				
			 }			
		}//end of function moveLeft
		
		/**
		* 切换左侧区域折叠状态的方法
		* @param	{Object}	objleft	需要移动的对象
		* @date 	2010.12.19
		*/
		function toggleLeft(objleft){
			//如果是折叠的，将其展开
			if(isLeftFolded){			
				 objleft.show().width( leftwidth ); 			
				 objleft.children().each(function(i){$(this).show();});				
			}else{
				//如果是展开的，将其折叠	
				 objleft.width(0); 
				 objleft.each(function(i){$(this).hide();});				 
			}		
		}//end of function toggleLeft

        initspliter(lefte, splitere, leftwidth);
    }  
})( jQuery );