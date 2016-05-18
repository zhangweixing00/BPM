<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<title>anchor</title>
		<style type="text/css" rel="stylesheet">
			body {
				margin: 0;
				font:12px/1.5 "sans serif",tahoma,verdana,helvetica;
				background-color:#F0F0EE;
				color:#222222;
				overflow:hidden;
			}
			label {
				cursor:pointer;
			}
			.main {
				margin: 0 10px;
			}
			.table {
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
				line-height:1.5;
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


			KE.event.ready(function() {
				var anchornameNode =KE.plugin['anchor'].getSelectedNode(id);
				var anchorNameBox = KE.$('anchorName', document);
				var anchorTitleBox = KE.$('anchorTitle', document);

				//if (anchornameNode) {
				//	anchorNameBox.value=anchornameNode.getAttribute("anchorname");
				//}
    				var now= new Date();      
    				var year=now.getFullYear();      
    				var month=now.getMonth()+1;      
    				var day=now.getDate();      
    				var hour=now.getHours();      
    				var minute=now.getMinutes();      
    				var second=now.getSeconds();
				anchorNameBox.value="anchor_"+year+"_"+month+"_"+day+"_"+hour+"_"+minute+"_"+second+"_"+parseInt(Math.random()*100000);

				KE.util.hideLoadingPage(id);


			}, window, document);
		</script>
	</head>
	<body>
		<div class="main">
			<ul class="table">
				<li>
					<label for="anchorName"><span id="lang.url"></span></label>
					<input type="text" id="anchorTitle" name="anchorTitle" value="" style="width:90%;" />
					<input type="hidden" id="anchorName" name="anchorName" value="" />
				</li>
			</ul>
		</div>
	</body>
</html>