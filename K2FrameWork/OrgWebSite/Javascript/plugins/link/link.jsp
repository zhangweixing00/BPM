<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ page pageEncoding="UTF-8" contentType="text/html;charset=utf-8" %>
<%@ include file="/WEB-INF/pages/common/taglibs.jsp" %><html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <%@ include file="/WEB-INF/pages/common/meta.jsp" %>
  <meta http-equiv="content-type" content="text/html; charset=utf-8" />
  <title>Flash</title>
 <style type="text/css" rel="stylesheet">
			body {
				font-size:12px;
				font-family: "sans serif",tahoma,verdana,helvetica;
				margin:0;
				background-color:#F0F0EE;
				overflow:hidden;
			}
			form {
				margin:0;
			}
			label {
				cursor:pointer;
			}
			#resetBtn {
				margin-left:10px;
				cursor:pointer;
			}
			.main {
				margin: 10px;
			}
			.tab-navi {
				width:100%;
				overflow:hidden;
				margin-bottom:10px;
			}
			.tab-navi ul  {
				list-style-image:none;
				list-style-position:outside;
				list-style-type:none;
				margin:0;
				padding:0;
				display:block;
				float:left;
				width:100%;
				border-bottom:1px solid #888888;
			}
			.tab-navi li {
				border: 1px solid #888888;
				margin:0 -1px -1px 0;
				float: left;
				padding: 5px;
				background-color: #F0F0EE;
				text-align: center;
				width: 60px;
				font-weight: normal;
				cursor: pointer;
			}
			.tab-navi li.selected {
				background-color: #E0E0E0;
				font-weight: bold;
				cursor: default;
			}
			.table  {
				list-style-image:none;
				list-style-position:outside;
				list-style-type:none;
				margin:0;
				padding:0;
				display:block;
			}
			.table li {
				padding:0;
				margin-bottom:10px;
				display:list-item;
			}
			.table li label {
				font-weight:bold;
			}
			.table li input {
				vertical-align:middle;
			}
			.table li img {
				vertical-align:middle;
			}
		</style>
  <script type="text/javascript">
  
		function urlChange(){
			var mdCheck=false;
			var v_URL=$("#url").val();
			var v_MD="";
			if(typeof(v_URL)=="string" && v_URL.length>v_URL.indexOf("#")+1 && (v_URL.indexOf("#")==0 || v_URL.indexOf("#")==1)){
				v_MD=v_URL.substring(v_URL.indexOf("#")+1);
			}
			var aSelect=$("#anchorSelect").get(0);
			for(i=0;i<aSelect.length;i++){
				if(v_MD==aSelect.options[i].value){
					aSelect.options[i].selected=true;
					mdCheck=true;
				}	
			}
			if(mdCheck==false){
				aSelect.options[0].selected=true;
			}
		}
  
  
  
  
  
  
  
  var KE = parent.KE;
			location.href.match(/\?id=([\w-]+)/i);
			var id = RegExp.$1;
			var fileManager = null;
			var allowUpload = (typeof KE.g[id].allowUpload == 'undefined') ? true : KE.g[id].allowUpload;
			var allowFileManager = (typeof KE.g[id].allowFileManager == 'undefined') ? false : KE.g[id].allowFileManager;
			var referMethod = (typeof KE.g[id].referMethod == 'undefined') ? '' : KE.g[id].referMethod;
			KE.event.ready(function() {
				var typeBox = KE.$('type', document);
				var urlBox = KE.$('url', document);
				var alignElements = document.getElementsByName('editparms.align');
				var fileBox = KE.$('imgFile', document);
				var widthBox = KE.$('width', document);
				var heightBox = KE.$('height', document);
				var titleBox = KE.$('otherTitle', document);
				var resetBtn = KE.$('resetBtn', document);
				var tabNavi = KE.$('tabNavi', document);
				var viewServer = KE.$('viewServer', document);
				var liList = tabNavi.getElementsByTagName('li');
                                var anchorSelectBox = KE.$('anchorSelect', document);
				var selectTab = function(num) {
					if (num == 1) resetBtn.style.display = 'none';
					else resetBtn.style.display = '';
					widthBox.value = '';
					heightBox.value = '';
					titleBox.value = '';
					alignElements[0].checked = true;
					for (var i = 0, len = liList.length; i < len; i++) {
						var li = liList[i];
						if (i === num) {
							li.className = 'selected';
							li.onclick = null;
						} else {
							if (allowUpload) {
								li.className = '';
								li.onclick = (function (i) {
									return function() {
										if (!fileManager) selectTab(i);
									};
								})(i);
							} else {
								li.parentNode.removeChild(li);
							}
						}
						KE.$('tab' + (i + 1), document).style.display = 'none';
					}
					typeBox.value = num + 1;
					KE.$('tab' + (num + 1), document).style.display = '';
				}
				if (!allowFileManager) {
					viewServer.parentNode.removeChild(viewServer);
					urlBox.style.width = '300px';
				}
				selectTab(0);
				var imgNode =KE.plugin['image'].getSelectedNode(id);
				
				if (imgNode) {
					var src = KE.format.getUrl(imgNode.src, KE.g[id].urlType);
					urlBox.value = src;
					//alert(src);
					widthBox.value = imgNode.width;
					heightBox.value = imgNode.height;
					titleBox.value = (typeof imgNode.alt != 'undefined') ? imgNode.alt : imgNode.title;
					for (var i = 0, len = alignElements.length; i < len; i++) {
						if (alignElements[i].value == imgNode.align) {
							alignElements[i].checked = true;
							break;
						}
					}
				}
				KE.event.add(viewServer, 'click', function () {
					if (fileManager) return false;
					fileManager = new KE.dialog({
						id : id,
						cmd : 'file_manager',
						file : 'file_manager/file_manager.html?id=' + id,
						width : 500,
						height : 400,
						loadingMode : true,
						title : '浏览服务器',
						noButton : '取消',
						afterHide : function() {
							fileManager = null;
						}
					});
					fileManager.show();
				});
				KE.$('referMethod', document).value = referMethod;
				var alignIds = ['default', 'left', 'right'];
				for (var i = 0, len = alignIds.length; i < len; i++) {
					KE.event.add(KE.$(alignIds[i] + 'Img', document), 'click', (function(i) {
						return function() {
							KE.$(alignIds[i] + 'Chk', document).checked = true;
						};
					})(i));
				}
				KE.event.add(resetBtn, 'click', function() {
					var g = KE.g[id];
					var img = KE.$$('img', g.iframeDoc);
					img.src = urlBox.value;
					img.style.position = 'absolute';
					img.style.visibility = 'hidden';
					img.style.top = '0px';
					img.style.left = '1000px';
					g.iframeDoc.body.appendChild(img);
					widthBox.value = img.width;
					heightBox.value = img.height;
					g.iframeDoc.body.removeChild(img);
				});




				KE.util.hideLoadingPage(id);
			}, window, document);
  
  		 /*
     *多媒体库浏览
    */
    function selectother()
    {
        var cid=$("#cid").val();
        if(cid !=null && cid >0)
        {
	        
            var cid=$("#cid").val();
	        var sValue = PortalUI.openDialog('getmmsfilelistforeditor.html?reference=other&columnId='+cid,null, 578, 450);
	        
	        $("#urls").val(sValue.url);
	        $("#fid").val(sValue.fid);
	        $("#ftype").val(sValue.ftype);
	        $("#fGroup").val(sValue.fGroup);
	        $("#filename").val(sValue.name);
        }
        else
        {
             //当栏目不存在时 去掉多媒体库浏览
             // $("#li3").hide();
        }
        
       
    }
    
   /*
    * 验证栏目文件组 
    */
    function  validatecolumngroup()
    {
      var cid=$("#cid").val();
       var param="columnId="+cid;
	    	 $.ajax({
		        type: "post",
		        async:false,
		        url: "validatemmscolumngroup.html",
		        data:param , 
		        success: function(data){
		            if(data&&data!="3"){
		            // $("#li3").hide();
					}
	             },
	             error:function(data){
	                    //当栏目不属于图片栏目时 清除多媒体库浏览
	                // $("#li3").hide(); 
	             }});
    }
    
    
    $(document).ready(function(){
        // validatecolumngroup();
	});

		var KE = parent.KE;
		location.href.match(/\?id=([\w-]+)/i);
		var id = RegExp.$1;
		
		KE.event.ready(function() {
			var anchorSelectBox = KE.$('anchorSelect', document);
			var v_URL="";

			var hyperLink = KE.$('url', document);
			
			if(hyperLink.length <1)
			{
			 hyperLink = KE.$('urls', document);
			}
			
			var linkType = KE.$('linkType', document);
			var linkNode = KE.plugin['link'].getSelectedNode(id);
			if (linkNode) {
				var src =KE.format.getUrl(linkNode.getAttribute("href"), KE.g[id].urlType);
				v_URL=src;
				var target = linkNode.target;
				hyperLink.value = src;
				linkType.value = target == '_blank' ? target : '_self';
				var range = KE.g[id].keRange;
				range.selectTextNode(linkNode);
				KE.g[id].keSel.addRange(range);
			}




			var i;
			var keDOM=KE.g[id].iframeDoc.getElementsByTagName("a");


			var newoption = document.createElement("OPTION");   
			newoption.text = "不设置本地锚点";   
			newoption.value = "";
			anchorSelectBox.add(newoption,null); 

				
			var v_MD="";
			if(typeof(v_URL)=="string" && v_URL.length>v_URL.indexOf("#")+1 && (v_URL.indexOf("#")==0 || v_URL.indexOf("#")==1)){
				v_MD=v_URL.substring(v_URL.indexOf("#")+1);
			}
				
			for(i=0;i<keDOM.length;i++){
				if(keDOM[i].className=="ke-anchor"){
					var newoption = document.createElement("OPTION");   
					newoption.text = keDOM[i].getAttribute("title");   
					newoption.value = keDOM[i].name;
                                        
					if(v_MD==keDOM[i].name){
						newoption.selected=true;
						
					}
					anchorSelectBox.add(newoption,null); 
                  }
            }


   			$("#url").bind("keyup",urlChange);
			$("#url").bind("blur",urlChange);
			//timerID = setInterval(urlChange, 600);










			KE.util.hideLoadingPage(id);
		}, window, document); 

		function anchorSelectChange(){
            var m=$("#anchorSelect").val();
            if(m!=""){
				$("#url").val("/#"+m);
			}
		}

	</script>
