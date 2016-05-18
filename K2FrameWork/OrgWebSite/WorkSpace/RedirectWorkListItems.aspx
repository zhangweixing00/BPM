<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedirectWorkListItems.aspx.cs" Inherits="K2.BDAdmin.Web.WorkSpace.RedirectWorkListItems" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Redirect WorkList Items</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../Javascript/jquery-1.6.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    
    <style type="text/css">
    .bg 
    {
	    display:none;
	    background-color: #f0f0f0;
	    filter:alpha(opacity=50);/*IE*/
	    opacity:0.5;/*FF*/
	    z-index:10;
	    position:absolute;
	    
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
        <input type="button" id="btnHTMLRedirect" value="Redirect" onclick="RedirectWorkListItems()" />
        
        <script language="javascript" type="text/javascript">
        function RedirectWorkListItems()
        {
            var vue = $$("txtUser").value;
                
            superman.show_bg();
            <%= Page.ClientScript.GetCallbackEventReference(this, "vue", "ReceiveServerData",null)%>;
        }
        
        function ReceiveServerData(rValue)
        {
            window.returnValue='';
            window.close();
        }
        </script>
        
        </div>
        <div id="bg" class="bg">
            <img alt="" src="../Img/Rotate.gif" />
        </div>
    </form>
</body>
</html>
