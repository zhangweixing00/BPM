<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="MyStarted.aspx.cs"
    Inherits="K2.BDAdmin.Web.WorkSpace.MyStarted" %>

<%@ Register Src="UC/MyStarted1.ascx" TagName="MyStarted" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>我的申请</title>
    
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            var main = $(window.parent.document).find("#iMyApplicant");
            main.height($(window.parent.document).height() - 192);
        });

        function ConfirmDel(vl)
        {
            var old = document.getElementById(vl);
            ymPrompt.confirmInfo("您确认要召回该申请单吗？", null, null, null, function (tp) { if (tp == 'ok') { if ($(old).parent().children().length == 2) $(old).parent().children()[1].click(); else { old.parentNode.childNodes[3].click(); } } });
        }
    </script>
    
    <script type="text/javascript">
        function MouseOut() {
            var objUnknown;

            objUnknown = window.event.srcElement;

            if (objUnknown.tagName.toUpperCase() == "INPUT") {
                objUnknown.style.backgroundImage = "url(../pic/b_base.gif)";
                objUnknown.style.borderRightColor = "#000000";
                objUnknown.style.borderTopColor = "#000000";
                objUnknown.style.borderLeftColor = "#000000";
                objUnknown.style.borderBottomColor = "#000000";

            }
            return true;
        }

        function MouseOver() {

            var objUnknown;

            objUnknown = window.event.srcElement;
            //alert("enter MouseOver:"+objUnknown.tagName );

            if (objUnknown.tagName.toUpperCase() == "INPUT") {
                objUnknown.style.backgroundImage = "url(../pic/b_blue.gif)";
                objUnknown.style.borderRightColor = "#000000";
                objUnknown.style.borderTopColor = "#000000";
                objUnknown.style.borderLeftColor = "#000000";
                objUnknown.style.borderBottomColor = "#000000";

            }
            return true;
        }
        function Search()
        {
            $$("MyStarted1_btnSearch").click();
        }
        function OpenWorkPage(objurl, objwinname)
        {
            ymPrompt.win(objurl, 830, 470, objwinname, Search, null, null, true);
        }
    </script>

</head>
<body style="width: 789px; overflow-x: hidden">
    <form id="form1" runat="server">
   <div style="margin: auto">        
        <div id="Div_Middle">
        <uc1:MyStarted ID="MyStarted1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
