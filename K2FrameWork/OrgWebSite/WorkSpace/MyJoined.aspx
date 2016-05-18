<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyJoined.aspx.cs" Inherits="K2.BDAdmin.Web.WorkSpace.MyJoined" %>

<%@ Register Src="UC/MyJoined1.ascx" TagName="MyJoined" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>已处理任务</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
   
    <script language="javascript" type="text/javascript">
        function SelectSubmitor()
        {
            var para = "?checkstyle=true";

            ymPrompt.win('../Search/K2FlowCheck/K2FlowCheck.aspx' + para, 760, 560, "人员选择", TrueInfos, null, null, true);
        }
        function TrueInfos(retValue)
        {
            switch (retValue)
            {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length != 0)
                    {
                        document.getElementById("MyJoined1_txtSubmittor").value = retValue[0].split(';')[0];
                        var employeeCode = retValue[0].split(';')[1]
                        if (employeeCode != "")
                            document.getElementById("MyJoined1_hfSubmittor").value = employeeCode;
                    }
                    else
                    {
                        document.getElementById("MyJoined1_txtSubmittor").value = "";
                        document.getElementById("MyJoined1_hfSubmittor").value = "";
                    }
                    ymPrompt.close();
                    break;
            }

            document.getElementById("MyJoined1_txtSubmittor").blur();
        }
    </script>
</head>
<body style="width: 789px; overflow-x: hidden">
    <form id="form1" runat="server">
   <div style="margin: auto">        
        <div id="Div_Middle">
        <uc1:MyJoined ID="MyJoined1" runat="server"></uc1:MyJoined>
        </div>
    </div>
    </form>
</body>
</html>
