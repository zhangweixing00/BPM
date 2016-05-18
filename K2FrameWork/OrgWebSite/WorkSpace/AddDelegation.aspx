<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDelegation.aspx.cs"
    Inherits="Sohu.OA.Web.WorkSpace.AddDelegation" %>

<%@ Register Src="UC/AddDelegation.ascx" TagName="AddDelegation" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt.js"></script>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <link href="../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function OkClick()
        {
            var values = '';
            values = document.getElementById("AddDelegation1_hfToUser").value;
            if (values == '')
            {
                ymPrompt.alert('请填写必填项！', null, null, '错误', null)
                return false;
            }
            values = document.getElementById("AddDelegation1_hfFromUser").value;
            if (values == '')
            {
                ymPrompt.alert('请填写必填项！', null, null, '错误', null)
                return false;
            }
            values = document.getElementById("AddDelegation1_txtStartDate").value;
            if (values == '')
            {
                ymPrompt.alert('请填写必填项！', null, null, '错误', null)
                return false;
            }
            values = document.getElementById("AddDelegation1_txtEndDate").value;
            if (values == '')
            {
                ymPrompt.alert('请填写必填项！', null, null, '错误', null)
                return false;
            }
            top.ymPrompt.doHandler("ok",true);
            return true;
        }
        function CancelClick()
        {
            top.ymPrompt.close();
            return false;
        }
        function SelectSubmitor()
        {
            var para = "?checkstyle=true";
            var retValue = window.showModalDialog('../Admin/Popup/SelectSingleUser.aspx?' + para, window, 'dialogHeight: 500px; scroll:yes; dialogWidth: 550px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
            if (retValue)
            {
                var retArray = retValue.split(';');
                document.getElementById("AddDelegation1_txtToUser").value = retArray[2];
                document.getElementById("AddDelegation1_hfToUser").value = retArray[2];
                return true;
            }
            else
            {
                return false;
            }
        }
       
    </script>
    <script language="javascript" type="text/javascript">
        function SelectSubmitor()
        {
            top.ymPrompt.resizeWin(760, 550);
            var para = "?checkstyle=true";
            ymPrompt.win({ message: '../Search/K2FlowCheck/K2FlowCheck.aspx' + para, width: 760, height: 560, title: "人员选择", handler: TrueInfos, iframe: true, titleBar: false });
        }
        function TrueInfos(retValue)
        {
            switch (retValue)
            {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
                default:
                    document.getElementById("AddDelegation1_txtToUser").value = retValue[0].split(';')[0];
                    document.getElementById("AddDelegation1_hfToUser").value = retValue[0].split(';')[1];
                    
                    ymPrompt.close();
                    break;
            }

            document.getElementById("AddDelegation1_txtToUser").blur();
            top.ymPrompt.resizeWin(380, 330);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:AddDelegation ID="AddDelegation1" runat="server" />
    </div>
    </form>
</body>
</html>
