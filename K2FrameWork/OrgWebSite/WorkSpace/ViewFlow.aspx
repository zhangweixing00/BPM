<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewFlow.aspx.cs" Inherits="K2.BDAdmin.Web.WorkSpace.ViewFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript" id="clientEventHandlersJS">
    function window_onload() {
        if (document.all('errormessage').value == '' && document.getElementById('strXML').value != '') {
            document.getElementById('ViewFlow').Xml = document.getElementById('strXML').value;
            //document.getElementById('ViewFlow').ScrollBars = true;
            return true;
        }
        //        else {
        //            alert(document.getElementById('errormessage').value);
        //            return false;
        //        }
    }

    function ViewFlow_UnLoad() {

        try {
            document.all("ViewFlow").Dispose();
        }
        catch (ex) {
        }

    }
</script>

<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>View Flow Page</title>    
</head>
<body onload="return window_onload()" onunload="return ViewFlow_UnLoad()">
    <form runat="server" id="Form1" method="post">       
    <input type="hidden" id="strXML" runat="server" />
    <input type="hidden" id="errormessage" runat="server" />
    <object classid="CLSID:C98A2736-4484-4128-B8A4-F5080755A21D" width="1500px" height="2000px"
        id="ViewFlow">
        <param name="Scrollbars" value="false" />
        <a href="K2ViewFlow.msi">Please click here to download and install, after please reload the page.></a>
    </object>
    </form>
</body>
</html>
