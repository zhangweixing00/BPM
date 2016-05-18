<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ page pageEncoding="UTF-8" contentType="text/html;charset=utf-8" %>
<%@ include file="/WEB-INF/pages/common/taglibs.jsp" %>
<html>
	<head>
	    <%@ include file="/WEB-INF/pages/common/meta.jsp" %>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<title>Image</title>
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
				width: 100px;
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
				var widthBox = KE.$('imgWidth', document);
				var heightBox = KE.$('imgHeight', document);
				var titleBox = KE.$('imgTitle', document);
				var resetBtn = KE.$('resetBtn', document);
				var tabNavi = KE.$('tabNavi', document);
				var viewServer = KE.$('viewServer', document);
				var liList = tabNavi.getElementsByTagName('li');
				var selectTab = function(num) {
					if (num == 1) resetBtn.style.display = 'none';
					else resetBtn.style.display = 'none';
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
				var imgNode = KE.plugin['image'].getSelectedNode(id);
				if (imgNode) {
					var src = KE.format.getUrl(imgNode.src, KE.g[id].urlType);
					urlBox.value = src;
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
    function selectimg()
    {
        var cid=$("#cid").val();
        if(cid !=null && cid >0)
        {
           var sValue = PortalUI.openDialog('getmmsfilelistforeditor.html?reference=image&columnId='+cid,null, 578, 450);
           $("#urls").val(sValue.url);
           $("#callbackfid").val(sValue.fid);
        }
        else
        {
             //当栏目不存在时 去掉多媒体库浏览
            // $("#li3").hide();
        }
    }
    
    /*
    * 验证栏目文件组 
    
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
		       
		            if(data&&data!="1"){
		            // $("#li3").hide();
					}
	             },
	             error:function(data){
	                    //当栏目不属于图片栏目时 清除多媒体库浏览
	                    // $("#li3").hide();
	             }});
    }
    */
    
    $(document).ready(function(){
        // validatecolumngroup();
	});
			
		</script>
	</head>
	<body>
		 <s:property   value="%{stringColoseDiv}"  escape="html" />
		<div class="main">
			<div id="tabNavi" class="tab-navi">
				<ul>
				 <%if(request.getParameter("showTab")!=null && request.getParameter("showTab").equals("1")) { %>
					<li>本地文件</li>
					<script type="text/javascript">
					$(document).ready(function(){
       				  $("#upurl").html("文件地址");
       				  $("#idescription").html("文件说明");
       				  $("#ialign").hide();
       				    $("#isize").hide();
					});
					</script>
 				<%
				}else{ %> 
				
				    <li>本地图片</li>
					<li>网络图片</li>
					<li id="li3">多媒体库</li>
				 <%} %>
				 
				  
				</ul>
			</div>
			<iframe name="uploadIframe" id="uploadIframe" style="display:none;"></iframe>
			<input type="hidden" id="type" name="type" value="" />
			<input type="hidden"  id="callbackfid" name="callbackfid"  value="" />
			<s:form name="uploadForm"  method="post" enctype="multipart/form-data" action="uploadmmsimage" target="uploadIframe">
				<input type="hidden" id="referMethod" name="referMethod" value="" />
				<input type="hidden" name="editparms.border" value="0" />
				<input id="cid" type="hidden"  name="columnId"  value="<%if(request.getParameter("cid")!=null) {out.print(request.getParameter("cid"));} %>"  />
	            <input type="hidden" id="editorId" name="editparms.editerId"  value="<%=request.getParameter("id")%>"  />
                <input   type="hidden"  name="editparms.dialogtype"  value="<%=request.getParameter("dialogtype")%>"  />
				<ul class="table">
					<li>
						<div id="tab1" style="display:none;">
							<label for="imgFile" id="upurl">图片地址</label>
							<input type="file" id="imgFile" name="upLoad.uploadfile" class="file" style="width:300px;" />
						</div>
						<div id="tab2" style="display:none;">
							<label for="url" >图片地址</label>
							<input type="text" id="url" name="url" value="http://" maxlength="255" style="width:250px;" />
							<input type="button" id="viewServer" name="viewServer" value="浏览..." />
						</div>
						<div id="tab3" style="display:none;">
							<label for="imgFile">图片地址</label>
							<input type="text" id="urls" name="url" value="" maxlength="255" style="width:250px;" />
							<a href="javascript:void(0)" >
						        <img onclick="selectimg()" id="queryimg"     alt="选择图片" src="<s:url value="/images/icons/inner_query.gif" includeParams="none" /> "  />
						    </a>
						</div>
					</li>
					<li id="isize">
						<label for="imgWidth">图片大小</label>
						宽 <input type="text" id="imgWidth" name="editparms.width" value="" maxlength="4" style="width:50px;text-align:right;" />
						高 <input type="text" id="imgHeight" name="editparms.height" value="" maxlength="4" style="width:50px;text-align:right;" />
						<img  src="images/refresh.gif" width="16" height="16" id="resetBtn" alt="重置大小" title="重置大小" />
					</li>
					<li id="ialign">
						<label>对齐方式</label>
						<input type="radio" id="defaultChk" name="editparms.align" value="" checked="checked" /> <img id="defaultImg" src="images/align_top.gif" width="23" height="25" border="0" alt="默认方式" title="默认方式" />
						<input type="radio" id="leftChk" name="editparms.align" value="left" /> <img id="leftImg" src="images/align_left.gif" width="23" height="25" border="0" alt="左对齐" title="左对齐" />
						<input type="radio" id="rightChk" name="editparms.align" value="right" /> <img id="rightImg" src="images/align_right.gif" width="23" height="25" border="0" alt="右对齐" title="右对齐" />
					</li>
					<li>
						<label id="idescription" for="imgTitle">图片说明</label>
						<input type="text" id="imgTitle" name="mmsFile.fileDescription" value="" maxlength="255" style="width:95%;" />
					</li>
				</ul>
			</s:form>
		</div>
	</body>
</html>
