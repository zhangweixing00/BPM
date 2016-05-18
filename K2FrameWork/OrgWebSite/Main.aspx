<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="OrgWebSite.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title>办公流程系统 </title>
    <link href="Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript">

        //进行任务转接
        function TaskRedirect(iframeId, pageName) {
            var frame = document.getElementById(iframeId);
            if (frame) {
                frame.setAttribute("src", pageName + "?random=" + Math.random());
            }
        }
        function Traggle(Id) {
            $("#" + Id).toggleClass("meauDisplay");
        }
        function TraggleShow(Id) {
            $("<%=this.menuIdListString %>").hide();
            $("#" + Id).toggle();
        }
        function reinitIframe() {
            var iframe = document.getElementById("frameContent");
            try {
                var bHeight = iframe.contentWindow.document.body.scrollHeight;
                var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                var height = Math.max(bHeight, dHeight);
                iframe.height = height;
            } catch (ex) { }
            var url = top.frames[0].location.pathname.toLowerCase(); ///logout?service
            if (url.indexOf("login.aspx") > -1 || url.indexOf("/logout?service") > -1) {
                top.window.location.reload();
            }
        }
        window.setInterval("reinitIframe()", 200);
        //根据页面大小自适应Iframe 高度
        function dyniframesize(down) {
            var pTar = null;

            if (document.getElementById) {
                pTar = document.getElementById(down);
            }
            else {
                eval('pTar = ' + down + ';');
            }
            if (pTar && !window.opera) {
                //begin resizing iframe 
                pTar.style.display = "block"
                if (pTar.contentDocument) {
                    //ns6 syntax 
                    pTar.height = $(pTar.contentDocument).height();
                    pTar.width = $(pTar.contentDocument).width();
                }
                else if (pTar.Document) {

                    pTar.height = pTar.contentWindow.document.documentElement.scrollHeight;
                    pTar.width = $(pTar.Document).width()
                }
            }
        }
        $(function () {
            try {
                var url = top.frames[0].location.pathname.toLowerCase();
                var arrUrls = url.split('/');
                //main.aspx在frameContent中
                if (arrUrls[arrUrls.length - 1] == "main.aspx") {
                    top.location.reload();
                    return;
                }
                //Login.aspx在frameContent中
                if (url.indexOf("Login.aspx") > -1) {

                    top.location.reload();
                    return;
                }
                //main.aspx在弹出层中
                if ($(parent.ymPrompt.getPage()).parent().parent().select('.ym-body').length == 1) {
                    //parent.ymPrompt.close();
                    top.location.reload();
                    return;
                }
            }
            catch (e) {

            }
            $("<%=this.menuIdListString %>").hide();
            $("<%=this.firstId %>").show();

            $(".left_menu_list ul").hover(function () { $(this).addClass("left_menu_onfoucs") }, function () { $(this).removeClass("left_menu_onfoucs") });

            if ($.browser.msie) {
                //$(".ztbox h2:not(:first)").css("margin-top", "-15px");
            }
            else {
                $(".ztbox h2").css("margin-top", "0px").css("margin-bottom", "5");
            }
        })

        function ymPromptclose(tp) {
            if (tp == 'ok') {
                if (document.all) {//IE
                    doc = document.frames["frameContent"].document;
                } else {//Firefox    
                    doc = document.getElementById("frameContent").contentDocument;
                }

                doc.getElementById('MyDelegation1_btnSearch').click();
            }
        }

        //弹出loading图片
        //显示图片 obj 为提示信息
        var waitmsg;
        function LoadMsgBegin(obj) {
            waitmsg = new WaitMsg("49%", obj);
            waitmsg.begin();
        }
        //关闭图片
        function LoadMsgEnd() {
            //IF判断由WuWeiMin2011-12-01添加，防止waitmsg不存时报错。
            if (waitmsg != null) {
                waitmsg.end();
            }

        }

        //        $(function () {
        //            var vhref = location.href;
        //            var begin = vhref.indexOf("?/");
        //            if (begin >= 0) {
        //                var end = vhref.length;
        //                var usehref = vhref.substring(begin + 1, end);

        //                var iframe = document.getElementById("frameContent");
        //                iframe.src = usehref;
        //            }
        //        });

        $(document).ready(function () {

            var vhref = location.href.toString();
            var begin = vhref.indexOf("Main.aspx?");
            if (begin == -1) {
                begin = vhref.indexOf("main.aspx?");
            }
            if (begin >= 0) {
                var end = vhref.length;
                var usehref = vhref.substring(begin + 10, end);
                $.ajax({
                    type: "POST",
                    url: "Main.ashx",
                    data: { "url": usehref },
                    async: false,
                    success: function (data) {

                        if (data != "no") {
                            //                            var iframe = document.getElementById("frameContent");
                            //                            iframe.src = data;

                            iframeUrl(data);
                        }
                        else {
                            iframeUrl("/Admin/ProcessNavigatorNew.aspx");
                        }
                    },
                    error: function () {
                        iframeUrl("/Admin/ProcessNavigatorNew.aspx");
                    }
                });
            }
        });

        function iframeUrl(objUrl) {
            var iframe = document.getElementById("frameContent");
            iframe.src = objUrl;
        }
       
    </script>
    <style type="text/css">
        a
        {
            text-decoration: none;
            color: Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="con_main">
        <div class="top">
            <div class="top_text_name">
                欢迎您!&nbsp
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>&nbsp
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ClientIDMode="Static">【模拟】</asp:LinkButton>&nbsp
            </div>
            <div class="top_text_copyright">
                </div>
        </div>
        <div class="conter_main">
            <div class="conter_main_left">
                <div class="conter_main_left_menu_top">
                    <img src="pic/system_menu_bg.jpg" /></div>
                <div class="conter_main_left_menu_mid">
                    <div id="ztbox" class="ztbox">
                        <%=LeftMenuString %>
                    </div>
                </div>
                <div class="conter_main_left_menu_bot">
                </div>
            </div>
            <div class="conter_right">
                <iframe id="frameContent" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                    name='frameContent' height="100%" width="100%" src="Admin/ProcessNavigatorNew.aspx"
                    onload="this.height=100"></iframe>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">
    var reg = new RegExp("(^|&)iframesrc=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        var iframe = document.getElementById("frameContent");
        iframe.src = r[2];
    }
</script>
</html>