</head>
<body>
    <s:property   value="%{stringColoseDiv}"   escape="html" />
    <div class="main">
			<div id="tabNavi" class="tab-navi">
				<ul>
					<li >本地上传</li>
					<li >网络连接</li>
					<li id="li3">多媒体库</li>
				</ul>
			</div>
			<iframe name="uploadIframe" id="uploadIframe" style="display:none;"></iframe>
			<input type="hidden" id="type" name="type" value="" />
			<input type="hidden" id="filename" name="filename" value="" />
			<s:form name="uploadForm"  method="post" enctype="multipart/form-data" action="uploadmmsother" target="uploadIframe">
				<input type="hidden" id="referMethod" name="referMethod" value="" />
				<input type="hidden" name="editparms.border" value="0" />
				<input id="cid" type="hidden"  name="columnId"  value="<%if(request.getParameter("cid")!=null) {out.print(request.getParameter("cid"));} %>"  />
	            <input type="hidden" id="editorId" name="editparms.editerId"  value="<%=request.getParameter("id")%>"  />
                <input   type="hidden"  name="editparms.dialogtype"  value="<%=request.getParameter("dialogtype")%>"  />
				<ul class="table">
					<li>
						<div id="tab1" style="display:none;">
							<label for="imgFile">链接地址</label>
							<input type="file" id="imgFile" name="upLoad.uploadfile" style="width:300px;" />
						</div>
						<div id="tab2" style="display:none;">
                                                    <ul>
                                                        <li>
							    <label for="url">链接地址</label>
							    <input type="text" id="url" name="url" value="http://" maxlength="255" style="width:220px;" />
							    <input type="button" id="viewServer" name="viewServer" value="浏览..." />
                                                        </li>
                                                        <li>
							    <label for="imgFile">本地锚点</label>
							    <select id="anchorSelect" style="width:220px;" onchange="anchorSelectChange()" ></select>
                                                        </li>
                                                    </ul>					
                                                </div>
						<div id="tab3" style="display:none;">
							<label for="imgFile">链接地址</label>
							<input type="text" id="urls" name="urls" value="" maxlength="255" style="width:220px;" />
							<a href="javascript:void(0)" >
						        <img onclick="selectother()" id="queryimg"     alt="选择视频" src="<s:url value="/images/icons/inner_query.gif" includeParams="none" /> "  />
						    </a>
						</div>
					</li>
					<li style="display:none">
						<label for="imgWidth">视频大小</label>
						宽 <input type="text" id="width" name="editparms.width" value="" maxlength="4" style="width:50px;text-align:right;" />
						高 <input type="text" id="height" name="editparms.height" value="" maxlength="4" style="width:50px;text-align:right;" />
						<img src="image/images/refresh.gif" width="16" height="16" id="resetBtn" alt="重置大小" title="重置大小" />
					</li>
					<li>
						<label for="imgTitle">链接说明</label>
						<input type="text" id="otherTitle" name="mmsFile.fileDescription" value="" maxlength="255" style="width:250px;" />
					</li>
					<li id="li_LinkType">
						 <label for="linkType">打开类型</label>
						<select id="linkType" name="editparms.linkType">
						  <option value="_blank">新窗口</option>
						  <option value="_self">当前窗口</option>
						</select>
					</li>
					
				</ul>
			</s:form>
		</div>
	  <div style="display:none"> 
	     <input type="hidden" id="fid" name="fid"  value=""  /> 
		 <input type="hidden" id="ftype" name="ftype"  value=""  /> 
		 <input type="hidden" id="fGroup" name="fGroup"  value=""  />  
		 
		<input type="radio" id="defaultChk" name="editparms.align" value="" checked="checked" /> <img id="defaultImg" src="images/align_top.gif" width="23" height="25" border="0" alt="默认方式" title="默认方式" />
		<input type="radio" id="leftChk" name="editparms.align" value="left" /> <img id="leftImg" src="images/align_left.gif" width="23" height="25" border="0" alt="左对齐" title="左对齐" />
		<input type="radio" id="rightChk" name="editparms.align" value="right" /> <img id="rightImg" src="images/align_right.gif" width="23" height="25" border="0" alt="右对齐" title="右对齐" />
	  </div>



	</body>
</html>