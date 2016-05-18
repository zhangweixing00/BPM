<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlTemplate.aspx.cs"
    Inherits="OrgWebSite.Admin.FormDesign.ControlTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>控件模板</title>
    <link href="../../Styles/EasyUI/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/EasyUI/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            window.setInterval("reinitIframeUI()", 200);

            //注册按钮
            $('#btnAccordion').linkbutton({
                plain: true
            });
            $('#btnFormLib').linkbutton({
                plain: true
            });
            $('#btnForm').linkbutton({
                plain: true
            });


            //注册事件
            $('#btnAccordion').click(function ()
            {
                $('#ifContent').attr('src', 'ChildPage/ControlManage.aspx');
            });
            $('#btnFormLib').click(function ()
            {
                $('#ifContent').attr('src', 'ChildPage/FormTemplateManage.aspx');
            });
            $('#btnForm').click(function ()
            {

            });
        });

        function reinitIframeUI()
        {
            var iframe = document.getElementById("ifContent");
            try
            {
                var bHeight = iframe.contentWindow.document.body.scrollHeight;
                var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                var height = Math.max(bHeight, dHeight);
                iframe.height = height;
            } catch (ex) { }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="dvPanel" class="easyui-layout" style="width: 790px; height: 500px;">
        <div region="south" title="帮助" split="true" style="height: 0px;">
        </div>
        <div region="west" split="true" title="工具栏" class="easyui-accordion" style="width: 150px;">
            <div id="dvAccordion" title="控件库" iconcls="icon-ok" style="overflow: auto; padding: 10px;">
                <a id="btnAccordion" href="javascript:void(0)" iconcls="icon-redo">控件库</a> <a id="btnFormLib"
                    href="javascript:void(0)" iconcls="icon-redo">表单模板库</a> <a id="btnForm" href="javascript:void(0)"
                        iconcls="icon-redo">表单</a>
            </div>
        </div>
        <div id="dvContent" region="center" title="内容" style="padding: 5px; overflow: hidden;">
            <iframe id="ifContent" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"
                height="100%" width="100%"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
