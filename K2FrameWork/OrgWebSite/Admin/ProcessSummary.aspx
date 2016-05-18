<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessSummary.aspx.cs"
    Inherits="OrgWebSite.Admin.ProcessSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程概要</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/EasyUI/default/easyui.css">
	<link rel="stylesheet" type="text/css" href="../Styles/EasyUI/icon.css">
    <script type="text/javascript" src="../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtProcessName').validatebox({
                required: true,
                validType: 'length[1,100]'
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="nav_1">
        <p>
            流程概要</p>
    </div>
    <div class="pro" style="margin: 10px 0; height: 60px;">
        <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
            <tr>
                <td style="width: 50px; text-align: right;">
                    流程类别：
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="ddlProcessType" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; text-align: right;">
                    流程名称：
                </td>
                <td>
                    <asp:TextBox ID="txtProcessName" runat="server" CssClass="easyui-validatebox" style="height: 22px; border: 1px #999999 solid;" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; text-align: right;">
                    流程描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" style="border: 1px #999999 solid;" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
