<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITSupport_Outside.aspx.cs"
    Inherits="Workflow_CustomWorkFlow_ITSupport_Outside" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IT支持平台--各系统对接地址</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="List">
            <Columns>
                <asp:BoundField HeaderText="类型" DataField="Name" />
                <asp:BoundField HeaderText="连接地址" DataField="Link" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <a onclick='<%# string.Format("copy_code(&#39;{0}&#39;)", Eval("Link").ToString()) %>'
                            href='#'>复制</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <%--    <div style="position: fixed; z-index: 1000; width: 50px; height: 20px; bottom: 2px; left: 2px" ><a  href="">系统支持</a></div>
    --%></form>
</body>
</html>
<script type="text/javascript">
    var itLink = "http://localhost:62180/Workflow/EditPage/E_OA_ITSupport.aspx?type=1";
    function copy_code(copyText) {
        if (window.clipboardData) {
            window.clipboardData.setData("Text", copyText);
            alert("复制成功");
        } else if (navigator.userAgent.indexOf("Opera") != -1) {
            window.location = s;
        } else if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
            }
            var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
            if (!clip)
                return;
            var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
            if (!trans)
                return;
            trans.addDataFlavor('text/unicode');
            var str = new Object();
            var len = new Object();
            var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
            var copytext = s;
            str.data = copytext;
            trans.setTransferData("text/unicode", str, copytext.length * 2);
            var clipid = Components.interfaces.nsIClipboard;
            if (!clip)
                return false;
            clip.setData(trans, null, clipid.kGlobalClipboard);
            alert("已经复制到剪切板！" + "\n" + s);
        }
    }
</script>
<script src="../../Resource/js/ITsupportEntry.js" type="text/javascript"></script>
