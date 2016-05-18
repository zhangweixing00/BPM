<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="MyWorklist.aspx.cs"
    Inherits="K2.BDAdmin.Web.WorkSpace.MyWorklist" %>

<%@ Register Src="UC/MyWorklist1.ascx" TagName="MyWorklist" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>我的任务</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../javascript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script src="../JavaScript/CheckBox.js" type="text/javascript"></script>
    <script src="../JavaScript/Alert.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CheckAllSN(chkAll)
        {
            var hfSelectedSN = $$("MyWorklist1_hfSelectedSN");
            hfSelectedSN.value = "";

            var myForm, obj;
            var vueSelectedSNs = "";
            myForm = document.forms[0];

            for (var i = 0; i < myForm.length; i++)
            {
                obj = myForm.elements[i];
                if (obj.type == "checkbox" && obj.id.indexOf("MyWorklist1_gvMyWorkList_ctl") > -1)
                {
                    obj.checked = chkAll.checked;
                }

                if (chkAll.checked)
                {
                    if (obj.type == "hidden" && obj.id.indexOf("MyWorklist1_gvMyWorkList_ctl") > -1)
                    {
                        vueSelectedSNs += obj.value + ";";
                    }
                }
            }

            hfSelectedSN.value = vueSelectedSNs;
        }

        function ChooseSN(obj)
        {
            var hfSelectedSN = $$("MyWorklist1_hfSelectedSN");
            var vueSelectedSN = hfSelectedSN.value;
            var hfSN = $$(ci(obj) + "SN");
            var chkAll = $$("chkAll");

            if (obj.checked)
            {
                vueSelectedSN += hfSN.value + ";";
            }
            else
            {
                if (chkAll.checked)
                    chkAll.checked = false;

                vueSelectedSN = vueSelectedSN.replace(hfSN.value + ";", "");
            }

            hfSelectedSN.value = vueSelectedSN;
        }

        function BatchRedirect()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            if (vueSelectedSN != "")
            {
                SelectSubmitor();
                return false;
            }
            else
            {
                alert("请选择任务");
                return false;
            }
        }
        function Release()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            if (vueSelectedSN == "")
            {
                alert("请选择释放任务");
                return false;
            }

            return true;
        }

        function BatchSeleted()
        {
            document.getElementById("divBatch").style.display = document.getElementById("MyWorklist1_chkBatch").checked ? "block" : "none";
        }

        function BatchApprove()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            var ddlProcName = document.getElementById("MyWorklist1_ddlProcName");
            var ddlProcNode = document.getElementById("MyWorklist1_ddlProcNode");
            if (ddlProcName.selectedIndex == 0 || ddlProcNode.selectedIndex == 0)
            {
                alert("请选择相同的流程进行审批");
                return false;
            }
            if (vueSelectedSN == "")
            {
                alert("请选择任务");
                return false;
            }

            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">

        function Sleep()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            if (vueSelectedSN == "")
            {
                alert("请选择休眠任务");
                return false;
            }
            else
            {
                var random = Math.round(Math.random() * 10000);
                var para = "random=" + random;
              
                ymPrompt.win('../WorkSpace/SleepTime.aspx?' + para, 370, 200, "睡眠时间", SleepTrueInfos, null, null, true);

                return false;
            }
        }
        function SleepTrueInfos(retValue)
        {
            switch (retValue)
            {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
                default:
                    $$("MyWorklist1_hfSleep").value = retValue;
                    if (retValue != "0")
                    {
                        document.getElementById("MyWorklist1_btnSleepHF").click();
                        ymPrompt.close();
                    }
                    else
                    {
                        alert("休眠时间不能为0");
                    }
                    break;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function BatchDelegate()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            if (vueSelectedSN != "")
            {
                var para = "?checkstyle=true";

                ymPrompt.win('../Search/K2FlowCheck/K2FlowCheck.aspx' + para, 760, 560, "人员选择", BatchDelegateTrueInfos, null, null, true);

                return false;
            }
            else
            {
                alert("请选择任务");
                return false;
            }
        }
        function BatchDelegateTrueInfos(retValue)
        {
            switch (retValue)
            {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length != 0)
                    {
                        var employeeCode = retValue[0].split(';')[1]
                        if (employeeCode != "")
                        {
                            document.getElementById("MyWorklist1_hfAdAcount").value = employeeCode;
                            document.getElementById("MyWorklist1_btnDelegateHF").click();
                            ymPrompt.close();
                        }
                        else
                        {
                            alert("请选择代理人");
                        }
                    }
                    else
                    {
                        alert("请选择代理人");
                    }
                    break;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

        function BatchRedirect()
        {
            var vueSelectedSN = $$("MyWorklist1_hfSelectedSN").value;
            if (vueSelectedSN != "")
            {
                var para = "?checkstyle=true";

                ymPrompt.win('../Search/K2FlowCheck/K2FlowCheck.aspx' + para, 760, 560, "人员选择", BatchRedirectTrueInfos, null, null, true);

                return false;
            }
            else
            {
                alert("请选择任务");
                return false;
            }
        }
        function BatchRedirectTrueInfos(retValue)
        {
            switch (retValue)
            {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
                default:
                    if (retValue && retValue.length != 0)
                    {
                        var employeeCode = retValue[0].split(';')[1]
                        if (employeeCode != "")
                        {
                            document.getElementById("MyWorklist1_hfAdAcount").value = employeeCode;
                            document.getElementById("MyWorklist1_btnRedirectHF").click();
                            ymPrompt.close();
                        }
                        else
                        {
                            alert("请选择转发人");
                        }
                    }
                    else
                    {
                        alert("请选择转发人");
                    }
                    break;
            }
        }
    </script>
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
                        document.getElementById("MyWorklist1_txtSubmittor").value = retValue[0].split(';')[0];
                        var employeeCode = retValue[0].split(';')[1]
                        if (employeeCode != "")
                            document.getElementById("MyWorklist1_hfSubmittor").value = employeeCode;
                    }
                    else
                    {
                        document.getElementById("MyWorklist1_txtSubmittor").value = "";
                        document.getElementById("MyWorklist1_hfSubmittor").value = "";
                    }
                    ymPrompt.close();
                    break;
            }

            document.getElementById("MyWorklist1_txtSubmittor").blur();
        }
    </script>
</head>
<body style="width: 789px; overflow-x: hidden">
    <form id="form1" runat="server">
    <div style="margin: auto">
        <div id="Div_Middle">
            <uc1:MyWorklist ID="MyWorklist1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
