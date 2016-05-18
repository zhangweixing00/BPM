<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SleepTime.aspx.cs" Inherits="OrgWebSite.WorkSpace.SleepTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function RelativeTime_Click()
        {
            var ctl15_SleepDate = document.getElementById("ctl15_SleepDate");
            ctl15_SleepDate.disabled = 'disabled';

            var ctl15_SleepDuration = document.getElementById("ctl15_SleepDuration");
            ctl15_SleepDuration.attributes.removeNamedItem("disabled");

            var ctl15_cmbSleepUnit = document.getElementById("ctl15_cmbSleepUnit");
            ctl15_cmbSleepUnit.attributes.removeNamedItem("disabled");
        }

        function AbsoluteTime_Click()
        {
            var ctl15_SleepDuration = document.getElementById("ctl15_SleepDuration");
            ctl15_SleepDuration.disabled = "disabled";

            var ctl15_cmbSleepUnit = document.getElementById("ctl15_cmbSleepUnit");
            ctl15_cmbSleepUnit.disabled = "disabled";

            var ctl15_SleepDate = document.getElementById("ctl15_SleepDate");
            ctl15_SleepDate.attributes.removeNamedItem("disabled");
            var date = new Date();
            ctl15_SleepDate.value = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + (date.getDate() + 1);
        }

        function checkKey()
        {
            if ((event.keyCode < 48) || (event.keyCode > 59))
            {
                event.returnValue = false;
            }
        }

        function ReturnAction(action)
        {
            if (action == 'OK')
            {
                var ctl15_RelativeTime = document.getElementById("ctl15_RelativeTime");
                if (ctl15_RelativeTime.checked)
                {
                    var ctl15_SleepDuration = document.getElementById("ctl15_SleepDuration");

                    var ctl15_cmbSleepUnit = document.getElementById("ctl15_cmbSleepUnit");
                    var sleeptime = 0;
                    switch (ctl15_cmbSleepUnit.value)
                    {
                        case "Days":
                            sleeptime = ctl15_SleepDuration.value * 24 * 60 * 60;
                            break;
                        case "Hours":
                            sleeptime = ctl15_SleepDuration.value * 60 * 60;
                            break;
                        case "Minutes":
                            sleeptime = ctl15_SleepDuration.value * 60;
                            break;
                        case "Seconds":
                            sleeptime = ctl15_SleepDuration.value;
                            break;
                        default:
                            sleeptime = ctl15_SleepDuration.value;
                            break;
                    }

                    //window.returnValue = sleeptime;
                    window.parent.ymPrompt.doHandler(sleeptime, false);
                }
                else
                {
                    var sleeptime = 0;
                    var ctl15_SleepDate = document.getElementById("ctl15_SleepDate");
                    if (ctl15_SleepDate.value != '')
                    {
                        var date = new Date(ctl15_SleepDate.value.replace('-', '/'));
                        var date1 = new Date();
                        sleeptime = parseInt((date - date1) / 1000);
                    }
                    //window.returnValue = sleeptime;
                    window.parent.ymPrompt.doHandler(sleeptime, false);
                }
            }
            else
            {
                window.parent.ymPrompt.doHandler("close", false);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ctl15_SleepTable" style="display: block; width: 270px; height: 100%; margin-top:20px; margin-left:20px;">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
            <tr>
                <td>
                    <div id="Div2" style="height: 105px; overflow: hidden;">
                        <table>
                            <tr>
                                <td colspan="3">
                                    <input type="radio" checked="checked" id="ctl15_RelativeTime" name="SleepTimeCheck"
                                        onclick="RelativeTime_Click()" />
                                    <label for="RelativeTime">
                                        相对时间</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    周期:
                                </td>
                                <td>
                                    <input type="text" id="ctl15_SleepDuration" value="1" style="width: 100px" onkeypress="checkKey()"
                                        maxlength="4" />
                                </td>
                                <td>
                                    <select id="ctl15_cmbSleepUnit" class="ms-input">
                                        <option value="Days" selected="selected">天</option>
                                        <option value="Hours">小时</option>
                                        <option value="Minutes">分钟</option>
                                        <option value="Seconds">秒</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <input type="radio" id="ctl15_AbsoluteTime" name="SleepTimeCheck" onclick="AbsoluteTime_Click()" />
                                    <label for="AbsoluteTime">
                                        绝对时间</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    日期:
                                </td>
                                <td colspan="2">
                                    <input type="text" id="ctl15_SleepDate" onclick="WdatePicker({readOnly:true})" onfocus="var date2 = document.getElementById('ctl15_SleepDate').value;WdatePicker({minDate:date2})"
                                        disabled="disabled" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input id="btnOK" type="button" value="确定" class='btnCommon' onclick="ReturnAction('OK')" />
                    <input id="btnCancel" type="button" value="关闭" class='btnCommon' onclick="ReturnAction('Close')" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
