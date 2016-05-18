<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDraft.aspx.cs" Inherits="K2.BDAdmin.Web.WorkSpace.MyDraft" %>

<%@ Register Src="UC/MyDraft1.ascx" TagName="MyDraft" TagPrefix="uc1" %>
<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>草稿箱</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
</head>
<body style="width: 789px; overflow-x: hidden">
    <form id="form1" runat="server">
   <div style="margin: auto">
        <div class="nav">
            <p>
                <uc2:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
        
        <div id="Div_Middle">
        <uc1:MyDraft ID="MyDraft1" runat="server" />
        </div>
        <script language="javascript" type="text/javascript">
        var formid;
            function DelDraft(draftid)
            {
            formid=draftid;
                ymPrompt.confirmInfo('确认删除草稿', null, null, null, handler);
            }
            function handler(tp)
            {
                if (tp == 'ok')
                {
				    <%= Page.ClientScript.GetCallbackEventReference(this, "formid", "ReceiveServerData",null)%>;
			    }			
		    }	
            
            function ReceiveServerData(rValue)
            {
                //alert(rValue);
                //superman.hide_bg();
                document.getElementById("MyDraft1_btnGoPage").click();
            }
            
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
                   $$("MyDraft1_btnSearch").click();
            }
             function OpenWorkPage(objurl, objwinname)
            {
                   ymPrompt.win(objurl, 830, 470, objwinname, Search, null, null, true);
            }
        </script>
    </div>
    </form>
</body>
</html>
