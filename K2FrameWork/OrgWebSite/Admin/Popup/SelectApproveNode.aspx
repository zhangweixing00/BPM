<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectApproveNode.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.SelectApproveNode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择审批节点</title>
    <base target="_self" />
    <link href="../../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script src="../../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Javascript/BtnStyle.js" type="text/javascript"></script>
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
            height: 18px;
            border: 1px #999999 solid;
        }
    </style>
    <script type="text/javascript">
        function recordValue(obj) {
            var hfValue = document.getElementById('hfSelectValue');
            var nodeID = obj.getAttribute('processID');
            if (hfValue) {
                //debugger;
                if (obj.checked && !(hfValue.value.indexOf(nodeID) > 0)) {
                    hfValue.value += nodeID + ';';
                }
                else if (!obj.checked && hfValue.value.indexOf(nodeID + ';') > 0) {
                    hfValue.value = hfValue.value.replace(nodeID + ';', '');
                }
            }
        }

        function Close() {
            var returnValue = document.getElementById('hfSelectValue').value;
            window.parent.ymPrompt.doHandler(returnValue, true);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 20px auto auto auto; width: 500px;">
        <table border="0" cellpadding="0" width="500px" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gvApproveNode" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        BorderColor="#F2DD81" BorderWidth="1px" Width="400px" CssClass="girdView" EmptyDataText="&lt;div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'&gt;无员工数据&lt;div&gt;">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <input id="cbSelect" type="checkbox" onclick='recordValue(this);' processid='<%#Eval("NodeID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="节点名称" DataField="NodeName" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="image" src="../../Pic/btnImg/btnAffirm_nor.png" id="btnOk" onclick="Close();"
                        onmouseover="SaveMouseover('btnOk','../../../pic/btnImg/btnAffirm_over.png')" onmouseout="SaveMouseout('btnOk','../../../pic/btnImg/btnAffirm_nor.png')" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectValue" runat="server" />
    </div>
    </form>
</body>
</html>
