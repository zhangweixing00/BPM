<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDelegation.aspx.cs" Inherits="Sohu.OA.Web.WorkSpace.MyDelegation" %>

<%@ Register Src="UC/MyDelegation.ascx" TagName="MyDelegation" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>我的代理</title>
    <meta http-equiv="Cache-Control" content="no-cache" />
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ymPromptclose(p)
        {
            ymPrompt.close();
            if (p = "ok")
            {
                parent.window.TaskRedirect('frameContent', 'WorkSpace/Mydelegation.aspx?random=' + Math.random());        //重定向
                alert('添加代理成功成功');
            }
        }
        function AddDelegation()
        {            
             ymPrompt.win('../WorkSpace/AddDelegation.aspx', 380, 330, "添加代理", null, null, null, true);
        }


        function ConfirmDel(vl)
        {
            var old = document.getElementById(vl);
            var l = document.getElementById(vl.replace("LinkButton0", "LinkButton1"));
            //debugger;
            ymPrompt.confirmInfo("您是否确定要取消该代理？", null, null, null, function (tp) { if (tp == 'ok') { l.click(); } });
        }
    </script>
</head>
<body style="width: 789px; overflow-x: hidden">
    <form id="form1" runat="server">
    <div style="margin: auto">
        <div id="Div_Middle">
            <uc2:MyDelegation ID="MyDelegation1" runat="server" />
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none;" />
    </form>
</body>
</html>
