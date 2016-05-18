<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigurationRuleResult.aspx.cs" Inherits="OrgWebSite.Admin.Popup.ConfigurationRuleResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配置结果浏览</title>
    <base target="_self" />
    <link href="/Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="/JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script src="/JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
    <link href="../../../Styles/PMS/PMS.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ddlcss
        {
            border: 1px #999999 solid;
        }
        #RoleAddTitle
        {
            width: 770px;
            margin: 10px 0 0 20px;
            height: 10px;
            padding: 10px;
            color: #76650b;
            font-weight: bold;
            background: url(../../../pic/right_list_title_bg2.png) no-repeat;
        }
        .table_title
        {
            text-align: right;
        }
        .table_content
        {
            width: 350px;
            text-align: left;
        }
        .table_xing
        {
            width: 50px;
        }
        .txtcss
        {
            padding-top: 4px;
            height:18px;
            border: 1px #999999 solid;
        }
       .datalist0{ width:800px; border:1px solid #f2dd81; color:#333; border-collapse:collapse;
        text-align: left;
        }
       .datalist0 td,.datalist0 th{ border:1px solid #f2dd81;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left: 18px;">
        <div class="rightTitle">
            <span>审批列表</span></div>
        <table style="width: 769px;">
            <tr>
                <td>
                    <asp:GridView ID="gvConfiguration" runat="server" 
                        onrowdatabound="gvConfiguration_RowDataBound" BorderColor="#F2DD81"
    EmptyDataText="&lt;div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'&gt;没有记录&lt;/div&gt;"
        BorderWidth="1px" Width="540px" CssClass="datalist0">
                        <Columns>
                            <asp:BoundField HeaderText="入口节点名称" DataField="RequestNodeName" 
                                HtmlEncode="false">
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
